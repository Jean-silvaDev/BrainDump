---
tags: [feature, revisao]
status: done
camadas: [Application, Maui, Web]
---

# Feature 4 — Tela de revisão

Como usuário, quero ver os itens sugeridos antes de confirmar, para corrigir erros de interpretação, ajustar categoria/prioridade/prazo ou descartar itens.

## Tarefas

- [x] Definir caso de uso `GetPendingReviewItems` e `UpdateParsedTaskItem` na Application #application
- [x] Definir caso de uso `ConfirmTasks` e `DiscardParsedTaskItem` na Application #application
- [x] Criar componente de revisão em `BrainDump.Shared.UI` (Blazor) renderizado como um Modal Deslizante (`ReviewBottomSheet.razor`) com Glassmorphism #ui
- [x] Implementar ação "aceitar tudo" (`ConfirmAll`) e atalhos rápidos #ui
- [x] Implementar edição individual de categoria/prioridade/prazo (cards com bordas sutis e fundo `hsl(220, 13%, 18%)`) #ui
- [x] Implementar descarte de item individual #ui
- [x] Testes unitários dos casos de uso de revisão/confirmação (`ConfirmParsedTasksUseCaseTests` e `TaskItemTests`) #testes

## Critérios de aceite

- Nenhuma tarefa é persistida como "confirmada" sem passar pela revisão ou aceite explícito em lote.
- Edições feitas na revisão refletem corretamente na tarefa final.
- A tela de revisão não deve bloquear a captura contínua de áudios (deve ser um painel lateral ou bottom sheet, mantendo o botão de gravar sempre visível).

## Notas de implementação

> - **UI/UX:** Componente em RCL (`ReviewBottomSheet.razor`) garante que a "Bottom Sheet" na Web e no App nativo se comportem da mesma forma.
> - **Atomicidade:** A confirmação em lote aceita todos os rascunhos pendentes ou seleções específicas, convertendo-os em `TaskItem` e marcando rascunhos como `Approved`.
> - **Minimal API Endpoints:** Criado o grupo `/api/review` (`GET /items`, `PUT /items/{id}`, `POST /confirm`, `DELETE /items/{id}`).
