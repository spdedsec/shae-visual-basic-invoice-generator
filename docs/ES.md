# shae-visual-basic-invoice-generator (Español)

Un generador de facturas escrito en Visual Basic .NET auténtico.
Crea una factura (cliente, ítems con cantidad y precio unitario, tasa de
impuesto), calcula subtotal / impuesto / total, imprime un diseño formateado
en consola y guarda la factura en un archivo de texto.

## Requisitos

- **Compilador de Visual Basic .NET** (`vbnc` mediante Mono) o el **.NET SDK**
  — no instalado localmente. Consulte `scripts/install.sh`.

## Instalación

```bash
./scripts/install.sh
```

## Uso

```bash
shae-visual-basic-invoice-generator          # imprime en consola, guarda invoice_INV-2026-001.txt
shae-visual-basic-invoice-generator mi_factura.txt
```

## Compilación y pruebas

```bash
make build
make test
make clean
```

## Diseño

Según el manual de campo SHAE: papel `#F2EEE5`, tinta `#191712`, señal óxido
`#B3341B`, JetBrains Mono para código, sin gradientes/vidrio/sombras.
Véase `DESIGN.md`.

## Licencia

MIT. Véase `LICENSE`.
