using MeuMenu.Application.Interfaces;
using MeuMenu.Application.ViewModels.Usuario;
using MeuMenu.Domain.Interfaces.Notificador;
using MeuMenu.Domain.Interfaces.Repositories;
using MeuMenu.Domain.Services.Base;
using MeuMenu.Domain.UoW;
using MeuMenu.Domain.Validations;
using MeuMenu.Test.Fixtures;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace MeuMenu.Test.AppServices.Usuario;

public class AtualizarUsuarioTests : IClassFixture<AtualizarUsuarioTests.Startup>, IClassFixture<UsuarioTestsFixtures>
{
    private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
    private readonly Mock<IUnitOfWork> _uowMock;
    private readonly IUsuarioAppService _appService;
    private readonly INotificador _notificador;
    private readonly NegocioService _negocioService;
    private readonly UsuarioTestsFixtures _usuarioTestsFixtures;

    public AtualizarUsuarioTests(Startup startup, UsuarioTestsFixtures usuarioTestsFixtures)
    {
        _usuarioTestsFixtures = usuarioTestsFixtures;
        ServiceProvider serviceProvider = startup.ServiceProvider;
        _usuarioRepositoryMock = Mock.Get(serviceProvider.GetService<IUsuarioRepository>()!);
        _uowMock = Mock.Get(serviceProvider.GetService<IUnitOfWork>()!);
        _appService = serviceProvider.GetService<IUsuarioAppService>()!;
        _notificador = serviceProvider.GetService<INotificador>()!;
        _negocioService = serviceProvider.GetService<NegocioService>()!;
    }


    [Fact(DisplayName = "Atualizar Usuario com Sucesso")]
    [Trait("Categoria", "Atualizar App Service Test (Atualizar)")]
    public async Task Usuario_Atualizar_DeveExecutarComSucesso()
    {
        ReiniciarDependencias();

        // Arrange
        var usuarioVm = _usuarioTestsFixtures.AppRetornaUsuarioAtualizarValido();
        _usuarioRepositoryMock.Setup(x => x.Atualizar(It.IsAny<Domain.Models.Usuario>())).ReturnsAsync(() => new Domain.Models.Usuario());
        _uowMock.Setup(x => x.Commit()).ReturnsAsync(() => 1);

        // Act
        var retorno = await _appService.Atualizar(usuarioVm);

        // Assert
        Assert.IsType<UsuarioRetornoViewModel>(retorno);
        _usuarioRepositoryMock.Verify(mock => mock.Atualizar(It.IsAny<Domain.Models.Usuario>()), Times.Once);
        _uowMock.Verify(mock => mock.Commit(), Times.Once);
        Assert.True(_negocioService.EhValido());
    }

    [Fact(DisplayName = "Atualizar Usuario senhas diferente")]
    [Trait("Categoria", "Atualizar App Service Test (Atualizar)")]
    public async Task Usuario_Atualizar_SenhaDiferentes()
    {
        ReiniciarDependencias();

        // Arrange
        var usuarioVm = _usuarioTestsFixtures.AppRetornaUsuarioSenhasDiferentes();
        _uowMock.Setup(x => x.Commit()).ReturnsAsync(() => 0);

        // Act
        var retorno = await _appService.Atualizar(usuarioVm);

        // Assert
        Assert.Null(retorno);
        _usuarioRepositoryMock.Verify(mock => mock.Atualizar(It.IsAny<Domain.Models.Usuario>()), Times.Never);
        _uowMock.Verify(mock => mock.Commit(), Times.Never);
        Assert.False(_negocioService.EhValido());
        Assert.Contains(_notificador.ObterNotificacoes(),
            n => n.Mensagem.Contains(MensagensValidacaoResources.ConfirmarSenhaNaoConfere));
    }



    [Fact(DisplayName = "Atualizar Usuario login não preenchdio")]
    [Trait("Categoria", "Atualizar App Service Test (Atualizar)")]
    public async Task Usuario_Atualizar_LoginNaoPreenchdio()
    {
        ReiniciarDependencias();

        // Arrange
        var usuarioVm = _usuarioTestsFixtures.AppRetornaUsuarioInvalido();
        _usuarioRepositoryMock.Setup(x => x.Atualizar(It.IsAny<Domain.Models.Usuario>())).ReturnsAsync(() => new Domain.Models.Usuario());
        _uowMock.Setup(x => x.Commit()).ReturnsAsync(() => 1);

        // Act
        var retorno = await _appService.Atualizar(usuarioVm);

        // Assert
        Assert.IsType<UsuarioRetornoViewModel>(retorno);
        _usuarioRepositoryMock.Verify(mock => mock.Atualizar(It.IsAny<Domain.Models.Usuario>()), Times.Once);
        _uowMock.Verify(mock => mock.Commit(), Times.Never);
        Assert.False(_negocioService.EhValido());
        Assert.Contains(_notificador.ObterNotificacoes(),
            n => n.Mensagem.Contains(MensagensValidacaoResources.LoginObrigatorio));
    }

