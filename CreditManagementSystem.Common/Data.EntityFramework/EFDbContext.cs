using CreditManagementSystem.Common.Domain;
using CreditManagementSystem.Common.Exceptions;
using CreditManagementSystem.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CreditManagementSystem.Common.Data.EntityFramework
{
    public class EFDbContext : DbContext, IUnitOfWork
    {
        private readonly IServiceProvider _provider;

        public EFDbContext(IServiceProvider provider, DbContextOptions options)
            : base(options)
        {
            this._provider = provider;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddEntities();

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            return this.SaveChangesAsync(new CancellationToken()).Result;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            object[] changedEntities = this.ChangeTracker.Entries()
                .Where(p => p.State != EntityState.Unchanged)
                .Select(s => s.Entity)
                .ToArray();

            var events = new List<IEvent>();

            foreach (IAggregateRoot aggregateRoot in changedEntities.OfType<IAggregateRoot>())
            {
                events.AddRange(aggregateRoot.GetEvents().Where(e => e.IsDomainEvent));
                aggregateRoot.ClearEvents();
            }

            if (events.Any())
            {
                var eventDispatcher = this._provider.GetRequiredService<IEventDispatcher>();

                var eventResponse = await eventDispatcher.DispatchAsync(events);

                if (eventResponse.GetEventsResponseFail().Any())
                {
                    throw new EventException("Errors have occurred with some events", eventResponse.GetEventsResponseFail());
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
