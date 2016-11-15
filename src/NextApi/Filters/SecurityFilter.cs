using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using NextApi.Models;

namespace NextApi.Filters
{
    public class SecurityFilter : IAsyncAuthorizationFilter
    {
        private string Key { get; }
        private string Value { get; }
        public SecurityFilter(IOptions<SecuritySettings> securitySettings)
        {
            var settings = securitySettings.Value;
            Key = settings.Key;
            Value = settings.Value;
        }

        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var hasKey = context.HttpContext.Request.Headers.Keys.Contains(Key);

            if (!hasKey && context.HttpContext.Request.Headers[Key] != Value)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
                context.HttpContext.Response.WriteAsync("Error in request");
            }

            return Task.FromResult<object>(null);
        }
    }
}
