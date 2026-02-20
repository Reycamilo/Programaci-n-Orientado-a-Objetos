using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonApp.Entities
{

    [Table("persons")] // forzando ha que la tabla se llame persons
    public class PersonEntity : BaseEntity
    {

        //  Propiedades

        // [Key] // forzando ha que sea una llave primaria.
        // [Column("id")]
        // // public Guid Id { get; set; }


        [Required(ErrorMessage = "El DNI es requerido")]
        public string DNI {get; set;}


        public string FirstName { get; set; }


        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender {get; set;}
    }
}