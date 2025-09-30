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
    public class AttemptAnswerRepository : GenericRepository<AttemptAnswer>, IAttemptAnswerRepository
    {
        private readonly AppDbContext _appDbContext;
        public AttemptAnswerRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }
}
