# Design e UI/UX

Este documento contém os guias de estilo, decisões de design e benchmarking para o BrainDump.
A interface do projeto prioriza a **agilidade extrema na captura de ideias** através de voz.

## Benchmarks de Design (Inspirações)

Analisamos aplicativos focados em "Voice-to-Text" e captura ágil, como *Braintoss*, *Voicenotes* e *Superlist*:

1. **Braintoss / Voicenotes:**
   - **Fluxo Principal:** Abertura do app direto na tela de captura (Microfone).
   - **UX:** "One-tap capture". O usuário não deve precisar navegar por menus para começar a falar.
   - **Estética:** Minimalista, focada na legibilidade.

2. **Superlist:**
   - **Fluxo Principal:** Transição suave entre captura de voz e lista estruturada.
   - **UX:** Uso de inteligência artificial para já formatar a lista de tarefas visualmente.

## Decisões Visuais

### Cores e Estética
- **Dark Mode First:** O aplicativo deve suportar Dark Mode por padrão, utilizando cores com baixo contraste (ex: fundo `hsl(220, 13%, 18%)` para dar um ar mais sofisticado).
- **Glassmorphism:** Uso sutil de desfoque (blur) em painéis sobrepostos (como a Bottom Sheet de gravação no MAUI e modais na Web).
- **Micro-interações:**
  - Botão de microfone deve pulsar (animação suave) enquanto a gravação estiver ocorrendo.
  - Cards de tarefas devem ter um efeito de "hover" e "press" responsivo.

### Tipografia
- Fonte primária: **Inter** ou **Outfit** (modernas, sem serifa, legíveis em telas pequenas).
- Hierarquia clara: Títulos (`H1`) apenas para as telas principais. Peso de fonte para destacar prioridade (ex: **Alta Prioridade** em `bold`).

## Estrutura Web (Blazor)
- Como optamos pelo **Blazor** e desenvolvimento próprio da Autenticação, os componentes serão desenhados com HTML/CSS (Vanilla ou biblioteca de componentes minimalista), sem Tailwind (a menos que seja solicitado).
- A tela principal na Web também focará num "Input Ágil" centralizado.

## Estrutura Mobile (MAUI)
- O MAUI usará as diretrizes nativas (Material 3 no Android, Cupertino no iOS) mas com um `ResourceDictionary` global (`App.xaml`) forçando nossa paleta Dark Mode e componentes estilizados para não parecer um app corporativo padrão.
