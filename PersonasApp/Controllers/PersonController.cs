using Microsoft.AspNetCore.Mvc;
using PersonsApp.Entities;


// using PersonsApp.Entities;
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
        public async Task<ActionResult> GetAll()
        {
            var result = await _personService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOne(string id)
        {
            var result = await _personService.GetOneByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public IActionResult Create(PersonEntity person)
        {
            var personExist = _persons.Any(p => p.DNI == person.DNI);

            if(!personExist)
            {
                _persons.Add(person);
                return Created();
            }

            return BadRequest(new {Message = "El DNI ya esta registrado."});

        }


        [HttpPut("{dni}")]
        public IActionResult Update(string dni, PersonEntity person)
        {
            var oldPerson = _persons.FirstOrDefault(p => p.DNI == dni);

            if(oldPerson is null)
            {
                return NotFound(new {Message = "Registro no encontrado."});
            }

            _persons.Remove(oldPerson);
            _persons.Add(person);

            Console.WriteLine($"Persona actualizada:  {person.DNI} - {person.FirstName} {person.LastName}");

            return Ok(new {Message = "Registro editado correctamente."});
        }

        [HttpDelete("{dni}")]
        public IActionResult Delete(string dni)
        {
            var person = _persons.FirstOrDefault(p => p.DNI == dni);

            if(person is null)
            {
                return NotFound(new {Message = "Registro no encontrado."});
            }

            _persons.Remove(person);

            Console.WriteLine($"Persona borrada:  {person.DNI} - {person.FirstName} {person.LastName}");

            return Ok(new {Message = "Registro borrado correctamente."});
        }


        [HttpGet("search")]
        public async Task<ActionResult> GetByFirstName(string firstName)
        {
            var result = await _personService.GetOneByFirstNameAsync(firstName);
            return Ok(result);
        }
    }
}