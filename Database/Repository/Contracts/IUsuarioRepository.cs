namespace API_EF.Database.Repository.Contracts
{
    public interface IUsuarioRepository
    {
        void AddUsuario(Usuario usuario);
        void Delete(int id);
        List<UsuarioResponse> GetUsuarios();
        Usuario GetUsuarioById(int idEmpleado);
        public void SetPassword(int id, string pass);
    }
}
