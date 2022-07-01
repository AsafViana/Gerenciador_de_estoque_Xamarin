using Firebase.Database;
using Gerenciador_de_estoque.Model;
using Gerenciador_de_estoque.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciador_de_estoque.ViewModel
{
    public class ListaDosItensController
    { 

        private string Empresa { get; set; }
        public List<Itens> itens { get; set; }
        UserService fire = new UserService();

        public ListaDosItensController(string empresa)
        {
            Empresa = empresa; 
        }

        
        public async Task<List<Itens>> ReceberItem()
        {
            var dados = (await fire.client
                .Child("estoque/Zaf-Tech")
                .OnceAsync<Itens>())
                .Select(item => new Itens
                {
                    Nome = item.Object.Nome,
                    Quantidade = item.Object.Quantidade,
                    Categoria = item.Object.Categoria,
                    Codigo = item.Object.Codigo,
                    Preco = item.Object.Preco,
                }).ToList();

            return dados;

        }
        
    }
}
