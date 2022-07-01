using Firebase.Database;
using Gerenciador_de_estoque.Model;
using System;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gerenciador_de_estoque.Views.UWP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListarDosItens : ContentPage
    {
        private static FirebaseClient client = new FirebaseClient("https://gerenciamento-de-estoque-9f83d-default-rtdb.firebaseio.com/");
        public ListarDosItens()
        {
            InitializeComponent();
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

        private Command AtualizarCommandAsync(object sender, EventArgs e)
        {
            ReceberItem();
            return null;
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

            Lis.ItemsSource = dados;

        }
    }
}