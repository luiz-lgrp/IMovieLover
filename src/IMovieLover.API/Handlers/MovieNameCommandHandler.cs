using MediatR;

using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;

using IMovieLoverAPI.Models;
using IMovieLover.API.Commands;
using IMovieLover.API.Validations;

namespace IMovieLover.API.Handlers
{
    public class MovieNameCommandHandler : IRequestHandler<MovieNameCommand, Choice?>
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public MovieNameCommandHandler(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<Choice?> Handle(MovieNameCommand request, CancellationToken cancellationToken)
        {
                var validationResult = new MovieNameCommandValidation().Validate(request);

                if (!validationResult.IsValid)
                    return null;

                var token = _configuration.GetValue<string>("ChatGptSecretKey");

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var model = new ChatGptRequest(request.MessageRequest.prompt);

                var requestBody = JsonSerializer.Serialize(model);

                var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("https://api.openai.com/v1/completions", content);

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
