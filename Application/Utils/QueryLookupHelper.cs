using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Utils
{
    public static class QueryLookupHelper
    {
        public static async Task<IReadOnlyList<TOut>> MapWithTitlesAsync<TAggregate, TEntity, TOut>(
            IReadOnlyCollection<TAggregate> aggregates,
            IQueryable<TEntity> source,
            Func<TAggregate, int> aggregateIdSelector,
            Expression<Func<TEntity, int>> entityIdSelector,
            Expression<Func<TEntity, string?>> entityTitleSelector,
            Func<TAggregate, string, TOut> projector,
            string fallbackFormat,
            CancellationToken ct = default)
        {
            if (aggregates.Count == 0)
            {
                return Array.Empty<TOut>();
            }

            if (aggregateIdSelector == null) throw new ArgumentNullException(nameof(aggregateIdSelector));
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (entityIdSelector == null) throw new ArgumentNullException(nameof(entityIdSelector));
            if (entityTitleSelector == null) throw new ArgumentNullException(nameof(entityTitleSelector));
            if (projector == null) throw new ArgumentNullException(nameof(projector));
            if (fallbackFormat == null) throw new ArgumentNullException(nameof(fallbackFormat));

            var ids = aggregates.Select(aggregateIdSelector).Distinct().ToList();
            if (ids.Count == 0)
            {
                return aggregates
                    .Select(a =>
                    {
                        var id = aggregateIdSelector(a);
                        return projector(a, string.Format(fallbackFormat, id));
                    })
                    .ToList();
            }

            var parameter = entityIdSelector.Parameters.Single();
            var idBody = ReplaceParameter(entityIdSelector.Body, entityIdSelector.Parameters[0], parameter);
            var titleBody = ReplaceParameter(entityTitleSelector.Body, entityTitleSelector.Parameters[0], parameter);

            var containsCall = Expression.Call(
                typeof(Enumerable),
                nameof(Enumerable.Contains),
                new[] { typeof(int) },
                Expression.Constant(ids),
                idBody);

            var predicate = Expression.Lambda<Func<TEntity, bool>>(containsCall, parameter);

            var titlePairInit = Expression.MemberInit(
                Expression.New(typeof(TitlePair)),
                Expression.Bind(typeof(TitlePair).GetProperty(nameof(TitlePair.Id))!, idBody),
                Expression.Bind(typeof(TitlePair).GetProperty(nameof(TitlePair.Title))!, titleBody));

            var selector = Expression.Lambda<Func<TEntity, TitlePair>>(titlePairInit, parameter);

            var titles = await source
                .Where(predicate)
                .Select(selector)
                .ToDictionaryAsync(x => x.Id, x => x.Title ?? string.Empty, ct);

            var results = aggregates
                .Select(aggregate =>
                {
                    var id = aggregateIdSelector(aggregate);
                    var title = titles.TryGetValue(id, out var value)
                        ? value
                        : string.Format(fallbackFormat, id);
                    return projector(aggregate, title);
                })
                .ToList();

            return results;
        }

        private static Expression ReplaceParameter(Expression expression, ParameterExpression source, ParameterExpression target)
        {
            return new ParameterReplaceVisitor(source, target).Visit(expression);
        }

        private sealed class ParameterReplaceVisitor : ExpressionVisitor
        {
            private readonly ParameterExpression _source;
            private readonly ParameterExpression _target;

            public ParameterReplaceVisitor(ParameterExpression source, ParameterExpression target)
            {
                _source = source;
                _target = target;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return node == _source ? _target : base.VisitParameter(node);
            }
        }

        private sealed class TitlePair
        {
            public int Id { get; set; }
            public string Title { get; set; } = string.Empty;
        }
    }
}
