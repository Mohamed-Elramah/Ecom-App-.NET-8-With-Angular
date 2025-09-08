using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entites.Product;
using Ecom.Core.interfaces;
using Ecom.Core.Services;
using Ecom.infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.infrastructure.Repositries
{
    public class ProductRepositry : GenericRepositry<Product>, IProductRepositry
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;
        private readonly IImageManagmentService imageManagmentService;
        public ProductRepositry(AppDbContext context, IMapper mapper, IImageManagmentService imageManagmentService) : base(context)
        {

            this.context = context;
            this.mapper = mapper;
            this.imageManagmentService = imageManagmentService;
        }

        public async Task<bool> AddAsync(AddProductDTO productDTO)
        {
          if(productDTO==null) return false;

          var product= mapper.Map<Product>(productDTO);
           await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            var ImagePath = await imageManagmentService.AddImageAsync(productDTO.Photo, productDTO.Name);

            var photo = ImagePath.Select(path => new Photo
            {
                ImageName = path,
                ProductId = product.Id
            }).ToList();
            
            await context.Photos.AddRangeAsync(photo);
            await context.SaveChangesAsync();
            return true;

           

        }

       
        public async Task<bool> UpdateAsync(UpdateProductDTO updateproductDTO)
        {
            if(updateproductDTO is null)
            {
                return false;
            }
            var FindProduct = await context.Products
                .Include(m=>m.Category)
                .Include(m=>m.Photos)
                .FirstOrDefaultAsync(m => m.Id == updateproductDTO.Id);

            if(FindProduct is null)
            {
                return false;
            }

            mapper.Map(updateproductDTO, FindProduct);
            
            // find photo to delete
            var FindPhoto = await context.Photos
                .Where(m => m.ProductId == updateproductDTO.Id)
                .ToListAsync();
            
            foreach (var item in FindPhoto)
            {
                 imageManagmentService.DeleteImageAsync(item.ImageName);
                
                
            }

            // delete photo from db
            context.Photos.RemoveRange(FindPhoto);

            // add new photo
            var ImagePath = await imageManagmentService.AddImageAsync(updateproductDTO.Photo, updateproductDTO.Name);
           
            // mapping
            var photo = ImagePath.Select(path => new Photo
            {
                ImageName = path,
                ProductId = updateproductDTO.Id
            }).ToList();

            await context.Photos.AddRangeAsync(photo);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(Product product)
        {
            var photo = await context.Photos
                .Where(m => m.ProductId == product.Id)
                .ToListAsync();
            foreach (var item in photo)
            {
                imageManagmentService.DeleteImageAsync(item.ImageName);
            }
            context.Products.Remove(product);
            await context.SaveChangesAsync();

        }


    }
}
