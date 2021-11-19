﻿using LiftSite.Domain.Entities;
using LiftSite.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public bool CreateImage(Image data)
        {
            context.Images.Add(data);
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
    }
}
