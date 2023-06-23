using MeuMenu.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeuMenu.Infra.Data.Context.Mappings;

public class PerfilMapping : IEntityTypeConfiguration<Perfil>
{
    public void Configure(EntityTypeBuilder<Perfil> builder)
    {
        //  Primary key
        builder.HasKey(x => x.PerfilId);

        builder.Property(x => x.PerfilDescricao)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(x => x.PerfilRole)
            .HasColumnType("varchar(30)")
            .IsRequired();

        //  Table
        builder.ToTable("Perfil", "Usuario");
    }
}