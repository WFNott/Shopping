using System.ComponentModel.DataAnnotations;

namespace Shopping.Data.Entities
{
    public class Category
    {
        // En entiti framework se exige una PK, en nuestro caso sera el ID de la Categoria
        public int Id { get; set; }

        // Para evitar ingresos errorneos del usuario se usan metodos para limitar sus respuestas

        /* "Display" sirve para mostrar una propiedad con un nombre personalizado para el usuario
         sin comprometer el apartado tecnico*/
        [Display(Name = "Categoria")]

        /* El "MaxLength" se crea para restringir la capacidad de los caracteres para la base de datos
         EN caso de que el usuario agrege mas de la capacidad permitida, se dispara el "Error Message"
         el cual mostrara un mensaje personalizado, el "{0}" y "{1}" agregara en orden la información y parametros definidas*/
        [MaxLength(50, ErrorMessage = "El {0} no puedes superar los {1} caracteres")]

        // El "Required" se usa para obligar al usuario llenar el campo

        [Required(ErrorMessage = "El {0} es obligatorio")]

        public string Name { get; set; }

        public ICollection<State> States { get; set;}


        [Display(Name = "Departamentos/Estados")]
        public int StatesNumer => States == null ? 0 : States.Count;

        public ICollection<ProductCategory> ProductCategories { get; set; }

       [ Display(Name = "# Productos")]
            public int ProductsNumber => ProductCategories == null ? 0 : ProductCategories.Count();

    }
}
