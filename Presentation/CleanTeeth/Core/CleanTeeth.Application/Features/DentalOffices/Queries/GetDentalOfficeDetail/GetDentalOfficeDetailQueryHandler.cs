using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Exceptions;
using CleanTeeth.Application.Utilities;

namespace CleanTeeth.Application.Features.DentalOffices.Queries.GetDentalOfficeDetail;

public class GetDentalOfficeDetailQueryHandler
    : IRequestHandler<GetDentalOfficeDetailQuery, DentalOfficeDetailDto>
{
    private readonly IDentalOfficeRepository _repository;

    public GetDentalOfficeDetailQueryHandler(IDentalOfficeRepository repository)
    {
        _repository = repository;
    }

    public async Task<DentalOfficeDetailDto> Handle(GetDentalOfficeDetailQuery query)
    {
        var dentalOffice = await _repository.GetByIdAsync(query.Id);

        if (dentalOffice is null)
        {
            throw new NotFoundException(
                $"No existe un consultorio con el Id {query.Id}.");
        }

        return dentalOffice.ToDetailDto();
    }
}
