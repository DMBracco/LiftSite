using LiftSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiftSite.Domain.IRepository
{
    public interface IEquRepository
    {
        public bool CreateEquipment(Equipment data);
        public bool EditEquipment(Equipment data);
        public bool DeleteEquipment(int id);
        public IEnumerable<Equipment> GetListEquipment();
        public Equipment GetEquipment(int id);
    }
}
