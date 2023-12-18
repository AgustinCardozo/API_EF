using QuestPDF.Infrastructure;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace API_EF.Services
{
    public class ProductoPdfService
    {
        private readonly IProductoRepository productoRepository;

        public ProductoPdfService(IProductoRepository productoRepository)
        {
            this.productoRepository = productoRepository;
        }

        private void ComposeHeader(IContainer container, Factura factura)
        {
            //Factura factura = new Factura()
            //{
            //    numeroFactura = new Random().Next(1_000, 10_000),
            //    fechaActual = DateTime.Now,
            //    fechaVencimiento = DateTime.Now + TimeSpan.FromDays(14),

            //    //SellerAddress = GenerateRandomAddress(),
            //    //CustomerAddress = GenerateRandomAddress(),

            //    //Items = items,
            //    //Comments = Placeholders.Paragraph()
            //};
            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column
                        .Item().Text($"Ticket #{factura.numeroFactura}")
                        .FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

                    column.Item().Text(text =>
                    {
                        text.Span("Fecha: ").SemiBold();
                        text.Span($"{factura.fechaActual:d}");
                    });

                    column.Item().Text(text =>
                    {
                        text.Span("Fecha de vencimiento: ").SemiBold();
                        text.Span($"{factura.fechaVencimiento:d}");
                    });
                });

                //row.ConstantItem(175).Image(LogoImage);
            });
        }

        //private void ComposeContent(IContainer container)
        //{
        //    InvoiceModel Model = new();
        //    Model.SellerAddress = new Address()
        //    {
        //        Email = "burak@mail.com",
        //        CompanyName = "BURAK SA",
        //        Street = "Darwin 111",
        //        City = "CABA",
        //        State = "CABA",
        //        Phone = "48213312"

        //    };
        //    Model.CustomerAddress = new Address()
        //    {
        //        Email = "sasha@mail.com",
        //        //CompanyName = "SASHA SA",
        //        //Street = "Darwin 111",
        //        City = "CABA",
        //        State = "CABA",
        //        Phone = "48213312"

        //    };
        //    container.PaddingVertical(40).Column(column =>
        //    {
        //        column.Spacing(20);

        //        column.Item().Row(row =>
        //        {
        //            row.RelativeItem().Component(new AddressComponent("From", Model.SellerAddress));
        //            row.ConstantItem(50);
        //            row.RelativeItem().Component(new AddressComponent("For", Model.CustomerAddress));
        //        });

        //        column.Item().Element(ComposeTable);

        //        //Model.Items.Add(new OrderItem()
        //        //{
        //        //    Name = "Piedras Burakcito",
        //        //    Price = 10m,
        //        //    Quantity = 20
        //        //});
        //        //Model.Items.Add(new OrderItem()
        //        //{
        //        //    Name = "Piedras Elegantes",
        //        //    Price = 15m,
        //        //    Quantity = 20
        //        //});

        //        //var totalPrice = Model.Items.Sum(x => x.Price * x.Quantity);
        //        //column.Item().PaddingRight(5).AlignRight().Text($"Total: ${totalPrice}").SemiBold();

        //        if (!string.IsNullOrWhiteSpace(Model.Comments))
        //            column.Item().PaddingTop(25).Element(ComposeComments);
        //    });
        //}

        private void ComposeTable(IContainer container)
        {
            //InvoiceModel Model = new();
            //Model.Items.Add(new OrderItem()
            //{
            //    Name = "Piedras Burakcito",
            //    Price = 10m,
            //    Quantity = 20
            //});
            //Model.Items.Add(new OrderItem()
            //{
            //    Name = "Piedras Elegantes",
            //    Price = 15m,
            //    Quantity = 20
            //});
            var headerStyle = TextStyle.Default.SemiBold();

            container.Table(async table =>
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

                var productos = await productoRepository.Get();

                foreach (var producto in productos)
                {
                    //var index = productos.IndexOf(producto) + 1;

                    table.Cell().Element(CellStyle).Text($"{producto.ProdCodigo.Trim()}");
                    table.Cell().Element(CellStyle).Text(producto.ProdDetalle.Trim());
                    table.Cell().Element(CellStyle).AlignRight().Text($"${producto.ProdPrecio}");
                    //table.Cell().Element(CellStyle).AlignRight().Text($"{producto.Quantity}");
                    //table.Cell().Element(CellStyle).AlignRight().Text($"${producto.Price * producto.Quantity}");

                    static IContainer CellStyle(IContainer container) => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                }
            });
        }

        private void ComposeComments(IContainer container)
        {
            //InvoiceModel Model = new();
            container.ShowEntire().Background(Colors.Grey.Lighten3).Padding(10).Column(column =>
            {
                column.Spacing(5);
                column.Item().Text("Comments").FontSize(14).SemiBold();
                //column.Item().Text(Model.Comments);
            });
        }
    }
}
