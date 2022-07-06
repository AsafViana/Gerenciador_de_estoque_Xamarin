
using Gerenciador_de_estoque.Model;
using Gerenciador_de_estoque.Services;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gerenciador_de_estoque.Views.Android
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaDosItens : ContentPage
    {
        string Empresa { get; set; }

        ItemService itemService { get; set; }

        public ListaDosItens(string empresa)
        {
            Empresa = empresa;
            itemService = new ItemService(Empresa);

            InitializeComponent();

            ReceberItem();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        protected override void OnAppearing()
        {
            ReceberItem();
        }

        private void AdicionarItem(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AdicionarItem());
        }

        public async void ReceberItem()
        {
            var dados = (await itemService.client
                .Child("estoque/" + itemService.Empresa)
                .OnceAsync<Itens>())
                .Select(item => new Itens
                {
                    Nome = item.Object.Nome,
                    Quantidade = item.Object.Quantidade,
                    Codigo = item.Object.Codigo,
                    Preco = item.Object.Preco,
                    Categoria = item.Object.Categoria
                }).ToList();
            Lista.ItemsSource = dados;
        }

        private async void Deletar(object sender, EventArgs e)
        {
            bool resposta = await DisplayAlert("Deletar", "Deseja deletar?", "Confirmar", "Cancelar");

            if (resposta)
            {
                var parametro = (Itens)((SwipeItem)sender).CommandParameter;
                _ = itemService.DeletarItem(parametro.Codigo);

                ReceberItem();
            }
            
        }

        private void ListaDetalhes(object sender, ItemTappedEventArgs e)
        {
            Itens parametro = (Itens)e.Item;
            Navigation.PushAsync(new ViewItens(parametro, Empresa));
        }
    }
}