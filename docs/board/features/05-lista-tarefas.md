---
tags: [feature, lista-tarefas]
status: todo
camadas: [Domain, Application, Infrastructure, Maui, Web]
---

# Feature 5 — Lista de tarefas

Como usuário, quero ver todas as minhas tarefas organizadas por categoria e prioridade, marcar como concluída, editar ou excluir.

## Tarefas

- [ ] Modelar entidade `Task` (com status, prazo, categoria, prioridade) no Domain 🔺 #domain
- [ ] Definir caso de uso `ListTasks` (com filtros) na Application #application
- [ ] Definir caso de uso `CompleteTask` na Application #application
- [ ] Definir caso de uso `EditTask` / `DeleteTask` na Application #application
- [ ] Criar componente de Listagem de Tarefas em `BrainDump.Shared.UI` (Blazor) com filtros iterativos #ui
- [ ] Implementar hierarquia tipográfica (fonte secundária para metadata, pesos variados para prioridades) nos cards #ui
- [ ] Adicionar micro-interações: hover/press states responsivos nos cards #ui
- [ ] Testes unitários dos casos de uso de listagem/edição/conclusão #testes

## Critérios de aceite

- Lista atualiza em tempo real após concluir, editar ou excluir uma tarefa.
- Filtros por categoria e prioridade funcionam corretamente.
- Tarefas de alta prioridade possuem destaque visual automático.
- Cards devem apresentar um sutil efeito visual ao interagir (hover ou click/touch).

## Notas de implementação

> - **UI/UX:** Componente em RCL para garantir que os estilos CSS (hover, active, focus) funcionem da mesma maneira na Web e no toque no Mobile.
