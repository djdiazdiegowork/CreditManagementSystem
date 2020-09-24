using CreditManagementSystem.Common.Responses;
using FluentValidation;
using System;
using System.Threading.Tasks;

namespace CreditManagementSystem.Common.Domain.Handler
{
    public sealed class CommadDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _provider;
        public CommadDispatcher(IServiceProvider provider)
        {
            this._provider = provider;
        }

        public async Task<IResponse> DispatchAsync<TCommand>(TCommand command, Type resultType = null) where TCommand : ICommand
        {
            var type = typeof(TCommand);

            var validator = (IValidator<TCommand>)this._provider.GetService(typeof(IValidator<TCommand>));

            if (validator != null)
            {
                var validationResult = await validator.ValidateAsync(command);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException("validation failure", validationResult.Errors);
                }
            }

            var commandHandler = (ICommandHandler<TCommand>)this._provider.GetService(typeof(ICommandHandler<>).MakeGenericType(type));

            return await commandHandler.HandleAsync(command, resultType);
        }
    }

}
