using CreditManagementSystem.Common.Response;
using System.Threading.Tasks;

namespace CreditManagementSystem.Common.Domain.Handler
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task<IResponse> HandleAsync(TCommand command);
    }
}
