using FluentValidation;
using GutierrezAPI.Models.DTOs.Documento;

namespace GutierrezAPI.Models.Validators
{
    public class DocumentoDTOValidator:AbstractValidator<DocumentoDTO>
    {
        public DocumentoDTOValidator()
        {
            RuleFor(x => x.Nombre).NotNull().NotEmpty().WithMessage("Ingresa el nombre del documento"); 
            RuleFor(x => x.EnviarCada).NotNull().NotEmpty().WithMessage("Ingresa el nombre del documento"); 
        }
    }
}
