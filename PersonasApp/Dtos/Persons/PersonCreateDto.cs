using System.ComponentModel.DataAnnotations;

public class PersonCreateDto
{
    
        //  Propiedades
        
        [Required(ErrorMessage = "El DNI es requerido")]
        [StringLength(13, ErrorMessage = "El DNI debe tener 13 digitos", MinimumLength =13)]
        public string DNI {get; set;}

        [Display(Name = "Nombres")]
        [Required(ErrorMessage = "los {0} nombres son requeridos.")]
        [StringLength(40, ErrorMessage = "Los {0} deven tener un minino de {2} y maximo de {1}", MinimumLength = 3)]
        public string FirstName { get; set; }


        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "los {0} nombres son requeridos.")]
        [StringLength(40, ErrorMessage = "Los {0} deven tener un minino de {2} y maximo de {1}", MinimumLength = 3)]
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender {get; set;}
}