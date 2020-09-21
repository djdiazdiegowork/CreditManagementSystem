using CreditManagementSystem.Client.Model;
using CreditManagementSystem.Common.Data;
using System;

namespace CreditManagementSystem.Data.Model
{
    public class Credit : Entity<Guid>
    {
        public Credit() { }

        public Credit(Guid id, Guid clientID, double amount)
        {
            this.ID = id;
            this.ClientID = clientID;
            this.Amount = amount;
            this.CreditStatusID = CreditStatusValue.Pending;
            this.CreationDay = DateTime.UtcNow;
        }

        public void UpdateCredit(Guid clientID, double amount, CreditStatusValue creditStatusId,
            double debtPaid, DateTime? dueDate)
        {
            this.ClientID = clientID;
            this.Amount = amount;
            this.CreditStatusID = creditStatusId;
            this.DebtPaid = debtPaid;
            this.DueDate = dueDate;
            this.ModificationDay = DateTime.UtcNow;
        }

        public Guid ClientID { get; private set; }
        public double Amount { get; private set; }
        public double DebtPaid { get; private set; }
        public DateTime CreationDay { get; private set; }
        public DateTime? ModificationDay { get; private set; }
        public DateTime? DueDate { get; private set; }
        public CreditStatusValue CreditStatusID { get; private set; }
        public virtual CreditStatus CreditStatus { get; private set; }
    }
}
