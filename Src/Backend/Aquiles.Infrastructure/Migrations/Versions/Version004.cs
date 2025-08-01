using FluentMigrator;
using System.Data;

namespace Aquiles.Infrastructure.Migrations.Versions;
[Migration(4, "Create table faturas")]

public class Version004 : BaseVersion
{
    public override void Up()
    {
        CreateTable("Faturas")
            .WithColumn("Status").AsInt16().NotNullable()
            .WithColumn("InicioVigencia").AsDateTime().NotNullable()
            .WithColumn("FimVigencia").AsDateTime().NotNullable()
            .WithColumn("DataPagamento").AsDateTime().Nullable()
            .WithColumn("DataVencimento").AsDateTime().NotNullable()
            .WithColumn("ValorTotal").AsDecimal(10, 2).NotNullable()
            .WithColumn("ValorDesconto").AsDecimal(10, 2).Nullable()
            .WithColumn("ValorPagamento").AsDecimal(10, 2).Nullable()
            .WithColumn("CodBoleto").AsString(45).Nullable()
            .WithColumn("IdTransacao").AsString(45).Nullable()
            .WithColumn("ClienteId").AsGuid().NotNullable()
            .WithColumn("PlanoId").AsGuid().NotNullable().ForeignKey("Planos", "Id").OnDelete(Rule.None);
    }
}
