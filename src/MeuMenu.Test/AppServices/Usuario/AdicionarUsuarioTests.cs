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

public class AdicionarUsuarioTests : IClassFixture<AdicionarUsuarioTests.Startup>, IClassFixture<UsuarioTestsFixtures>
{
    private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
    private readonly Mock<IUnitOfWork> _uowMock;
    private readonly IUsuarioAppService _appService;
    private readonly INotificador _notificador;
    private readonly NegocioService _negocioService;
    private readonly UsuarioTestsFixtures _usuarioTestsFixtures;

    public AdicionarUsuarioTests(Startup startup, UsuarioTestsFixtures usuarioTestsFixtures)
    {
        _usuarioTestsFixtures = usuarioTestsFixtures;
        ServiceProvider serviceProvider = startup.ServiceProvider;
        _usuarioRepositoryMock = Mock.Get(serviceProvider.GetService<IUsuarioRepository>()!);
        _uowMock = Mock.Get(serviceProvider.GetService<IUnitOfWork>()!);
        _appService = serviceProvider.GetService<IUsuarioAppService>()!;
        _notificador = serviceProvider.GetService<INotificador>()!;
        _negocioService = serviceProvider.GetService<NegocioService>()!;
    }


    [Fact(DisplayName = "Adicionar Usuario com Sucesso")]
    [Trait("Categoria", "Adicionar App Service Test (Adicionar)")]
    public async Task Usuario_Adicionar_DeveExecutarComSucesso()
    {
        ReiniciarDependencias();

        // Arrange
        var usuarioVm = _usuarioTestsFixtures.AppRetornaUsuarioValido();
        _usuarioRepositoryMock.Setup(x => x.Adicionar(It.IsAny<Domain.Models.Usuario>())).ReturnsAsync(() => new Domain.Models.Usuario());
        _uowMock.Setup(x => x.Commit()).ReturnsAsync(() => 1);

        // Act
        var retorno = await _appService.Adicionar(usuarioVm);

        // Assert
        Assert.IsType<UsuarioRetornoViewModel>(retorno);
        _usuarioRepositoryMock.Verify(mock => mock.Adicionar(It.IsAny<Domain.Models.Usuario>()), Times.Once);
        _uowMock.Verify(mock => mock.Commit(), Times.Once);
        Assert.True(_negocioService.EhValido());
    }

    [Fact(DisplayName = "Adicionar Usuario senhas diferente")]
    [Trait("Categoria", "Adicionar App Service Test (Adicionar)")]
    public async Task Usuario_Adicionar_SenhaDiferentes()
    {
        ReiniciarDependencias();

        // Arrange
        var usuarioVm = _usuarioTestsFixtures.AppRetornaUsuarioSenhasDiferentes();
        _uowMock.Setup(x => x.Commit()).ReturnsAsync(() => 0);

        // Act
        var retorno = await _appService.Adicionar(usuarioVm);

        // Assert
        Assert.Null(retorno);
        _usuarioRepositoryMock.Verify(mock => mock.Adicionar(It.IsAny<Domain.Models.Usuario>()), Times.Never);
        _uowMock.Verify(mock => mock.Commit(), Times.Never);
        Assert.False(_negocioService.EhValido());
        Assert.Contains(_notificador.ObterNotificacoes(),
            n => n.Mensagem.Contains(MensagensValidacaoResources.ConfirmarSenhaNaoConfere));
    }



    [Fact(DisplayName = "Adicionar Usuario login não preenchdio")]
    [Trait("Categoria", "Adicionar App Service Test (Adicionar)")]
    public async Task Usuario_Adicionar_LoginNaoPreenchdio()
    {
        ReiniciarDependencias();

        // Arrange
        var usuarioVm = _usuarioTestsFixtures.AppRetornaUsuarioInvalido();
        _usuarioRepositoryMock.Setup(x => x.Adicionar(It.IsAny<Domain.Models.Usuario>())).ReturnsAsync(() => new Domain.Models.Usuario());
        _uowMock.Setup(x => x.Commit()).ReturnsAsync(() => 1);

        // Act
        var retorno = await _appService.Adicionar(usuarioVm);

        // Assert
        Assert.IsType<UsuarioRetornoViewModel>(retorno);
        _usuarioRepositoryMock.Verify(mock => mock.Adicionar(It.IsAny<Domain.Models.Usuario>()), Times.Once);
        _uowMock.Verify(mock => mock.Commit(), Times.Never);
        Assert.False(_negocioService.EhValido());
        Assert.Contains(_notificador.ObterNotificacoes(),
            n => n.Mensagem.Contains(MensagensValidacaoResources.LoginObrigatorio));
    }

