using CleanTeeth.Domain.Entities;

namespace CleanTeeth.Application.Features.DentalOffices.Queries.GetDentalOfficeDetail;

internal static class DentalOfficeDetailMapper
{
    public static DentalOfficeDetailDto ToDetailDto(this DentalOffice dentalOffice)
    {
        return new DentalOfficeDetailDto
        {
            Id = dentalOffice.Id,
            Name = dentalOffice.Name
        };
    }
}
