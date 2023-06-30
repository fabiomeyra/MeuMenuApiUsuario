using MeuMenu.Domain.Filtros;
using MeuMenu.Domain.Interfaces.Repositories.Base;
using MeuMenu.Domain.Models;
using MeuMenu.Domain.ValueObjects;

namespace MeuMenu.Domain.Interfaces.Repositories;

public interface IUsuarioRepository : IBaseRepository<Usuario>
{
    Task<RetornoPaginadoValueObject<Usuario>> Pesquisar(PesquisarUsuarioFiltro filtro);
}