    [Fact(DisplayName = "Atualizar Usuario nome não preenchdio")]
    [Trait("Categoria", "Atualizar App Service Test (Atualizar)")]
    public async Task Usuario_Atualizar_NomeNaoPreenchdio()
    {
        ReiniciarDependencias();

        // Arrange
        var usuarioVm = _usuarioTestsFixtures.AppRetornaUsuarioInvalido();
        _usuarioRepositoryMock.Setup(x => x.Atualizar(It.IsAny<Domain.Models.Usuario>())).ReturnsAsync(() => new Domain.Models.Usuario());
        _uowMock.Setup(x => x.Commit()).ReturnsAsync(() => 1);

        // Act
        var retorno = await _appService.Atualizar(usuarioVm);

        // Assert
        Assert.IsType<UsuarioRetornoViewModel>(retorno);
        _usuarioRepositoryMock.Verify(mock => mock.Atualizar(It.IsAny<Domain.Models.Usuario>()), Times.Once);
        _uowMock.Verify(mock => mock.Commit(), Times.Never);
        Assert.False(_negocioService.EhValido());
        Assert.Contains(_notificador.ObterNotificacoes(),
            n => n.Mensagem.Contains(MensagensValidacaoResources.NomeObrigatorio));
    }

    [Fact(DisplayName = "Atualizar Usuario perfil não preenchdio")]
    [Trait("Categoria", "Atualizar App Service Test (Atualizar)")]
    public async Task Usuario_Atualizar_PerfilNaoPreenchdio()
    {
        ReiniciarDependencias();

        // Arrange
        var usuarioVm = _usuarioTestsFixtures.AppRetornaUsuarioInvalido();
        _usuarioRepositoryMock.Setup(x => x.Atualizar(It.IsAny<Domain.Models.Usuario>())).ReturnsAsync(() => new Domain.Models.Usuario());
        _uowMock.Setup(x => x.Commit()).ReturnsAsync(() => 1);

        // Act
        var retorno = await _appService.Atualizar(usuarioVm);

        // Assert
        Assert.IsType<UsuarioRetornoViewModel>(retorno);
        _usuarioRepositoryMock.Verify(mock => mock.Atualizar(It.IsAny<Domain.Models.Usuario>()), Times.Once);
        _uowMock.Verify(mock => mock.Commit(), Times.Never);
        Assert.False(_negocioService.EhValido());
        Assert.Contains(_notificador.ObterNotificacoes(),
            n => n.Mensagem.Contains(MensagensValidacaoResources.DeveInformarPerfilUsuario));
    }

    [Fact(DisplayName = "Atualizar Usuario login já cadastrado")]
    [Trait("Categoria", "Atualizar App Service Test (Atualizar)")]
    public async Task Usuario_Atualizar_LoginJaCadastrado()
    {
        ReiniciarDependencias();

        // Arrange
        var usuarioVm = _usuarioTestsFixtures.AppRetornaUsuarioAtualizarValido();
        _usuarioRepositoryMock.Setup(x => x.Atualizar(It.IsAny<Domain.Models.Usuario>())).ReturnsAsync(() => new Domain.Models.Usuario());
        _usuarioRepositoryMock.Setup(x => x.Obter(It.IsAny<Expression<Func<Domain.Models.Usuario, bool>>>(), y => y.UsuarioLogin, true)).ReturnsAsync(() => "teste");
        _uowMock.Setup(x => x.Commit()).ReturnsAsync(() => 1);

        // Act
        var retorno = await _appService.Atualizar(usuarioVm);

        // Assert
        Assert.IsType<UsuarioRetornoViewModel>(retorno);
        _usuarioRepositoryMock.Verify(mock => mock.Atualizar(It.IsAny<Domain.Models.Usuario>()), Times.Once);
        _uowMock.Verify(mock => mock.Commit(), Times.Never);
        Assert.False(_negocioService.EhValido());
        Assert.Contains(_notificador.ObterNotificacoes(),
            n => n.Mensagem.Contains(MensagensValidacaoResources.LoginJaExistente));
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