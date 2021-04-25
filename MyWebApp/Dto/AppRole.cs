using Microsoft.AspNetCore.Identity;
using System;

namespace MyWebApp.Dto
{
    public class AppRole : IdentityRole<Guid>
    {
        public AppRole()
        {
        }
        public AppRole(string roleName) : base(roleName)
        {
            Id = Guid.NewGuid();
        }
    }
}
