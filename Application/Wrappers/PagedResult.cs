using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrappers
{
    public class PagedResult<T> : Result<IEnumerable<T>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public bool HasNext => PageNumber < TotalPages;
        public bool HasPrevious => PageNumber > 1;

        public static PagedResult<T> Success(
            IEnumerable<T> data,
            int totalCount,
            int pageNumber,
            int pageSize,
            string message = "Operation successful.")
        {
            int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return new PagedResult<T>
            {
                Succeeded = true,
                StatusCode = 200,
                Message = message,
                Data = data,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }
    }
}
