using Gerenciador_de_estoque.Model;
using Gerenciador_de_estoque.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Gerenciador_de_estoque.ViewModel
{
    public class LogarController : BaseViewModel
    {
        FirebaseModel firebase = new FirebaseModel();


        private string _Username;
        private string _Password;
        private string _Email;
        private bool _Result;
        private bool _IsBusy;

        public string Username
        {
            set
            {
                this._Username = value;
                OnPropertyChanged();
            }

            get { return this._Username; }
        }
        public string Password
        {
            set
            {
                this._Password = value;
                OnPropertyChanged();
            }

            get { return this._Password; }
        }
        public string Email
        {
            set
            {
                this._Email = value;
                OnPropertyChanged();
            }

            get { return this._Email; }
        }
        public bool Result
        {
            set
            {
                this._IsBusy = value;
                OnPropertyChanged();
            }
            get
            {
                return this._IsBusy;
            }
        }
        public bool IsBusy
        {
            set
            {
                this._Result = value;
                OnPropertyChanged();
            }
            get
            {
                return this._Result;
            }
        }

        

        public Command LoginCommand { get; set; }
        public Command RegisterCommand { get; set; }
        public LogarController()
        {
            LoginCommand = new Command(async () => await LoginCommandAsync());
            RegisterCommand = new Command(async () => await RegisterCommandAsync());
        }
        private async Task LoginCommandAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var userServer = new UserService();
                Result = await userServer.LoginUsuario(Username, Password);
                if (Result)
                    Preferences.Set("Username", Username);
                if (Device.RuntimePlatform == Device.Android)
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new Views.Android.ListaDosItens("Zaf-Tech"));
                }
                else if (Device.RuntimePlatform == Device.UWP)
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new Views.UWP.ListarDosItens());
                }
                else
                    await Application.Current.MainPage.DisplayAlert("Erro", "Usuario/Senha inválido(s)", "Ok");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }
        private async Task RegisterCommandAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var userServer = new UserService();
                Result = await userServer.RegistrarUsuario(Username, Email, Password);
                if (Result)
                    await Application.Current.MainPage.DisplayAlert("Sucesso", "Usuário Registrado", "Ok");
                else
                    await Application.Current.MainPage.DisplayAlert("Erro", "Falha no registro", "Ok");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
