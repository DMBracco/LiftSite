using LiftSite.Domain.Entities;
using System.Collections.Generic;

namespace LiftSite.Domain.IRepository
{
    public interface IBrandRepository
    {
        public bool CreateBrand(Brand brand);
        public bool UpdateBrand(Brand brand);
        public bool DeleteBrand(int id);
        IEnumerable<Brand> GetListBrand();
        public Brand GetBrand(int id);
    }
}
