
using FluentValidation;
using FluentValidation.Results;
using IMovieLover.API.Models;
using MediatR;

namespace IMovieLover.API.Behaviors
{
    public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : ErrorResponse
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var failures = _validators
                .Select(validator => validator.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(failure => failure is not null)
                .ToList();

            return failures.Any()
                ? ValidationResponse(failures)
                : next();
        }

        private static Task<TResponse> ValidationResponse(IEnumerable<ValidationFailure> failures)
        {
            var response = new ErrorResponse();

            foreach (var failure in failures)
            {
                response.AddError(failure.ErrorMessage);
            }

            return Task.FromResult(response as TResponse)!;
        }
    }
}
