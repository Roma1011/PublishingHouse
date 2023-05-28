using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PublishingHouse.Filters;

public class AuthorizationFilter:Attribute,IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!IsUserAuthorized(context.HttpContext.User))
        {
            context.Result = new StatusCodeResult(403);
        }
    }
    
    private bool IsUserAuthorized(ClaimsPrincipal user)
    { 
        return user.IsInRole("Manager");
    }
}