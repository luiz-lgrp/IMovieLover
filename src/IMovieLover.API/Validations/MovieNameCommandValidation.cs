using FluentValidation;

using IMovieLover.API.Commands;

namespace IMovieLover.API.Validations
{
    public class MovieNameCommandValidation : AbstractValidator<MovieNameCommand>
    {
        public MovieNameCommandValidation()
        {
            RuleFor(command => command.MessageRequest.prompt)
                .NotEmpty().WithMessage("Faça a sua pergunta.");
        }
    }
}
