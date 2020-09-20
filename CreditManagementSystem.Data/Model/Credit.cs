using CreditManagementSystem.Client.Model;
using CreditManagementSystem.Common.Data;
using System;

namespace CreditManagementSystem.Data.Model
{
    public class Credit : Entity<Guid>
    {
        public Guid ClientID { get; set; }
        public double Amount { get; set; }
        public double DebtPaid { get; set; }
        public DateTime CreationDay { get; set; }
        public DateTime? ModificationDay { get; set; }
        public DateTime? DueDate { get; set; }
        public CreditStatusValue CreditStatusID { get; set; }
        public virtual CreditStatus CreditStatus { get; set; }
    }
}
