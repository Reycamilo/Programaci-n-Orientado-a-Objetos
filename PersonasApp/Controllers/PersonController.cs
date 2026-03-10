using Microsoft.AspNetCore.Mvc;
using PersonsApp.Dtos.Persons;
using PersonsApp.Entities;
using PersonsApp.Services.Persons;

namespace PersonsApp.Controllers
{
    [Route("api/person")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly List<PersonEntity> _persons;
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;

            //_persons = new List<PersonEntity>();
            // _persons.Add(new PersonEntity
            // {
            //     DNI = "0401200012345",
            //     FirstName = "Juan Carlos",
            //     LastName = "Perez Hernandez",
            //     Gender = "M",
            //     BirthDate = DateTime.Parse("01/06/2000")
            // });
            
            _persons = new List<PersonEntity>
            {
                new PersonEntity
                {
                    DNI = "0401200012345",
                    FirstName = "Juan Carlos",
                    LastName = "Perez Hernandez",
                    Gender = "M",
                    BirthDate = DateTime.Parse("01/06/2000")
                },
                new PersonEntity
                {
                    DNI = "0401200012346",
                    FirstName = "Maria Michelle",
                    LastName = "Lopez Pineda",
                    Gender = "F",
                    BirthDate = DateTime.Parse("15/03/2000")
                },
                new PersonEntity
                {
                    DNI = "0401200012347",
                    FirstName = "Carlos Ismael",
                    LastName = "Rodriguez Mejia",
                    Gender = "M",
                    BirthDate = DateTime.Parse("07/08/1998")
                }
            };
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_persons);
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
        public IActionResult Delete(string id)
        {
            var person = _persons.FirstOrDefault(p => p.Id == id);

            if(person is null)
            {
                return NotFound(new {Message = "Registro no encontrado."});
            }

            _persons.Remove(person);

            Console.WriteLine($"Persona borrada:  {person.DNI} - {person.FirstName} {person.LastName}");

            return Ok(new {Message = "Registro borrado correctamente."});
        }
    }
}