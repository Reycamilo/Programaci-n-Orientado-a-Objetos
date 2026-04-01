
using PersonsApp.Dtos.Persons;
using PersonsApp.Entities;

namespace PersonsApp.Mappers
{
    public static class PersonMapper
    {
        public static PersonEntity CreateDtoToEntity(PersonCreateDto dto)
        {
            return new PersonEntity
            {
                Id = Guid.NewGuid().ToString(),
                DNI = dto.DNI,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate,
                Gender = dto.Gender,
                CountryId = dto.CountryId
            };
        }

        public static PersonEntity EditDtoToEntity(PersonEditDto dto, PersonEntity entity) 
        {
            entity.FirstName = dto.FirstName;
            entity.LastName = dto.LastName;
            entity.BirthDate = dto.BirthDate;
            entity.Gender = dto.Gender;
            entity.CountryId = dto.CountryId;
            return entity;
        }

        public static List<PersonDto> ListEntitytoListDto(List<PersonEntity> entities)
        {

             return entities.Select(entity => new PersonDto
                {
                    Id        = entity.Id,
                    DNI  = entity.DNI,
                    FirstName     = entity.FirstName,
                    LastName = entity.LastName,
                    BirthDate = entity.BirthDate,
                    Gender = entity.Gender

                }).ToList();

        }
    }
}