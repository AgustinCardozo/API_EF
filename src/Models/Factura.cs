namespace API_EF.Models
{
    public class Factura
    {
        public int numeroFactura { get; set; }
        public DateTime fechaActual { get; set; }
        public DateTime fechaVencimiento { get; set; }
    }
}
