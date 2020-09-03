using FluentValidation;

namespace CreditManagementSystem.Common.Domain.Handler
{
    public abstract class CommandValidator<TCommand> : AbstractValidator<TCommand> where TCommand : ICommand
    {
        protected CommandValidator()
        {
            this.CascadeMode = FluentValidation.CascadeMode.Stop;
        }
    }
}
