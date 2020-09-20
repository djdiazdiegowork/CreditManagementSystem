using CreditManagementSystem.Common.Data;
using CreditManagementSystem.Data.Model;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;

namespace CreditManagementSystem.Domain.Handler.CommandHandlerCredit.Validator
{
    public static class ValidatorExtension
    {
        public static IRuleBuilderOptions<T, Guid> CreditMustBeValid<T>(
            this IRuleBuilder<T, Guid> builder,
            IQueryRepository<Credit> queryRepository)
        {
            return builder.MustAsync((ID, cancellationToken) =>
            {
                return queryRepository.Find(e => e.ID == ID).AnyAsync(cancellationToken);
            }).WithMessage("credit not valid");
        }
    }
}
