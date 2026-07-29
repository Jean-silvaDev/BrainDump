---
tags: [feature, notificacoes]
status: todo
camadas: [Application, Infrastructure, Maui]
---

# Feature 7 — Notificações de prazo (should have)

Como usuário, quero receber um lembrete quando uma tarefa com prazo estiver próxima de vencer.

## Tarefas

- [ ] Definir caso de uso `ScheduleDeadlineReminder` na Application #application
- [ ] Integrar serviço de notificações push no MAUI #maui
- [ ] Implementar job/verificação periódica de prazos próximos na Infrastructure #infrastructure
- [ ] Testes unitários da lógica de agendamento de lembretes #testes

## Critérios de aceite

- Usuário recebe notificação antes do vencimento de uma tarefa com prazo definido

## Notas de implementação
