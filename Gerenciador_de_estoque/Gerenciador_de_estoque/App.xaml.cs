using Gerenciador_de_estoque.Views;
using Xamarin.Forms;

namespace Gerenciador_de_estoque
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var pagina = new MasterDetailPage();

            if (Device.RuntimePlatform == Device.Android)
            {
                //MainPage = new NavigationPage(new LogarAndroid());
                MainPage = new NavigationPage(new Views.Android.ListaDosItens("Zaf-Tech"));
            }
            else if (Device.RuntimePlatform == Device.UWP)
            {


                MainPage = new NavigationPage(new Views.UWP.ListaDetalhe("Zaf-Tech"));
            }

            

            //MainPage = new Abas();

            //MainPage = new MenuLateral();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
