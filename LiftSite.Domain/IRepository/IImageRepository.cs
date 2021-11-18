using LiftSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiftSite.Domain.IRepository
{
    public interface IImageRepository
    {
        public bool CreateImage(Image data);
        public bool EditImage(Image data);
        public bool DeleteImage(int id);
        public IEnumerable<Image> GetListImage();
        public Image GetImage(int id);
    }
}
