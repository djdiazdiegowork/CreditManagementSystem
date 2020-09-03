using CreditManagementSystem.Client.Model;
using CreditManagementSystem.Common.Data;
using CreditManagementSystem.Data.Model;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CreditManagementSystem.Domain.Handler.CommandHandlerCreditStatus.Validator
{
    public static class ValidatorExtension
    {
        public static IRuleBuilderOptions<T, CreditStatusValue> CreditStatusMustBeValid<T>(
            this IRuleBuilder<T, CreditStatusValue> builder,
            IQueryRepository<CreditStatus> queryRepository)
        {
            return builder.MustAsync((ID, cancellationToken) => {
                return queryRepository.Find(e => e.ID == ID).AnyAsync(cancellationToken);
            }).WithMessage("credit status not valid");
        }
    }
}
