using System.Net.Http.Headers;

namespace IMovieLover.API.Handlers
{
    public class ChatGptAuthorizationHandler : DelegatingHandler
    {
        private readonly IConfiguration _configuration;

        public ChatGptAuthorizationHandler(IConfiguration configuration) 
            => _configuration = configuration;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = _configuration.GetValue<string>("ChatGptSecretKey");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
