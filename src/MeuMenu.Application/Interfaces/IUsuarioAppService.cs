using MeuMenu.Application.ViewModels.Usuario;

namespace MeuMenu.Application.Interfaces;

public interface IUsuarioAppService
{
    Task<ICollection<UsuarioRetornoViewModel>> BuscarTodos();
    Task<UsuarioRetornoViewModel> Obter(Guid usuarioId);
    Task<UsuarioRetornoViewModel> Adicionar(UsuarioSalvarViewModel usuarioVm);
    Task<UsuarioRetornoViewModel> Atualizar(UsuarioSalvarViewModel usuarioVm);
    Task Excluir(Guid usuarioId);
    Task<UsuarioRetornoViewModel?> RealizarLogin(UsuarioLoginViewModel usuarioVm);
}