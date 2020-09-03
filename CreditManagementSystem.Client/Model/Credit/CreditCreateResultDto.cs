using System;

namespace CreditManagementSystem.Client.Model.Credit
{
    public class CreditCreateResultDto
    {
        public Guid ID { get; set; }
        public Guid ClientID { get; set; }
        public double Amount { get; set; }
        public double? DebtPaid { get; set; }
        public DateTime CreationDay { get; set; }
        public DateTime? DueDate { get; set; }
        public CreditStatusValue CreditStatusID { get; set; }
    }
}
