using CreditManagementSystem.Common.Response;
using System.Threading.Tasks;

namespace CreditManagementSystem.Common.Domain.Handler
{
    public interface ICommandDispatcher
    {
        Task<IResponse> DispatchAsync<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
