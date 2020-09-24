using CreditManagementSystem.Common.Responses;
using System;
using System.Threading.Tasks;

namespace CreditManagementSystem.Common.Domain
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task<IResponse> HandleAsync(TCommand command, Type resultType = default);
    }
}
