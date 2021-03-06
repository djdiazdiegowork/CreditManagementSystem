﻿using System;

namespace CreditManagementSystem.Common.Responses
{
    public sealed class Response<T> : IResponse
    {
        public Response()
        {
        }

        public Response(int code, T body, string uiText)
        {
            this.Code = code;
            this.Body = body;
            this.UIText = uiText;
        }

        public int Code { get; private set; }

        public string UIText { get; private set; }

        public T Body { get; private set; }

        public bool IsSuccess
        {
            get
            {
                return 200 <= this.Code && this.Code < 300;
            }
        }

        public Type GetBodyType()
        {
            T body = this.Body;
            ref T local = ref body;
            return local == null ? (Type)null : local.GetType();
        }
    }
}
