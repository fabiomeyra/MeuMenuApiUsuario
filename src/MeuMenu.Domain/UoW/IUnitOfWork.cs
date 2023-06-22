namespace MeuMenu.Domain.UoW;

public interface IUnitOfWork
{
    Task<int> Commit();
}