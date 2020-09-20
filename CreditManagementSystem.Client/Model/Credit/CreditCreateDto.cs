using System;

namespace CreditManagementSystem.Client.Model.Credit
{
    public sealed class CreditCreateDto
    {
        public Guid ClientID { get; set; }
        public double Amount { get; set; }
    }
}
