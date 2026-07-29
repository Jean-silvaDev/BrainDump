# BrainDump — Glossário e Linguagem Ubíqua

Este documento padroniza os termos usados no código, na arquitetura e nas conversas sobre o sistema (Ubiquitous Language do Domain-Driven Design).
Qualquer nova entidade ou conceito central deve ser registrado aqui.

## Entidades e Conceitos Principais

- **User (Usuário):** O dono das informações. Todo conteúdo gerado pertence exclusivamente a um único usuário.
- **VoiceEntry (Entrada de Voz):** O arquivo bruto de áudio gravado pelo usuário e seu respectivo status de processamento (pendente, concluído, erro).
- **Transcription (Transcrição):** O texto extraído de um `VoiceEntry` pelo serviço de STT.
- **Task (Tarefa / Item):** A unidade fundamental de ação gerada a partir da fala. Uma fala pode gerar múltiplas Tasks.
- **Category (Categoria):** Agrupamento semântico de uma Task (ex: Pessoal, Trabalho, Compras).
- **Priority (Prioridade):** Nível de urgência da Task (Baixa, Média, Alta).
- **Deadline (Prazo):** Data/hora opcional extraída da fala em que a Task precisa ser cumprida.
- **Inbox (Caixa de Entrada):** Estado ou lista de Tasks que foram recém-geradas pela IA e ainda não foram revisadas ou confirmadas pelo usuário.
- **STT (Speech-to-Text):** Serviço responsável por converter a voz humana em texto.
- **LLM (Large Language Model):** Serviço responsável por interpretar o texto, identificar intenções e estruturar os dados.

## Padrões de Nomenclatura no Código
- Não use "Item", "Todo" ou "Note" de forma solta. O conceito principal é **Task**.
- A ação de falar gera um **VoiceEntry**, que então sofre **Parsing** para virar **Tasks**.
