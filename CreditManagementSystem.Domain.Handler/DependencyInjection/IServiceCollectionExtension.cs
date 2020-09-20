using CreditManagementSystem.Common.Domain;
using CreditManagementSystem.Common.Domain.Handler;
using CreditManagementSystem.Common.Extension;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CreditManagementSystem.Domain.Handler.DependencyInjection
{
    public static class IServiceCollectionExtenssion
    {
        public static void AddDomainHandlerServices(this IServiceCollection services)
        {
            services.AddServicesHandler(typeof(IService).GetEntityTypes());
            services.AddCommandHandler(typeof(ICommandHandler<>), typeof(ICommand).GetEntityTypes());
            services.AddCommandValidator(typeof(IValidator<>), typeof(ICommand).GetEntityTypes());
            services.AddScoped<ICommandDispatcher, CommadDispatcher>();
        }
    }
}
