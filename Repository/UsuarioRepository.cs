using System.Security.Cryptography;
using System.Text;

namespace API_EF.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IConfiguration configuration;

        public UsuarioRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void AddUsuario(Usuario usuario)
        {
            using var db = new DBContext();
            db.Usuarios.Add(usuario);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            using var db = new DBContext();
            var user = SearchUsuario(db, id);
            db.Usuarios.Remove(user);
            db.SaveChanges();
        }

        public List<Usuario> GetUsuarios()
        {
            using var db = new DBContext();
            return db.Usuarios.ToList();
        }

        public Usuario GetUsuarioById(int idEmpleado)
        {
            using var db = new DBContext();
            return db.Usuarios.Find(idEmpleado);
        }

        public void SetPassword(int id, string pass)
        {
            using var db = new DBContext();
            var user = SearchUsuario(db, id);
            user.Password = Encrypt(pass, configuration.GetValue<string>("API_EF:Hash"));
            db.SaveChanges();
        }

        private static Usuario SearchUsuario(DBContext db, int id)
        {
            var user = db.Usuarios.FirstOrDefault(u => u.Id == id);
            if (user == null)
                throw new Exception($"No se encontro el usuario con id {id}");
            return user;
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
