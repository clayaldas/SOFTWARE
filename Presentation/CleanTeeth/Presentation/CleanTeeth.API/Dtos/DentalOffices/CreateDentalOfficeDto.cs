using System.ComponentModel.DataAnnotations;

namespace CleanTeeth.API.Dtos.DentalOffices;

public class CreateDentalOfficeDto
{
    [Required(ErrorMessage = "El nombre del consultorio es requerido.")]
    [StringLength(150, ErrorMessage = "El nombre no puede superar los 150 caracteres.")]
    public required string Name { get; set; }
}
