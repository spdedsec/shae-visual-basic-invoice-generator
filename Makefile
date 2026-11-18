# shae-visual-basic-invoice-generator
# Build / test / release for the Visual Basic .NET invoice generator.
#
# Requires the Visual Basic .NET compiler (vbnc from Mono) or the .NET SDK.
# Neither is available locally. See scripts/install.sh.

PROJECT := shae-visual-basic-invoice-generator
SRC     := src/InvoiceGenerator.vb
BIN     := InvoiceGenerator.exe
PREFIX  ?= /usr/local/bin

.PHONY: all build test clean install release help

all: build

build:
	@echo "Building $(PROJECT) from $(SRC)"
	@if command -v vbnc >/dev/null 2>&1; then \
		vbnc -o $(BIN) $(SRC); \
	elif command -v dotnet >/dev/null 2>&1; then \
		echo "dotnet found but no project file; add an InvoiceGenerator.vbproj to build with dotnet"; \
		exit 1; \
	else \
		echo "error: vbnc (Mono) or dotnet SDK is required to build $(PROJECT)"; \
		echo "install it (see scripts/install.sh) or set PATH"; \
		exit 1; \
	fi

test: build
	@echo "--- smoke test ---"
	@./$(BIN) /tmp/shae-invoice-test.txt
	@cat /tmp/shae-invoice-test.txt
	@rm -f /tmp/shae-invoice-test.txt
	@echo "--- smoke test finished ---"

clean:
	rm -f $(BIN) invoice_*.txt *.exe
	rm -rf bin obj

install: build
	install -m 0755 $(BIN) "$(PREFIX)/$(PROJECT)"
	@echo "installed $(PROJECT) to $(PREFIX)/$(PROJECT)"

release: build
	@echo "Release artifacts are produced by .github/workflows/release.yml on tag push."
	@echo "Binary: $(BIN)"

help:
	@echo "Targets: build test clean install release"
