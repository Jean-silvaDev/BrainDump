---
tags: [feature, design-system, rcl]
status: todo
camadas: [Maui, Web]
---

# Feature 8 — Design System e Componentes Compartilhados (RCL)

Como usuário, quero que as interfaces do aplicativo móvel e da versão web tenham a mesma aparência (Dark Mode, tipografia e espaçamentos), garantindo uma experiência consistente em todas as plataformas sem retrabalho visual.

## Tarefas

- [ ] Criar o projeto `BrainDump.Shared.UI` (Razor Class Library) na solution #web #maui
- [ ] Configurar a paleta de cores (CSS variables) focada no Dark Mode (ex: fundo `hsl(220, 13%, 18%)`) no RCL #ui
- [ ] Configurar tipografia (Inter ou Outfit) e pesos via CSS global no RCL #ui
- [ ] Criar classes CSS utilitárias para Glassmorphism (blur, backgrounds semitransparentes) #ui
- [ ] Configurar o projeto `BrainDump.Maui` para hospedar o `BlazorWebView` referenciando o RCL #maui
- [ ] Garantir que o MAUI não bloqueie animações CSS ou possua conflitos de margem nas safe areas (notch) #maui #ui

## Critérios de aceite

- O projeto MAUI e o projeto Web renderizam exatamente os mesmos botões e estilos (mesmo HTML/CSS compartilhado).
- A interface renderiza corretamente em Dark Mode em ambas as plataformas.

## Notas de implementação

> Adotado o padrão Blazor Hybrid para evitar duplicação entre HTML/CSS e XAML. A navegação visual de fluxo principal e a interface de captura e listagem ocorrerão através do Blazor, injetado dentro das ViewModels do MAUI quando necessário, permitindo usar serviços nativos (microfone) pelas interfaces definidas no Domain/Application.
