using MediatR;

using IMovieLoverAPI.Models;

namespace IMovieLover.API.Commands
{
    public class MovieNameCommand : IRequest<Choice?> //não será nulo, pois vc está validando 
    {
        public ChatGptRequest MessageRequest { get; set; }

        public MovieNameCommand(ChatGptRequest messageRequest)
        {
            MessageRequest = messageRequest;
        }
    }
}
