using Gerenciador_de_estoque.Model;
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
        public ViewItens(Itens item)
        {
            InitializeComponent();
            Item = item;
            text.Text = Item.Nome;

            Lista.ItemsSource = RecebeDados();
        }

        protected override void OnAppearing()
        {
            text.Text = Item.Nome;

            Lista.ItemsSource = RecebeDados();
        }

        private void Lista_Refreshing(object sender, EventArgs e)
        {
            text.Text = Item.Nome;

            Lista.ItemsSource = RecebeDados();
            Lista.EndRefresh();
        }
        private void Lista_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            teste();
        }

        private List<Dados> RecebeDados()
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

        private async Task teste()
        {
            string result = await DisplayPromptAsync("Question 1", "What's your name?");
        }
    }

    public class Dados
    {
        public string Definicao { get; set; }
        public string Dado { get; set; }
    }
}