using CreditManagementSystem.Common.Domain;
using System;

namespace CreditManagementSystem.Domain.CommandCredit
{
    public abstract class CreditCUCommand : ICommand
    {
        public Guid ClientID { get; set; }

        public double Amount { get; set; }
    }

}
