using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class CityViewModel
    {
        // propiedad PK
        public int Id { get; set; }

        // Nombre modificado para el usuario

        [Display(Name = "Ciudades")]

        // Limitacion de los Caracteres 

        [MaxLength(50, ErrorMessage = "El {0} no puedes superar los {1} caracteres")]

        // Obligacion de llenar el campo para el usuario

        [Required(ErrorMessage = "El {0} es obligatorio")]

        public string Name { get; set; }
        public int StateId { get; set; }
    }
}
