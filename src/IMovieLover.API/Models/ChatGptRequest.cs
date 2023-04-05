namespace IMovieLoverAPI.Models
{
    public class ChatGptRequest
    {
        public const string CONTEXT = "Nome e informações do filme";

        public string model { get; private set; }
        public string prompt { get; set; }
        public int max_tokens { get; private set; }
        public decimal temperature { get; private set; }

        public ChatGptRequest(string prompt)
        {
            model = "text-davinci-003";
            this.prompt = $"{CONTEXT} {prompt}";
            max_tokens = 150;
            temperature = 0.2m;
        }
    }
}
