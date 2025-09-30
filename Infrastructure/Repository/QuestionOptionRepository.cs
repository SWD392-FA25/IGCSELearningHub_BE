using Application.IRepository;
using Domain.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class QuestionOptionRepository : GenericRepository<QuestionOption>, IQuestionOptionRepository
    {
        private readonly AppDbContext _appDbContext;
        public QuestionOptionRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }
}
