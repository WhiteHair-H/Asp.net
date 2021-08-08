using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCPortFolio.Models;

namespace MVCPortFolio.Data
{
    public class MVCPortFolioContext : IdentityDbContext
    {
        public MVCPortFolioContext (DbContextOptions<MVCPortFolioContext> options)
            : base(options)
        {
        }

        public DbSet<Board> Board { get; set; }

        public DbSet<Contact> Contact { get; set; }

        public DbSet<Account> Account { get; set; }

        public DbSet<User> User { get; set; }

    }
}
