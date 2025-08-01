using Aquiles.Communication.Enums;

namespace Aquiles.Application.UseCases.Relatorios.RelatorioFaturas;
public interface IRelatorioFaturas
{
    Task<byte[]> Executar(string usuarioNome, DateTime? dataAbertura, DateTime? dataFechamento, EnumStatusFatura? status, List<string> clienteId);
}
