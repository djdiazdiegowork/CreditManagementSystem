using AutoMapper;
using CreditManagementSystem.Common.Data;
using CreditManagementSystem.Data.Model;
using CreditManagementSystem.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreditManagementSystem.Domain.Handler.Services
{
    public class RiskCenterService : IRiskCenterService
    {
        private readonly IQueryRepository<RiskCenter> _riskCenterQueryRepository;
        private readonly IMapper _mapper;

        public RiskCenterService(IQueryRepository<RiskCenter> riskCenterQueryRepository, IMapper mapper)
        {
            this._riskCenterQueryRepository = riskCenterQueryRepository;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<TEntity>> GetAll<TEntity>()
        {
            var entity = await this._riskCenterQueryRepository.FindAll().ToListAsync();

            return this._mapper.Map<IEnumerable<TEntity>>(entity);
        }
    }
}
