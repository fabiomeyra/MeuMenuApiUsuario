using MeuMenu.Domain.Filtros;
using MeuMenu.Domain.Interfaces.Services.Base;
using MeuMenu.Domain.Models;
using MeuMenu.Domain.ValueObjects;

namespace MeuMenu.Domain.Interfaces.Services;

public interface IUsuarioService : IBaseService<Usuario>
{
    Task<RetornoPaginadoValueObject<Usuario>> Pesquisar(PesquisarUsuarioFiltro filtro);
}