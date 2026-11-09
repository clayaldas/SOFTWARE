using CleanTeeth.API.Dtos.DentalOffices;
using CleanTeeth.Application.Features.DentalOffices.Commands.CreateDentalOffice;
using CleanTeeth.Application.Features.DentalOffices.Queries.GetDentalOfficeDetail;
using CleanTeeth.Application.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CleanTeeth.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DentalOfficesController : ControllerBase
{
    private readonly IMediator _mediator;

    public DentalOfficesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateDentalOfficeDto dto)
    {
        var command = new CreateDentalOfficeCommand
        {
            Name = dto.Name
        };
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<DentalOfficeDetailDto>> Get(Guid id)
    {
        var query = new GetDentalOfficeDetailQuery
        {
            Id = id
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}

