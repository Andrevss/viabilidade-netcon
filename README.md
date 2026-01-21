# 🚀 Projeto Viabilidade Netcon - API REST

API REST desenvolvida em .NET 8 (C#) com arquitetura DDD (Domain-Driven Design) para verificar a viabilidade de ativos (equipamentos) dentro de um raio específico a partir de coordenadas geográficas.

## 📋 Índice

- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Arquitetura](#arquitetura)
- [Pré-requisitos](#pré-requisitos)
- [Como Executar](#como-executar)
- [Endpoints da API](#endpoints-da-api)
- [Testes](#testes)
- [Estrutura do Projeto](#estrutura-do-projeto)

## 🛠️ Tecnologias Utilizadas

- **.NET 8.0** - Framework principal
- **C#** - Linguagem de programação
- **JWT (JSON Web Token)** - Autenticação
- **xUnit** - Framework de testes
- **Moq** - Mock de dependências nos testes
- **Docker** - Containerização
- **Swagger/OpenAPI** - Documentação da API

## 🏗️ Arquitetura

O projeto segue os princípios de **Domain-Driven Design (DDD)** com separação em camadas:

```
ViabilidadeNetcon/
├── src/
│   ├── ViabilidadeNetcon.Domain/          # Entidades, Value Objects, Interfaces
│   ├── ViabilidadeNetcon.Application/     # Serviços, DTOs, Casos de Uso
│   ├── ViabilidadeNetcon.Infrastructure/  # Repositórios, JWT, Acesso a Dados
│   └── ViabilidadeNetcon.API/             # Controllers, Configuração da API
└── tests/
    └── ViabilidadeNetcon.Tests/           # Testes Unitários
```

### Camadas:

- **Domain**: Coração da aplicação com regras de negócio puras
- **Application**: Orquestra o domínio e implementa casos de uso
- **Infrastructure**: Implementação de detalhes técnicos (JSON, JWT)
- **API**: Interface HTTP, controllers e middlewares

## 📦 Pré-requisitos

### Opção 1: Docker (Recomendado)
- [Docker](https://www.docker.com/get-started) instalado
- [Docker Compose](https://docs.docker.com/compose/install/) instalado

### Opção 2: Desenvolvimento Local
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) instalado

## 🚀 Como Executar

### Opção 1: Usando Docker (Recomendado)

1. **Clone o repositório**
```bash
git clone <seu-repositorio>
cd ViabilidadeNetcon
```

2. **Certifique-se de que o arquivo `dataset.json` está em `src/ViabilidadeNetcon.API/Data/`**

3. **Build e execute com Docker Compose**
```bash
docker-compose up --build
```

4. **A API estará disponível em:**
   - HTTP: http://localhost:5000
   - Swagger UI: http://localhost:5000/swagger

### Opção 2: Desenvolvimento Local

1. **Clone o repositório**
```bash
git clone <seu-repositorio>
cd ViabilidadeNetcon
```

2. **Restaure as dependências**
```bash
dotnet restore
```

3. **Execute a aplicação**
```bash
cd src/ViabilidadeNetcon.API
dotnet run
```

4. **A API estará disponível em:**
   - HTTP: http://localhost:5000
   - HTTPS: http://localhost:5001
   - Swagger UI: http://localhost:5000/swagger

## 📡 Endpoints da API

### 1. Autenticação (Obter Token JWT)

**POST** `/authorization`

Gera um token JWT para autenticação.

**Body:**
```json
{
  "name": "admin",
  "password": "admin"
}
```

**Resposta de Sucesso (200):**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

**Exemplo com cURL:**
```bash
curl -X POST http://localhost:5000/authorization \
  -H "Content-Type: application/json" \
  -d '{"name":"admin","password":"admin"}'
```

### 2. Verificar Viabilidade (Buscar Ativos no Raio)

**GET** `/api/feasibility?latitude={lat}&longitude={lon}&radius={radius}`

Retorna os ativos dentro do raio especificado a partir das coordenadas informadas.

**Parâmetros:**
- `latitude` (obrigatório): Latitude do ponto (-90 a 90, mínimo 5 casas decimais)
- `longitude` (obrigatório): Longitude do ponto (-180 a 180, mínimo 5 casas decimais)
- `radius` (obrigatório): Raio em metros (10 a 1000)

**Headers Obrigatórios:**
```
Authorization: Bearer {seu_token_jwt}
```

**Resposta de Sucesso (200):**
```json
[
  {
    "id": 34,
    "nome": "CTO-RJ-0004",
    "latitude": -23.551000,
    "longitude": -46.632000,
    "radius": 15.56
  },
  {
    "id": 35,
    "nome": "CTO-RJ-0005",
    "latitude": -23.561000,
    "longitude": -46.637000,
    "radius": 16.78
  }
]
```

**Resposta quando não há ativos no raio (200):**
```json
[]
```

**Resposta de Erro de Validação (400):**
```json
{
  "code": "400",
  "reason": "empty field",
  "message": "latitude is mandatory",
  "status": "bad request",
  "timestamp": "2025-02-13T14:25:00Z"
}
```

**Exemplo com cURL:**
```bash
# Primeiro, obtenha o token
TOKEN=$(curl -X POST http://localhost:5000/authorization \
  -H "Content-Type: application/json" \
  -d '{"name":"admin","password":"admin"}' | jq -r '.token')

# Depois, faça a requisição
curl -X GET "http://localhost:5000/api/feasibility?latitude=-23.556456&longitude=-46.635653&radius=100" \
  -H "Authorization: Bearer $TOKEN"
```

## 🧪 Testes

### Executar Testes Unitários

```bash
# Na raiz do projeto
dotnet test

# Com detalhes
dotnet test --verbosity detailed

# Com cobertura de código
dotnet test /p:CollectCoverage=true
```

### Testes Implementados

- ✅ **Domain Tests**
  - Validação de Coordenadas
  - Validação de Raio
  - Cálculo de Distância (Haversine)

- ✅ **Application Tests**
  - Busca de ativos no raio
  - Validações de entrada
  - Cenários de lista vazia

## 📂 Estrutura do Projeto

```
ViabilidadeNetcon/
├── src/
│   ├── ViabilidadeNetcon.Domain/
│   │   ├── Entities/
│   │   │   └── Ativo.cs
│   │   ├── ValueObjects/
│   │   │   ├── Coordenada.cs
│   │   │   └── Raio.cs
│   │   └── Interfaces/
│   │       └── IRepositorioAtivo.cs
│   │
│   ├── ViabilidadeNetcon.Application/
│   │   ├── DTOs/
│   │   │   ├── AtivoResponseDto.cs
│   │   │   ├── ErrorResponseDto.cs
│   │   │   ├── LoginRequestDto.cs
│   │   │   └── LoginResponseDto.cs
│   │   └── Services/
│   │       ├── IViabilidadeService.cs
│   │       └── ViabilidadeService.cs
│   │
│   ├── ViabilidadeNetcon.Infrastructure/
│   │   ├── Repositories/
│   │   │   └── RepositorioAtivoJson.cs
│   │   └── Security/
│   │       ├── IJwtService.cs
│   │       └── JwtService.cs
│   │
│   └── ViabilidadeNetcon.API/
│       ├── Controllers/
│       │   ├── AuthorizationController.cs
│       │   └── FeasibilityController.cs
│       ├── Data/
│       │   └── dataset.json
│       ├── Program.cs
│       └── appsettings.json
│
├── tests/
│   └── ViabilidadeNetcon.Tests/
│       ├── Domain/
│       │   ├── AtivoTests.cs
│       │   ├── CoordenadaTests.cs
│       │   └── RaioTests.cs
│       └── Application/
│           └── ViabilidadeServiceTests.cs
│
├── Dockerfile
├── docker-compose.yml
└── README.md
```

## 🔑 Características Principais

### ✨ Clean Code
- Nomes descritivos e significativos
- Métodos pequenos e focados
- Separação de responsabilidades
- Princípios SOLID

### 🏛️ DDD (Domain-Driven Design)
- Entidades ricas com comportamento
- Value Objects para validação
- Interfaces no Domain
- Inversão de dependência

### 🔒 Segurança
- Autenticação JWT
- Tokens com expiração configurável
- Validação rigorosa de inputs

### 📊 Validações
- Latitude: -90 a 90 (mínimo 5 casas decimais)
- Longitude: -180 a 180 (mínimo 5 casas decimais)
- Raio: 10 a 1000 metros

### 📐 Cálculo de Distância
- Fórmula de **Haversine** para cálculo preciso de distâncias geográficas
- Considera a curvatura da Terra
- Resultado em metros com 2 casas decimais

## 🐛 Troubleshooting

### Erro: "Arquivo dataset