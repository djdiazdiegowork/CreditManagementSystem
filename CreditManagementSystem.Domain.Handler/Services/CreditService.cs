using AutoMapper;
using CreditManagementSystem.Common.Data;
using CreditManagementSystem.Data.Models;
using CreditManagementSystem.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreditManagementSystem.Domain.Handler.Services
{
    public sealed class CreditService : ICreditService
    {
        private readonly IQueryRepository<Credit> _creditQueryRepository;
        private readonly IMapper _mapper;

        public CreditService(IQueryRepository<Credit> creditQueryRepository, IMapper mapper)
        {
            this._creditQueryRepository = creditQueryRepository;
            this._mapper = mapper;
        }

        public async Task<TEntity> Get<TEntity>(Guid id)
        {
            var entity = await this._creditQueryRepository.Find(e => e.ID == id).FirstOrDefaultAsync();

            return this._mapper.Map<TEntity>(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAll<TEntity>()
        {
            var entities = await this._creditQueryRepository.FindAll().ToListAsync();

            return this._mapper.Map<IEnumerable<TEntity>>(entities);
        }
    }
}
