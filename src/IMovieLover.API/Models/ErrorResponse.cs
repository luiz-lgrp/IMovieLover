using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IMovieLover.API.Models
{
    public class ErrorResponse
    {
        public string Error { get; set; }

        public bool Success { get { return !Error.Any(); } }

        public ErrorResponse()
        {
            Error = string.Empty;
        }

        public void AddError(string errorMessage)
        {
            Error = errorMessage;
        }

    }
}
