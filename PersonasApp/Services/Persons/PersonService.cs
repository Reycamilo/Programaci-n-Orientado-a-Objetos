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

        private readonly int PAGE_SIZE;
        private readonly int PAGE_SIZE_LIMIT;


        // ******************************* CONSTRUCTOR **********************************
        public PersonsService(PersonsDbContext context, IConfiguration configuration)
        {
            _context = context;

            PAGE_SIZE = configuration.GetValue<int>("PageSize");
            PAGE_SIZE_LIMIT = configuration.GetValue<int>("PageSizeLimit");
        }

//  ******************************************* METODOS ****************************************************************
        
        // METODO DE PAGINACION
        public async Task<ResponseDto<PageDto<List<PersonDto>>>> GetPageAsync(string serachTerm = "", int page = 1, int pageSize = 10){
            
            page = Math.Abs(page);
            pageSize = Math.Abs(pageSize);
            pageSize = pageSize <= 0 ? PAGE_SIZE : pageSize;
            pageSize = pageSize > PAGE_SIZE_LIMIT ? PAGE_SIZE_LIMIT : pageSize;

            IQueryable<PersonEntity> personsQuery = _context.Persons;

            int startIndex = (page - 1) * pageSize;

            if (!string.IsNullOrEmpty(serachTerm))
            {
                personsQuery = personsQuery.Where( x => (x.DNI + " " + x.FirstName + " " + x.LastName).Contains(serachTerm));
            }

            int totalRows = await personsQuery.CountAsync();

            var personsEntity = await personsQuery
            .OrderBy(x => x.FirstName)
            .Skip(startIndex)
            .Take(pageSize)
            .ToListAsync();

            var personsDto = PersonMapper.ListEntitytoListDto(personsEntity);


            return new ResponseDto<PageDto<List<PersonDto>>>
            {
                StatusCode = HttpStatusCode.OK,
                Message = HttpMessageResponse.REGISTERS_FOUND,
                Status = true,
                Data = new PageDto<List<PersonDto>>
                {
                    CurrentPage = page == 0 ? 1 : page,
                    PageSize = pageSize,
                    TotalItems = totalRows,
                    TotalPages = (int)Math.Ceiling((double)totalRows/pageSize),
                    Items = PersonMapper.ListEntitytoListDto(personsEntity),
                    HasNextPage = startIndex + pageSize < PAGE_SIZE_LIMIT && 
                    page < (int)Math.Ceiling((double)totalRows/pageSize),
                    HasPreviousPage = page > 1 
                }
            };
        }
        
        
        // METODO PARA BUSCAR POR ID
        public async Task<ResponseDto<PersonDto>> GetOneByIdAsync(string id)
        {
            var personEntity = await _context.Persons.FirstOrDefaultAsync(p => p.Id == id);

            if ( personEntity is null)
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
    

        // METODO PARA CREAR PERSONA
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
    

        // METODO PARA EDITAR 
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


        // MEOTODO PARA ELIMINAR
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
                  Data = new PersonActionResponseDto
                  {
                      Id = id
                  }
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


