using System;
using System.Reflection;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EMS_Web_App.StaticFunc
{
	public class AuthorizeToken : AttributeAuthorizationHandler<TokenAuthorizationRequirement, TokenAttribute>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, TokenAuthorizationRequirement requirement, IEnumerable<TokenAttribute> attributes)
        {
            foreach (var permissionAttribute in attributes)
            {
                if (!await AuthorizeAsync(context.User, permissionAttribute.Name))
                {
                    return;
                }
            }

            context.Succeed(requirement);
        }

        private async Task<bool> AuthorizeAsync(ClaimsPrincipal user, string permission)
        {
            //Implement your custom user permission logic here
            bool result = true;
            return await Task.FromResult(result);
        }
    }
    public class TokenAuthorizationRequirement : IAuthorizationRequirement
    {
        //Add any custom requirement properties if you have them
    }
    public class TokenAttribute : AuthorizeAttribute
    {
        public string Name { get; }

        public TokenAttribute(string name) : base("Permission")
        {
            Name = name;
        }
    }
    public abstract class AttributeAuthorizationHandler<TRequirement, TAttribute> : AuthorizationHandler<TRequirement> where TRequirement : IAuthorizationRequirement where TAttribute : Attribute
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TRequirement requirement)
        {
            var attributes = new List<TAttribute>();

            var action = (context.Resource as AuthorizationFilterContext)?.ActionDescriptor as ControllerActionDescriptor;
            if (action != null)
            {
                attributes.AddRange(GetAttributes(action.ControllerTypeInfo.UnderlyingSystemType));
                attributes.AddRange(GetAttributes(action.MethodInfo));
            }

            return HandleRequirementAsync(context, requirement, attributes);
        }

        protected abstract Task HandleRequirementAsync(AuthorizationHandlerContext context, TRequirement requirement, IEnumerable<TAttribute> attributes);

        private static IEnumerable<TAttribute> GetAttributes(MemberInfo memberInfo)
        {
            return memberInfo.GetCustomAttributes(typeof(TAttribute), false).Cast<TAttribute>();
        }
    }
}

