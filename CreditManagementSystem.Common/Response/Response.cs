using System;

namespace CreditManagementSystem.Common.Response
{
    public class Response<T> : IResponse
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

        public int Code { get; set; }

        public string UIText { get; set; }

        public T Body { get; set; }

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
            return (object)local == null ? (Type)null : local.GetType();
        }
    }
}
