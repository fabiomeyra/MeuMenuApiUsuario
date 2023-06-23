using MeuMenu.Application.ViewModels.Perfil;

namespace MeuMenu.Application.Interfaces;

public interface IPerfilAppService
{
    ICollection<PerfilViewModel> BuscarParaSelecao();
    Task<PerfilViewModel> Obter(int perfilId);
}