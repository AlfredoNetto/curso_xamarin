using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PodePedir.View.Produto
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProdutoListView : ContentPage
    {
        public ProdutoListView()
        {
            InitializeComponent();
        }

        private async void BtnNovoItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ProdutoDetailView());
        }
    }
}