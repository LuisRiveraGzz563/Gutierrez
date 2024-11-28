using FluentValidation;
using GutierrezAPI.Models.DTOs.Proveedor;

namespace GutierrezAPI.Models.Validators
{
    public class ProveedorDTOValidator:AbstractValidator<ProveedorDTO>
    {
        public ProveedorDTOValidator()
        {
            RuleFor(x => x.Rfc).NotNull().NotEmpty().WithMessage("Ingresa un rfc");
            RuleFor(x => x.NumRegistroRepse).NotNull().NotEmpty().WithMessage("Ingrese su repse");
        }
    }
}
