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
        public bool EditEquipment(Equipment data)
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
            var data = context.Equipments;
            var result = data.ToArray();
            return result;
        }
        public Equipment GetEquipment(int id)
        {
            var data = context.Equipments.FirstOrDefault(p => p.Id == id);
            return data;
        }
    }
}
