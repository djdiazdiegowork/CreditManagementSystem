using CreditManagementSystem.Common.Domain;
using System;

namespace CreditManagementSystem.Domain.CommandCredit
{
    public sealed class CreditDeleteCommand : ICommand
    {
        public Guid ID { get; set; }
    }
}
