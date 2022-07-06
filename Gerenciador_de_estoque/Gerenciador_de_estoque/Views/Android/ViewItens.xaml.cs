using Gerenciador_de_estoque.Model;
using Gerenciador_de_estoque.Services;
using Gerenciador_de_estoque.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gerenciador_de_estoque.Views.Android
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewItens : ContentPage
    {        
        Itens Item { get; set; }

        string[] categoria = new string[]
        {
            "Casaco Moletom",
            "Mascara",
            "Tecido",
            "Camisa",
            "Folha de estampa"
        };

        string Empresa { get; set; }

        List<Dados> dados { get; set; }

        ItemService itemService { get; set; }

        private ListaDosItensController Controller;

        public ViewItens(Itens item, string empresa)
        {
            InitializeComponent();
            Item = item;
            text.Text = Item.Nome;
            Empresa = empresa;
            dados = RecebeDados(item);
            Lista.ItemsSource = dados;

            itemService = new ItemService(Empresa);
        }

        protected override void OnAppearing()
        {
            text.Text = Item.Nome;

            Lista.ItemsSource = dados;
        }

        private void Lista_Refreshing(object sender, EventArgs e)
        {
            text.Text = Item.Nome;

            Lista.ItemsSource = RecebeDados(Item);
            Lista.EndRefresh();
        }

        private void ListaAtualizar(object sender, ItemTappedEventArgs e)
        {
            var _ = e.Item;
            AtualizarInformacao((Dados)e.Item);
        }

        public async void AtualizarInformacao(Dados ItemDado)
        {
            Dados AtualDado = ItemDado;
            string definicao = AtualDado.Definicao + ":";
            string dado = AtualDado.Dado;

            if (definicao == "Categoria:")
            {
                string d = await DisplayActionSheet("Atualizar", "Cancelar", null, categoria);

                if (d != "Cancelar")
                {
                    Item.Categoria = d;
                }
            }
            else
            {
                string d = await DisplayPromptAsync("Atualizar", definicao, initialValue: dado, keyboard:Keyboard.Numeric);

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
                }
            }

            

            var _ = itemService.UpdateItem(Item);

            text.Text = Item.Nome;

            Lista.ItemsSource = RecebeDados(Item);
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

        private async void AtualizarNome(object sender, EventArgs e)
        {

            string d = await DisplayPromptAsync("Atualizar", "Nome:", initialValue: text.Text);

            Item.Nome = d;

            var _ = itemService.UpdateItem(Item);

            text.Text = Item.Nome;
        }
    }
}