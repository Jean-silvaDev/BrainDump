---
tags: [feature, captura-voz]
status: done
camadas: [Application, Infrastructure, Maui, Web]
---

# Feature 2 — Captura de voz

Como usuário, quero gravar um áudio tocando um botão simples e ver que o app está me ouvindo, para registrar meus pensamentos rapidamente.

## Tarefas

- [x] Definir caso de uso `RecordVoiceEntry` na Application #application
- [x] Implementar captura de microfone no MAUI (Android/iOS via RCL / BlazorWebView) 🔺 #maui
- [x] Implementar captura de microfone na Web (MediaRecorder API) #web
- [x] Criar componente de Botão de Gravação (Microfone) no `BrainDump.Shared.UI` (Blazor) com foco em "One-tap capture" #ui
- [x] Criar micro-interação (CSS keyframes) de botão pulsando (indicador visual) #ui
- [x] Permitir cancelar gravação antes de enviar (deslize ou botão secundário leve) #ui
- [x] Upload do áudio para o backend #infrastructure
- [x] Testes unitários do caso de uso `RecordVoiceEntry` #testes

## Critérios de aceite

- Gravação inicia em menos de 1s após o toque ("One-tap capture").
- O botão central deve pulsar suavemente durante toda a gravação.
- Usuário pode cancelar a gravação antes de enviar.

## Notas de implementação

> - **UI/UX:** O botão de captura é o elemento primário da tela inicial e é 100% compartilhado entre Web e MAUI via a nova biblioteca `BrainDump.Shared.UI` (RCL).
> - **Performance:** Arquivos de áudio são validados para no máximo 10MB e 5 minutos, e armazenados em disco via `LocalAudioStorageService` (`IAudioStorageService`), salvando o caminho relativo no banco.
> - **Desacoplamento:** O endpoint `POST /api/voice/entries` responde com `202 Accepted`, persistindo a entrada com status `PendingTranscription` para processamento assíncrono na Fase 3.
