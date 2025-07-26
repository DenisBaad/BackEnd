using Aquiles.Communication.Requests.Usuarios;
using FluentValidation;

namespace Aquiles.Application.UseCases.Usuarios.Create;
public class UsuarioValidator : AbstractValidator<RequestCreateUsuariosJson>
{
    public UsuarioValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome deve ser informado");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Deve ser um email válido");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O email deve ser informado");

        RuleFor(x => x.Senha.Length)
            .GreaterThanOrEqualTo(6).WithMessage("A senha deve ser igual ou maior que 6 caracteres");
    }
}
