using Microsoft.EntityFrameworkCore;
using System;
using LiftSite.Domain.Entities;

namespace LiftSite.DataAccess
{
    public class LiftSiteContext : DbContext
    {
        private readonly string connectionString;

        public LiftSiteContext(string connectionString)
        {
            if (connectionString == null) throw new ArgumentNullException(nameof(connectionString));
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Значение не должно быть пустым. ", nameof(connectionString));

            this.connectionString = connectionString;
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<TypeEquipment> TypeEquipments { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Equipment> Equipments { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.connectionString); //определяет конфигурация подключения
        }

        //public LiftSiteContext(DbContextOptions<LiftSiteContext> options)
        //    : base(options)
        //{
        //    Database.EnsureCreated();
        //}
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    string adminRoleName = "admin";
        //    string userRoleName = "user";

        //    string adminEmail = "admin@mail.ru";
        //    string adminPassword = "123456";

        //    // добавляем роли
        //    Role adminRole = new Role { Id = 1, Name = adminRoleName };
        //    Role userRole = new Role { Id = 2, Name = userRoleName };
        //    User adminUser = new User { Id = 1, Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id };

        //    modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
        //    modelBuilder.Entity<User>().HasData(new User[] { adminUser });
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
