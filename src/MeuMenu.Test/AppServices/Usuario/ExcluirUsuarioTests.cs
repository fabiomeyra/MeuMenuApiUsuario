using MeuMenu.Application.Interfaces;
using MeuMenu.Domain.Interfaces.Notificador;
using MeuMenu.Domain.Interfaces.Repositories;
using MeuMenu.Domain.Services.Base;
using MeuMenu.Domain.UoW;
using MeuMenu.Domain.Validations;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MeuMenu.Test.AppServices.Usuario;

public class ExcluirUsuarioTests : IClassFixture<ExcluirUsuarioTests.Startup>
{
    private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
    private readonly Mock<IUnitOfWork> _uowMock;
    private readonly IUsuarioAppService _appService;
    private readonly INotificador _notificador;
    private readonly NegocioService _negocioService;

    public ExcluirUsuarioTests(Startup startup)
    {
        ServiceProvider serviceProvider = startup.ServiceProvider;
        _usuarioRepositoryMock = Mock.Get(serviceProvider.GetService<IUsuarioRepository>()!);
        _uowMock = Mock.Get(serviceProvider.GetService<IUnitOfWork>()!);
        _appService = serviceProvider.GetService<IUsuarioAppService>()!;
        _notificador = serviceProvider.GetService<INotificador>()!;
        _negocioService = serviceProvider.GetService<NegocioService>()!;
    }


    [Fact(DisplayName = "Excluir Usuario com Sucesso")]
    [Trait("Categoria", "Excluir App Service Test (Excluir)")]
    public async Task Usuario_Excluir_DeveExecutarComSucesso()
    {
        ReiniciarDependencias();

        // Arrange
        _uowMock.Setup(x => x.Commit()).ReturnsAsync(() => 1);

        // Act
        await _appService.Excluir(Guid.NewGuid());

        // Assert
        _usuarioRepositoryMock.Verify(mock => mock.Excluir(It.IsAny<Domain.Models.Usuario>()), Times.Once);
        _uowMock.Verify(mock => mock.Commit(), Times.Once);
        Assert.True(_negocioService.EhValido());
    }

    [Fact(DisplayName = "Excluir Usuario Id não informado")]
    [Trait("Categoria", "Excluir App Service Test (Excluir)")]
    public async Task Usuario_Excluir_SenhaDiferentes()
    {
        ReiniciarDependencias();

        // Arrange
        _uowMock.Setup(x => x.Commit()).ReturnsAsync(() => 0);

        // Act
        await _appService.Excluir(Guid.Empty);

        // Assert
        _usuarioRepositoryMock.Verify(mock => mock.Excluir(It.IsAny<Domain.Models.Usuario>()), Times.Once);
        _uowMock.Verify(mock => mock.Commit(), Times.Never);
        Assert.False(_negocioService.EhValido());
        Assert.Contains(_notificador.ObterNotificacoes(),
            n => n.Mensagem.Contains(MensagensValidacaoResources.DeveInformarIdentificadorExluir));
    }

    private void ReiniciarDependencias()
    {
        _usuarioRepositoryMock.Reset();
        _uowMock.Reset();
        _negocioService.LimparNotificacoes();
        _negocioService.LimparValidacoes();
    }

    public class Startup : BaseStartup
    {
        public override IServiceCollection OnConfigureServices(IServiceCollection services)
        {
            return services
                .AddScoped(_ => Mock.Of<IUsuarioRepository>())
                .AddScoped(_ => Mock.Of<IUnitOfWork>());
        }
    }
}