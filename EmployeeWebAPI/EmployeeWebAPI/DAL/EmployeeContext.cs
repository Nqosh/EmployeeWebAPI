using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using EmployeeWebAPI.Models;

namespace EmployeeWebAPI.DAL
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext() : base("DefaultConnection")
        {
        }

        public DbSet<Person> Person { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<EmployeeContext>(null);
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Conventions.Remove(Pluraliza)
        }

    }
}