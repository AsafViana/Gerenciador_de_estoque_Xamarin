
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
        ItemService itemService = new ItemService("Zaf-Tech");

        public ListaDosItens()
        {
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
            var dados = itemService.RecebeItens().Result;
            Lista.ItemsSource = dados;

        }

        private void SwipeItem_Invoked(object sender, EventArgs e)
        {

        }

        private void Lista_Refreshing(object sender, EventArgs e)
        {
            ReceberItem();
            Lista.EndRefresh();
        }

        private void Lista_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var parametro = e.Item as Itens;
            Navigation.PushAsync(new ViewItens(parametro));
        }
    }
}