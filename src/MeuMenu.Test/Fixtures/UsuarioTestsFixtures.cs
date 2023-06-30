using System;
using MeuMenu.Application.ViewModels.Usuario;
using MeuMenu.Domain.Enums;
using MeuMenu.Domain.Models;
using Xunit;

namespace MeuMenu.Test.Fixtures;

[CollectionDefinition(nameof(UsuarioCollectoin))]
public class UsuarioCollectoin : ICollectionFixture<UsuarioTestsFixtures>
{
    
}

public class UsuarioTestsFixtures : IDisposable
{
    public UsuarioSalvarViewModel AppRetornaUsuarioValido()
    {
        return new UsuarioSalvarViewModel
        {
            UsuarioNome = "Usuário de teste",
            UsuarioLogin = "teste",
            UsuarioSenha = "senha123",
            UsuarioSenhaConfirmacao = "senha123",
            PerfilId = 1
        };
    }

    public UsuarioSalvarViewModel AppRetornaUsuarioAtualizarValido()
    {
        return new UsuarioSalvarViewModel
        {
            UsuarioId = Guid.NewGuid(),
            UsuarioNome = "Usuário de teste",
            UsuarioLogin = "teste",
            UsuarioSenha = "senha123",
            UsuarioSenhaConfirmacao = "senha123",
            PerfilId = 1
        };
    }

    public UsuarioSalvarViewModel AppRetornaUsuarioSenhasDiferentes()
    {
        return new UsuarioSalvarViewModel
        {
            UsuarioNome = "Usuário de teste",
            UsuarioLogin = "teste",
            UsuarioSenha = "senha123",
            UsuarioSenhaConfirmacao = "senha",
            PerfilId = 1
        };
    }

    public UsuarioSalvarViewModel AppRetornaUsuarioInvalido()
    {
        return new UsuarioSalvarViewModel
        {
            UsuarioSenha = "senha123",
            UsuarioSenhaConfirmacao = "senha123"
        };
    }

    public Usuario RetornaUsuarioValido()
    {
        var usuario = new Usuario()
        {
            UsuarioNome = "Usuário de teste",
            UsuarioLogin = "teste",
            UsuarioSenha = "senha123",
            PerfilId = PerfilEnum.Adimin
        };

        return usuario;
    }
    public Usuario RetornaUsuarioAtualizarValido()
    {
        var usuario = new Usuario()
        {
            UsuarioId = Guid.Parse("ee6431b2-0255-4ba3-81bd-aa0a57f354e3"),
            UsuarioNome = "Usuário de teste",
            UsuarioLogin = "teste",
            UsuarioSenha = "senha123",
            PerfilId = PerfilEnum.Adimin
        };

        return usuario;
    }

    public Usuario RetornaUsuarioInvalido()
    {
        return new Usuario();
    }
    
    public Usuario RetornaUsuarioAtualizarInvalido()
    {
        return new Usuario
        {
            UsuarioId = Guid.NewGuid()
        };
    }

    public void Dispose()
    {
    }
}