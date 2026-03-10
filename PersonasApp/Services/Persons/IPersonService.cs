using PersonsApp.Dtos.Common;
using PersonsApp.Dtos.Persons;

namespace PersonsApp.Services.Persons
{
    public interface IPersonService
    {
        Task<ResponseDto<PersonDto>> GetOneByIdAsync(String id);

        // haciendo pruebas
        Task<ResponseDto<List<PersonDto>>> GetAllAsync();

        Task<ResponseDto<List<PersonDto>>> GetOneByFirstNameAsync(string firstName);

        Task<ResponseDto<PersonActionResponseDto>> CreateAsync(PersonCreateDto dto);

        Task<ResponseDto<PersonActionResponseDto>> EditAsync(string id, PersonEditDto dot);

    }
}