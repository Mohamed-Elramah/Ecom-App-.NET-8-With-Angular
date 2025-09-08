using AutoMapper;
using Ecom.Core.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
   
    public class BugController : BaseController
    {
        public BugController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }

        [HttpGet("not-found")]
        public async Task<IActionResult> GetNotFound()
        {
            var category = await work.CategoryRepositry.GetByIdAsync(100);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpGet("server-error")]
        public async Task<IActionResult> GetServerError()
        {
            var category = await work.CategoryRepositry.GetByIdAsync(100);
            category.Name = "";
            return Ok(category);
        }
        [HttpGet("bad-request/{Id}")]
        public IActionResult GetBadRequest(int id)
        {
            return Ok();
        }
        [HttpGet("bad-request")]
        public IActionResult GetBadRequest()
        {
            return BadRequest();
        }


    }
}
