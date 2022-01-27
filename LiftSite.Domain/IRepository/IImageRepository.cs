using LiftSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiftSite.Domain.IRepository
{
    public interface IImageRepository
    {
        public bool CreateImage(Image data);
        public bool CreateImageAsync(Image data);
        public bool CreateImagesForeach(List<Image> data);
        public bool EditImage(Image data);
        public bool DeleteImage(int id);
        public bool DeleteImageByBrand(int brandId);
        public IEnumerable<Image> GetListImage();
        public Image GetImage(int id);
        public Image GetImageByGuid(string guid);
        public Image GetImageByBrandId(int brandId);
    }
}
