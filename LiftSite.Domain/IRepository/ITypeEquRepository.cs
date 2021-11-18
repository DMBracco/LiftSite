using LiftSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiftSite.Domain.IRepository
{
    public interface ITypeEquRepository
    {
        public bool CreateTypeEqu(TypeEquipment brand);
        public bool EditTypeEqu(TypeEquipment brand);
        public bool DeleteTypeEqu(int id);
        IEnumerable<TypeEquipment> GetListTypeEqu();
    }
}
