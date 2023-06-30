using MeuMenu.Domain.Filtros;
using MeuMenu.Domain.Interfaces.Repositories;
using MeuMenu.Domain.Models;
using MeuMenu.Domain.ValueObjects;
using MeuMenu.Infra.Data.Context;
using MeuMenu.Infra.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace MeuMenu.Infra.Data.Repositories;

public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(MeuMenuUsuarioContext contexto) : base(contexto)
    {
    }

    public async Task<RetornoPaginadoValueObject<Usuario>> Pesquisar(PesquisarUsuarioFiltro filtro)
    {
        var retorno = new RetornoPaginadoValueObject<Usuario>();

        IQueryable<Usuario> query = Entidades;

        if (filtro.PerfilId is > 0)
            query = query.Where(x => x.PerfilId == filtro.PerfilId);

        if (!string.IsNullOrWhiteSpace(filtro.UsuarioNome))
            query = query
                .Where(x => x.UsuarioNome != null 
                            && x.UsuarioNome.ToUpper().Contains(filtro.UsuarioNome.ToUpper()));

        if (!string.IsNullOrWhiteSpace(filtro.UsuarioLogin))
            query = query
                .Where(x => x.UsuarioLogin != null &&
                            x.UsuarioLogin.ToUpper().Contains(filtro.UsuarioLogin.ToUpper()));

        if (filtro.PaginaAtual.HasValue) retorno.QuantidadeTotalRegistros = await query.CountAsync();

        query = filtro.PaginaAtual is null or 0
            ? query
            : query.Skip((filtro.PaginaAtual.Value - 1) * (filtro.QuantidadePorPagina ?? 15))
                .Take(filtro.QuantidadePorPagina ?? 15);

        retorno.Lista = await query.Select(x => new Usuario
        {
            UsuarioId = x.UsuarioId,
            PerfilId = x.PerfilId,
            UsuarioNome = x.UsuarioNome,
            UsuarioLogin = x.UsuarioLogin
        }).ToArrayAsync();

        retorno.QuantidadeTotalRegistros = retorno.QuantidadeTotalRegistros > 0
            ? retorno.QuantidadeTotalRegistros
            : retorno.Lista.Count;

        return retorno;
    }
}