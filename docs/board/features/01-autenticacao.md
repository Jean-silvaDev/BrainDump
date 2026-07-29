---
tags: [feature, autenticacao]
status: todo
camadas: [Domain, Application, Infrastructure, Maui, Web]
---

# Feature 1 — Autenticação

Como usuário, quero me cadastrar e fazer login com segurança, para ter minha conta protegida e continuar logado entre sessões.

## Tarefas

- [x] Modelar entidade `User` no Domain 🔺 #domain
- [x] Definir interfaces de contrato e DTOs na Application #application
- [x] Implementar cadastro (e-mail + senha) na Infrastructure 🔺 #infrastructure
- [x] Implementar login com emissão de JWT + refresh token 🔺 #infrastructure #seguranca
- [ ] Configurar armazenamento seguro do token no MAUI (`SecureStorage`) 🔺 #maui #seguranca
- [ ] Criar componente de login/cadastro no `BrainDump.Shared.UI` (Blazor) com visual Glassmorphism #ui
- [ ] Incorporar o componente compartilhado nas rotas do Blazor Web e BlazorWebView no MAUI #web #maui
- [x] Testes unitários dos casos de uso de autenticação #testes
- [ ] Testes de integração do endpoint de login #testes

## Critérios de aceite

- Senha nunca trafega nem é armazenada em texto puro
- Token JWT expira em curto prazo, refresh token com rotação
- Usuário permanece logado entre sessões no mobile e na web
- A interface de login apresenta fundo escuro (Dark Mode) e painel modal em *Glassmorphism*.

## Notas de implementação

> - **Autenticação:** O sistema utilizará autenticação própria com JWT + Refresh Token, implementada na Web API com EF Core e ASP.NET Core Identity (ou gerenciamento customizado de hash de senhas).
> - **UI/UX:** Componente de login é 100% compartilhado entre Web e MAUI via RCL.
