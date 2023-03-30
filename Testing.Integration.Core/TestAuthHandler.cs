using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Testing.Integration.Core.Constants;

namespace Testing.Integration.Core
{
    /// <summary>
    /// This class override auth handlers
    /// </summary>
    public class TestAuthHandler : AuthenticationHandler<TestAuthenticationSchemeOptions>
    {
        public const string AuthenticationScheme = AuthenticationConstants.TestAuthenticationScheme;

        private string UserName { get; set; }

        private string UserRole { get; set; }

        public TestAuthHandler(IOptionsMonitor<TestAuthenticationSchemeOptions> options,
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
            this.UserName = options.CurrentValue.UserName;
            this.UserRole = options.CurrentValue.UserRole;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var claims = new[] 
            { 
                new Claim(ClaimTypes.Name, UserName),
                new Claim(ClaimTypes.Role, UserRole)
            };

            var identity = new ClaimsIdentity(claims, AuthenticationConstants.TestAuthenticationType);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, AuthenticationScheme);

            var result = AuthenticateResult.Success(ticket);

            return Task.FromResult(result);
        }
    }
}
