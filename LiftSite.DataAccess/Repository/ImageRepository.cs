using LiftSite.Domain.Entities;
using LiftSite.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiftSite.DataAccess.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly LiftSiteContext context;

        public ImageRepository(LiftSiteContext context)
        {
            if (context == null) throw new ArgumentNullException("context");

            this.context = context;
        }

        public bool CreateImage(Image file)
        {
            context.Images.Add(file);
            context.SaveChanges();
            return true;
        }
        public bool CreateImageAsync(Image file)
        {
            context.Images.AddAsync(file);
            context.SaveChangesAsync();
            return true;
        }
        public bool CreateImagesForeach(List<Image> data)
        {
            foreach(var item in data)
            {
                context.Images.Add(item);
            }
            context.SaveChanges();
            return true;
        }
        public bool EditImage(Image data)
        {
            context.Images.Update(data);
            context.SaveChanges();
            return true;
        }
        public bool DeleteImage(int id)
        {
            var q = context.Images.FirstOrDefault(p => p.Id == id);
            if (q != null)
            {
                context.Images.Remove(q);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool DeleteImageByBrand(int brandId)
        {
            var imageByBrand = context.Images.Where(p => p.BrandId == brandId);
            if (imageByBrand != null)
            {
                foreach(var item in imageByBrand)
                {
                    context.Images.Remove(item);
                }
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public IEnumerable<Image> GetListImage()
        {
            var data = context.Images;
            var result = data.ToArray();
            return result;
        }
        public Image GetImage(int id)
        {
            var data = context.Images.FirstOrDefault(p => p.Id == id);
            return data;
        }

        public Image GetImageByGuid(string guid)
        {
            var data = context.Images.FirstOrDefault(p => p.Guid == guid);
            return data;
        }

        public Image GetImageByBrandId(int brandId)
        {
            var data = context.Images.FirstOrDefault(p => p.BrandId == brandId);
            return data;
        }
        public Image GetImageByEquipmentId(int id)
        {
            var data = context.Images.FirstOrDefault(p => p.EquipmentId == id);
            return data;
        }
    }
}
