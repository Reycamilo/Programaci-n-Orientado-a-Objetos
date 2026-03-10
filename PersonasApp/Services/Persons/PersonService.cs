using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PersonsApp.Mappers;
using PersonsApp.Constants;
using PersonsApp.Database;
using PersonsApp.Dtos.Common;
using PersonsApp.Dtos.Persons;
using PersonsApp.Entities;

namespace PersonsApp.Services.Persons
{
    public class PersonsService : IPersonService
    {
        private readonly PersonsDbContext _context;

        public PersonsService(PersonsDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDto<PersonDto>> GetOneByIdAsync(String id)
        {
            var personEntity = await _context.Persons.FirstOrDefaultAsync(p => p.Id == id);

            if(personEntity is null)
            {
                return new ResponseDto<PersonDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND,
                    Status = false,                    
                };
            }

            return new ResponseDto<PersonDto>
            {
                StatusCode = HttpStatusCode.OK,
                Message = HttpMessageResponse.REGISTER_FOUND,
                Status = true,
                Data = new PersonDto
                {
                    Id = personEntity.Id,
                    DNI = personEntity.DNI,
                    FirstName = personEntity.FirstName,
                    LastName = personEntity.LastName,
                    BirthDate = personEntity.BirthDate,
                    Gender = personEntity.Gender
                }
            };

        }

        // haciendo pruebas
        public async Task<ResponseDto<List<PersonDto>>> GetAllAsync()
        {
            // 1. Traemos todas las personas de la base de datos.
            var personEntities = await _context.Persons.ToListAsync();

            // 2. Convertimos la lista de entidades a una lista de DTOs.
            var personDto = personEntities.Select(p => new PersonDto
            {
                Id = p.Id,
                DNI = p.DNI,
                FirstName = p.FirstName,
                LastName = p.LastName,
                BirthDate = p.BirthDate,    
            }).ToList();

            // 3. Devolvemos la caja con la lista de personas
            return new ResponseDto<List<PersonDto>>
            {
                StatusCode = HttpStatusCode.OK,
                Message = HttpMessageResponse.REGISTER_FOUND,
                Status = true,
                Data = personDto
            };
        }
        
        public async Task<ResponseDto<List<PersonDto>>> GetOneByFirstNameAsync (string firstName)
        {
            var personEntities = await _context.Persons.Where(p => p.FirstName == firstName).ToListAsync();

           var personDto = personEntities.Select(p => new PersonDto
            {
                Id = p.Id,
                DNI = p.DNI,
                FirstName = p.FirstName,
                LastName = p.LastName,
                BirthDate = p.BirthDate,    
                Gender = p.Gender
            }).ToList();

            return new ResponseDto<List<PersonDto>>
            {
                StatusCode = HttpStatusCode.OK,
                Message = HttpMessageResponse.REGISTER_FOUND,
                Status = true,
                Data = personDto
            };
        }

        public async Task<ResponseDto<PersonActionResponseDto>> CreateAsync(PersonCreateDto dto)
        {
            PersonEntity personEntity = PersonMapper.CreateDtoToEntity(dto);

            _context.Persons.Add(personEntity);

            await _context.SaveChangesAsync(); // Guardamos los cambios en la base de datos.

               return new ResponseDto<PersonActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Message = HttpMessageResponse.REGISTER_CREATED,
                Status = true,
                Data = new PersonActionResponseDto
                {
                    Id = personEntity.Id
                }
            }; 
        }
    
        public async Task<ResponseDto<PersonActionResponseDto>> EditAsync(string id, PersonEditDto dot) {
            var personEntity = await _context.Persons.FirstOrDefaultAsync(p => p.Id == id);

            if (personEntity is null)
            {
                return new ResponseDto<PersonActionResponseDto>
                {
                  StatusCode = HttpStatusCode.NOT_FOUND,
                  Message = HttpMessageResponse.REGISTER_NOT_FOUND,
                  Status = false,  
                };
            }

            var personEntityUpdated = PersonMapper.EditDtoToEntity(dot, personEntity);

            _context.Persons.Update(personEntityUpdated);

            await _context.SaveChangesAsync();

            return new ResponseDto<PersonActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTER_UPDATED,
                Data = new PersonActionResponseDto
                {
                    Id = id
                }
            };
        }

        public async Task<ResponseDto<PersonActionResponseDto>> DeleteAsync(string id)
        {
            var personEntity = await _context.Persons.FirstOrDefaultAsync(p => p.Id == id);

             if (personEntity is null)
            {
                return new ResponseDto<PersonActionResponseDto>
                {
                  StatusCode = HttpStatusCode.NOT_FOUND,
                  Message = HttpMessageResponse.REGISTER_NOT_FOUND,
                  Status = false,  
                };
            }

            _context.Persons.Remove(personEntity);
            await _context.SaveChangesAsync();


            return new ResponseDto<PersonActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTER_DELETED,
                Data = new PersonActionResponseDto
                {
                    Id = id
                }
                   
            };
        }
    }


}