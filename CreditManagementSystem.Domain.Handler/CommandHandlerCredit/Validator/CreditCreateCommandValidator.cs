using CreditManagementSystem.Common.Data;
using CreditManagementSystem.Common.Domain.Handler;
using CreditManagementSystem.Data.Model;
using CreditManagementSystem.Domain.CommandCredit;
using CreditManagementSystem.Domain.Handler.CommandHandlerCreditStatus.Validator;
using FluentValidation;

namespace CreditManagementSystem.Domain.Handler.CommandHandlerCredit.Validator
{
    public abstract class CreditCUCommandValidator<TCommand> : CommandValidator<TCommand> where TCommand : CreditCUCommand
    {
        public CreditCUCommandValidator()
        {
            RuleFor(e => e.Amount).GreaterThanOrEqualTo(1000);
        }
    }

    public sealed class CreditCreateCommandValidator : CreditCUCommandValidator<CreditCreateCommand>
    {
        public CreditCreateCommandValidator()
        {

        }
    }

    public sealed class CreditUpdateCommandValidator : CreditCUCommandValidator<CreditUpdateCommand>
    {
        public CreditUpdateCommandValidator(
            IQueryRepository<CreditStatus> creditStatusQueryRepository,
            IQueryRepository<Credit> creditQueryRepository)
        {
            RuleFor(e => e.CreditStatusID).CreditStatusMustBeValid(creditStatusQueryRepository);
            RuleFor(e => e.DebtPaid).GreaterThanOrEqualTo(0);
            RuleFor(e => e.ID).CreditMustBeValid(creditQueryRepository);
        }
    }
}
