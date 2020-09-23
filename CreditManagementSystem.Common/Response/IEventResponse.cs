﻿using CreditManagementSystem.Common.Data;
using System;

namespace CreditManagementSystem.Common.Response
{
    public interface IEventResponse
    {
        int Code { get; }

        IEvent Event { get; }

        object Body { get; }

        bool IsSuccess
        {
            get;
        }

        Type GetBodyType();
    }
}
