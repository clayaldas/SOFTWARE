using CleanTeeth.Application.Features.DentalOffices.Commands.CreateDentalOffice;
using CleanTeeth.Application.Features.DentalOffices.Queries.GetDentalOfficeDetail;
using CleanTeeth.Application.Utilities;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CleanTeeth.Application;

public static class RegisterApplicationServices
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddScoped<IMediator, SimpleMediator>();

        services.AddScoped<
            IRequestHandler<CreateDentalOfficeCommand, Guid>,
            CreateDentalOfficeCommandHandler>();

        services.AddScoped<
            IValidator<CreateDentalOfficeCommand>,
            CreateDentalOfficeCommandValidator>();

        services.AddScoped<
            IRequestHandler<GetDentalOfficeDetailQuery, DentalOfficeDetailDto>,
            GetDentalOfficeDetailQueryHandler>();

        return services;
    }
}
