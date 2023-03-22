namespace IMovieLover.API.Models
{
    public class ErrorResponse
    {
        public List<string> Errors { get; set; }
        public bool Success { get { return !Errors.Any(); } }

        public ErrorResponse()
        {
            Errors = new List<string>();
        }

        public void AddError(string errorMessage)
        {
            Errors.Add(errorMessage);
        }
    }
}
