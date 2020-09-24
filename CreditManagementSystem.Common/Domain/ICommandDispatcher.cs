using CreditManagementSystem.Common.Responses;
using System;
using System.Threading.Tasks;

namespace CreditManagementSystem.Common.Domain
{
    public interface ICommandDispatcher
    {
        Task<IResponse> DispatchAsync<TCommand>(TCommand command, Type resultType = null) where TCommand : ICommand;
    }
}
