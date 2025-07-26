using Aquiles.Application.Helpers;
using Aquiles.Communication.Requests.Clientes;
using FluentValidation;
using FluentValidation.Results;
using System.Text.RegularExpressions;

namespace Aquiles.Application.UseCases.Clientes;
public class ClienteValidator : AbstractValidator<RequestCreateClientesJson>
{
    public ClienteValidator()
    {
        When(p => !string.IsNullOrEmpty(p.CpfCnpj), () =>
        {
            RuleFor(p => p.CpfCnpj)
           .Length(11, 14)
           .WithMessage("O Campo CPFCNPJ não pode ter menos que 11 e nem mais que 14 números");

            RuleFor(p => p.CpfCnpj).Custom((cpfCnpj, context) =>
            {
                if (cpfCnpj.Length == 11 && !ValidaCPF.IsCpf(cpfCnpj))
                    context.AddFailure(new ValidationFailure(nameof(cpfCnpj), "O CPF informado é inválido"));
                else if (cpfCnpj.Length == 14 && !ValidaCNPJ.IsCnpj(cpfCnpj))
                    context.AddFailure(new ValidationFailure(nameof(cpfCnpj), "O CNPJ informado é inválido"));
            });
        });

        RuleFor(p => p.Nome)
            .NotEmpty()
            .WithMessage("O Nome do cliente deve ser informado");

        When(p => !string.IsNullOrEmpty(p.Nome), () =>
        {
            RuleFor(p => p.Nome)
                .Length(3, 45)
                .WithMessage("O Nome do cliente deve ter no mínimo 3 e no máximo 45 caracteres");
        });

        When(p => !string.IsNullOrEmpty(p.Identidade), () =>
        {
            RuleFor(p => p.Identidade).Custom((identidade, context) =>
            {
                if (Regex.IsMatch(identidade, @"[^0-9\s]", RegexOptions.None, TimeSpan.FromSeconds(5)))
                {
                    context.AddFailure(new ValidationFailure(nameof(identidade), "O campo identidade deverá conter somente números"));
                }
            });

            RuleFor(p => p.Identidade)
                .Length(6, 20)
                .WithMessage("O campo Identidade deverá ter no mínimo 6 e no máximo 20 números");

            RuleFor(p => p.OrgaoExpedidor)
                .NotEmpty()
                .WithMessage("Quando informada identidade deverá ser informado o órgão expedidor");

            When(p => !string.IsNullOrEmpty(p.OrgaoExpedidor), () =>
            {
                RuleFor(p => p.OrgaoExpedidor).Custom((orgao, context) =>
                {
                    if (Regex.IsMatch(orgao, @"[^a-zA-Z0-9\s]", RegexOptions.None, TimeSpan.FromSeconds(5)))
                    {
                        context.AddFailure(new ValidationFailure(nameof(orgao), "O Órgão expedidor não poderá conter caracteres especiais"));
                    }
                });

                RuleFor(p => p.OrgaoExpedidor)
                    .Length(1, 10)
                    .WithMessage("O Órgao expedidor deverá ter no máximo 10 caracteres");
            });
        });

        RuleFor(p => p.Tipo)
            .IsInEnum()
            .WithMessage("O tipo de cliente deve ser informado corretamente");

        When(p => !string.IsNullOrEmpty(p.NomeFantasia), () =>
        {
            RuleFor(p => p.NomeFantasia).Custom((fantasia, context) =>
            {
                if (Regex.IsMatch(fantasia, @"[^a-zA-Z0-9\s]", RegexOptions.None, TimeSpan.FromSeconds(5)))
                {
                    context.AddFailure(new ValidationFailure(nameof(fantasia), "O Nome fantasia não poderá conter caracteres especiais"));
                }
            });

            RuleFor(p => p.NomeFantasia)
                .Length(1, 45)
                .WithMessage("O Nome fantasia deve ter no mínimo 3 e no máximo 45 caracteres");
        });
    }
}
