using AutoMapper;
using MeuMenu.Application.AppServices.Base;
using MeuMenu.Application.Interfaces;
using MeuMenu.Application.ViewModels.Perfil;
using MeuMenu.Domain.Enums;
using MeuMenu.Domain.Interfaces.Repositories;
using MeuMenu.Domain.Services.Base;
using MeuMenu.Domain.UoW;
using MeuMenu.Domain.Utils;

namespace MeuMenu.Application.AppServices;

public class PerfilAppService : BaseAppService, IPerfilAppService
{
    private readonly IPerfilRepository _perfilRepository;

    public PerfilAppService(
        IUnitOfWork ouw, 
        NegocioService negocioService, 
        IMapper mapper, 
        IPerfilRepository perfilRepository) 
            : base(ouw, negocioService, mapper)
    {
        _perfilRepository = perfilRepository;
    }

    public async Task<PerfilViewModel> Obter(int perfilId)
    {
        var retorno = await _perfilRepository.Obter(x => x.PerfilId == perfilId);
        return Mapper.Map<PerfilViewModel>(retorno);
    }

    public ICollection<PerfilViewModel> BuscarParaSelecao()
    {
        var lista = PerfilEnum.Adimin.ParaListaDeSelecao();
        return lista.Select(x => new PerfilViewModel
        {
            PerfilId = x.Valor,
            PerfilDescricao = x.Nome
        }).ToList();
    }
}