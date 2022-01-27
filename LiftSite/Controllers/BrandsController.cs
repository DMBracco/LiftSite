using LiftSite.Domain.Entities;
using LiftSite.Domain.IRepository;
using LiftSite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace LiftSite.Controllers
{
    [Authorize]
    public class BrandsController : Controller
    {
        private readonly IBrandRepository brandRepository;
        private readonly IImageRepository imageRepository;

        public BrandsController(IBrandRepository brandRepository, IImageRepository imageRepository)
        {
            if (brandRepository == null) throw new ArgumentNullException(nameof(brandRepository));
            if (imageRepository == null) throw new ArgumentNullException(nameof(imageRepository));

            this.imageRepository = imageRepository;
            this.brandRepository = brandRepository;
        }
        public ActionResult Index()
        {
            var Brands = brandRepository.GetListBrand();
            var list = new List<BrandViewModel>();

            foreach (var brand in Brands)
            {
                var item = new BrandViewModel
                {
                    Id = brand.Id,
                    Name = brand.Name,
                    BrandImage = brand.BrandImage,
                    Number = brand.Number,
                    Sorting = brand.Sorting,
                };

                list.Add(item);
            }
            return View(list);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(BrandEditViewModel brand)
        {
            if (ModelState.IsValid)
            {
                var model = new Brand
                {
                    Name = brand.Name,
                    Number = brand.Number,
                    Sorting = brand.Sorting,
                };
                brandRepository.CreateBrand(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            Brand brand = brandRepository.GetBrand(id);
            var item = new BrandViewModel
            {
                Id = brand.Id,
                Name = brand.Name,
                BrandImage = brand.BrandImage,
                Number = brand.Number,
                Sorting = brand.Sorting,
            };

            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(BrandViewModel brand)
        {
            if (ModelState.IsValid)
            {
                var model = new Brand
                {
                    Id = brand.Id,
                    Name = brand.Name,
                    Number = brand.Number,
                    Sorting = brand.Sorting,
                };
                brandRepository.UpdateBrand(model);

            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            imageRepository.DeleteImageByBrand(id);
            brandRepository.DeleteBrand(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddBrandImg(IFormFile file, int id)
        {
            var image = imageRepository.GetImageByBrandId(id);
            string guid = Guid.NewGuid().ToString();
            if (file.Length > 0)
            {
                var filePath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot\\uploadedImg",
                        file.FileName);
                var filePathWeb = Path.Combine("\\uploadedImg",
                        file.FileName);

                if (null == image)
                {
                    var model = new Image
                    {
                        Name = file.FileName,
                        Path = filePathWeb,
                        Guid = guid,
                        BrandId = id
                    };

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);

                        imageRepository.CreateImageAsync(model);
                    }
                    return Ok(guid);
                }
                else
                {
                    image.Name = file.FileName;
                    image.Path = filePathWeb;


                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);

                        imageRepository.EditImage(image);
                    }
                    return Ok(guid);
                }
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult DeleteBrandImg(int id)
        {

            return Ok();
        }

        [HttpPost]
        public ActionResult SaveFile()
        {
            //if (Request.Files.Count > 0)
            //{
            //    var file = Request.Files[0];

            //    if (file != null && file.ContentLength > 0)
            //    {
            //        file.SaveAs(Server.MapPath($"/Content/{file.FileName}"));
            //    }
            //}

            return Json(true);
        }

        ///FilePond
        [HttpPost]
        public async Task<ActionResult> Process([FromForm] int brandId, IFormFile file, CancellationToken cancellationToken)
        {
            if (file is null)
            {
                return BadRequest("Process Error: No file submitted");
            }

            // We do some internal application validation here with our caseId

            try
            {
                string guid = Guid.NewGuid().ToString();
                if (file.Length > 0)
                {
                    var filePath = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot\\uploadedImg\\brand",
                            file.FileName);
                    var model = new Image
                    {
                        Name = file.FileName,
                        Path = filePath,
                        Guid = guid,
                        BrandId = brandId
                    };

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);

                        imageRepository.CreateImageAsync(model);
                    }
                }
                return Ok(guid);
            }
            catch (Exception e)
            {
                return BadRequest($"Process Error: {e.Message}"); // Oops!
            }
        }
        // DELETE: api/RaffleImagesUpload/
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpDelete]
        public async Task<ActionResult> Revert()
        {
            // The server id will be send in the delete request body as plain text
            //using StreamReader reader = new(Request.Body, Encoding.UTF8);
            //string guid = await reader.ReadToEndAsync();
            //if (string.IsNullOrEmpty(guid))
            //{
            //    return BadRequest("Revert Error: Invalid unique file ID");
            //}
            //var attachment = _context.Attachments.FirstOrDefault(i => i.Guid == guid);
            // We do some internal application validation here
            try
            {
                // Form the request to delete from s3
                //var deleteObjectRequest = new DeleteObjectRequest
                //{
                //    BucketName = GetBucketName(), // add your own bucket name
                //    Key = guid
                //};
                //// https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/net-dg-config-netcore.html
                //await DeleteFromS3Async(deleteObjectRequest);

                //attachment.Deleted = true;
                //_context.Update(attachment);
                //await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(string.Format("Revert Error:'{0}' when writing an object", e.Message));
            }
        }

        [HttpGet("Load/{id}")]
        public async Task<IActionResult> Load(string id)
        {
            //if (string.IsNullOrEmpty(id))
            //{
            //    return NotFound("Load Error: Invalid parameters");
            //}
            //var attachment = await _context.Attachments.SingleOrDefaultAsync(i => i.Guid.Equals(id));
            //if (attachment is null)
            //{
            //    return NotFound("Load Error: File not found");
            //}

            //var imageKey = string.Format("{0}.{1}", attachment.Guid, attachment.FileType);
            //using Stream ImageStream = GetS3FileStreamAsync(GetBucketName(), imageKey);
            //Response.Headers.Add("Content-Disposition", new ContentDisposition
            //{
            //    FileName = string.Format("{0}.{1}", attachment.FileName, attachment.FileType),
            //    Inline = true // false = prompt the user for downloading; true = browser to try to show the file inline
            //}.ToString());
            //return File(ImageStream, "image/" + attachment.FileType);
            return Ok();
        }
    }
}
