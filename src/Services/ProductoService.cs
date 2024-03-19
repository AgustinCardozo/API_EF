using QuestPDF.Infrastructure;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace API_EF.Services
{
    public interface IProductoService
    {
        byte[] GeneratePDF();
    }

    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository productoRepository;

        public ProductoService(IProductoRepository productoRepository)
        {
            this.productoRepository = productoRepository;
        }

        public byte[] GeneratePDF()
        {
            var document = Document.Create(
                container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(50);

                        page.Header().Height(70).Background(Colors.Green.Lighten3).Element(ComposeHeader);
                        page.Content().Background(Colors.LightGreen.Lighten5).Element(ComposeContent);
                        page.Footer().Height(50).Background(Colors.Green.Lighten3).AlignCenter().Text(text =>
                        {
                            text.CurrentPageNumber();
                            text.Span(" / ");
                            text.TotalPages();
                        });

                    });
                }
            );
            return document.GeneratePdf();
        }

        protected void ComposeHeader(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column
                        .Item().Text($"Ticket #{new Random().Next()}")
                        .FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

                    column.Item().Text(text =>
                    {
                        text.Span("Fecha: ").SemiBold();
                        text.Span($"{DateTime.Now:d}");
                    });

                    column.Item().Text(text =>
                    {
                        text.Span("Fecha de vencimiento: ").SemiBold();
                        text.Span($"{DateTime.Now.AddDays(10):d}");
                    });
                });

                //row.ConstantItem(175).Image(LogoImage);
            });
        }

        private void ComposeContent(IContainer container)
        {
            container.PaddingVertical(20).Column(column =>
            {
                column.Spacing(5);

                //column.Item().Row(row =>
                //{
                //    row.RelativeItem().Component(new AddressComponent("From", Model.SellerAddress));
                //    row.ConstantItem(50);
                //    row.RelativeItem().Component(new AddressComponent("For", Model.CustomerAddress));
                //});

                column.Item().Element(ComposeTable);

                //var totalPrice = Model.Items.Sum(x => x.Price * x.Quantity);
                //column.Item().PaddingRight(5).AlignRight().Text($"Total: ${totalPrice}").SemiBold();

                //if (!string.IsNullOrWhiteSpace(Model.Comments))
                //    column.Item().PaddingTop(25).Element(ComposeComments);
            });
        }

        private void ComposeTable(IContainer container)
        {
            var headerStyle = TextStyle.Default.SemiBold();

            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(80);
                    columns.RelativeColumn(5);
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                table.Header(header =>
                {
                    header.Cell().AlignLeft().Text("Código").Style(headerStyle);
                    header.Cell().AlignLeft().Text("Descripción").Style(headerStyle);
                    header.Cell().AlignLeft().Text("Precio").Style(headerStyle);

                    header.Cell().ColumnSpan(5).PaddingTop(5).BorderBottom(1).BorderColor(Colors.Black);
                });

                var productos = productoRepository.Get();

                foreach (var producto in productos.Take(10))
                {
                    table.Cell().Element(CellStyle).PaddingRight(20).AlignLeft().Text($"{producto.ProdCodigo.Trim()}").FontSize(10);
                    table.Cell().Element(CellStyle).PaddingRight(20).AlignLeft().Text(producto.ProdDetalle.Trim()).FontSize(10);
                    table.Cell().Element(CellStyle).PaddingRight(20).AlignLeft().Text($"${producto.ProdPrecio}").FontSize(10);

                    static IContainer CellStyle(IContainer container) => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                }
            });
        }

        //private void ComposeComments(IContainer container)
        //{
        //    //InvoiceModel Model = new();
        //    container.ShowEntire().Background(Colors.Grey.Lighten3).Padding(10).Column(column =>
        //    {
        //        column.Spacing(5);
        //        column.Item().Text("Comments").FontSize(14).SemiBold();
        //        //column.Item().Text(Model.Comments);
        //    });
        //}
    }
}
