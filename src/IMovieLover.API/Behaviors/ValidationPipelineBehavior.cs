using MediatR;
using FluentValidation;
using IMovieLover.API.Models;
using FluentValidation.Results;

namespace IMovieLover.API.Behaviors
{
    public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : ErrorResponse
    {
        private readonly IValidator<TRequest> _validator;

        public ValidationPipelineBehavior(IValidator<TRequest> validator)
        {
            _validator = validator;
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            var failure = validationResult.Errors.FirstOrDefault();

            return failure is not null
                ? ValidationResponse(failure)
                : next();
        }

        private static Task<TResponse> ValidationResponse(ValidationFailure failure)
        {
            var response = new ErrorResponse();

            response.AddError(failure.ErrorMessage);
            
            return Task.FromResult(response as TResponse)!;
        }
    }
}
