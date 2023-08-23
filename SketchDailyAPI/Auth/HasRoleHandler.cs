using Microsoft.AspNetCore.Authorization;

namespace SketchDailyAPI.Auth
{
    public class HasRoleHandler : AuthorizationHandler<HasRoleRequirement>
    {
        private readonly string @ROLE_CLAIM = @"https://reference.sketchdaily.net/roles";

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasRoleRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ROLE_CLAIM && c.Issuer == requirement.Issuer))
                return Task.CompletedTask;

            var roles = context.User.FindFirst(c => c.Type == ROLE_CLAIM && c.Issuer == requirement.Issuer).Value;

            if (roles == "admin")
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
