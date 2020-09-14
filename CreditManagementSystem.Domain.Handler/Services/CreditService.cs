using AutoMapper;
using CreditManagementSystem.Common.Data.EntityFramework;
using CreditManagementSystem.Data.Model;
using CreditManagementSystem.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreditManagementSystem.Domain.Handler.Services
{
    public class CreditService : ICreditService
    {
        private readonly IQueryRepository<Credit> _creditQueryRepository;
        private readonly IMapper _mapper;

        public CreditService(IQueryRepository<Credit> creditQueryRepository, IMapper mapper)
        {
            this._creditQueryRepository = creditQueryRepository;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<TEntity>> GetAll<TEntity>()
        {
            var entity = await this._creditQueryRepository.FindAll().ToListAsync();

            return this._mapper.Map<IEnumerable<TEntity>>(entity);
        }
    }
}
