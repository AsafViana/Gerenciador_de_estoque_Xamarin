using Firebase.Database;
using Firebase.Database.Query;
using Gerenciador_de_estoque.Model;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciador_de_estoque.Services
{
    class UserService
    {
        public FirebaseClient client;
        string UsuarioCripto = Regras.Criptografia.Criptografando("Usuario");

        public UserService()
        {
            client = new FirebaseClient("https://gerenciamento-de-estoque-9f83d-default-rtdb.firebaseio.com/");
        }
        public async Task<bool> UsuarioExiste(string empresa)
        {
            var usuario = (await client.Child(UsuarioCripto)
                .OnceAsync<Usuario>())
                .Where(u => u.Object.Empresa == empresa)
                .FirstOrDefault();

            return (usuario != null);
        }

        public async Task<bool> RegistrarUsuario(string empresa, string email, string senha)
        {
            if (await UsuarioExiste(empresa) == false)
            {
                await client.Child(UsuarioCripto).PostAsync(new Usuario()
                {
                    Email = Regras.Criptografia.Criptografando(email),
                    Senha = Regras.Criptografia.Criptografando(senha),
                    Empresa = empresa
                });
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> LoginUsuario(string empresa, string senha)
        {
            var usuario = (await client.Child(UsuarioCripto)
                .OnceAsync<Usuario>())
                .Where(u => u.Object.Empresa.ToLower() == empresa)
                .Where(u => Regras.Criptografia.Descriptografando(u.Object.Senha) == senha)
                .FirstOrDefault();
            bool resultado = usuario != null;

            return resultado;
        }
    }
}
