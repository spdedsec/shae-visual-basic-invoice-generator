#!/usr/bin/env bash
#
# scripts/install.sh - install shae-visual-basic-invoice-generator
#
# Installs the Visual Basic .NET compiler (vbnc via Mono) or the .NET SDK,
# builds the invoice generator from src/InvoiceGenerator.vb, and installs
# the InvoiceGenerator.exe binary.
#
# Usage:
#   ./scripts/install.sh
#   PREFIX=~/.local/bin ./scripts/install.sh
#
set -euo pipefail

PROJECT="shae-visual-basic-invoice-generator"
BIN="InvoiceGenerator.exe"
PREFIX="${PREFIX:-/usr/local/bin}"
ROOT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"

have_cmd() { command -v "$1" >/dev/null 2>&1; }

# -----------------------------------------------------------------------------
# 1. Ensure a VB.NET compiler is available
# -----------------------------------------------------------------------------
if ! have_cmd vbnc && ! have_cmd dotnet; then
  echo "[install] vbnc/dotnet not found; attempting to install Mono (vbnc) via apt (Debian/Ubuntu)"
  if have_cmd apt-get; then
    sudo apt-get update -y
    sudo apt-get install -y mono-vbnc || true
  elif have_cmd apk; then
    sudo apk add mono-dev || true
  fi
fi

if ! have_cmd vbnc && have_cmd dotnet; then
  echo "[install] dotnet SDK found; add an InvoiceGenerator.vbproj to build with dotnet"
fi

have_cmd vbnc || have_cmd dotnet || { echo "[install] error: no VB.NET compiler available (vbnc or dotnet SDK)" >&2; exit 1; }

if have_cmd vbnc; then echo "[install] vbnc: $(vbnc --version 2>/dev/null || echo unknown)"; fi
if have_cmd dotnet; then echo "[install] dotnet: $(dotnet --version 2>/dev/null || echo unknown)"; fi

# -----------------------------------------------------------------------------
# 2. Build
# -----------------------------------------------------------------------------
echo "[install] building $(PROJECT)"
make -C "$ROOT_DIR" build

# -----------------------------------------------------------------------------
# 3. Install binary
# -----------------------------------------------------------------------------
echo "[install] installing $(BIN) to $(PREFIX)"
install -d "$PREFIX"
if [ -f "$ROOT_DIR/$BIN" ]; then
  install -m 0755 "$ROOT_DIR/$BIN" "$PREFIX/$(PROJECT)"
elif have_cmd dotnet; then
  echo "[install] note: no compiled exe produced (dotnet without project file)"; \
else
  echo "[install] warning: binary not found after build" >&2
fi
echo "[install] done"
echo "[install] usage: $(PREFIX)/$(PROJECT) [output-file.txt]"
