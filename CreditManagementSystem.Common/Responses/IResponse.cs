using System;

namespace CreditManagementSystem.Common.Responses
{
    public interface IResponse
    {
        int Code { get; }

        string UIText { get; }

        bool IsSuccess { get; }

        Type GetBodyType();
    }
}
