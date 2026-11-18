# shae-visual-basic-invoice-generator (Deutsch)

Ein Rechnungsgenerator in echtem Visual Basic .NET. Erstellt eine Rechnung
(Kunde, Positionen mit Menge und Einzelpreis, Steuersatz), berechnet
Zwischensumme / Steuer / Gesamtbetrag, gibt ein formatiertes Layout auf der
Konsole aus und speichert die Rechnung in einer Textdatei.

## Anforderungen

- **Visual Basic .NET Compiler** (`vbnc` via Mono) oder das **.NET SDK** —
  nicht lokal installiert. Siehe `scripts/install.sh`.

## Installation

```bash
./scripts/install.sh
```

## Verwendung

```bash
shae-visual-basic-invoice-generator          # Ausgabe auf Konsole, speichert invoice_INV-2026-001.txt
shae-visual-basic-invoice-generator meine_rechnung.txt
```

## Build & Test

```bash
make build
make test
make clean
```

## Design

Entsprechend dem SHAE Field Manual: Papier `#F2EEE5`, Tinte `#191712`,
Oxid-Signal `#B3341B`, JetBrains Mono für Code, keine Gradiente/Glas/Schatten.
Siehe `DESIGN.md`.

## Lizenz

MIT. Siehe `LICENSE`.
