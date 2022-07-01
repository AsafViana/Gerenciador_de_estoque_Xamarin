using Gerenciador_de_estoque.Model;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gerenciador_de_estoque.Views.Android
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdicionarItem : ContentPage
    {
        private string categoria;
        private string empresa { get; set; }
        public AdicionarItem()
        {
            InitializeComponent();

            //empresa = Empresa;
        }
        private ViewModel.AdicionarItemController adicionar = new ViewModel.AdicionarItemController();

        private async void Button_Clicked(object sender, EventArgs e)
        {

            var dados = new Itens()
            {
                Nome = Produto.Text,
                Quantidade = Convert.ToInt32(Quantidade.Text),
                Codigo = Convert.ToInt32(Codigo.Text),
                Preco = Convert.ToDouble(Preco.Text),
                Categoria = categoria

            };

            if (Produto.Text == null)
            {
                await DisplayAlert("Dados", "Está faltando informar o nome.", "Ok");
            }
            else if (Quantidade.Text == null)
            {
                await DisplayAlert("Dados", "Está faltando informar aquantide.", "Ok");
            }
            else if (Codigo.Text == null)
            {
                await DisplayAlert("Dados", "Está faltando informar o Código de barra ou identificador.", "Ok");
            }
            else if (Preco.Text == null)
            {
                await DisplayAlert("Dados", "Está faltando informar o Preço.", "Ok");
            }
            else if (categoria == null)
            {
                await DisplayAlert("Dados", "Está faltando informar a Categoria.", "Ok");
            }
            else
            {
                
                bool resultado = adicionar.NovoItemAsync(dados, "Zaf-Tech").Result;
                if (resultado)
                {
                    await DisplayAlert("Confirmado", "Dados enviados com sucesso", "Ok");
                    _ = Navigation.PopModalAsync();
                }
                else
                {
                    await DisplayAlert("Erro", "Algo aconteceu! Verifique a conexão e tente novamente.", "Ok");
                    _ = Navigation.PopModalAsync();
                }
            }
        }

        private void AumentandoPreco(object sender, EventArgs e)
        {
            double valor = Convert.ToDouble(Preco.Text);
            valor += 0.5;
            Preco.Text = valor.ToString();
        }

        private void DiminuiPreco(object sender, EventArgs e)
        {
            if (Preco.Text == "0")
            {
                Preco.Text = null;
            }
            else
            {
                double valor = Convert.ToDouble(Preco.Text);
                valor -= 1;
                if (valor <= 0)
                {
                    Preco.Text = null;
                }
                else
                {
                    Preco.Text = valor.ToString();
                }

            }
        }

        private void AumentaQuantidade(object sender, EventArgs e)
        {
            int valor = Convert.ToInt32(Quantidade.Text);
            valor += 1;
            Quantidade.Text = valor.ToString();
        }

        private void DiminuiQuantidade(object sender, EventArgs e)
        {
            if (Quantidade.Text == "0" || Quantidade.Text == null)
            {
                Quantidade.Text = null;
            }
            else
            {
                int valor = Convert.ToInt32(Quantidade.Text);
                valor -= 1;
                if (Quantidade.Text == "1")
                {
                    Quantidade.Text = null;
                }
                else
                {
                    Quantidade.Text = valor.ToString();
                }
            }
        }

        private void Categoria(object sender, CheckedChangedEventArgs e)
        {
            RadioButton button = sender as RadioButton;

            categoria = (string)button.Content;
        }


    }
}