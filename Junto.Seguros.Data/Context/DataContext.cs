using System;
using System.Collections.Generic;
using System.Text;
using Junto.Seguros.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Junto.Seguros.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
    }
}
