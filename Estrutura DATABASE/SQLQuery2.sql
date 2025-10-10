
-- Cria e usa banco de dados
CREATE DATABASE projetoCinemantica
GO

Use projetoCinemantica
GO

---------------------------------------------------------

-- CRIANDO TABELAS
-- Tabela usuario

CREATE TABLE Usuario(
id_usuario INT PRIMARY KEY IDENTITY(1, 1),
nome VARCHAR(40) NOT NULL,
email VARCHAR(30) NOT NULL UNIQUE,
senha VARCHAR(20) NOT NULL,
nick_name VARCHAR(12) NOT NULL,
data_nascimento DATE NOT NULL,
desc_perfil NVARCHAR(100),
foto_perfil VARBINARY(max)
)
GO

----------------------------------------------------------


----------------------------------------------------------

-- Tabela Filmes

CREATE TABLE Filme(
id_filme INT PRIMARY KEY IDENTITY(1001, 1),
nome VARCHAR(40) NOT NULL,
id_genero INT FOREIGN KEY REFERENCES generoFilme(id_genero),
descricao_filme VARCHAR(500) NOT NULL,
data_postagem DATE NOT NULL
)
GO

---------------------------------------------------------


---------------------------------------------------------

-- Tabela Comentarios

CREATE TABLE Comentario(
id_comentario INT PRIMARY KEY IDENTITY(101,1),
tipo_comentario VARCHAR(20),
id_usuario INT FOREIGN KEY REFERENCES Usuario(id_usuario),
data_postagem DATETIME
)
GO

--Esqueci de dar link dos comentarios nos filmes
ALTER TABLE Comentario 
ADD id_filme INT FOREIGN KEY REFERENCES Filme(id_filme)
GO

-----------------------------------------------------------


-----------------------------------------------------------
-- ESTRUTURAS DEPENDENTES
-- Tabela Diretor Filme

CREATE TABLE diretorFilme(
id_diretor INT PRIMARY KEY IDENTITY(1, 1),
nome VARCHAR(50) NOT NULL,
id_filme INT FOREIGN KEY REFERENCES Filme(id_filme)
)
GO

-- Tabela genero filme

CREATE TABLE generoFilme (
id_genero INT PRIMARY KEY IDENTITY(1, 1),
nome_genero VARCHAR(10) NOT NULL
)
GO

---------------------------------------------------


-- Ver se funcionou

SELECT * FROM Usuario;
SELECT * FROM Filme;
SELECT * FROM Comentario;
SELECT * FROM diretorFilme;
SELECT * FROM generoFilme