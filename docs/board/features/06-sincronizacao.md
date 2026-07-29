---
tags: [feature, sincronizacao]
status: todo
camadas: [Application, Infrastructure, Maui, Web]
---

# Feature 6 — Sincronização mobile/web

Como usuário, quero que uma tarefa criada no mobile apareça também na versão web, e vice-versa, sem precisar dar refresh manual.

## Tarefas

- [ ] Garantir que mobile e web consomem a mesma Web API (fonte única de verdade) #application #infrastructure
- [ ] Avaliar necessidade de atualização em tempo real (SignalR / polling) 🔺 #infrastructure
- [ ] Implementar isolamento de dados por usuário (multi-tenant lógico) #infrastructure #seguranca
- [ ] Testes de integração validando sincronismo entre clientes #testes

## Critérios de aceite

- Sincronização ocorre em poucos segundos após a ação, sem exigir refresh manual (quando online)
- Nenhum usuário consegue acessar dados de outro usuário

## Notas de implementação
