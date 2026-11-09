using FluentValidation;

namespace CleanTeeth.Application.Features.DentalOffices.Commands.CreateDentalOffice;

public class CreateDentalOfficeCommandValidator : AbstractValidator<CreateDentalOfficeCommand>
{
    public CreateDentalOfficeCommandValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty()
            .WithMessage("El nombre del consultorio es requerido.");
    }
}
