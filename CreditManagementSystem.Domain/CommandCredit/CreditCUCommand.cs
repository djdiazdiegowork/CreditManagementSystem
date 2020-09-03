using CreditManagementSystem.Common.Domain;
using System;

namespace CreditManagementSystem.Domain.ComandCredit
{
    public abstract class CreditCUCommand : Command
    {
        public Guid ClientID { get; set; }
        public double Amount { get; set; }
    }

}
