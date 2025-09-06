# Quiz.API 🎯

Backend em **.NET 9 (Minimal API / Controllers)** para gerenciar quizzes, perguntas e tentativas de usuários.  
Faz parte do projeto de modernização do antigo **Quiz** criado originalmente em 2014.

---

## 🚀 Tecnologias
- [.NET 9](https://dotnet.microsoft.com/)
- ASP.NET Core (Minimal API / Controllers)
- [Swagger / OpenAPI](https://swagger.io/) para documentação interativa
- C# 12
- (futuro) PostgreSQL para persistência

---

## 📂 Estrutura do projeto
```
Quiz.API/
 ├── Controllers/       # Controllers da aplicação
 ├── Data/              # Repositórios e acesso a dados (in-memory, futuro DB)
 ├── Models/            # Modelos de domínio
 ├── Properties/        # launchSettings.json (configurações de debug/porta)
 ├── Program.cs         # Ponto de entrada da aplicação
 └── Quiz.API.csproj
```

---

## ⚙️ Configuração e execução

### Pré-requisitos
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) instalado
- Git configurado (com SSH ou HTTPS)

### Clonar o repositório
```bash
git clone git@github.com:edvaldofarias/Quiz.git
cd Quiz/backend/Quiz.API
```

### Restaurar dependências
```bash
dotnet restore
```

### Rodar a API
```bash
dotnet run
```

A aplicação sobe em:
- HTTP → `http://localhost:5000`
- HTTPS → `https://localhost:5001`

---

## 📖 Documentação da API

Se **Swagger** estiver habilitado, acesse:

👉 [https://localhost:5001/swagger](https://localhost:5001/swagger)

Endpoints disponíveis:
- `GET /api/quizzes` → lista quizzes
- `GET /api/quizzes/{id}` → detalhe de um quiz

---

## 🗂 Roadmap inicial
- [x] Estrutura básica em Minimal API
- [x] Integração Swagger
- [ ] Migrar para Controllers
- [ ] CRUD de Quizzes e Questions
- [ ] Implementar Attempts (tentativas/respostas)
- [ ] Persistência em PostgreSQL
- [ ] Autenticação e ranking de usuários

---

## 📜 Licença
Projeto pessoal aberto sob licença MIT.
