using LiftSite.Domain.Entities;
using LiftSite.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiftSite.DataAccess.Repository
{
    class TypeEquRepository : ITypeEquRepository
    {
        private readonly LiftSiteContext context;

        public TypeEquRepository(LiftSiteContext context)
        {
            if (context == null) throw new ArgumentNullException("context");

            this.context = context;
        }

        public bool CreateTypeEqu(TypeEquipment data)
        {
            context.TypeEquipments.Add(data);
            context.SaveChanges();
            return true;
        }
        public bool EditTypeEqu(TypeEquipment data)
        {
            context.TypeEquipments.Update(data);
            context.SaveChanges();
            return true;
        }
        public bool DeleteTypeEqu(int id)
        {
            var q = context.TypeEquipments.FirstOrDefault(p => p.Id == id);
            if (q != null)
            {
                context.TypeEquipments.Remove(q);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public IEnumerable<TypeEquipment> GetListTypeEqu()
        {
            var data = context.TypeEquipments;
            var result = data.ToArray();
            return result;
        }
    }
}
