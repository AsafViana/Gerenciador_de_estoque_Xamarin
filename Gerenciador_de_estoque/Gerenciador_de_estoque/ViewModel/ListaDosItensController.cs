using Firebase.Database;
using Gerenciador_de_estoque.Model;
using Gerenciador_de_estoque.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Gerenciador_de_estoque.ViewModel
{
    public class ListaDosItensController
    {
        #region Instancias
        private Itens Item { get; set; }

        public FirebaseClient client = new FirebaseClient("https://gerenciamento-de-estoque-9f83d-default-rtdb.firebaseio.com/");

        private string Empresa { get; set; }

        private ItemService itemService;

        public List<Itens> itens { get; set; }
        #endregion

        public ListaDosItensController(string empresa)
        {
            Empresa = empresa;
            itemService = new ItemService(Empresa);
        }


        public async Task<List<Itens>> ReceberItemAsync()
        {
            var dados = itemService.RecebeItens();

            return (await client
                .Child("estoque/" + Empresa)
                .OnceAsync<Itens>())
                .Select(item => new Itens
                {
                    Nome = item.Object.Nome,
                    Quantidade = item.Object.Quantidade,
                    Codigo = item.Object.Codigo,
                    Preco = item.Object.Preco,
                    Categoria = item.Object.Categoria
                }).ToList();
        }

        public List<Dados> RecebeDados(Itens Item)
        {
            return new List<Dados>()
            {
                new Dados
                {
                    Definicao = "Quantidade",
                    Dado = Item.Quantidade.ToString()
                },
                new Dados
                {
                    Definicao = "Código",
                    Dado = Item.Codigo.ToString()
                },
                new Dados
                {
                    Definicao = "Preço",
                    Dado = Item.Preco.ToString()
                },
                new Dados
                {
                    Definicao = "Categoria",
                    Dado = Item.Categoria
                },

            };
        }

        public async void AtualizarInformacao(Dados ItemDado)
        {
            Dados AtualDado = ItemDado;
            string definicao = AtualDado.Definicao + ":";
            string dado = AtualDado.Dado;

            string d = await Application.Current.MainPage.DisplayPromptAsync("Atualizar", definicao, initialValue: dado);

            if (d != null)
            {
                switch (definicao)
                {
                    case "Quantidade:":
                        Item.Quantidade = Convert.ToInt32(d);
                        break;
                    case "Código:":
                        Item.Codigo = Convert.ToInt32(d);
                        break;
                    case "Preço:":
                        Item.Preco = Convert.ToDouble(d);
                        break;
                    case "Categoria:":
                        Item.Categoria = d;
                        break;
                }

                var _ = itemService.UpdateItem(Item);
            }
            
        }
    }
    public class Dados
    {
        public string Definicao { get; set; }
        public string Dado { get; set; }
    }
}


