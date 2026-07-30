---
tags: [feature, lista-tarefas]
status: done
camadas: [Domain, Application, Infrastructure, Maui, Web]
---

# Feature 5 — Lista de tarefas

Como usuário, quero ver todas as minhas tarefas organizadas por categoria e prioridade, marcar como concluída, editar ou excluir.

## Tarefas

- [x] Modelar entidade `TaskItem` (com status, prazo, categoria, prioridade) no Domain 🔺 #domain
- [x] Definir caso de uso `GetTasks` (com filtros por categoria, prioridade, status e busca) na Application #application
- [x] Definir caso de uso `ToggleTaskCompletion` (concluir/reabrir) na Application #application
- [x] Definir caso de uso `EditTask` / `DeleteTask` na Application #application
- [x] Criar componente de Listagem de Tarefas em `BrainDump.Shared.UI` (Blazor `TaskList.razor`) com filtros iterativos e `MainDashboard.razor` #ui
- [x] Implementar hierarquia tipográfica (fonte secundária para metadata, pesos variados para prioridades) nos cards #ui
- [x] Adicionar micro-interações: hover/press states responsivos e destaque em tarefas urgentes #ui
- [x] Testes unitários dos casos de uso de listagem/edição/conclusão (`GetTasksUseCaseTests` e `ToggleTaskCompletionUseCaseTests`) #testes

## Critérios de aceite

- Lista atualiza em tempo real após concluir, editar ou excluir uma tarefa.
- Filtros por categoria e prioridade funcionam corretamente.
- Tarefas de alta prioridade possuem destaque visual automático.
- Cards apresentam um sutil efeito visual ao interagir (hover ou click/touch).

## Notas de implementação

> - **Server-side Filtering:** `ITaskItemRepository.GetFilteredAsync` aceita `Category?`, `Priority?`, `IsCompleted?` e `SearchTerm` executando filtros otimizados no banco de dados.
> - **Dashboard Consolidado:** Criado `MainDashboard.razor` no RCL unindo Captura de Voz, Tela de Revisão e Lista de Tarefas em um único container.
> - **Optimistic UI:** O componente `TaskList.razor` atualiza o estado de conclusão instantaneamente no client antes da resposta da API.
> - **Minimal API Endpoints:** Criado o grupo `/api/tasks` (`GET /`, `PATCH /{id}/toggle`, `PUT /{id}`, `DELETE /{id}`).
