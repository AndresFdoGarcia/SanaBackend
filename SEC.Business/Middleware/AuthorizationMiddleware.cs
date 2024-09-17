using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SEC.Business.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEC.Business.Middleware
{
    public class AuthorizationMiddleware : Attribute, IAsyncAuthorizationFilter
    {
        private List<long> EndpointPermissions { get; }
        private bool StrictPermissions { get; }
        private bool WithOutToken { get; }
        public AuthorizationMiddleware(string endpointPermissions = "*", bool strictPermissions = false,
           bool withOutToken = false)
        {
            EndpointPermissions = endpointPermissions.Equals("*")
                ? new List<long>()
                : endpointPermissions.Split(',').Select(long.Parse).ToList();
            StrictPermissions = strictPermissions;
            WithOutToken = withOutToken;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                var authorizationService = GetAuthorizationService(context);
                context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authorizationToken);
                if (!string.IsNullOrEmpty(authorizationToken))
                {
                    var user = await authorizationService.ValidateCredentials(authorizationToken);
                    if (user == null)
                        context.Result = new ObjectResult(new { message = "Unauthorized" })
                        {
                            StatusCode = StatusCodes.Status401Unauthorized
                        };
                    else
                        context.HttpContext.Items["UserApi"] = user;
                }
                else
                {
                    context.Result = new ObjectResult(new { message = "Unauthorized" })
                    {
                        StatusCode = StatusCodes.Status401Unauthorized
                    };
                }
            }
            catch (Exception e)
            {
                context.Result = new ObjectResult(new { message = "An error occurred processing the request, try again" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        private static AuthorizationService GetAuthorizationService(ActionContext context)
        {
            var serviceProvider = context.HttpContext.RequestServices;
            return serviceProvider.GetService(typeof(AuthorizationService)) as AuthorizationService;
        }
    }
}

