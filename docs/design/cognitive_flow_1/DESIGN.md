---
name: Cognitive Flow
colors:
  surface: '#13121c'
  surface-dim: '#13121c'
  surface-bright: '#393842'
  surface-container-lowest: '#0e0d16'
  surface-container-low: '#1b1b24'
  surface-container: '#1f1f28'
  surface-container-high: '#2a2933'
  surface-container-highest: '#35343e'
  on-surface: '#e4e1ee'
  on-surface-variant: '#c7c4d8'
  inverse-surface: '#e4e1ee'
  inverse-on-surface: '#302f39'
  outline: '#918fa1'
  outline-variant: '#464555'
  surface-tint: '#c3c0ff'
  primary: '#c3c0ff'
  on-primary: '#1d00a5'
  primary-container: '#635bff'
  on-primary-container: '#fefaff'
  inverse-primary: '#4c42e9'
  secondary: '#54d8e8'
  on-secondary: '#00363c'
  secondary-container: '#02aebe'
  on-secondary-container: '#003b42'
  tertiary: '#ffb68f'
  on-tertiary: '#542100'
  tertiary-container: '#be5400'
  on-tertiary-container: '#fffaf9'
  error: '#ffb4ab'
  on-error: '#690005'
  error-container: '#93000a'
  on-error-container: '#ffdad6'
  primary-fixed: '#e2dfff'
  primary-fixed-dim: '#c3c0ff'
  on-primary-fixed: '#0f0069'
  on-primary-fixed-variant: '#321ed2'
  secondary-fixed: '#90f1ff'
  secondary-fixed-dim: '#54d8e8'
  on-secondary-fixed: '#001f23'
  on-secondary-fixed-variant: '#004f56'
  tertiary-fixed: '#ffdbca'
  tertiary-fixed-dim: '#ffb68f'
  on-tertiary-fixed: '#331100'
  on-tertiary-fixed-variant: '#773200'
  background: '#13121c'
  on-background: '#e4e1ee'
  surface-variant: '#35343e'
typography:
  display:
    fontFamily: Outfit
    fontSize: 40px
    fontWeight: '700'
    lineHeight: 48px
    letterSpacing: -0.02em
  headline-lg:
    fontFamily: Outfit
    fontSize: 32px
    fontWeight: '600'
    lineHeight: 40px
    letterSpacing: -0.01em
  headline-lg-mobile:
    fontFamily: Outfit
    fontSize: 24px
    fontWeight: '600'
    lineHeight: 32px
  headline-md:
    fontFamily: Outfit
    fontSize: 24px
    fontWeight: '500'
    lineHeight: 32px
  body-lg:
    fontFamily: Outfit
    fontSize: 18px
    fontWeight: '400'
    lineHeight: 28px
  body-md:
    fontFamily: Outfit
    fontSize: 16px
    fontWeight: '400'
    lineHeight: 24px
  label-md:
    fontFamily: Outfit
    fontSize: 14px
    fontWeight: '600'
    lineHeight: 20px
    letterSpacing: 0.02em
  label-sm:
    fontFamily: Outfit
    fontSize: 12px
    fontWeight: '500'
    lineHeight: 16px
rounded:
  sm: 0.25rem
  DEFAULT: 0.5rem
  md: 0.75rem
  lg: 1rem
  xl: 1.5rem
  full: 9999px
spacing:
  unit: 4px
  xs: 4px
  sm: 8px
  md: 16px
  lg: 24px
  xl: 40px
  gutter: 16px
  margin-mobile: 20px
  margin-desktop: 64px
---

## Brand & Style

The design system is centered on the concept of "unburdening the mind." It targets high-performance individuals who need to offload thoughts instantly via voice. The aesthetic is a fusion of **Glassmorphism** and **Modern Minimalist Dark Mode**, creating an environment that feels both high-tech and calming. 

