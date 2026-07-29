---
name: Cognitive Flow
colors:
  surface: '#fcf8ff'
  surface-dim: '#ddd8e4'
  surface-bright: '#fcf8ff'
  surface-container-lowest: '#ffffff'
  surface-container-low: '#f7f1fe'
  surface-container: '#f1ecf8'
  surface-container-high: '#ebe6f3'
  surface-container-highest: '#e5e0ed'
  on-surface: '#1c1a23'
  on-surface-variant: '#474554'
  inverse-surface: '#312f38'
  inverse-on-surface: '#f4effb'
  outline: '#787586'
  outline-variant: '#c9c4d7'
  surface-tint: '#5b45d6'
  primary: '#5842d3'
  on-primary: '#ffffff'
  primary-container: '#725ded'
  on-primary-container: '#fffbff'
  inverse-primary: '#c8bfff'
  secondary: '#00687a'
  on-secondary: '#ffffff'
  secondary-container: '#6ee1fd'
  on-secondary-container: '#006374'
  tertiary: '#894d00'
  on-tertiary: '#ffffff'
  tertiary-container: '#ad6200'
  on-tertiary-container: '#fffbff'
  error: '#ba1a1a'
  on-error: '#ffffff'
  error-container: '#ffdad6'
  on-error-container: '#93000a'
  primary-fixed: '#e5deff'
  primary-fixed-dim: '#c8bfff'
  on-primary-fixed: '#180064'
  on-primary-fixed-variant: '#4326bd'
  secondary-fixed: '#acedff'
  secondary-fixed-dim: '#62d5f1'
  on-secondary-fixed: '#001f26'
  on-secondary-fixed-variant: '#004e5c'
  tertiary-fixed: '#ffdcc0'
  tertiary-fixed-dim: '#ffb875'
  on-tertiary-fixed: '#2d1600'
  on-tertiary-fixed-variant: '#6b3b00'
  background: '#fcf8ff'
  on-background: '#1c1a23'
  surface-variant: '#e5e0ed'
typography:
  display:
    fontFamily: Outfit
    fontSize: 48px
    fontWeight: '700'
    lineHeight: 56px
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
    fontWeight: '600'
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
    fontWeight: '500'
    lineHeight: 20px
    letterSpacing: 0.01em
  label-sm:
    fontFamily: Outfit
    fontSize: 12px
    fontWeight: '600'
    lineHeight: 16px
    letterSpacing: 0.05em
rounded:
  sm: 0.25rem
  DEFAULT: 0.5rem
  md: 0.75rem
  lg: 1rem
  xl: 1.5rem
  full: 9999px
spacing:
  base_unit: 8px
  container_max_width: 1280px
  gutter: 24px
  margin_mobile: 16px
  margin_desktop: 40px
---

## Brand & Style
The design system focuses on the concept of "Cognitive Flow"—a state of effortless productivity and mental clarity. It is designed for a target audience that values both efficiency and aesthetic tranquility. 

The style is a sophisticated blend of **Modern Minimalism** and **Glassmorphism**. It utilizes a "soft-tech" aesthetic where high-performance functionality meets organic, comfortable visuals. The interface should feel like a premium digital sanctuary: breathable, translucent, and highly organized. We leverage depth and subtle motion to guide the user's focus without inducing cognitive load.

## Colors
The palette is centered around two main "modes" that adapt to the user's environment. 

- **Primary & Secondary:** Indigo serves as the action color (brand primary), while Teal is used for supplemental features and creative highlights.
- **Surface Strategy:** Backgrounds are deep and desaturated in dark mode to reduce eye strain, while light mode remains crisp and airy.
- **Semantic Priority:** Use the high-visibility Red, Amber, and Green sparingly for status indicators and task priority to maintain the calm aesthetic of the design system.

## Typography
This design system utilizes **Outfit** across all roles to maintain a cohesive, modern sans-serif feel. 

- **Hierarchy:** Use bold weights and negative letter-spacing for large display text to create a contemporary "tech-forward" appearance.
- **Readability:** Body text should maintain a 1.5x line-height ratio to ensure the "flow" is maintained during long reading sessions.
- **Labels:** Use uppercase for small labels (`label-sm`) to distinguish meta-information from primary content.

## Layout & Spacing
The layout follows a **Fluid Grid** model with a distinct emphasis on "negative space as a feature."

- **Grid:** A 12-column system for desktop and a 4-column system for mobile.
- **Rhythm:** All spacing (padding, margins) should be multiples of the 8px base unit. 
- **Breathing Room:** Components within glass panels should utilize generous internal padding (minimum 24px) to prevent visual clutter and maintain the "airy" feel of the design system.

## Elevation & Depth
Depth is the cornerstone of this design system, achieved through **Glassmorphism** and **Tonal Layering**.

- **Backdrop Blur:** All surface elements must utilize a 16px backdrop-blur. 
- **Opacity:** Surfaces should be set to 70-85% opacity, allowing background colors to bleed through subtly.
- **Shadows:** Avoid pure black shadows. Use "Color-Tinted Shadows"—low-opacity shadows that take on a hint of the Primary Indigo or the Base background color.
- **Borders:** Use a 1px inner stroke (border) on glass panels with a high-transparency white (in light mode) or light-grey (in dark mode) to simulate a "glass edge."

## Shapes
The shape language is friendly and approachable, favoring "generous" curves over sharp edges.

- **Standard Radius:** 16px for secondary containers and inputs.
- **Large Radius:** 24px for primary cards and main content panels.
- **Buttons:** Use fully rounded (pill-shaped) ends for primary actions to distinguish them from structural layout containers.

## Components
- **Buttons:** Primary buttons use a solid gradient of Indigo to Teal. Secondary buttons are "Ghost" style with a glass background and a subtle border.
- **Cards:** These are the primary "Glassmorphic" containers. They must feature the 16px blur and a subtle 1px border.
- **Inputs:** Fields should have a 16px radius. In focus state, the border glows with the Primary Indigo and a subtle outer shadow.
- **Chips:** Used for tagging and filtering. They should be pill-shaped with a low-opacity version of the accent colors (Indigo/Teal) to signify the category.
- **Lists:** Use "Floating" list items with 8px vertical gaps between them rather than a continuous list with dividers, reinforcing the modular feel.
- **Priority Indicators:** Use small, high-saturation circular "pips" next to task names using the Semantic colors (Red, Amber, Green).