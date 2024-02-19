using System.Security.Cryptography;
using System.Text;

namespace API_EF.Database.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IConfiguration configuration;
        private readonly ICommonRepository<Usuario> commonRepository;

        public UsuarioRepository(IConfiguration configuration, ICommonRepository<Usuario> commonRepository)
        {
            this.configuration = configuration;
            this.commonRepository = commonRepository;
        }

        public void AddUsuario(Usuario usuario)
        {
            commonRepository.Insert(usuario);
            commonRepository.SaveChange();
        }

        public void Delete(int id)
        {
            var user = SearchUsuario(id);
            commonRepository.Delete(user);
            commonRepository.SaveChange();
        }

        public List<UsuarioResponse> GetUsuarios()
        {
            return commonRepository.GetAll()
                .Select(user => new UsuarioResponse { 
                    id = user.Id,
                    usuario = user.Usuario1,
                    nombre = user.Nombre,
                    mail = user.Mail,
                    rol = user.Rol
                })
                .ToList();
        }

        public Usuario GetUsuarioById(int idEmpleado)
        {
            return commonRepository.FindById(idEmpleado);
        }

        public void SetPassword(int id, string pass)
        {
            var user = SearchUsuario(id);
            user.Password = Encrypt(pass, configuration.GetValue<string>("Hash"));
            commonRepository.SaveChange();
        }

        private Usuario SearchUsuario(/*DBContext db,*/int id)
        {
            //var user = db.Usuarios.FirstOrDefault(u => u.Id == id);
            var user = commonRepository.FindById(id);
            return user ?? throw new Exception($"No se encontro el usuario con id {id}");
        }

        private static string Encrypt(string message, string hash)
        {
            var data = UTF8Encoding.UTF8.GetBytes(message);
            var md5 = MD5.Create();
            var tripleDES = TripleDES.Create();

            tripleDES.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripleDES.Mode = CipherMode.ECB;

            var transform = tripleDES.CreateEncryptor();
            var result = transform.TransformFinalBlock(data, 0, data.Length);

            return Convert.ToBase64String(result);
        }
    }
}
