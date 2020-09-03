using AutoMapper;
using CreditManagementSystem.Client.Model.Credit;
using CreditManagementSystem.Common.Data;
using CreditManagementSystem.Common.Domain.Handler;
using CreditManagementSystem.Common.Response;
using CreditManagementSystem.Common.SequentialGuidGenerator;
using CreditManagementSystem.Data.Model;
using CreditManagementSystem.Domain.ComandCredit;
using System;
using System.Threading.Tasks;

namespace CreditManagementSystem.Domain.Handler.CommandHandlerCreditStatus.Validator
{
    public class CreditCUDCommandHandler : ICommandHandler<CreditCreateCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Credit> _creditRepository;
        private readonly IQueryRepository<Credit> _creditQueryRepository;
        private readonly IIdGenerator _idGenerator;
        private readonly IMapper _mapper;

        public CreditCUDCommandHandler(
            IUnitOfWork unitOfWork,
            IRepository<Credit> creditRepository,
            IQueryRepository<Credit> creditQueryRepository,
            IIdGenerator idGenerator,
            IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._creditRepository = creditRepository;
            this._creditQueryRepository = creditQueryRepository;
            this._idGenerator = idGenerator;
            this._mapper = mapper;
        }

        public async Task<IResponse> HandleAsync(CreditCreateCommand command)
        {
            var dbCredit = new Credit();

            this.Common(dbCredit, command);

            this._creditRepository.Add(dbCredit);

            await this._unitOfWork.SaveChangesAsync(default);

            var result = this._mapper.Map<CreditCreateResultDto>(dbCredit);

            return command.OkResponse(result);
        }

        private void Common(Credit dbCredit, CreditCUCommand command)
        {
            dbCredit.ID = this._idGenerator.NewId();
            dbCredit.ClientID = command.ClientID;
            dbCredit.Amount = command.Amount;
            dbCredit.CreditStatusID = Client.Model.CreditStatusValue.Pending;
            dbCredit.CreationDay = DateTime.Now;
        }
    }
}
