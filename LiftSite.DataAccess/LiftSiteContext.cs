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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.connectionString); //определяет конфигурация подключения
        }
    }
}
