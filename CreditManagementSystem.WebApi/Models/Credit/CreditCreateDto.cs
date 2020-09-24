using System;

namespace CreditManagementSystem.WebApi.Models.Credit
{
    public sealed class CreditCreateDto
    {
        public Guid ClientID { get; set; }
        public double Amount { get; set; }
    }
}
