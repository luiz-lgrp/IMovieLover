using FluentValidation;

using IMovieLover.API.Commands;

namespace IMovieLover.API.Validations
{
    public class MovieNameCommandValidation : AbstractValidator<MovieNameCommand>
    {
        public MovieNameCommandValidation()
        {
            RuleFor(command => command.MessageRequest.prompt)
                .Must(prompt => prompt.Length >= "Nome e informações do filme".Length + 15)
                .WithMessage("Vamos lá, faça uma pergunta!");
        }
    }
}
