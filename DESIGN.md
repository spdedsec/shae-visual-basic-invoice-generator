# DESIGN.md — shae-visual-basic-invoice-generator

Application of the [SHAE Field Manual design system] to this project.

[SHAE Field Manual design system]: ../shared/design-system.md

## Scope

This is a command-line tool, so the design system applies to (a) the project
documentation and (b) the shape of CLI output (the printed invoice layout).

## Colour

- Warm paper `#F2EEE5` — document backgrounds / README ground.
- Ink `#191712` — primary text.
- Oxide signal `#B3341B` — the single signal colour, used for error messages
  and emphasis. One oxide signal per view; never decorative.

## Typography

- Body: Instrument Sans (README, DESIGN).
- Code / commands: JetBrains Mono (inline examples, usage blocks).
- Labels: uppercase short labels only (e.g. `USAGE`, `ERROR`).
- Invoice amounts use tabular numerals (fixed-width numeric columns).

## Layout & density

- 62–68ch prose measure in README/DESIGN/docs.
- 4px spacing base for any presentational rendering.
- 1280px max container for rendered docs.
- The console invoice is a fixed-width layout with aligned columns (hairline
  rule separators, no ornamental noise).

## Motion

- Any interactive docs/transitions: state changes 140–240ms, page transitions
  320ms, transform/opacity/clip-path only.
- Reduced-motion and reduced-transparency fallbacks provided.
- No animation libraries; no centered mobile heroes.

## Accessibility

- WCAG AA contrast for ink on paper and oxide on paper.
- Visible oxide focus rings for any interactive doc elements.
- Complete keyboard paths; 44px touch targets; no horizontal overflow.

## CLI output conventions

- Plain text, fixed-width invoice layout; left-aligned description, right-
  aligned numeric columns.
- No box-drawing ornamentation beyond hairline separators, no gradients, no
  pseudo-glass cards.
- Errors to stderr with an `Error:` prefix.

## Anti-patterns avoided

Gradients, glows, pills, glass cards, shadows, rounded floating panels,
animation libraries for simple output, centered heroes.
