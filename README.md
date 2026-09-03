# 🎬 Movie API — Catálogo de Filmes

## 📌 Tema e Objetivo

O tema escolhido foi um **Catálogo de Filmes**. A API permite gerenciar um acervo
de filmes, oferecendo operações completas de CRUD (Criar, Ler, Atualizar e Remover)
para a entidade `Filme`. O objetivo é demonstrar, de forma simples e didática, a
construção de uma Web API em **ASP.NET Core (.NET 10)** seguindo boas práticas de
arquitetura (separação em camadas), uso de DTOs, injeção de dependência e
documentação via Swagger/OpenAPI.

## 👥 Integrantes

| Nome completo              | RM        |
|-----------------------------|-----------|
| Lucas Catroppa Piratininga Dias | [RM555450] |
| Fernando Gonzales Alexandre  | [RM555045] |
| Gabriel Guerreiro Escobosa Vallejo  | [RM554973] |
| Luiz Felipe Coelho Ramos  | [RM555074] |
| Vitor Musolino Teixeira  | [RM555012] |

## 🏗️ Arquitetura do Projeto

```
MovieApi/
├── MovieApi.sln
├── README.md
├── .gitignore
└── MovieApi/
    ├── Controllers/
    │   └── FilmesController.cs      # Endpoints da API (ControllerBase)
    ├── Models/
    │   └── Filme.cs                 # Entidade de domínio
    ├── DTOs/
    │   ├── FilmeRequestDto.cs       # DTO de entrada (criação/atualização, sem Id)
    │   └── FilmeResponseDto.cs      # DTO de saída
    ├── Data/
    │   └── AppDbContext.cs          # "Banco de dados" em memória (lista)
    ├── Program.cs                   # Configuração da aplicação e Swagger
    ├── appsettings.json
    └── Properties/
        └── launchSettings.json
```

### Entidade de domínio — `Filme`

| Campo             | Tipo   | Descrição                          |
|--------------------|--------|-------------------------------------|
| `Id`               | int    | Identificador único (gerado pela API) |
| `Titulo`           | string | Título do filme                     |
| `Diretor`          | string | Nome do diretor                     |
| `Genero`           | string | Gênero do filme                     |
| `AnoLancamento`    | int    | Ano de lançamento                   |
| `DuracaoMinutos`   | int    | Duração em minutos                  |

### Persistência em memória

A classe `AppDbContext` (pasta `Data/`) mantém uma `List<Filme>` em memória,
simulando um banco de dados. Ela é registrada no container de injeção de
dependência como **Singleton**:

```csharp
builder.Services.AddSingleton<AppDbContext>();
```

Isso garante que a mesma instância — e, portanto, os mesmos dados — seja
compartilhada durante todo o tempo de vida da aplicação. O contexto já vem
com dois filmes cadastrados por padrão (seed), para facilitar os primeiros
testes no Swagger.

## 🔗 Endpoints

Base da rota: `api/v1/filmes`

| Verbo    | Rota                     | Descrição                          | Códigos de retorno       |
|----------|--------------------------|-------------------------------------|---------------------------|
| `GET`    | `/api/v1/filmes`         | Lista todos os filmes               | `200 OK`                  |
| `GET`    | `/api/v1/filmes/{id}`    | Busca um filme pelo Id              | `200 OK`, `404 Not Found` |
| `POST`   | `/api/v1/filmes`         | Cria um novo filme                  | `201 Created`, `400 Bad Request` |
| `PUT`    | `/api/v1/filmes/{id}`    | Atualiza um filme existente         | `200 OK`, `400 Bad Request`, `404 Not Found` |
| `DELETE` | `/api/v1/filmes/{id}`    | Remove um filme pelo Id             | `204 No Content`, `404 Not Found` |

## ▶️ Como executar o projeto

### Pré-requisitos
- [.NET 10 SDK](https://dotnet.microsoft.com/) instalado.

### Passos

```bash
# 1. Clonar o repositório
git clone <URL_DO_REPOSITORIO>
cd MovieApi

# 2. Restaurar as dependências
dotnet restore MovieApi/MovieApi.csproj

# 3. Executar a aplicação
dotnet run --project MovieApi/MovieApi.csproj
```

Por padrão, a aplicação sobe em:
- `http://localhost:5000`
- `https://localhost:5001`

O **Swagger UI** fica disponível na raiz do projeto assim que a aplicação é
iniciada:

```
http://localhost:5000/
```

## 🧪 Exemplos de chamadas (via cURL)

### Listar todos os filmes
```bash
curl -X GET http://localhost:5000/api/v1/filmes
```

### Buscar um filme por Id
```bash
curl -X GET http://localhost:5000/api/v1/filmes/1
```

### Criar um novo filme
```bash
curl -X POST http://localhost:5000/api/v1/filmes \
  -H "Content-Type: application/json" \
  -d '{
        "titulo": "Matrix",
        "diretor": "Lana Wachowski, Lilly Wachowski",
        "genero": "Ficção Científica",
        "anoLancamento": 1999,
        "duracaoMinutos": 136
      }'
```

### Atualizar um filme existente
```bash
curl -X PUT http://localhost:5000/api/v1/filmes/1 \
  -H "Content-Type: application/json" \
  -d '{
        "titulo": "O Poderoso Chefão (Edição Restaurada)",
        "diretor": "Francis Ford Coppola",
        "genero": "Drama",
        "anoLancamento": 1972,
        "duracaoMinutos": 175
      }'
```

### Remover um filme
```bash
curl -X DELETE http://localhost:5000/api/v1/filmes/1
```

## 📷 Prints dos testes no Swagger

```markdown
![GET - Listar filmes] 
![POST - Criar filme]
![PUT - Atualizar filme]
![DELETE - Remover filme]
```
<img width="1279" height="975" alt="image" src="https://github.com/user-attachments/assets/ac1909e3-7983-48ef-bfd7-413441bfd904" />
<img width="1279" height="974" alt="image" src="https://github.com/user-attachments/assets/b6ff4eb9-1135-424a-8f1f-ba62cec02d9f" />
<img width="1279" height="975" alt="image" src="https://github.com/user-attachments/assets/7ebffb86-05fc-4d2f-b0d1-af1e1a6edd71" />
<img width="1279" height="977" alt="image" src="https://github.com/user-attachments/assets/eb5bce6f-a8d4-4e98-baf2-293ef90a50bc" />
<img width="1279" height="983" alt="image" src="https://github.com/user-attachments/assets/c567a722-683d-4153-96f3-3e583a9bea41" />


## 🛠️ Tecnologias utilizadas

- ASP.NET Core (.NET 10)
- Swashbuckle.AspNetCore (Swagger/OpenAPI)
- Injeção de Dependência nativa do ASP.NET Core (`AddSingleton`)
