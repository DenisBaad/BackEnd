using FluentMigrator;

namespace Aquiles.Infrastructure.Migrations.Versions;
[Migration(3, "Create table planos")]

public class Version003 : BaseVersion
{
    public override void Up()
    {
        CreateTable("Planos")
            .WithColumn("Descricao").AsString(14).NotNullable()
            .WithColumn("ValorPlano").AsDecimal(10, 2).NotNullable()
            .WithColumn("QuantidadeUsuarios").AsInt16().NotNullable().WithDefaultValue(1)
            .WithColumn("VigenciaMeses").AsInt16().NotNullable()
            .WithColumn("UsuarioId").AsGuid().NotNullable();
    }
}
