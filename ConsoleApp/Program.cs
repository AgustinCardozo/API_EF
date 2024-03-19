// See https://aka.ms/new-console-template for more information
using ConsoleApp;
using ConsoleApp.Model;
using QuestPDF;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;

Settings.License = LicenseType.Community;
Document.Create(
    container =>
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(50);

            page.Header().Height(100).Background(Colors.Green.Lighten3).Element(ComposeHeader);
            page.Content().Background(Colors.LightGreen.Lighten5).Element(ComposeContent);
            page.Footer().Height(50).Background(Colors.Green.Lighten3).AlignCenter().Text(text =>
            {
                text.CurrentPageNumber();
                text.Span(" / ");
                text.TotalPages();
            }); 

        });
    }).ShowInPreviewer();

void ComposeHeader(IContainer container)
{
    InvoiceModel Model = new InvoiceModel
    {
        InvoiceNumber = new Random().Next(1_000, 10_000),
        IssueDate = DateTime.Now,
        DueDate = DateTime.Now + TimeSpan.FromDays(14),

        //SellerAddress = GenerateRandomAddress(),
        //CustomerAddress = GenerateRandomAddress(),

        //Items = items,
        Comments = Placeholders.Paragraph()
    };
    container.Row(row =>
    {
        row.RelativeItem().Column(column =>
        {
            column
                .Item().Text($"Invoice #{Model.InvoiceNumber}")
                .FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

            column.Item().Text(text =>
            {
                text.Span("Issue date: ").SemiBold();
                text.Span($"{Model.IssueDate:d}");
            });

            column.Item().Text(text =>
            {
                text.Span("Due date: ").SemiBold();
                text.Span($"{Model.DueDate:d}");
            });
        });

        //row.ConstantItem(175).Image(LogoImage);
    });
}

void ComposeContent(IContainer container)
{
    InvoiceModel Model = new();
    Model.SellerAddress = new Address()
    {
        Email = "burak@mail.com",
        CompanyName = "BURAK SA",
        Street = "Darwin 111",
        City = "CABA",
        State = "CABA",
        Phone = "48213312"

    };
    Model.CustomerAddress = new Address()
    {
        Email = "sasha@mail.com",
        //CompanyName = "SASHA SA",
        //Street = "Darwin 111",
        City = "CABA",
        State = "CABA",
        Phone = "48213312"

    };
    container.PaddingVertical(40).Column(column =>
    {
        column.Spacing(20);

        column.Item().Row(row =>
        {
            row.RelativeItem().Component(new AddressComponent("From", Model.SellerAddress));
            row.ConstantItem(50);
            row.RelativeItem().Component(new AddressComponent("For", Model.CustomerAddress));
        });

        column.Item().Element(ComposeTable);

        Model.Items.Add(new OrderItem()
        {
            Name = "Piedras Burakcito",
            Price = 10m,
            Quantity = 20
        });
        Model.Items.Add(new OrderItem()
        {
            Name = "Piedras Elegantes",
            Price = 15m,
            Quantity = 20
        });

        var totalPrice = Model.Items.Sum(x => x.Price * x.Quantity);
        column.Item().PaddingRight(5).AlignRight().Text($"Total: ${totalPrice}").SemiBold();

        if (!string.IsNullOrWhiteSpace(Model.Comments))
            column.Item().PaddingTop(25).Element(ComposeComments);
    });
}

void ComposeTable(IContainer container)
{
    InvoiceModel Model = new();
    Model.Items.Add(new OrderItem()
    {
        Name = "Piedras Burakcito",
        Price = 10m,
        Quantity = 20
    });
    Model.Items.Add(new OrderItem()
    {
        Name = "Piedras Elegantes",
        Price = 15m,
        Quantity = 20
    });
    var headerStyle = TextStyle.Default.SemiBold();

    container.Table(table =>
    {
        table.ColumnsDefinition(columns =>
        {
            columns.ConstantColumn(25);
            columns.RelativeColumn(3);
            columns.RelativeColumn();
            columns.RelativeColumn();
            columns.RelativeColumn();
        });

        table.Header(header =>
        {
            header.Cell().Text("#");
            header.Cell().Text("Product").Style(headerStyle);
            header.Cell().AlignRight().Text("Unit price").Style(headerStyle);
            header.Cell().AlignRight().Text("Quantity").Style(headerStyle);
            header.Cell().AlignRight().Text("Total").Style(headerStyle);

            header.Cell().ColumnSpan(5).PaddingTop(5).BorderBottom(1).BorderColor(Colors.Black);
        });

        foreach (var item in Model.Items)
        {
            var index = Model.Items.IndexOf(item) + 1;

            table.Cell().Element(CellStyle).Text($"{index}");
            table.Cell().Element(CellStyle).Text(item.Name);
            table.Cell().Element(CellStyle).AlignRight().Text($"${item.Price}");
            table.Cell().Element(CellStyle).AlignRight().Text($"{item.Quantity}");
            table.Cell().Element(CellStyle).AlignRight().Text($"${item.Price * item.Quantity}");

            static IContainer CellStyle(IContainer container) => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
        }
    });
}

void ComposeComments(IContainer container)
{
    InvoiceModel Model = new();
    container.ShowEntire().Background(Colors.Grey.Lighten3).Padding(10).Column(column =>
    {
        column.Spacing(5);
        column.Item().Text("Comments").FontSize(14).SemiBold();
        column.Item().Text(Model.Comments);
    });
}