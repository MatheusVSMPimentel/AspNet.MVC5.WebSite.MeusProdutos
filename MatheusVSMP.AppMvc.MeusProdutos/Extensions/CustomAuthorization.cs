using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace MatheusVSMP.AppMvc.MeusProdutos.Extensions
{
    public class CustomAuthorization
    {
        public static bool ValidarClaimsUsuario(string claimName, string claimValue)
        {
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            var claim = identity.Claims.FirstOrDefault(c => c.Type == claimName);
            return claim != null && claim.Value.Contains(claimValue);
        }
    }

    public class ClaimsAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string _claimValue;
        private readonly string _claimName;

        public ClaimsAuthorizeAttribute(string claimValue, string claimName)
        {
            _claimValue = claimValue;
            _claimName = claimName;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                filterContext.Result = new HttpStatusCodeResult((int)HttpStatusCode.Forbidden);
            }
            else {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return CustomAuthorization.ValidarClaimsUsuario(_claimValue, _claimName);
        }
    }
}