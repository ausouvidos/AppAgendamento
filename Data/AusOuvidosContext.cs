using System;
using Microsoft.EntityFrameworkCore;
using Models.Identity;

namespace Data
{
    public class AusOuvidosContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public AusOuvidosContext(DbContextOptions<AusOuvidosContext> options) : base(options)
        {

        }
    }
}
