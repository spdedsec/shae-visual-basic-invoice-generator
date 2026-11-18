# shae-visual-basic-invoice-generator

An invoice generator written in authentic Visual Basic .NET.
Creates an invoice (customer, items with quantity and unit price, tax rate),
calculates subtotal / tax / total, prints a formatted layout to the console,
and saves the invoice to a text file.

Built to the [SHAE Field Manual design system] (see `DESIGN.md`).

[SHAE Field Manual design system]: ../shared/design-system.md

## Requirements

- **Visual Basic .NET compiler** (`vbnc` from Mono) or the **.NET SDK** — not
  installed locally. Install it yourself (see `scripts/install.sh`).

## Install

```bash
./scripts/install.sh
# or, with a custom prefix:
PREFIX=~/.local/bin ./scripts/install.sh
```

Equivalent manual step (Mono):

```bash
vbnc -o InvoiceGenerator.exe src/InvoiceGenerator.vb
install -m 0755 InvoiceGenerator.exe /usr/local/bin/shae-visual-basic-invoice-generator
```

## Usage

```bash
shae-visual-basic-invoice-generator          # prints to console, saves invoice_INV-2026-001.txt
shae-visual-basic-invoice-generator my_inv.txt  # saves to a custom path
```

Example console output:

```
INVOICE INV-2026-001
Date: 2026-11-18
Customer: Acme Corporation

DESCRIPTION                           QTY   UNIT PRICE   LINE TOTAL
------------------------------------------------------------------
Consulting services                    10      $125.00    $1,250.00
Software license                        2      $499.00      $998.00
Annual support plan                     1      $150.00      $150.00
------------------------------------------------------------------
                                                Subtotal:    $2,398.00
                                             Tax (7.0%):     $167.86
                                                  Total:    $2,565.86
Invoice saved to invoice_INV-2026-001.txt
```

## Build & test

```bash
make build   # compile with vbnc or dotnet
make test    # run the generator and show the saved invoice (requires a compiler)
make clean   # remove build artifacts
```

## Design

Documentation and help output follow the SHAE field manual: warm paper ground
`#F2EEE5`, ink `#191712`, oxide signal `#B3341B`, JetBrains Mono for code,
0px radius, 1px borders, no gradients/glass/shadows/pills. See `DESIGN.md`.

## License

MIT. See `LICENSE`.
