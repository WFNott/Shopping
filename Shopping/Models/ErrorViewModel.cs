namespace Shopping.Models
{
    public class ErrorViewModel
    {
#pragma warning disable CS8632 // La anotaci�n para tipos de referencia que aceptan valores NULL solo debe usarse en el c�digo dentro de un contexto de anotaciones "#nullable".
        public string? RequestId { get; set; }
#pragma warning restore CS8632 // La anotaci�n para tipos de referencia que aceptan valores NULL solo debe usarse en el c�digo dentro de un contexto de anotaciones "#nullable".

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}