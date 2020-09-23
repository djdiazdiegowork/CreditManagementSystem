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
            var body = exception.Errors.Select(e =>
            {
                var result = new Dictionary<string, string>();

                if (e.Body is Exception exception)
                {
                    var eventName = e.Event?.GetType().Name ?? "";

                    result.Add(eventName, exception.Message);

                    BuildErrorsDictionary(result, exception.InnerException, eventName);
                }

                return result;
            }).ToArray();

            return new Response<object>(500, body, exception.Message);
        }

        public static IResponse ResponseException(this Exception exception)
        {
            var body = new Dictionary<string, IEnumerable<string>> {
                        { "Fatal error", new string[] { exception.Message } }
                    };

            return new Response<object>(500, body, "Server Error");
        }

        private static void BuildErrorsDictionary(
            Dictionary<string, string> result,
            Exception exception,
            string eventName)
        {
            var count = 0;

            while (exception != null)
            {
                result.Add(eventName + $" ({nameof(Exception.InnerException)}: {count})", exception.Message);
                exception = exception.InnerException;
                count++;
            }
        }
    }
}
