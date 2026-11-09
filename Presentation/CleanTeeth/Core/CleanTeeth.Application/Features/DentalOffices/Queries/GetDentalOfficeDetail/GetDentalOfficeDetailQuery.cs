using CleanTeeth.Application.Utilities;

namespace CleanTeeth.Application.Features.DentalOffices.Queries.GetDentalOfficeDetail;

public class GetDentalOfficeDetailQuery : IRequest<DentalOfficeDetailDto>
{
    public required Guid Id { get; set; }
}
