using CreditManagementSystem.Common.Data;
using CreditManagementSystem.Data.ValueModels;

namespace CreditManagementSystem.Data.Models
{
    public class CreditStatus : Enumeration<CreditStatusValue>
    {
        public static CreditStatus Accepted = new CreditStatus { ID = CreditStatusValue.Accepted, Name = CreditStatusValue.Accepted.ToString() };
        public static CreditStatus Denied = new CreditStatus { ID = CreditStatusValue.Denied, Name = CreditStatusValue.Denied.ToString() };
        public static CreditStatus Pending = new CreditStatus { ID = CreditStatusValue.Pending, Name = CreditStatusValue.Pending.ToString() };
    }
}
