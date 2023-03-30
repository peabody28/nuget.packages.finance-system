using Microsoft.AspNetCore.Authentication;

namespace Testing.Integration.Core
{
    public class TestAuthenticationSchemeOptions : AuthenticationSchemeOptions
    {
        public string UserName { get; set; } = null!;

        public string UserRole { get; set; } = null!;
    }
}
