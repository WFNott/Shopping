using Microsoft.AspNetCore.Mvc.Rendering;
using Shopping.Enum;
using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
	public class AddUserViewModel
	{
		public string Id { get; set; }

		[Display(Name = "Documento")]
		[MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
		[Required(ErrorMessage = "El campo {0} es obligatorio.")]
		public string Document { get; set; }

		[Display(Name = "Nombres")]
		[MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
		[Required(ErrorMessage = "El campo {0} es obligatorio.")]
		public string FirstName { get; set; }

		[Display(Name = "Apellidos")]
		[MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
		[Required(ErrorMessage = "El campo {0} es obligatorio.")]
		public string LastName { get; set; }

		[Display(Name = "Dirección")]
		[MaxLength(200, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
		[Required(ErrorMessage = "El campo {0} es obligatorio.")]
		public string Address { get; set; }

		[Display(Name = "Teléfono")]
		[MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
		[Required(ErrorMessage = "El campo {0} es obligatorio.")]
		public string PhoneNumber { get; set; }

		[Display(Name = "Foto")]
		public Guid ImageId { get; set; }

		//TODO: Pending to put the correct paths
		[Display(Name = "Foto")]
		public string ImageFullPath => ImageId == Guid.Empty
			? $"https://localhost:7057/images/noimage.png"
			: $"https://shoppingprep.blob.core.windows.net/users/{ImageId}";

		[Display(Name = "Image")]
		public IFormFile ImageFile { get; set; }

		[Display(Name = "País")]
		[Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar un país.")]
		[Required(ErrorMessage = "El campo {0} es obligatorio.")]
		public int CountryId { get; set; }

		public IEnumerable<SelectListItem> Countries { get; set; }

		[Display(Name = "Departmento / Estado")]
		[Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar un departamento/estado.")]
		[Required(ErrorMessage = "El campo {0} es obligatorio.")]
		public int StateId { get; set; }

		public IEnumerable<SelectListItem> States { get; set; }

		[Display(Name = "Ciuadad")]
		[Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar una ciudad.")]
		public int CityId { get; set; }

		public IEnumerable<SelectListItem> Cities { get; set; }

		[Display(Name = "Email")]
		[EmailAddress(ErrorMessage = "Debes ingresar un correo válido.")]
		[MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
		[Required(ErrorMessage = "El campo {0} es obligatorio.")]
		public string Username { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Contraseña")]
		[Required(ErrorMessage = "El campo {0} es obligatorio.")]
		[StringLength(20, MinimumLength = 6, ErrorMessage = "El campo {0} debe tener entre {2} y {1} carácteres.")]
		public string Password { get; set; }

		[Compare("Password", ErrorMessage = "La contraseña y la confirmación no son iguales.")]
		[Display(Name = "Confirmación de contraseña")]
		[DataType(DataType.Password)]
		[Required(ErrorMessage = "El campo {0} es obligatorio.")]
		[StringLength(20, MinimumLength = 6, ErrorMessage = "El campo {0} debe tener entre {2} y {1} carácteres.")]
		public string PasswordConfirm { get; set; }

		[Display(Name = "Tipo de usuario")]
		public UserTypes UserType { get; set; }

	}
}

