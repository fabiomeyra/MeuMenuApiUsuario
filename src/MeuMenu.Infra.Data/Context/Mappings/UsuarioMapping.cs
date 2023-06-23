using MeuMenu.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeuMenu.Infra.Data.Context.Mappings;

public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        //  Primary key
        builder.HasKey(x => x.UsuarioId);

        builder.Property(x => x.UsuarioNome)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(x => x.UsuarioLogin)
            .HasColumnType("varchar(30)")
            .IsRequired();

        builder.Property(x => x.UsuarioSenha)
            .HasColumnType("varchar(500)")
            .IsRequired();

        builder.Property(x => x.PerfilId)
            .HasColumnType("int")
            .IsRequired();

        builder.Property(x => x.DataCadastro)
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(x => x.DataAlteracao)
            .HasColumnType("datetime");

        //  Table
        builder.ToTable("Usuario", "Usuario");
    }
}