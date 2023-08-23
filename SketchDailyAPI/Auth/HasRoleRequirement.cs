using Microsoft.AspNetCore.Authorization;

namespace SketchDailyAPI.Auth
{
    public class HasRoleRequirement : IAuthorizationRequirement
    {
        public string Issuer { get; }
        public string Role { get; }

        public HasRoleRequirement(string role, string issuer)
        {
            Role = role ?? throw new ArgumentNullException(nameof(role));
            Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
        }
    }
}
