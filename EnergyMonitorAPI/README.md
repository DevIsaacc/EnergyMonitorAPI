# EnergyMonitorAPI 🌱⚡

API RESTful desenvolvida em .NET Core 8 para monitoramento e otimização do consumo de energia, como parte do projeto ESG da FIAP.

## 📋 Sobre o Projeto

O **EnergyMonitorAPI** é um sistema de **Eficiência Energética** que permite:
- Cadastrar e gerenciar equipamentos de uma empresa
- Registrar leituras de consumo de energia via sensores IoT
- Gerar alertas automáticos quando o consumo ultrapassa o limite
- Emitir relatórios de eficiência energética por período

## 🏗️ Arquitetura

O projeto segue o padrão **MVVM** com as seguintes camadas:
EnergyMonitorAPI/

├── Controllers/     → Endpoints da API
├── Models/          → Entidades do banco de dados
├── ViewModels/      → DTOs de entrada e saída
├── Services/        → Lógica de negócio
├── Repositories/    → Acesso a dados
├── Data/            → DbContext e configurações
└── Migrations/      → Migrações do banco de dados

## 🚀 Tecnologias

- .NET Core 8
- Entity Framework Core
- SQL Server
- JWT Bearer Authentication
- xUnit (testes)
- Docker

## 📡 Endpoints

### Autenticação
| Método | Rota | Descrição |
|--------|------|-----------|
| POST | `/api/auth/register` | Cadastrar usuário |
| POST | `/api/auth/login` | Obter token JWT |

### Equipamentos
| Método | Rota | Auth | Descrição |
|--------|------|------|-----------|
| GET | `/api/equipamentos?pagina=1&tamanhoPagina=10` | ✅ | Listar com paginação |
| POST | `/api/equipamentos` | ✅ | Cadastrar equipamento |
| DELETE | `/api/equipamentos/{id}` | ✅ | Desativar equipamento |

### Leituras IoT
| Método | Rota | Auth | Descrição |
|--------|------|------|-----------|
| POST | `/api/leituras` | ✅ | Registrar leitura de consumo |

### Alertas
| Método | Rota | Auth | Descrição |
|--------|------|------|-----------|
| GET | `/api/alertas?pagina=1` | ✅ | Listar alertas com paginação |
| PATCH | `/api/alertas/{id}/resolver` | ✅ | Marcar alerta como resolvido |

### Relatórios
| Método | Rota | Auth | Descrição |
|--------|------|------|-----------|
| GET | `/api/relatorios/eficiencia?inicio=2025-01-01&fim=2025-12-31` | ✅ | Relatório de eficiência |

## ⚙️ Como Executar

### Pré-requisitos
- .NET 8 SDK
- SQL Server (LocalDB)
- Visual Studio 2026

### Passos

1. Clone o repositório:
```bash
git clone https://github.com/seu-usuario/EnergyMonitorAPI.git
```

2. Configure o banco de dados no `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=EnergyMonitorDB;Trusted_Connection=True;"
}
```

3. Execute as migrações:
```bash
dotnet ef database update
```

4. Rode a aplicação:
```bash
dotnet run
```

### Com Docker
```bash
docker build -t energymonitorapi .
docker run -p 8080:8080 energymonitorapi
```

## 🔐 Autenticação

A API utiliza **JWT Bearer Token**. Para acessar os endpoints protegidos:

1. Faça o registro em `POST /api/auth/register`
2. Copie o token retornado
3. Use no header: `Authorization: Bearer {seu-token}`

## 🧪 Testes

```bash
dotnet test
```

O projeto inclui testes unitários com **xUnit** validando o status code 200 dos endpoints.

## 👥 Integrantes do Grupo

- Nome Isaac Amaral Fonseca — RM: 562348

## 📄 Licença

Projeto acadêmico desenvolvido para a FIAP — 2026.