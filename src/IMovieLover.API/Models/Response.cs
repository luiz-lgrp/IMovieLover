namespace IMovieLover.API.Models
{
    public class Response
    {
        public List<string> Errors { get; set; }
        public bool Success { get { return !Errors.Any(); } }

        public Response()
        {
            Errors = new List<string>();
        }

        public void AddError(string errorMessage)
        {
            Errors.Add(errorMessage);
        }
    }
}
