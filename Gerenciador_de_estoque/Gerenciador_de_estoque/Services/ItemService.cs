using Firebase.Database;
using Firebase.Database.Query;
using Gerenciador_de_estoque.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciador_de_estoque.Services
{
    public class ItemService
    {
        private string Empresa { get; set; }

        public FirebaseClient client = new FirebaseClient("https://gerenciamento-de-estoque-9f83d-default-rtdb.firebaseio.com/");

        public ItemService(string empresa)
        {
            Empresa = empresa;
        }

        public async Task AdicionarItem(string nome, int quantidade, int codigo, double preco, string categoria)
        {
            await client.Child("estoque/"+Empresa)
                .PostAsync(new Itens()
                {
                    Nome = nome,
                    Quantidade = quantidade,
                    Codigo = codigo,
                    Preco = preco,
                    Categoria = categoria
                });
        }

        public async Task<List<Itens>> RecebeItens()
        {
            return (await client
                .Child("estoque")
                .OnceAsync<Itens>()).Select(item => new Itens
                {
                    Nome = item.Object.Nome,
                    Quantidade = item.Object.Quantidade,
                    Codigo = item.Object.Codigo,
                    Preco = item.Object.Preco,
                    Categoria = item.Object.Categoria
                }).ToList();
        }

        public async Task<Itens> RecebeItem(int itemId)
        {
            try
            {
                var item = (await client
                        .Child("estoque/"+Empresa)
                        .OnceAsync<Itens>())
                        .Where(a => a.Object.Codigo == itemId).FirstOrDefault();

                return (Itens)await client.Child("estoque/"+Empresa)
                    .Child(item.Key).OnceAsync<Itens>();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task DeletarItem(int codigo)
        {
            try
            {
                var toDeleteItem = (await client
                    .Child("estoque/" + Empresa)
                    .OnceAsync<Itens>())
                    .Where(a => a.Object.Codigo == codigo).FirstOrDefault();
                await client
                    .Child("estoque/" + Empresa)
                    .Child(toDeleteItem.Key)
                    .DeleteAsync();
            }
            catch (System.Exception)
            {
                throw;
            }

        }

        public bool EnviarDadosAsync(object Dados)
        {
            client.Child("estoque/" + Empresa).PostAsync(Dados);
            return true;

        }

        public async Task UpdateItem(string nome, int quantidade, int codigo, double preco, string categoria)
        {
            try
            {
                var toUpdateItem = (await client
                    .Child("estoque/" + Empresa)
                    .OnceAsync<Itens>())
                    .Where(a => a.Object.Codigo == codigo).FirstOrDefault();
                await client
                    .Child("estoque/" + Empresa)
                    .Child(toUpdateItem.Key)
                    .PutAsync(new Itens()
                    {
                        Nome = nome,
                        Categoria = categoria,
                        Codigo = codigo,
                        Preco = preco,
                        Quantidade = quantidade,
                    });
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
