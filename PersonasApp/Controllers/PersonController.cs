using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace PersonApp.Entities
{

    // Anotaciones se utilizan con []
    [Route("api/person")]

    [ApiController]

    public class PersonController : ControllerBase
    {
        private readonly List<PersonEntity> _persons;


        public PersonController()
        {
            // _persons = new List<PersonEntity>();

            _persons = new List<PersonEntity>
            {
                
                new PersonEntity
                {
                    DNI = "0412200400061",
                    FirstName = "Jose Camilo",
                    LastName = "Alvarado Leiva",
                    BirthDate = DateTime.Parse("24/02/2004"),
                    Gender = "Macho"
                },

                new PersonEntity
                {
                    DNI = "0412200400061",
                    FirstName = "Camilo Rachelle",
                    LastName = "Welcehz Lara",
                    BirthDate = DateTime.Parse("13/03/2010"),
                    Gender = "Hembra"
                },

                 new PersonEntity
                {
                    DNI = "0412200400061",
                    FirstName = "Ismael",
                    LastName = "Rodriguez",
                    BirthDate = DateTime.Parse("13/05/1998"),
                    Gender = "Macho"
                }

            };
        }

        // nuestro primer einpoint ************************************************************************************************
        [HttpGet]

        public IActionResult GetAll()
        {
            return Ok(_persons);

        }
        // final del einpoint *****************************************************************************************************

        [HttpGet("{dni}")]
        public IActionResult GetOne(string dni)
        {

            var person = _persons.FirstOrDefault( p => p.DNI == dni);
            return person != null ? Ok(person) : NotFound(new {Menssage = "Persona no encontrada."});
        }

        // ************************************************************************************************************************

        [HttpPost]
        public IActionResult Creat(PersonEntity person)
        {

             var personExist = _persons.Any( p => p.DNI == person.DNI);

                if(!personExist)
                {
                    _persons.Add(person);
                    return Created();
                }

            return BadRequest(new {Message = "El DNI ya esta registrado."});

        }

        // ****************************************************************************************************************************

        [HttpPut("{dni}")]
        public IActionResult Update(string dni, PersonEntity person )
        {   
            var oldPerson = _persons.FirstOrDefault(p => p.DNI == dni);

            if (oldPerson is null )
            {
                return NotFound(new {Message = "Persona no encontrada."});
            }

            _persons.Remove(oldPerson);

            _persons.Add(person);

            Console.WriteLine($"Persona actulizada: {person.DNI} - {person.FirstName} {person.LastName}");

            return Ok(new {Message = "Registro Editado Correctamente !!"});
        }

        // **************************************************************************************************************************
        
        [HttpDelete("{dni}")]
        public IActionResult Delete(string dni)
        {
            var person = _persons.FirstOrDefault(p => p.DNI == dni);

            if( person is null)
            {
                return NotFound(new {Message = "Persona no encontrada."});
            }

            _persons.Remove(person);


            Console.WriteLine($"Persona Eliminada: {person.DNI} - {person.FirstName} {person.LastName}");

            return Ok(new {Message = "Registro Borrada !!"});
        }

        // ****************************************************************************************************************************

    }
}