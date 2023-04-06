using FluentValidation;

using IMovieLoverAPI.Models;
using IMovieLover.API.Commands;

namespace IMovieLover.API.Validations
{
    public class MovieNameCommandValidation : AbstractValidator<MovieNameCommand>
    {
        public MovieNameCommandValidation()
        {
            RuleFor(command => command.MessageRequest.prompt)
                .Must(prompt => prompt.Length >= ChatGptRequest.CONTEXT.Length + 15)
                .WithMessage("A sua pergunta deve conter pelo menos quinze caracteres.");
        }
    }
}
