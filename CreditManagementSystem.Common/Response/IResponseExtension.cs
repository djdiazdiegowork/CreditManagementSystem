using CreditManagementSystem.Common.Domain;

namespace CreditManagementSystem.Common.Response
{
    public static class IResponseExtension
    {
        public static IResponse OkResponse<T>(this ICommand _, T Body, string uiText = null)
        {
            return new Response<T> {
                //Command = command,
                Body = Body,
                Code = 200,
                UIText = uiText
            };
        }
    }
}
