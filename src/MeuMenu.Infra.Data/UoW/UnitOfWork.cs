using MeuMenu.Domain.UoW;
using MeuMenu.Infra.Data.Context;

namespace MeuMenu.Infra.Data.UoW;

public class UnitOfWork : IUnitOfWork
{
    private readonly MeuMenuUsuarioContext _context;

    public UnitOfWork(MeuMenuUsuarioContext context)
    {
        _context = context;
    }

    public async Task<int> Commit()
    {
        return await _context.SaveChangesAsync();
    }
}