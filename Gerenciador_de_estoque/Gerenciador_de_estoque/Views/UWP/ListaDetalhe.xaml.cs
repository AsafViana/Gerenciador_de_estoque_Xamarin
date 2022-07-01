using Firebase.Database;
using Gerenciador_de_estoque.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gerenciador_de_estoque.Views.UWP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaDetalhe : ContentPage
    {
        #region Instancias
        private Itens Item;

        private static FirebaseClient client = new FirebaseClient("https://gerenciamento-de-estoque-9f83d-default-rtdb.firebaseio.com/");

        
        #endregion

        public ListaDetalhe()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            ReceberItem();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        private void AdicionarItem(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AdicionarItem());
        }

        private async void Atualizar(object sender, EventArgs e)
        {
            ReceberItem();
        }

        public async void ReceberItem()
        {
            var dados = (await client
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

            Lista1.ItemsSource = dados;

        }

        private void Lista_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Imagem.Source = null;
            Imagem.WidthRequest = 0;
            Imagem.Margin = new Thickness(0, 0, 0, 0);

            Item = e.Item as Itens;

            Nome.Text = Item.Nome;
            Detalhes.ItemsSource = RecebeDados();
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

        private async void AtualizarInformacao(object sender, ItemTappedEventArgs e)
        {
            Dados AtualDado = (Dados)e.Item;
            string definicao = AtualDado.Definicao + ":";
            string dado = AtualDado.Dado;

            string d = await DisplayPromptAsync("Atualizar", definicao, initialValue:dado);


        }
    }

    public class Dados
    {
        public string Definicao { get; set; }
        public string Dado { get; set; }
    }
}