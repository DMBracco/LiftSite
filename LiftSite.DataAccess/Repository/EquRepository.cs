using LiftSite.Domain.Entities;
using LiftSite.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiftSite.DataAccess.Repository
{
    public class EquRepository : IEquRepository
    {
        private readonly LiftSiteContext context;

        public EquRepository(LiftSiteContext context)
        {
            if (context == null) throw new ArgumentNullException("context");

            this.context = context;
        }

        public bool CreateEquipment(Equipment data)
        {
            context.Equipments.Add(data);
            context.SaveChanges();
            return true;
        }
        public bool UpdateEquipment(Equipment data)
        {
            context.Equipments.Update(data);
            context.SaveChanges();
            return true;
        }
        public bool DeleteEquipment(int id)
        {
            var q = context.Equipments.FirstOrDefault(p => p.Id == id);
            if (q != null)
            {
                context.Equipments.Remove(q);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public IEnumerable<Equipment> GetListEquipment()
        {
            var data = from e in context.Equipments
                       join b in context.Brands on e.BrandId equals b.Id into BrandGroup
                       from brand in BrandGroup.DefaultIfEmpty()
                       join t in context.TypeEquipments on e.TypeId equals t.Id into TypeGroup
                       from type in TypeGroup.DefaultIfEmpty()
                       select new Equipment
                       {
                           Id = e.Id,
                           Name = e.Name,
                           Brand = brand,
                           Model = e.Model,
                           Description = e.Description,
                           Images = e.Images,
                           TypeEquipment = type
                       };
            var result = data.ToArray();
            return result;
        }
        public Equipment GetEquipment(int id)
        {
            var data = (from e in context.Equipments
                        join b in context.Brands on e.BrandId equals b.Id into BrandGroup
                        from brand in BrandGroup.DefaultIfEmpty()
                        join t in context.TypeEquipments on e.TypeId equals t.Id into TypeGroup
                        from type in TypeGroup.DefaultIfEmpty()
                        where e.Id == id
                        select new Equipment
                        {
                            Id = e.Id,
                            Name = e.Name,
                            Brand = brand,
                            Model = e.Model,
                            Description = e.Description,
                            Images = e.Images,
                            TypeEquipment = type
                        }).FirstOrDefault();
            return data;
        }
    }
}
