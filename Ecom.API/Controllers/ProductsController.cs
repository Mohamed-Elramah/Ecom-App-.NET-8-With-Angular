using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core.DTO;
using Ecom.Core.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {
        public ProductsController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> get()
        {
            try
            {
                var product = await work.ProductRepositry.GetAllAsync(
                    x=>x.Category,x=>x.Photos);

                var result = mapper.Map<List<AddProductDTO>>(product);
                if(product is null)
                    return BadRequest(new ResponseAPI(400));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> getById(int id)
        {
            try
            {
                var product = await work.ProductRepositry.GetByIdAsync(id,
                    x=>x.Category,x=>x.Photos);
                var result = mapper.Map<AddProductDTO>(product);
                if (product is null)
                    return BadRequest(new ResponseAPI(400, $"not found product id={id}"));
                return Ok(result);


            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPost("Add-Product")]
        public async Task<IActionResult> add(AddProductDTO productDTO)
        {
            try
            {
                await work.ProductRepositry.AddAsync(productDTO);
                return Ok(new ResponseAPI(200, "Product Added"));


            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }
        [HttpPut("Update-Product")]
        public async Task<IActionResult> update(UpdateProductDTO updateproductDTO)
        {
            try
            {

                await work.ProductRepositry.UpdateAsync(updateproductDTO);
                return Ok(new ResponseAPI(200, "Product Updated"));

            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));


            }
        }
        //[HttpPut("Delete-Product/{id}")]


    }
}
