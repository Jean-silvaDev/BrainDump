---
tags: [feature, revisao]
status: todo
camadas: [Application, Maui, Web]
---

# Feature 4 — Tela de revisão

Como usuário, quero ver os itens sugeridos antes de confirmar, para corrigir erros de interpretação, ajustar categoria/prioridade/prazo ou descartar itens.

## Tarefas

- [ ] Definir caso de uso `ReviewParsedItems` na Application #application
- [ ] Definir caso de uso `ConfirmTasks` na Application #application
- [ ] Criar componente de revisão em `BrainDump.Shared.UI` (Blazor) renderizado como um Modal Deslizante (Bottom Sheet) com Glassmorphism #ui
- [ ] Implementar ação "aceitar tudo" e atalhos rápidos #ui
- [ ] Implementar edição individual de categoria/prioridade/prazo (cards com bordas sutis e fundo `hsl(220, 13%, 18%)`) #ui
- [ ] Implementar descarte de item individual (swipe to delete) #ui
- [ ] Testes unitários dos casos de uso de revisão/confirmação #testes

## Critérios de aceite

- Nenhuma tarefa é persistida como "confirmada" sem passar pela revisão ou aceite explícito em lote.
- Edições feitas na revisão refletem corretamente na tarefa final.
- A tela de revisão não deve bloquear a captura contínua de áudios (deve ser um painel lateral ou bottom sheet, mantendo o botão de gravar sempre visível).

## Notas de implementação

> - **UI/UX:** Componente em RCL garante que a "Bottom Sheet" na Web e no App nativo se comportem da mesma forma. A UI deve encorajar a agilidade, permitindo deslizar (swipe) para descartar.
