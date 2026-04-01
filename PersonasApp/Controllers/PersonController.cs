using Microsoft.AspNetCore.Mvc;
using PersonsApp.Database;
using PersonsApp.Dtos.Persons;
using PersonsApp.Entities;
using PersonsApp.Services.Persons;

namespace PersonsApp.Controllers
{
    [Route("api/person")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
           
        }

        [HttpGet]
        public async Task<ActionResult> GetPage(
            string serachTerm = "", int page = 1, int pageSize = 10
        )
        {
            var response = await _personService.GetPageAsync(serachTerm, page, pageSize); 
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOne(string id)
        {
            var result = await _personService.GetOneByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult> Create(PersonCreateDto dto)
        {
            var result = await _personService.CreateAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, PersonEditDto dto)
        {
            var result = await _personService.EditAsync(id, dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _personService.DeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}