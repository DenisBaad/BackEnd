using Aquiles.Communication.Requests.Faturas;
using FluentValidation;

namespace Aquiles.Application.UseCases.Faturas;
public class FaturaValidator : AbstractValidator<RequestCreateFaturaJson>
{
    public FaturaValidator()
    {
        RuleFor(fatura => fatura.ValorTotal)
            .GreaterThanOrEqualTo(0).WithMessage("Fatura não pode ser negativa");
        RuleFor(fatura => fatura.DataVencimento)
            .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Fatura vencimento invalido");
    }
}


