namespace MeuMenu.Application.ViewModels.Usuario;

public class UsuarioRetornoViewModel
{
    public Guid UsuarioId { get; set; }
    public string? UsuarioNome { get; set; }
    public string? UsuarioLogin { get; set; }
    public int? PerfilId { get; set; }
    public string? PerfilDescricao { get; set; }
    public string? Permissao { get; set; }
}