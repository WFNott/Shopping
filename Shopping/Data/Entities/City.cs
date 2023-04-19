using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Shopping.Data.Entities
{
    public class City
    {

        // propiedad PK
        public int Id { get; set; }

        // Nombre modificado para el usuario

        [Display(Name = "ciudad")]

        // Limitacion de los Caracteres 

        [MaxLength(50, ErrorMessage = "El {0} no puedes superar los {1} caracteres")]

        // Obligacion de llenar el campo para el usuario

        [Required(ErrorMessage = "El {0} es obligatorio")]

        public string Name { get; set; }

        [JsonIgnore]
        public State State { get; set; }

        public ICollection<User> Users { get; set; }

    }
}
