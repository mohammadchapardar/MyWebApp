using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyWebApp.Dto;
using MyWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApp.Repository
{
    public class AuthDbContext:IdentityDbContext<AppUser,AppRole,Guid>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options):base(options)
        {
        }
    }
}
