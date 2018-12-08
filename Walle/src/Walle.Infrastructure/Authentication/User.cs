using Microsoft.AspNetCore.Identity;

namespace Walle.Infrastructure.Authentication
{
    public class User : IdentityUser<int>
    {
        public string Name { get; set; }
    }
}