    [Fact(DisplayName = "Adicionar Usuario nome não preenchdio")]
    [Trait("Categoria", "Adicionar App Service Test (Adicionar)")]
    public async Task Usuario_Adicionar_NomeNaoPreenchdio()
    {
        ReiniciarDependencias();

        // Arrange
        var usuarioVm = _usuarioTestsFixtures.AppRetornaUsuarioInvalido();
        _usuarioRepositoryMock.Setup(x => x.Adicionar(It.IsAny<Domain.Models.Usuario>())).ReturnsAsync(() => new Domain.Models.Usuario());
        _uowMock.Setup(x => x.Commit()).ReturnsAsync(() => 1);

        // Act
        var retorno = await _appService.Adicionar(usuarioVm);

        // Assert
        Assert.IsType<UsuarioRetornoViewModel>(retorno);
        _usuarioRepositoryMock.Verify(mock => mock.Adicionar(It.IsAny<Domain.Models.Usuario>()), Times.Once);
        _uowMock.Verify(mock => mock.Commit(), Times.Never);
        Assert.False(_negocioService.EhValido());
        Assert.Contains(_notificador.ObterNotificacoes(),
            n => n.Mensagem.Contains(MensagensValidacaoResources.NomeObrigatorio));
    }

    [Fact(DisplayName = "Adicionar Usuario perfil não preenchdio")]
    [Trait("Categoria", "Adicionar App Service Test (Adicionar)")]
    public async Task Usuario_Adicionar_PerfilNaoPreenchdio()
    {
        ReiniciarDependencias();

        // Arrange
        var usuarioVm = _usuarioTestsFixtures.AppRetornaUsuarioInvalido();
        _usuarioRepositoryMock.Setup(x => x.Adicionar(It.IsAny<Domain.Models.Usuario>())).ReturnsAsync(() => new Domain.Models.Usuario());
        _uowMock.Setup(x => x.Commit()).ReturnsAsync(() => 1);

        // Act
        var retorno = await _appService.Adicionar(usuarioVm);

        // Assert
        Assert.IsType<UsuarioRetornoViewModel>(retorno);
        _usuarioRepositoryMock.Verify(mock => mock.Adicionar(It.IsAny<Domain.Models.Usuario>()), Times.Once);
        _uowMock.Verify(mock => mock.Commit(), Times.Never);
        Assert.False(_negocioService.EhValido());
        Assert.Contains(_notificador.ObterNotificacoes(),
            n => n.Mensagem.Contains(MensagensValidacaoResources.DeveInformarPerfilUsuario));
    }

    [Fact(DisplayName = "Adicionar Usuario login já cadastrado")]
    [Trait("Categoria", "Adicionar App Service Test (Adicionar)")]
    public async Task Usuario_Adicionar_LoginJaCadastrado()
    {
        ReiniciarDependencias();

        // Arrange
        var usuarioVm = _usuarioTestsFixtures.AppRetornaUsuarioValido();
        _usuarioRepositoryMock.Setup(x => x.Adicionar(It.IsAny<Domain.Models.Usuario>())).ReturnsAsync(() => new Domain.Models.Usuario());
        _usuarioRepositoryMock.Setup(x => x.Obter(It.IsAny<Expression<Func<Domain.Models.Usuario, bool>>>(), y => y.UsuarioLogin, true)).ReturnsAsync(() => "teste");
        _uowMock.Setup(x => x.Commit()).ReturnsAsync(() => 1);

        // Act
        var retorno = await _appService.Adicionar(usuarioVm);

        // Assert
        Assert.IsType<UsuarioRetornoViewModel>(retorno);
        _usuarioRepositoryMock.Verify(mock => mock.Adicionar(It.IsAny<Domain.Models.Usuario>()), Times.Once);
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