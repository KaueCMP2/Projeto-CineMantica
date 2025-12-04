
-- Cria e usa banco de dados
CREATE DATABASE projetoCinemantica 
GO

Use projetoCinemantica
GO

---------------------------------------------------------

-- CRIANDO TABELAS


-- Tabela genero filme
CREATE TABLE generoFilme (
id_genero INT PRIMARY KEY IDENTITY(1, 1),
nome_genero VARCHAR(10) NOT NULL
)
GO

-- Tabela Diretor Filme
CREATE TABLE diretorFilme(
id_diretor INT PRIMARY KEY IDENTITY(1, 1),
nome VARCHAR(50) NOT NULL
)
GO

--Tabela Regras
CREATE TABLE RegraPerfil (
	IdRegra INT IDENTITY(1,1) PRIMARY KEY,
	Nome NVARCHAR(40) NOT NULL UNIQUE
)
GO

-- Tabela usuario

CREATE TABLE Usuario(
id_usuario INT PRIMARY KEY IDENTITY(1, 1),
nome VARCHAR(40) NOT NULL,
email VARCHAR(30) NOT NULL UNIQUE,
senha VARBINARY(32) NOT NULL,
nick_name VARCHAR(12) NOT NULL,
data_nascimento DATE NOT NULL,
desc_perfil NVARCHAR(100),
foto_perfil VARBINARY(max),
 RegraId INT NOT NULL, 
 CONSTRAINT FK_Usuario_Regra FOREIGN KEY (RegraId) 
 REFERENCES RegraPerfil(IdRegra) ON DELETE CASCADE
)
GO

----------------------------------------------------------


----------------------------------------------------------

-- Tabela Filmes

CREATE TABLE Filme(
id_filme INT PRIMARY KEY IDENTITY(1001, 1),
nome VARCHAR(40) NOT NULL,
descricao_filme VARCHAR(500) NOT NULL,
data_postagem DATE NOT NULL,
id_diretor INT,
id_genero INT, 
CONSTRAINT fk_idGenero_filme FOREIGN KEY (id_genero) REFERENCES generoFilme(id_genero),
CONSTRAINT fk_idDiretor_filme FOREIGN KEY (id_diretor) REFERENCES diretorFilme(id_diretor)
)
GO

---------------------------------------------------------


---------------------------------------------------------

-- Tabela Comentarios

CREATE TABLE Comentario(
id_comentario INT PRIMARY KEY IDENTITY(101,1),
tipo_comentario VARCHAR(20),
id_usuario INT FOREIGN KEY REFERENCES Usuario(id_usuario),
data_postagem DATETIME,
id_filme INT,
CONSTRAINT fk_idFilme_Comentario FOREIGN KEY (id_filme) REFERENCES Filme(id_filme)
)
GO

-----------------------------------------------------------
-- Ver se funcionou

SELECT * FROM Usuario;
SELECT * FROM Filme;
SELECT * FROM Comentario;
SELECT * FROM diretorFilme;
SELECT * FROM generoFilme;
