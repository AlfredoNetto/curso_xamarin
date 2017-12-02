using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using PodePedir.Model;

namespace PodePedir.View.Pedido
{
    
    public partial class PedidoDetailView : ContentPage
    {
        List<Model.ItemPedido> Lista_pedidos = new List<Model.ItemPedido>();
        private HttpClient client = new HttpClient();

        public PedidoDetailView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //Se for diferente de nulo é porque tem cliente para ser exibido
            if(Global.GCliente != null)
            {
                txtCliente.Text = Global.GCliente.Nome;
            }

            if(Global.GProduto != null)
            {
                txtProduto.Text = Global.GProduto.Nome;
            }
        }
    }
}