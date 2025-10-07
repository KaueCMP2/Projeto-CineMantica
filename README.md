# 🎬 Modelo de Dados – Sistema de Filmes e Interações de Usuários

Este projeto representa a **modelagem de dados** de uma plataforma onde usuários interagem com filmes, fazem comentários e classificações.  
O modelo foi construído com base em um **Diagrama Entidade-Relacionamento (DER)**.

---

## 🧩 Entidades e Atributos

### 🧑‍💻 Usuário
**Descrição:** Representa os usuarios cadastrados na plataforma.

|   Atributo  |     Tipo    | Restrições |          Descrição             |
|-------------|-------------|------------|--------------------------------|
| id_usuario  | INT IDENTITY| PK         | Identificador único do usuário |
| nome        | VARCHAR(40) | NOT NULL   | Nome completo do usuário       |
| email       | VARCHAR(30) | NOT NULL, UNIQUE | E-mail de login          |
| senha       | VARCHAR(12) | NOT NULL   | Senha de acesso                |
| nick        | VARCHAR(12) | NOT NULL   | Apelido do usuário             |
| data_nasc   | DATE        | NOT NULL   | Data de nascimento             |
| desc_perfil | VARCHAR(100)|            | Descrição do perfil            |
| foto_perfil | VARBINARY   |            | Foto de perfil do usuário      |

**Relacionamentos:**
- Um usuário pode **entrar** em vários filmes.  
- Um usuário pode **fazer** vários comentários.

---

### 🎞️ Filme
**Descrição:** Armazena informações sobre os filmes cadastrados na plataforma.

| Atributo  | Tipo         | Restrições |       Descrição        |
|-----------|--------------|------------|------------------------|
| id_filme  | INT IDENTITY | PK         | Identificador do filme |
| nome      | VARCHAR(40)  | NOT NULL   | Título do filme        |
| id_genero | INT          | FK → Gênero| Gênero do filme        |
| id_criador| INT          | FK → Criador| Criador responsável   |
| desc      | VARCHAR(500) | NOT NULL    | Descrição do filme    |
| data_postagem | DATE     | NOT NULL    | Data de postagem no sistema |

**Relacionamentos:**
- Um filme pode ter **vários comentários**.  
- Um filme pertence a **um gênero**.  
- Um filme tem **um criador**.

---

### 💬 Comentário
**Descrição:** Registra os comentários feitos pelos usuários sobre os filmes.

| Atributo        |     Tipo     | Restrições  |            Descrição        |
|-----------------|--------------|-------------|-----------------------------|
| id_comentario   | INT IDENTITY | PK          | Identificador do comentário |
| tipo_comentario | VARCHAR(20)  |             | Tipo de comentário          |
| id_usuario      | INT          | FK → Usuário| Autor do comentário         |
| data_post       | DATETIME     |             | Data e hora da publicação   |

**Relacionamentos:**
- Um comentário é feito por um **usuário**.  
- Um comentário pertence a um **filme**.

---

### 🧑‍🎨 Criador
**Descrição:** Representa os criadores (diretores, estúdios ou produtores) de filmes.

| Atributo   |     Tipo     | Restrições |       Descrição          |
|------------|--------------|------------|--------------------------|
| id_criador | INT IDENTITY | PK         | Identificador do criador |
| nome       | VARCHAR(50)  | NOT NULL   | Nome do criador          |
| id_filme   | INT          | FK → Filme | Filme associado          |

**Relacionamentos:**
- Um criador pode estar associado a **vários filmes**.

---

### 🎭 Gênero
**Descrição:** Define os gêneros disponíveis para os filmes.

| Atributo |     Tipo     | Restrições |              Descrição                 |
|----------|--------------|------------|----------------------------------------|
| id_genero| INT IDENTITY | PK         | Identificador do gênero                |
| genero   | VARCHAR(10)  | NOT NULL   | Nome do gênero (ex: Ação, Drama, etc.) |

**Relacionamentos:**
- Um gênero pode estar associado a **vários filmes**.

---

## 🔗 Relacionamentos (Resumo)

|  Relação  | Entidades Envolvidas | Cardinalidade | Descrição |
|-----------|----------------------|----------------|------------|
| **Entra** | Usuário ↔ Filme | 1:N | Um usuário pode assistir ou interagir com vários filmes |
| **Faz** | Usuário ↔ Comentário | 1:N | Um usuário pode fazer vários comentários |
| **Tem (Filme–Comentário)** | Filme ↔ Comentário | 1:N | Um filme pode ter vários comentários |
| **Tem (Filme–Gênero)** | Filme ↔ Gênero | N:1 | Vários filmes podem pertencer a um gênero |
| **Tem (Filme–Criador)** | Filme ↔ Criador | N:1 | Vários filmes podem ter o mesmo criador |

---

## 🧠 Visão Geral do Sistema

O sistema modela uma **plataforma de filmes e interação social**, onde:
- Usuários criam perfis, interagem com filmes e deixam comentários.  
- Filmes são classificados por gênero e associados a criadores.  
- Cada comentário é vinculado a um filme e feito por um usuário.  

---

## 🗄️ Possíveis Extensões

- Implementar avaliações (nota) de filmes por usuário.  
- Adicionar categorias personalizadas ou tags.  
- Criar histórico de visualização.  
- Permitir curtidas em comentários.

---

📅 **Data:** Outubro de 2025  
🧱 **Base:** Diagrama Entidade-Relacionamento (DER)
