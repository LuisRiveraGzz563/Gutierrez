using FluentValidation;
using GutierrezAPI.Models.DTOs.Usuario;

namespace GutierrezAPI.Models.Validators
{
    public class UserDTOValidator: AbstractValidator<UserDTO>
    {
        public UserDTOValidator()
        {
            RuleFor(x => x.Correo).NotEmpty().NotNull().WithMessage("Ingresa el correo electronico");
            RuleFor(x => x.Contraseña).NotEmpty().NotNull().WithMessage("Ingresa la contraseña");
            RuleFor(x => x.IdRol).GreaterThanOrEqualTo(1).LessThan(3).WithMessage("Ingresa un rol válido");
            RuleFor(x => x.Usuario).NotEmpty().NotNull().WithMessage("Ingresa un nombre de usuario");
        }
    }
}
