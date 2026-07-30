---
tags: [feature, transcricao, parsing]
status: done
camadas: [Domain, Application, Infrastructure]
---

# Feature 3 — Transcrição e parsing automático

Como usuário, quero que minha fala vire texto e seja separada em itens distintos com categoria, prioridade e prazo sugeridos, para não precisar organizar manualmente.

## Tarefas

- [x] Modelar entidade `VoiceEntry` e value objects `Category`/`Priority` no Domain 🔺 #domain
- [x] Definir interface `ITranscriptionService` na Application #application
- [x] Definir interface `IItemClassifierService` (separação + classificação) na Application #application
- [x] Integrar serviço de transcrição (STT com suporte Whisper e Mock) 🔺 #infrastructure
- [x] Integrar serviço de classificação/parsing (IA GPT-4o-mini e Mock) 🔺 #infrastructure
- [x] Implementar extração de prazo mencionado na fala ("até sexta" → data UTC) #infrastructure
- [x] Tratar caso de baixa confiança do serviço de STT (sinalizar pro usuário via `ConfidenceScore`) #application #infrastructure
- [x] Testes unitários das regras de classificação e entidades no Domain #testes
- [x] Testes unitários do caso de uso de transcrição+parsing (`ProcessVoiceEntryTranscriptionUseCase`) #testes

## Critérios de aceite

- Cada item gerado contém texto, categoria sugerida, prioridade sugerida e prazo (se mencionado)
- Tempo de resposta abaixo de ~5s para áudios de até 1 minuto
- Baixa confiança na transcrição é sinalizada para permitir correção manual

## Notas de implementação

> - **Pluggable AI Architecture:** Suporte dual no `appsettings.json` via `AiProvider: "Mock"` ou `"OpenAI"`. Em desenvolvimento/testes, o `Mock` simula STT e extração de datas em PT-BR sem custo de API.
> - **Background Processing:** O upload do áudio enfileira o `VoiceEntryId` em um `Channel<Guid>` (`VoiceProcessingQueue`), consumido de forma assíncrona não-bloqueante por um `VoiceProcessingBackgroundService` (`IHostedService`).
> - **Entidade ParsedTaskItem:** Registra tarefas rascunho com `Category`, `Priority`, `DueDate` calculado a partir da data de referência UTC, e `Status: PendingReview` aguardando a Fase 4 (Tela de Revisão).
