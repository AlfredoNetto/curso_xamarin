using Newtonsoft.Json;
using PodePedir.DAL;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Forms;

namespace PodePedir.View.Pedido
{

    public partial class PedidoListView : ContentPage
    {
        public PedidoListView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //Função que carrega todos os pedidos na ListView.
            CarregaPedidos();
        }

        async void CarregaPedidos()
        {
            try
            {
                //Mostra a animação de Load carregando
                listIndicator.IsVisible = true;
                //Executa a animação
                listIndicator.IsRunning = true;

                HttpClient client = new HttpClient();
                var result = await client.GetAsync(
                    "http://unespapp.96.lt/podepedir/ListaPedidos.php");

                result.EnsureSuccessStatusCode();
                string json = await result.Content.ReadAsStringAsync();
                List<Model.Pedido> res = JsonConvert.DeserializeObject<List<Model.Pedido>>(json);
                List<Model.Pedido> pedidos_registrados = new List<Model.Pedido>();

                ClienteDAL dalcli = new ClienteDAL();

                foreach (var item in res)
                {
                    if (item.id > 0)
                    {
                        Model.Pedido pedido = new Model.Pedido();
                        pedido.Descricao = "Pedido em " + item.Data + " às " + item.Hora;
                        pedido.NomeCliente = dalcli.GetByID(item.IdCliente).Nome;
                        pedido.IdCliente = item.IdCliente;
                        pedido.id = item.id;

                        pedidos_registrados.Add(pedido);
                    }
                }

                lvClientes.ItemsSource = pedidos_registrados;
                listIndicator.IsVisible = false;
                listIndicator.IsRunning = false;

            }
            catch { }
        }

        public async void OnRemoverClick(object sender, EventArgs e)
        {

        }

        public async void OnItemTapped(object o, ItemTappedEventArgs e)
        {

        }
    }
}