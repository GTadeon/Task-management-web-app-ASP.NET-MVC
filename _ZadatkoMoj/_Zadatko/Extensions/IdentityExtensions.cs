using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace _Zadatko.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetIme(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Ime");
            return (claim!=null)?claim.Value:string.Empty;
        }

        public static string GetPrezime(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Prezime");
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}