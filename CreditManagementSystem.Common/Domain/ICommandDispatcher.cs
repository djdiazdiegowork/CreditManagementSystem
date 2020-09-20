using CreditManagementSystem.Common.Response;
using System.Threading.Tasks;

namespace CreditManagementSystem.Common.Domain
{
    public interface ICommandDispatcher
    {
        Task<IResponse> DispatchAsync<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
