namespace API_EF.Models
{
    public partial class Usuario
    {
        public int Id { get; set; }
        public string Usuario1 { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public string Nombre { get; set; }
        public string Rol { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }

    public class UsuarioResponse
    {
        public int id { get; set; }
        public string usuario { get; set; }
        public string mail { get; set; }
        public string nombre { get; set; }
        public string rol { get; set; }
    }
}
