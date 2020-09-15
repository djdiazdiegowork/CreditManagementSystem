using CreditManagementSystem.Common.Domain;
using CreditManagementSystem.Common.Response;

namespace CreditManagementSystem.Common.Extension
{
    public static class IResponseExtension
    {
        public static IResponse OkResponse<T>(this ICommand _, T body, string uiText = null)
        {
            return new Response<T>(200, body, uiText);
        }
    }
}
