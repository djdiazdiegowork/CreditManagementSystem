using CreditManagementSystem.Common.Data;
using CreditManagementSystem.Common.Response;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CreditManagementSystem.Common.Extension
{
    public static class ExceptionExtension
    {
        public static IResponse ResponseValidationException(this ValidationException exception)
        {
            var body = exception.Errors.Select(e => new Dictionary<string, string> {
                {e.PropertyName, e.ErrorMessage }
            });

            return new Response<object>(400, body, exception.Message);
        }

        public static IResponse ResponseEventException(this EventException exception)
        {
            var errorsList = exception.Errors.Select(e =>
            {
                var result = new Dictionary<string, IEnumerable<string>>();

                if (e.Body is Exception exception)
                {
                    var eventName = e.Event?.GetType().Name ?? "";
                    result.Add(eventName, BuildErrorsList(exception));
                }

                return result;
            }).ToArray();

            var body = new Dictionary<string, IEnumerable<Dictionary<string, IEnumerable<string>>>>
            {
                { exception.Message, errorsList }
            };

            return new Response<object>(500, body, exception.Message);
        }

        public static IResponse ResponseException(this Exception exception)
        {
            var body = new Dictionary<string, IEnumerable<string>> {
                { "Fatal error", BuildErrorsList(exception)}
            };

            return new Response<object>(500, body, "Server Error");
        }

        private static IEnumerable<string> BuildErrorsList(Exception exception)
        {
            while (exception != null)
            {
                yield return exception.Message;
                exception = exception.InnerException;
            }
        }
    }
}
