using AutoMapper;
using Ecom.Core.interfaces;
using Ecom.Core.Services;
using Ecom.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.infrastructure.Repositries
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IImageManagmentService _imageManagmentService;
        public ICategoryRepositry CategoryRepositry { get; }

        public IProductRepositry ProductRepositry { get; }

        public IPhotoRepositry PhotoRepositry { get; }
        public UnitOfWork(AppDbContext context, IMapper mapper, IImageManagmentService imageManagmentService)
        {

            _context = context;
            _mapper = mapper;
            _imageManagmentService = imageManagmentService;

            CategoryRepositry = new CategoryRepositry(_context);
            ProductRepositry = new ProductRepositry(_context,_mapper,_imageManagmentService);
            PhotoRepositry = new PhotoRepositry(_context);
           
        }
    }
}
