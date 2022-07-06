using Firebase.Database;
using Gerenciador_de_estoque.Model;
using Gerenciador_de_estoque.Services;
using Gerenciador_de_estoque.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace Gerenciador_de_estoque.Views.UWP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaDetalhe : ContentPage
    {
        #region Instancias
        string[] categoria = new string[]
        {
            "Casaco Moletom",
            "Mascara",
            "Tecido",
            "Camisa",
            "Folha de estampa"
        };

        private ListaDosItensController Controller;

        private Itens Item;

        private string Empresa;

        private static FirebaseClient client = new FirebaseClient("https://gerenciamento-de-estoque-9f83d-default-rtdb.firebaseio.com/");

        private ItemService itemService;
        #endregion

        public ListaDetalhe(string empresa)
        {
            InitializeComponent();
            Empresa = empresa;
            itemService = new ItemService(Empresa);
            Controller = new ListaDosItensController(Empresa);
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

        private async void AtualizarNome(object sender, EventArgs e)
        {
            string d = await DisplayPromptAsync("Atualizar", "Nome", initialValue: Nome.Text);

            Item.Nome = d;

            var _ = itemService.UpdateItem(Item);

            Detalhes.ItemsSource = RecebeDados();
        }

        public async void ReceberItem()
        {
            var dados = (await client
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

            Lista1.ItemsSource = dados;

        }

        private void ViewDetalhes(object sender, ItemTappedEventArgs e)
        {
            Imagem.Source = null;
            Imagem.WidthRequest = 0;
            Imagem.Margin = new Thickness(0, 0, 0, 0);

            Item = (Itens)e.Item;

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
            Controller.AtualizarInformacao((Dados)e.Item, Item, categoria);

            Detalhes.ItemsSource = RecebeDados();
        }

        private async void Deletar(object sender, EventArgs e)
        {
            bool resposta = await DisplayAlert("Deletar", "Deseja deletar?", "Confirmar", "Cancelar");

            if (resposta)
            {
                var parametro = (Itens)((MenuItem)sender).CommandParameter;
                 _ = itemService.DeletarItem(parametro.Codigo);

                ReceberItem();
            }
        }
    }
}