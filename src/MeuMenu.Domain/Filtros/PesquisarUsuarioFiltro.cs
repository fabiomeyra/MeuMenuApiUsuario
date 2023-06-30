using MeuMenu.Domain.Enums;
using MeuMenu.Domain.Filtros.Base;

namespace MeuMenu.Domain.Filtros;

public class PesquisarUsuarioFiltro : BaseFiltro
{
    public Guid UsuarioId { get; set; }
    public string? UsuarioNome { get; set; }
    public string? UsuarioLogin { get; set; }
    public PerfilEnum? PerfilId { get; set; }
}