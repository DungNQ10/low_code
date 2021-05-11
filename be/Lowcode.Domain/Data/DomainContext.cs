using log4net.Repository.Hierarchy;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;

namespace Lowcode.Domain.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.IO;
    using Lowcode.Domain.Data.Entity;

    public partial class DomainContext : DbContext
    {
        public DomainContext()
        {
        }

        public DomainContext(DbContextOptions<DomainContext> options) : base(options)
        {
            // Database.SetInitializer<DomainContext>(null);
        }

        public static string ConnectionString { get; private set; }

        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Attachments> Attachments { get; set; }
       

        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<GroupUser> GroupUser { get; set; }
        public virtual DbSet<DynamicForm> DynamicForm { get; set; }
        public virtual DbSet<DynamicFormApi> DynamicFormApi { get; set; }
        public virtual DbSet<Country> Country { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Roles>().ToTable("Roles");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Attachments>().ToTable("Attachments");
            modelBuilder.Entity<Group>().ToTable("Group");
            modelBuilder.Entity<GroupUser>().ToTable("GroupUser");
            modelBuilder.Entity<DynamicForm>().ToTable("DynamicForm");
            modelBuilder.Entity<Country>().ToTable("Country");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!String.IsNullOrEmpty(ConnectionString))
            {
                optionsBuilder.UseSqlServer(ConnectionString);
                return;
            }

            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationBuilder builder = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json");

                IConfigurationRoot configuration = builder.Build();
                var env = configuration.GetValue<string>("EnvironmentName");

                configuration = builder.AddJsonFile($"appsettings.{env}.json", optional: true).Build();

                ConnectionString = configuration.GetConnectionString("DefaultConnection");
                
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }
    }
}