The interface should evoke a sense of mental clarity and fluid motion. By using frosted glass surfaces and vibrant indigo accents, the UI mimics a futuristic HUD that is nonetheless soft and approachable. The emotional response is one of focus, where the "noise" of the interface recedes to highlight the user's captured ideas.

## Colors

The palette is optimized for low-light environments and high focus. 

- **Primary Indigo:** Used for active voice states, primary buttons, and critical progress indicators.
- **Secondary Teal:** Used for secondary actions, successful captures, and calm interactions.
- **Neutral Backgrounds:** The base uses a deep navy-gray to reduce eye strain, while the surfaces use a lighter variant to create hierarchical separation.
- **Semantic Accents:** High, Medium, and Low priorities use vibrant, high-saturation tones that pop against the dark background without feeling discordant.

## Typography

This design system utilizes **Outfit** for its geometric clarity and modern character. 

- **Display & Headlines:** Use tighter letter spacing and heavier weights to create a strong visual anchor for voice transcription text.
- **Body Text:** Generous line heights ensure that long "brain dumps" remain readable.
- **Labels:** Uppercase or semi-bold styling is preferred for tags and metadata to differentiate them from the conversational flow of the primary content.

## Layout & Spacing

The layout philosophy follows a **Fluid Grid** model with an emphasis on vertical rhythm. 

- **Mobile:** 4-column grid with 20px side margins. The interaction model is thumb-driven, with the primary "Record" action anchored to the bottom center.
- **Desktop/Tablet:** 12-column grid. Content cards are centered in a max-width container (800px) to mimic the intimacy of a mobile device while utilizing the screen's width for metadata sidebars.
- **Spacing Rhythm:** Based on a 4px scale. Use 24px (lg) for major component grouping and 16px (md) for internal card padding.

## Elevation & Depth

Hierarchy is achieved through **Glassmorphism** and **Tonal Layering** rather than traditional black shadows.

1.  **Level 0 (Base):** Solid `background_color_hex`.
2.  **Level 1 (Cards):** Semi-transparent `surface_color_hex` (80% opacity) with a 1px inner stroke (10% white) to catch the light.
3.  **Level 2 (Modals/Overlays):** Glassmorphic panels with a 16px backdrop blur and a slight indigo-tinted drop shadow (10% opacity) to suggest they are floating closer to the user.
4.  **Voice Active State:** A radial glow using the Primary Indigo behind the active component to simulate energy and focus.

## Shapes

The design system employs a **Generous Roundedness** strategy to appear friendly and organic.

- **Main Cards:** 24px (`rounded-xl`) to create a soft, container-like feel.
- **Buttons & Inputs:** 16px (`rounded-lg`) for a balanced, modern look.
- **Tags & Priority Badges:** Full pill-shape (999px) to distinguish them as interactive, movable objects.
- **Focus Rings:** Use a 2px offset with the secondary teal color to maintain accessibility without cluttering the glass surfaces.

## Components

- **Voice Trigger:** A prominent, circular button at the bottom center. When active, it expands into a wide pill-shaped waveform visualizer.
- **Task Cards:** Use a glassmorphic background. Headlines use `body-lg` in `text_primary`. Metadata (time, priority) sits at the bottom in `label-sm` with `text_secondary`.
- **Priority Chips:** Pill-shaped with a low-opacity background tint of the priority color and a solid text/icon label.
- **Lists:** Items are separated by 12px gaps rather than dividers to maintain the floating "card" aesthetic.
- **Input Fields:** Subdued background with a 1px border that glows Indigo on focus.
- **Persistent Bottom Nav:** A frosted glass bar that spans the bottom of the screen, housing the voice trigger and navigation icons. It uses a heavy backdrop blur (20px) to ensure legibility over scrolling content.
- **Transcription Feed:** Real-time text appearing as the user speaks should use a "faded-in" animation, with the most recent words in `text_primary` and older words slightly dimmed.