using MeuMenu.Application.Filtros.Base;

namespace MeuMenu.Application.Filtros;

public class PesquisarUsuarioFiltroViewModel : BaseFiltroViewModel
{
    public Guid UsuarioId { get; set; }
    public string? UsuarioNome { get; set; }
    public string? UsuarioLogin { get; set; }
    public int? PerfilId { get; set; }
}