using MediatR;

using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;

using IMovieLoverAPI.Models;
using IMovieLover.API.Commands;
using IMovieLover.API.Validations;
using System.Net.Http;

namespace IMovieLover.API.Handlers
{
    public class MovieNameCommandHandler : IRequestHandler<MovieNameCommand, Choice?>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        
        public MovieNameCommandHandler(IHttpClientFactory httpClientFactory) 
            => _httpClientFactory = httpClientFactory;
        
        public async Task<Choice?> Handle(MovieNameCommand request, CancellationToken cancellationToken)
        {
                var client = _httpClientFactory.CreateClient("chatGpt");

                var model = new ChatGptRequest(request.MessageRequest.prompt);

                var requestBody = JsonSerializer.Serialize(model);

                var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("completions", content);

                var result = await response.Content.ReadFromJsonAsync<ChatGptResponse>();

                var promptResponse = result.choices.First();

                if (promptResponse is null)
                {
                    throw new Exception("A resposta da API veio nula.");
                }

                var text = promptResponse.text.Replace("\n", " ").Replace("\t", " ");

                var textResponse = new Choice
                {
                    text = text,
                };

                return textResponse;
        }
    }
}
