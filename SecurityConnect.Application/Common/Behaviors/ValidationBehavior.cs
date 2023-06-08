using ErrorOr;
using FluentValidation;
using MediatR;

namespace SecurityConnect.Application.Common.Behaviors
{
    // MediatR pipeline behavior for validating requests
    public class ValidationBehavior<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse>
            where TRequest : IRequest<TResponse>
            where TResponse : IErrorOr
    {
        private readonly IValidator<TRequest>? _validator;

        public ValidationBehavior(IValidator<TRequest>? validator = null)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (_validator is null)
            {
                return await next();
            }

            var validationResult = await _validator.ValidateAsync(request);

            // If validation is successful, pass to the next pipeline handler
            if (validationResult.IsValid)
            {
                return await next();
            }

            // Otherwise, construct an error list from the validation failures
            var errors = validationResult.Errors
                .Select(validationFailure => Error.Validation(
                    validationFailure.PropertyName,
                    validationFailure.ErrorMessage))
                .ToList();

            // Return the error list
            return (dynamic)errors; // Note: dynamic usage can be risky without proper type checks
        }
    }
}
