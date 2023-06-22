using MeuMenu.Application.ViewModels.Usuario;

namespace MeuMenu.Application.Interfaces;

public interface IUsuarioAppService
{
    Task<ICollection<UsuarioViewModel>> BuscarTodos();
    Task<UsuarioViewModel> Obter(Guid usuarioId);
    Task<UsuarioViewModel> Adicionar(UsuarioViewModel usuarioVm);
    Task<UsuarioViewModel> Atualizar(UsuarioViewModel usuarioVm);
    Task Excluir(Guid usuarioId);
}