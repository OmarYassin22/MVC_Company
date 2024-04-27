using Demo.DataAccess.Contexts.Configurations;
using Demo.DataAccess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Contexts
{
    public class MainContext:IdentityDbContext<AppUser>
    {

        public DbSet<Department> departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public MainContext(DbContextOptions<MainContext> options):base(options)
        {
            
        }
        public DbSet<Department> Departments{ get; set; }
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;DataBase=MVC_Session03;Trusted_Connection=True");
        }*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new DepartmentConf());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
