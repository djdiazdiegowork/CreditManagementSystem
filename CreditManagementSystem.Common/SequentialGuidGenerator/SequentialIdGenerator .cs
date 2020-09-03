using System;

namespace CreditManagementSystem.Common.SequentialGuidGenerator
{
    public class SequentialIdGenerator : IIdGenerator
    {
        public Guid NewId() => SequentialGuid.SequentialSqlGuidGenerator.Instance.NewGuid();
    }
}
