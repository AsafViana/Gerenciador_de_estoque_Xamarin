using Gerenciador_de_estoque.Model;
using Gerenciador_de_estoque.Services;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciador_de_estoque.ViewModel
{
    internal class AdicionarItemController
    {
        ItemService fire = new ItemService("Zaf-Tech");
        public async Task<bool> NovoItemAsync(Itens dados, string empresa)
        {
            /*var dado = RecebePerfilAsync("Zaf-Tech").Result;

            dados.ItemId = dado.Contagem;*/

            return fire.EnviarDadosAsync( dados);
        }

        private async Task<Perfil> RecebePerfilAsync(string empresa)
        {
            var dados = (await fire.client
              .Child($"Perfil/{empresa}")
              .OnceAsync<Perfil>())
              .Select(item => new Perfil
              {
                  Contagem = item.Object.Contagem
              });

            return (Perfil)dados;
        }
    }
}
