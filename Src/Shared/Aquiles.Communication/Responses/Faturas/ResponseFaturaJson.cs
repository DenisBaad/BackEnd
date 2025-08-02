using Aquiles.Communication.Enums;

namespace Aquiles.Communication.Responses.Faturas;
public class ResponseFaturaJson
{
    public Guid Id { get; set; }
    public EnumStatusFatura Status { get; set; }
    public DateTime InicioVigencia { get; set; }
    public DateTime FimVigencia { get; set; }
    public DateTime? DataPagamento { get; set; }
    public DateTime DataVencimento { get; set; }
    public decimal ValorTotal { get; set; }
    public decimal? ValorDesconto { get; set; }
    public decimal? ValorPagamento { get; set; }
    public string? CodBoleto { get; set; }
    public string? IdTransacao { get; set; }
    public Guid ClienteId { get; set; }
    public Guid PlanoId { get; set; }
}
