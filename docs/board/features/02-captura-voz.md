---
tags: [feature, captura-voz]
status: todo
camadas: [Application, Infrastructure, Maui, Web]
---

# Feature 2 — Captura de voz

Como usuário, quero gravar um áudio tocando um botão simples e ver que o app está me ouvindo, para registrar meus pensamentos rapidamente.

## Tarefas

- [ ] Definir caso de uso `RecordVoiceEntry` na Application #application
- [ ] Implementar captura de microfone no MAUI (Android/iOS) 🔺 #maui
- [ ] Implementar captura de microfone na Web (MediaRecorder API) #web
- [ ] Criar componente de Botão de Gravação (Microfone) no `BrainDump.Shared.UI` (Blazor) com foco em "One-tap capture" #ui
- [ ] Criar micro-interação (CSS keyframes) de botão pulsando (indicador visual) #ui
- [ ] Permitir cancelar gravação antes de enviar (deslize ou botão secundário leve) #ui
- [ ] Upload do áudio para o backend #infrastructure
- [ ] Testes unitários do caso de uso `RecordVoiceEntry` #testes

## Critérios de aceite

- Gravação inicia em menos de 1s após o toque ("One-tap capture").
- O botão central deve pulsar suavemente durante toda a gravação.
- Usuário pode cancelar a gravação antes de enviar.

## Notas de implementação

> - **UI/UX:** O botão de captura é o elemento primário da tela inicial e deve ser compartilhado entre Web e MAUI via RCL.
> - **Performance:** Compressão do áudio (ex: Opus) localmente antes do upload é altamente recomendada para evitar demoras na rede móvel.
