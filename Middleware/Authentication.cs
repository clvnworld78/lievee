using lievee.Global;
using lievee.Models;
using lievee.Services;
using System.Security.Claims;

namespace lievee.Middleware
{
    public class Authentication
    {
        private readonly RequestDelegate _next;
        private const string AUTH_COOKIE = "token";
        public Authentication(RequestDelegate next) { _next = next; }

        public async Task InvokeAsync(HttpContext ctx, ISessionService service)
        {
            var token = ExtractToken(ctx.Request);
            if (token == null)
            {
                await _next(ctx);
                return;
            }

            var svc = await service.AuthenticateUserAsync(token);
            if (!svc.IsSuccess || svc.Data == null)
            {
                // ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
                // await ctx.Response.WriteAsync("invalid or expired token");
                await _next(ctx);
                return;
            }

            var user = svc.Data;

            ctx.Items[GlobalConstants.AuthUserCtx] = user;
            ctx.User = CreateClaimsPrincipal(user);

            await _next(ctx);
        }

        private string? ExtractToken(HttpRequest req)
        {
            if (req.Cookies.TryGetValue(AUTH_COOKIE, out var cookieToken))
            {
                return cookieToken;
            }
            else return null;
        }

        private ClaimsPrincipal? CreateClaimsPrincipal(Users? user)
        {
            if (user == null || user.Id == null || user.Username == null || user.Role == null)
            {
                return null;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()!),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString()!)
            };

            var identity = new ClaimsIdentity(claims, "Auth");
            return new ClaimsPrincipal(identity);
        }
    }
}
