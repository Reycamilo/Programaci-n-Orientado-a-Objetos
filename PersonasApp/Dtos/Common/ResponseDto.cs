
namespace PersonsApp.Dtos.Common
{
    public class ResponseDto<T>
    {
        public int StatusCode { get; set; }//codigo de respuesta
        public string  Message { get; set; }//mensaje de la respuesta
        public bool Status { get; set; }//verrdadero para respuestas sin errores y sino falso
        public T Data { get; set; }
    }
}