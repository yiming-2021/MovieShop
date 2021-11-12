using ApplicationCore.ServiceInterfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CastController : ControllerBase
    {
        private readonly ICastService _castService;

        public CastController(ICastService castService)
        {
            _castService = castService;
        }

        [Route("{id:int}")]
        [HttpGet]
        public async Task<IActionResult> GetCastDetailsById(int id)
        {
            var cast = await _castService.GetCastDetails(id);
            if(cast == null)
            {
                return NotFound($"No Cast Found For Id = {id}");
            }
            return Ok(cast);
        }

    }
}