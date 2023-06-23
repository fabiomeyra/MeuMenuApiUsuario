using MeuMenu.Domain.Interfaces.Repositories;
using MeuMenu.Domain.Models;
using MeuMenu.Infra.Data.Context;
using MeuMenu.Infra.Data.Repositories.Base;

namespace MeuMenu.Infra.Data.Repositories;

public class PerfilRepository : BaseRepository<Perfil>, IPerfilRepository
{
    public PerfilRepository(MeuMenuUsuarioContext contexto) 
        : base(contexto)
    {
    }
}