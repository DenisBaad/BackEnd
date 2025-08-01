using Aquiles.Communication.Requests.Planos;
using FluentValidation;

namespace Aquiles.Application.UseCases.Planos;
public class PlanoValidator : AbstractValidator<RequestCreatePlanoJson>
{
    public PlanoValidator()
    {
        RuleFor(plano => plano.Descricao)
        .NotEmpty().WithMessage("Descrição obrigatória");
        
        RuleFor(plano => plano.ValorPlano)
            .GreaterThan(0).WithMessage("Valor do plano não pode ser negativo");
        
        RuleFor(plano => plano.QuantidadeUsuarios)
            .GreaterThan(0).WithMessage("Quantidade usuario não pode ser zero");
        
        RuleFor(plano => plano.VigenciaMeses)
            .GreaterThan(0).WithMessage("Vigencia do mês deve ser maior que zero");
    }
}
