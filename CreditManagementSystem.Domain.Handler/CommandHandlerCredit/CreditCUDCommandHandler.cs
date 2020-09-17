using AutoMapper;
using CreditManagementSystem.Client.Model.Credit;
using CreditManagementSystem.Common.Data;
using CreditManagementSystem.Common.Domain;
using CreditManagementSystem.Common.Extension;
using CreditManagementSystem.Common.Response;
using CreditManagementSystem.Common.SequentialGuidGenerator;
using CreditManagementSystem.Data.Model;
using CreditManagementSystem.Domain.CommandCredit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CreditManagementSystem.Domain.Handler.CommandHandlerCreditStatus.Validator
{
    public class CreditCUDCommandHandler :
        ICommandHandler<CreditCreateCommand>,
        ICommandHandler<CreditUpdateCommand>,
        ICommandHandler<CreditDeleteCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Credit> _creditRepository;
        private readonly IIdGenerator _idGenerator;
        private readonly IMapper _mapper;

        public CreditCUDCommandHandler(
            IUnitOfWork unitOfWork,
            IRepository<Credit> creditRepository,
            IIdGenerator idGenerator,
            IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._creditRepository = creditRepository;
            this._idGenerator = idGenerator;
            this._mapper = mapper;
        }

        public async Task<IResponse> HandleAsync(CreditCreateCommand command)
        {
            var dbCredit = new Credit
            {
                ID = this._idGenerator.NewId(),
                CreationDay = DateTime.Now
            };

            this.Common(dbCredit, command);

            await this._creditRepository.AddAsync(dbCredit);

            await this._unitOfWork.SaveChangesAsync();

            var result = this._mapper.Map<CreditResultDto>(dbCredit);

            return command.OkResponse(result);
        }

        public async Task<IResponse> HandleAsync(CreditUpdateCommand command)
        {
            var dbCredit = await this._creditRepository.Find(e => e.ID == command.ID).FirstOrDefaultAsync();

            dbCredit.ModificationDay = DateTime.UtcNow;
            dbCredit.CreditStatusID = command.CreditStatusID;
            dbCredit.DebtPaid = command.DebtPaid;
            dbCredit.DueDate = command.DueDate;

            this.Common(dbCredit, command);

            this._creditRepository.Update(dbCredit);

            await this._unitOfWork.SaveChangesAsync();

            var result = this._mapper.Map<CreditResultDto>(dbCredit);

            return command.OkResponse(result);
        }

        public async Task<IResponse> HandleAsync(CreditDeleteCommand command)
        {
            await this._creditRepository.DeleteByIDAsync(command.ID);

            await this._unitOfWork.SaveChangesAsync();

            return command.OkResponse(command.ID);
        }

        private void Common(Credit dbCredit, CreditCUCommand command)
        {
            dbCredit.ClientID = command.ClientID;
            dbCredit.Amount = command.Amount;
        }
    }
}
