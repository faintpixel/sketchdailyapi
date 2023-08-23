using Auth0.ManagementApi.Models;
using Microsoft.AspNetCore.Mvc;
using SketchDailyAPI.Models.References;
using User = SketchDailyAPI.Models.References.User;

namespace SketchDailyAPI.Controllers.References
{
    public class BaseController : Controller
    {
        protected User GetCurrentUser()
        {
            var user = new User();

            try
            {
                user.Name = HttpContext.User.Claims.First(c => c.Type == "nickname" && c.Issuer == @"https://sketchdaily.auth0.com/").Value;
                user.Email = HttpContext.User.Claims.First(c => c.Type == @"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress" && c.Issuer == @"https://sketchdaily.auth0.com/").Value;

                var role = HttpContext.User.Claims.FirstOrDefault(c => c.Type == @"https://reference.sketchdaily.net/roles" && c.Issuer == @"https://sketchdaily.auth0.com/").Value;
                user.IsAdmin = role == "admin";
            }
            catch (Exception)
            {
                user.IsAdmin = false;
                user.Name = "Unknown";
                user.Email = "Unknown";
            }
            return user;
        }
    }
}
