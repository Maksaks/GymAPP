using Course_project_GYMAPP.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_project_GYMAPP.DAL
{
    public class AppDbContext : DbContext
    {

        public DbSet<User> User { get; set; }
        public DbSet<User> InGymUsers { get; set; }
        public DbSet<Trainer> Trainer { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<PersonalCard> PersonalCards { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }
    }
}
