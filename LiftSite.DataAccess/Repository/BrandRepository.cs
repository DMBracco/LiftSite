using LiftSite.Domain.Entities;
using LiftSite.Domain.IRepository;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace LiftSite.DataAccess.Repository
{
    public class BrandRepository : IBrandRepository
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
        public bool UpdateBrand(Brand brand)
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
            var data = from b in context.Brands
                              join i in context.Images
                              on b.Id equals i.BrandId
                              into BrandImagesGroup
                              from brandImg in BrandImagesGroup.DefaultIfEmpty()
                              select new Brand{
                                  Id = b.Id,
                                  Name = b.Name,
                                  BrandImage = brandImg,
                                  Number = b.Number,
                                  Sorting = b.Sorting,
                              };

            var result = data.ToArray();
            return result;
        }
        public Brand GetBrand(int id)
        {
            var result = (from b in context.Brands
                             join i in context.Images
                             on b.Id equals i.BrandId
                             into BrandImagesGroup
                             from brandImg in BrandImagesGroup.DefaultIfEmpty()
                             where b.Id == id
                              select new Brand
                             {  
                                 Id = b.Id,
                                 Name = b.Name,
                                 BrandImage = brandImg,
                                 Number = b.Number,
                                 Sorting = b.Sorting,
                             }).FirstOrDefault(); // результат

            return result;
        }
    }
}
