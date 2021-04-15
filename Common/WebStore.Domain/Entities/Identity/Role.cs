using Microsoft.AspNetCore.Identity;

namespace WebStore.Domain.Entities.Identity
{
    public class Role : IdentityRole
    {
        public const string _administrators = "Administrator";

        public const string _users = "Users";
    }
}
