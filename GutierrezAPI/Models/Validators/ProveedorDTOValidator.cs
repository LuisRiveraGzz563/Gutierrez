using FluentValidation;
using GutierrezAPI.Models.DTOs.Proveedor;

namespace GutierrezAPI.Models.Validators
{
    public class ProveedorDTOValidator:AbstractValidator<ProveedorDTO>
    {
        public ProveedorDTOValidator()
        {
            RuleFor(x => x.Estado).Must(IsValidStatus).WithMessage("Ingresa un estado valido");

            RuleFor(x => x.Rfc).NotNull().NotEmpty().WithMessage("Ingresa un rfc");

        }
        // 
        static bool IsValidStatus(int stat) => stat == 0 || stat == 1 || stat == 2;
    
    }
}
