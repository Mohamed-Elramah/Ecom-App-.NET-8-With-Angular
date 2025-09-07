using Ecom.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.infrastructure.Repositries.Service
{
    public class ImageManagmentService : IImageManagmentService
    {
        private readonly IFileProvider fileProvider;
        public ImageManagmentService(IFileProvider fileProvider)
        {
            this.fileProvider = fileProvider;
        }
      

        public async Task<List<string>> AddImageAsync(IFormFileCollection files, string src)
        {
            var savedImageUrls = new List<string>();

            // 1- تحديد مسار حفظ الصور داخل wwwroot
            var directoryPath = Path.Combine("wwwroot", "Images", src);

            // 2- لو الفولدر مش موجود، أنشئه
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    // 3- تنظيف اسم الملف
                    var fileNameWithoutExt = Path.GetFileNameWithoutExtension(file.FileName);
                    fileNameWithoutExt = fileNameWithoutExt.Replace(" ", "_")
                                                           .Replace("(", "")
                                                           .Replace(")", "");

                    var extension = Path.GetExtension(file.FileName);

                    // 4- ضمان اسم ملف فريد
                    var imageName = $"{fileNameWithoutExt}_{Guid.NewGuid()}{extension}";

                    // 5- المسار الفيزيائي الكامل
                    var filePath = Path.Combine(directoryPath, imageName);

                    // 6- الحفظ على السيرفر
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // 7- إرجاع الـ URL بدل المسار الفيزيائي
                    var imageUrl = Path.Combine("Images", src, imageName).Replace("\\", "/");
                    savedImageUrls.Add("/" + imageUrl); // يبدأ بـ "/" علشان يشتغل مع الـ static files
                }
            }

            return savedImageUrls;
        }





























        public void DeleteImageAsync(string src)
        {
            var info = fileProvider.GetFileInfo(src);
            
            var filePath = info.PhysicalPath;
            File.Delete(filePath);
        }
    }
}
