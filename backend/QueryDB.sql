USE [BRANEF]
GO

-- Criação da tabela EmpresaPorte
CREATE TABLE [dbo].[EmpresaPorte](
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Descricao] NVARCHAR(50) NOT NULL,
    CONSTRAINT [PK_EmpresaPortes] PRIMARY KEY CLUSTERED (
        [Id] ASC
    )
) ON [PRIMARY];
GO

-- Criação da tabela Cliente
CREATE TABLE [dbo].[Cliente](
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Nome] NVARCHAR(250) NOT NULL,
    [EmpresaPorteId] INT NOT NULL,
    CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED (
        [Id] ASC
    ),
    CONSTRAINT [IX_Cliente] UNIQUE NONCLUSTERED (
        [Nome] ASC
    )
) ON [PRIMARY];
GO

-- Criação da chave estrangeira Cliente -> EmpresaPorte
ALTER TABLE [dbo].[Cliente]
ADD CONSTRAINT [FK_Cliente_EmpresaPorte] FOREIGN KEY ([EmpresaPorteId])
REFERENCES [dbo].[EmpresaPorte] ([Id]);
GO

-- Inserção dos valores fixos na tabela EmpresaPorte
INSERT INTO [dbo].[EmpresaPorte] ([Descricao])
VALUES
    ('Pequena'),
    ('Média'),
    ('Grande');
GO