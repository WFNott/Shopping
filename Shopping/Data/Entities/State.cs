using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Shopping.Data.Entities
{
    public class State
    {

        // propiedad PK
        public int Id { get; set; }

        [Display(Name = "Departamentos/Estados")]
        [MaxLength(50, ErrorMessage = "El {0} no puedes superar los {1} caracteres")]
        [Required(ErrorMessage = "El {0} es obligatorio")]

        public string Name { get; set; }


        [JsonIgnore]
        public Country Country { get; set; } 
        

       
        // Se crea una colleción, poniendo a ciudad como una lista, llamando a esta lista Cities (Ciudad)
        public ICollection<City> Cities { get; set; }

        // Para saber la cantidad de ciudades, creamos una variable entera donde le agregamos un condicionador
        // ternario en caso de que no haya ninguna ciudad, y a esta variable guardamos la cantidad de ciudades

        [Display(Name = "Ciudades")]
        public int CitiesNumber => Cities == null ? 0 : Cities.Count;




    }
}
