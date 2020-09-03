using System;

namespace CreditManagementSystem.Common.SequentialGuidGenerator
{
    public interface IIdGenerator
    {
        Guid NewId();
    }
}
