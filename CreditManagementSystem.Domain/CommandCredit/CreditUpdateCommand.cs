using CreditManagementSystem.Data.ValueModels;
using System;

namespace CreditManagementSystem.Domain.CommandCredit
{
    public sealed class CreditUpdateCommand : CreditCUCommand
    {
        public Guid ID { get; set; }
        public double DebtPaid { get; set; }
        public CreditStatusValue CreditStatusID { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
