using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Shopping.Models
{
    public class StateViewModel
    {
        // propiedad PK
        public int Id { get; set; }

        // Nombre modificado para el usuario

        [Display(Name = "Departamentos/Estados")]

        // Limitacion de los Caracteres 

        [MaxLength(50, ErrorMessage = "El {0} no puedes superar los {1} caracteres")]

        // Obligacion de llenar el campo para el usuario

        [Required(ErrorMessage = "El {0} es obligatorio")]

        public string Name { get; set; }
        [JsonIgnore]
        public int CountryId { get; set; }
    }
}
