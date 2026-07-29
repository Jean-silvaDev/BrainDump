---
tags: [feature, transcricao, parsing]
status: todo
camadas: [Domain, Application, Infrastructure]
---

# Feature 3 — Transcrição e parsing automático

Como usuário, quero que minha fala vire texto e seja separada em itens distintos com categoria, prioridade e prazo sugeridos, para não precisar organizar manualmente.

## Tarefas

- [ ] Modelar entidade `VoiceEntry` e value objects `Category`/`Priority` no Domain 🔺 #domain
- [ ] Definir interface `ITranscriptionService` na Application #application
- [ ] Definir interface `IItemClassifierService` (separação + classificação) na Application #application
- [ ] Integrar serviço de transcrição (STT) na Infrastructure 🔺 #infrastructure
- [ ] Integrar serviço de classificação/parsing (IA) na Infrastructure 🔺 #infrastructure
- [ ] Implementar extração de prazo mencionado na fala ("até sexta" → data) #infrastructure
- [ ] Tratar caso de baixa confiança do serviço de STT (sinalizar pro usuário) #application #infrastructure
- [ ] Testes unitários das regras de classificação no Domain #testes
- [ ] Testes unitários do caso de uso de transcrição+parsing #testes

## Critérios de aceite

- Cada item gerado contém texto, categoria sugerida, prioridade sugerida e prazo (se mencionado)
- Tempo de resposta abaixo de ~5s para áudios de até 1 minuto
- Baixa confiança na transcrição é sinalizada para permitir correção manual

## Notas de implementação
