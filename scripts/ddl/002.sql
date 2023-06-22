USE [MeuMenuDb]
GO

CREATE TABLE Usuario.Perfil (
	PerfilId int not null,
	PerfilDescricao varchar(100) not null,
	PerfilRole varchar(30) not null
)
go

ALTER TABLE Usuario.Perfil
ADD CONSTRAINT PK_PERFIL PRIMARY KEY (PerfilId)
GO

CREATE TABLE Usuario.Usuario (
	UsuarioId uniqueidentifier NOT NULL,
	UsuarioNome varchar(100) NOT NULL,
	UsuarioLogin varchar(30) NOT NULL,
	UsuarioSenha varchar(500) NOT NULL,
	PerfilId INT NOT NULL
)

ALTER TABLE Usuario.Usuario
ADD DataCadastro datetime NOT NULL
GO

ALTER TABLE Usuario.Usuario
ADD DataAlteracao datetime
GO

ALTER TABLE Usuario.Usuario
ADD CONSTRAINT PK_USUARIO PRIMARY KEY (UsuarioId)
GO

ALTER TABLE Usuario.Usuario
ADD CONSTRAINT FK_USUARIO_PERFIL FOREIGN KEY(PerfilId) REFERENCES Usuario.Perfil(PerfilId)
GO