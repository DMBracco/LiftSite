using LiftSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiftSite.DataAccess.Repository
{
    public class EquRepository
    {
        private readonly LiftSiteContext context;

        public EquRepository(LiftSiteContext context)
        {
            if (context == null) throw new ArgumentNullException("context");

            this.context = context;
        }

        public bool CreateTypeEqu(Equipment data)
        {
            context.Equipments.Add(data);
            context.SaveChanges();
            return true;
        }
        public bool EditTypeEqu(Equipment data)
        {
            context.Equipments.Update(data);
            context.SaveChanges();
            return true;
        }
        public bool DeleteImage(int id)
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
