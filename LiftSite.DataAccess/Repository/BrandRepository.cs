using LiftSite.Domain.Entities;
using LiftSite.Domain.IRepository;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace LiftSite.DataAccess.Repository
{
    class BrandRepository : IBrandRepository
    {
        private readonly LiftSiteContext context;

        public BrandRepository(LiftSiteContext context)
        {
            if (context == null) throw new ArgumentNullException("context");

            this.context = context;
        }

        public bool CreateBrand(Brand brand)
        {
            context.Brands.Add(brand);
            context.SaveChanges();
            return true;
        }
        public bool EditBrand(Brand brand)
        {
            context.Brands.Update(brand);
            context.SaveChanges();
            return true;
        }
        public bool DeleteBrand(int id)
        {
            var b = context.Brands.FirstOrDefault(p => p.Id == id);
            if (b != null)
            {
                context.Brands.Remove(b);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public IEnumerable<Brand> GetListBrand()
        {
            var data = context.Brands;
            var result = data.ToArray();
            return result;
        }
    }
}
