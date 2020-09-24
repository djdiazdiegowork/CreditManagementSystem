using System;

namespace CreditManagementSystem.Common.SequentialGuidGenerator
{
    public sealed class SequentialIdGenerator : IIdGenerator
    {
        public Guid NewId() => SequentialGuid.SequentialSqlGuidGenerator.Instance.NewGuid();
    }
}
