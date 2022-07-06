using Gerenciador_de_estoque.ViewModel;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gerenciador_de_estoque.Views.Android
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogarAndroid : ContentPage
    {
        //LogarController logar = new LogarController();
        public LogarAndroid()
        {
            InitializeComponent();
        }

        private async void NovoUsuario(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NovoUsuario());
        }
    }
}