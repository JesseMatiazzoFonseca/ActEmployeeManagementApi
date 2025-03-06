# ActEmployeeManagementApi

# API CRUD - Gestão de Usuários (ASP.NET 8 + SQL Server)

Esta API foi desenvolvida com **ASP.NET 8** e utiliza **SQL Server** como banco de dados. O banco de dados está hospedado no Docker junto com a API. A comunicação entre o frontend e a API utiliza **JWT** para autenticação.  

A API conta com **Swagger** para documentação e **ILogger** para logs.

---

## 🚀 Funcionalidades

- **Autenticação e Cadastro**
  - Cadastro de usuários com diferentes níveis de permissão.
  - Autenticação via **JWT**.

- **Gerenciamento de Usuários**
  - Usuários comuns podem alterar seus próprios dados.
  - Administradores podem visualizar a lista de usuários e alterar permissões.
  - Administradores podem promover usuários a **Gestores**.
  - Gestores podem editar os dados de outros usuários.

- **Banco de Dados**
  - A API verifica se o banco e as tabelas existem. Se não, são criados automaticamente ao iniciar.

- **Documentação da API**
  - Implementação do **Swagger** para facilitar o teste dos endpoints.

- **Logs**
  - A API utiliza **ILogger** para registrar logs de eventos importantes.

---

## 🛠️ Tecnologias Utilizadas

- **Backend:** .NET 8 (ASP.NET Core)
- **Banco de Dados:** SQL Server
- **ORM:** Dapper
- **Autenticação:** JWT
- **Documentação:** Swagger
- **Logs:** ILogger
- **Docker:** API e banco de dados rodando em containers

---

## 📦 Como Rodar a API

### 🏃 Rodando Sem Docker

1. **Clone o repositório**  
  - git clone -b (branch) https://github.com/JesseMatiazzoFonseca/ActEmployeeManagementFront.git
  - cd seu-repositorio
1.1 **Start a aplicação** 
  - dotnet restore
  - dotnet run
     
### 🐳 Rodando com Docker
 1. **Clone o repositório**
  - git clone -b (branch) https://github.com/JesseMatiazzoFonseca/ActEmployeeManagementFront.git
  - cd seu-repositorio
**Certifique-se de ter o Docker instalado**
  - docker-compose up --build -d

