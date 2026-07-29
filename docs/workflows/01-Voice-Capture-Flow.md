# Fluxo de Captura de Voz

Este documento descreve o processo fim-a-fim desde o momento em que o usuário toca no botão de gravar até a tarefa aparecer na tela.

## 1. Interação do Usuário (Client-side)
1. O usuário abre o app (Mobile ou Web).
2. Na tela principal, o usuário toca (ou segura) o botão de microfone.
3. O app solicita permissão de microfone (se for a primeira vez).
4. O app inicia a gravação e exibe um feedback visual (animação de pulsação).
5. O usuário termina de falar e solta/toca novamente para parar.
6. (Opcional) O usuário pode cancelar deslizando o dedo ou tocando no botão X.

## 2. Processamento Local (Client-side)
1. O app pega o buffer de áudio (formato nativo).
2. O áudio é comprimido (ex: AAC, M4A, Opus) para reduzir o tamanho.
3. O app envia uma requisição `POST /api/voice/entries` com o áudio (via Multipart Form Data) incluindo o `Authorization: Bearer <token>`.
4. A UI do app mostra o status "Processando...".

## 3. Recepção e Fila (Backend)
1. A API valida o JWT do usuário e o formato do áudio.
2. O arquivo de áudio é salvo em um Storage (ex: pasta local no volume do Docker ou Azure Blob Storage) gerando um `FileUrl`.
3. A API cria um registro `VoiceEntry` no banco de dados com status `Processing`.
4. A API retorna imediatamente `202 Accepted` para o Client com o ID do `VoiceEntry`.

## 4. Transcrição e Parsing (Backend - Assíncrono)
1. O backend dispara uma tarefa em background (ex: `IHostedService` ou Hangfire) usando o ID do `VoiceEntry`.
2. O áudio é enviado para o serviço de STT (Speech-to-Text - ex: OpenAI Whisper).
3. O STT retorna o texto transcrito completo.
4. O texto é enviado para um LLM (ex: GPT-4o-mini) junto com um prompt de sistema para classificar e extrair itens.
5. O LLM retorna um JSON estruturado com uma lista de tarefas (Categoria, Prioridade, Prazo).

## 5. Persistência e Notificação (Backend)
1. O backend atualiza o `VoiceEntry` para `Completed`.
2. Para cada item identificado pelo LLM, o backend cria um registro de `Task` no banco de dados, associado ao usuário.
3. (Futuro) Se houver SignalR configurado, emite um evento `TasksUpdated` para notificar os clients conectados do usuário.

## 6. Atualização da UI (Client-side)
1. O client, que recebeu o `202 Accepted`, pode adotar duas estratégias:
   - Fazer polling (consultar `GET /api/voice/entries/{id}` a cada 2 segundos) até o status ser `Completed`.
   - Esperar um evento via SignalR.
2. Quando concluído, o client faz um refresh silencioso na lista de tarefas não revisadas (Inbox) e remove o status "Processando...".
