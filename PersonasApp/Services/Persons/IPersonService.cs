using PersonsApp.Dtos.Common;
using PersonsApp.Dtos.Persons;

namespace PersonsApp.Services.Persons
{
    public interface IPersonService
    {
        Task<ResponseDto<PageDto<List<PersonDto>>>> GetPageAsync(string serachTerm = "", int page = 1, int pageSize = 10);

        Task<ResponseDto<PersonDto>> GetOneByIdAsync(string id);

        Task<ResponseDto<PersonActionResponseDto>> CreateAsync(PersonCreateDto dto);

        Task<ResponseDto<PersonActionResponseDto>> EditAsync(string id, PersonEditDto dot);

        Task<ResponseDto<PersonActionResponseDto>> DeleteAsync(string id);

        // Task<ResponseDto<PersonDto>> GetByFirstName(string firstName);


    }
}