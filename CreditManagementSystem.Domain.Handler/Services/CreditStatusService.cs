using AutoMapper;
using CreditManagementSystem.Common.Data;
using CreditManagementSystem.Data.Model;
using CreditManagementSystem.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreditManagementSystem.Domain.Handler.Services
{
    public class CreditStatusService : ICreditStatusService
    {
        private readonly IQueryRepository<CreditStatus> _creditStatusQueryRepository;
        private readonly IMapper _mapper;

        public CreditStatusService(IQueryRepository<CreditStatus> creditStatusQueryRepository, IMapper mapper)
        {
            this._creditStatusQueryRepository = creditStatusQueryRepository;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<TEntity>> GetAll<TEntity>()
        {
            var entity = await this._creditStatusQueryRepository.FindAll().ToListAsync();

            return this._mapper.Map<IEnumerable<TEntity>>(entity);
        }
    }
}
