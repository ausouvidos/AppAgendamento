using System;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class AusOuvidosContext : DbContext
    {
        public AusOuvidosContext(DbContextOptions<AusOuvidosContext> options) : base(options)
        {

        }
    }
}
