using System;

namespace CreditManagementSystem.Client.Model.Credit
{
    public class CreditCreateDto
    {
        public Guid ClientID { get; set; }
        public double Amount { get; set; }
    }
}
