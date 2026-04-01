using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonsApp.Database;

namespace PersonasApp.Controllers
{

    [ApiController]
    [Route("api/country")]
    public class CountryController : ControllerBase
    {
        private readonly PersonsDbContext _context;

        public CountryController(PersonsDbContext context)
        {
            _context = context;
        }

        // ***************************** End Points ****************************************88
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var country = await _context.Countries.FindAsync(id);

            if (country is null)
            {
                return NotFound(new {Message =  "Pais no encontrado."});
            }

            return Ok(country);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetByname([FromQuery] string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                return BadRequest(new {Message = "Envie un nombre."});
            }

            var pais = await _context.Countries.Where(c => c.Name.Contains(nombre)).ToListAsync();

            if (pais is null)
            {
                return NotFound(new {Message = "Pais no encontrado."});
            }
            
            return Ok(pais);
        }
    }
}