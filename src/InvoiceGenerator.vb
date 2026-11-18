Imports System
Imports System.IO
Imports System.Text
Imports System.Globalization
Imports System.Collections.Generic

Module InvoiceGenerator

    ' shae-visual-basic-invoice-generator
    ' Authentic Visual Basic .NET invoice generator.
    ' Creates an invoice (customer, items with qty/price, tax rate),
    ' calculates subtotal/tax/total, prints a formatted layout to the
    ' console, and saves the invoice to a text file.
    '
    ' Build:  vbnc -o InvoiceGenerator.exe src/InvoiceGenerator.vb   (Mono)
    '         dotnet build                                          (.NET SDK + project file)
    ' Run:    mono InvoiceGenerator.exe  [output-file.txt]
    '         InvoiceGenerator.exe [output-file.txt]

    Public Structure InvoiceItem
        Public Description As String
        Public Quantity As Integer
        Public UnitPrice As Decimal
        Public ReadOnly Property LineTotal() As Decimal
            Get
                Return Quantity * UnitPrice
            End Get
        End Property
    End Structure

    Public Structure Invoice
        Public InvoiceNumber As String
        Public Customer As String
        Public DateIssued As DateTime
        Public Items As List(Of InvoiceItem)
        Public TaxRate As Decimal
    End Structure

    ''' <summary>Format a decimal as currency.</summary>
    Private Function Money(ByVal value As Decimal) As String
        Return String.Format(CultureInfo.InvariantCulture, "${0:N2}", value)
    End Function

    ''' <summary>Sum of all item line totals.</summary>
    Private Function Subtotal(ByVal inv As Invoice) As Decimal
        Dim total As Decimal = 0D
        For Each it As InvoiceItem In inv.Items
            total += it.LineTotal
        Next
        Return total
    End Function

    ''' <summary>Tax amount for the invoice.</summary>
    Private Function TaxAmount(ByVal inv As Invoice) As Decimal
        Return Subtotal(inv) * inv.TaxRate
    End Function

    ''' <summary>Subtotal plus tax.</summary>
    Private Function GrandTotal(ByVal inv As Invoice) As Decimal
        Return Subtotal(inv) + TaxAmount(inv)
    End Function

    ''' <summary>Render the invoice as a formatted text layout.</summary>
    Private Function Render(ByVal inv As Invoice) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("INVOICE " & inv.InvoiceNumber)
        sb.AppendLine("Date: " & inv.DateIssued.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))
        sb.AppendLine("Customer: " & inv.Customer)
        sb.AppendLine()
        sb.AppendLine(String.Format("{0,-32} {1,5} {2,12} {3,13}", "DESCRIPTION", "QTY", "UNIT PRICE", "LINE TOTAL"))
        sb.AppendLine(New String("-"c, 66))
        For Each it As InvoiceItem In inv.Items
            sb.AppendLine(String.Format("{0,-32} {1,5} {2,12} {3,13}", it.Description, it.Quantity, Money(it.UnitPrice), Money(it.LineTotal)))
        Next
        sb.AppendLine(New String("-"c, 66))
        sb.AppendLine(String.Format("{0,50} {1,13}", "Subtotal:", Money(Subtotal(inv))))
        sb.AppendLine(String.Format("{0,50} {1,13}", String.Format("Tax ({0:P1}):", inv.TaxRate), Money(TaxAmount(inv))))
        sb.AppendLine(String.Format("{0,50} {1,13}", "Total:", Money(GrandTotal(inv))))
        Return sb.ToString()
    End Function

    ''' <summary>Application entry point.</summary>
    Sub Main(ByVal args As String())
        Dim inv As New Invoice With {
            .InvoiceNumber = "INV-2026-001",
            .Customer = "Acme Corporation",
            .DateIssued = DateTime.Now,
            .TaxRate = 0.07D,
            .Items = New List(Of InvoiceItem)()
        }

        inv.Items.Add(New InvoiceItem With {.Description = "Consulting services", .Quantity = 10, .UnitPrice = 125.00D})
        inv.Items.Add(New InvoiceItem With {.Description = "Software license", .Quantity = 2, .UnitPrice = 499.00D})
        inv.Items.Add(New InvoiceItem With {.Description = "Annual support plan", .Quantity = 1, .UnitPrice = 150.00D})

        Dim output As String = Render(inv)
        Console.WriteLine(output)

        Dim path As String = If(args.Length > 0, args(0), "invoice_" & inv.InvoiceNumber & ".txt")
        File.WriteAllText(path, output)
        Console.WriteLine("Invoice saved to " & path)
    End Sub

End Module
