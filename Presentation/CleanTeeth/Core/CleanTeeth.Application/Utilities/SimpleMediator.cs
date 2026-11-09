using CleanTeeth.Application.Exceptions;
using FluentValidation;

namespace CleanTeeth.Application.Utilities;

public class SimpleMediator : IMediator
{
    private readonly IServiceProvider _serviceProvider;

    public SimpleMediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
    {
        var requestType = request.GetType();

        var validatorType = typeof(IValidator<>).MakeGenericType(requestType);
        if (_serviceProvider.GetService(validatorType) is IValidator validator)
        {
            var context = new ValidationContext<object>(request);
            var result = await validator.ValidateAsync(context);

            if (!result.IsValid)
            {
                var errors = result
                    .Errors.GroupBy(failure => failure.PropertyName)
                    .ToDictionary(
                        group => group.Key,
                        group => group.Select(failure => failure.ErrorMessage).ToArray()
                    );

                throw new CustomValidationException(errors);
            }
        }

        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(
            requestType,
            typeof(TResponse)
        );
        var handler = _serviceProvider.GetService(handlerType);

        if (handler is null)
        {
            throw new MediatorException($"No hay un Handler registrado para {requestType.Name}");
        }

        var handle = handlerType.GetMethod("Handle")!;
        return await (Task<TResponse>)handle.Invoke(handler, new object[] { request })!;
    }
}
