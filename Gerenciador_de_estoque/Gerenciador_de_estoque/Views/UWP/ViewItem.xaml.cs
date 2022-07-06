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
    public partial class ViewItem : ContentPage
    {
        public ViewItem()
        {
            InitializeComponent();

            NavigationPage.SetBackButtonTitle(this, "Back");
        }

        public ViewItem(string queue)
        {
            InitializeComponent();

            if (queue != null)

                NavigationPage.SetBackButtonTitle(this, "Back");
        }
    }
}