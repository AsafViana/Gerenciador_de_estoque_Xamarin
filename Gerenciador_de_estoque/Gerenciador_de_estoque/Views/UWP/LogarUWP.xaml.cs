using System;
using Gerenciador_de_estoque.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gerenciador_de_estoque.Views.UWP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogarUWP : ContentPage
    {
        //LogarController logar =  new LogarController();
        public LogarUWP()
        {
            InitializeComponent();
        }

        private async void NovoUsuario(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListarDosItens());
        }
    }
}