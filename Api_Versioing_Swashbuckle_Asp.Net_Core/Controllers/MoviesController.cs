using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api_Versioing_Swashbuckle_Asp.Net_Core.Core.Entities;
using Api_Versioing_Swashbuckle_Asp.Net_Core.Infra.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Versioing_Swashbuckle_Asp.Net_Core.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("AllMovie")]
        public async Task<ActionResult<IEnumerable<Movie>>> Get()
        {
            var movie = new List<Movie>()
            { new Movie { Id = 1, Name = "The Eight Hundred", Price = 100.0M },
                new Movie { Id = 2, Name = "Bad Boys for Life", Price = 200.0M}

            };
            return Ok(movie);
        }
    }
}
