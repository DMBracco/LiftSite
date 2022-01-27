using LiftSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiftSite.Domain.IRepository
{
    public interface ITypeEquRepository
    {
        public bool CreateTypeEqu(TypeEquipment typeEq);
        public bool UpdateTypeEqu(TypeEquipment typeEq);
        public bool DeleteTypeEqu(int id);
        IEnumerable<TypeEquipment> GetListTypeEqu();
        TypeEquipment GetTypeEqu(int id);
    }
}
