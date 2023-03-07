using MediatR;

using IMovieLoverAPI.Models;

namespace IMovieLover.API.Commands
{
    public class MovieNameCommand : IRequest<Choice?>
    {
        public ChatGptRequest MessageRequest { get; set; }

        public MovieNameCommand(ChatGptRequest messageRequest)
        {
            MessageRequest = messageRequest;
        }
    }
}
