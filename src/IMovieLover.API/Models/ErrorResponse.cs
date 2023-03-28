namespace IMovieLover.API.Models
{
    public class ErrorResponse
    {
        public string Error { get; set; }

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
