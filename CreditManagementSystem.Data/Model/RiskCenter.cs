using CreditManagementSystem.Client.Model;
using CreditManagementSystem.Common.Data;

namespace CreditManagementSystem.Data.Model
{
    public class RiskCenter : Enumeration<RiskCenterValue>
    {
        public static RiskCenter Good = new RiskCenter { ID = RiskCenterValue.Good, Name = RiskCenterValue.Good.ToString() };
        public static RiskCenter Bad = new RiskCenter { ID = RiskCenterValue.Bad, Name = RiskCenterValue.Bad.ToString() };
        public static RiskCenter Regular = new RiskCenter { ID = RiskCenterValue.Regular, Name = RiskCenterValue.Regular.ToString() };
    }
}
