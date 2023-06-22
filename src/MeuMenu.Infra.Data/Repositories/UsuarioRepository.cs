using MeuMenu.Domain.Interfaces.Repositories;
using MeuMenu.Domain.Models.Usuario;
using MeuMenu.Infra.Data.Context;
using MeuMenu.Infra.Data.Repositories.Base;

namespace MeuMenu.Infra.Data.Repositories;

public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(MeuMenuUsuarioContext contexto) : base(contexto)
    {
    }
}