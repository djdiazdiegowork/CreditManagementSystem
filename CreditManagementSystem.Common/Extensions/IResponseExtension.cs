using CreditManagementSystem.Common.Domain;
using CreditManagementSystem.Common.Responses;

namespace CreditManagementSystem.Common.Extensions
{
    public static class IResponseExtension
    {
        public static IResponse OkResponse<T>(this ICommand _, T body, string uiText = null)
        {
            return new Response<T>(200, body, uiText);
        }
    }
}
