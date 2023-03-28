using IMovieLover.API.Models;

namespace IMovieLoverAPI.Models
{
    public class Choice : ErrorResponse
    {
        public string text { get; set; }
        public int index { get; set; }
        public object logprobs { get; set; }
        public string finish_reason { get; set; }
    }
}
