using Newtonsoft.Json;
using Plugin.Geolocator;
using PodePedir.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            //Carrega todos os pedidos na ListView.
            CarregaPedidos();

        }




        async void CarregaPedidos()
        {
            try
            {

                listIndicator.IsVisible = true;
                listIndicator.IsRunning = true;


                HttpClient client = new HttpClient();
                var result = await client.GetAsync("http://unespapp.96.lt/podepedir/ListaPedidos.php");

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
            //Resgata o item selecionado
            var mi = ((MenuItem)sender);
            //Converte o objeto selecionado em um objeto Cliente.
            var item = mi.CommandParameter as Model.Pedido;

            //Dispara uma mensagem com dois botões: Sim = true e Não = false. A variavél opcao pega o botao relecionado.
            var opcao = await DisplayAlert("Excluir Pedido?", null, "Sim", "Não");

            //Se opcao = true, é porque o usuario clicou no botão Sim confirmand a exclusão.
            if (opcao)
            {

                Uri url = new Uri("http://unespapp.96.lt/podepedir/DeletarPedido.php");

                var postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("idpedido", item.id.ToString()));

                var content = new FormUrlEncodedContent(postData);

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        await DisplayAlert("Pedido Excluído!", null, "Ok");
                        CarregaPedidos();
                    }
                }
            }


        }


        //Abre os detalhes do pedido
        public async void OnItemTapped(object o, ItemTappedEventArgs e)
        {
            var item = (o as ListView).SelectedItem as Model.Pedido;

            ClienteDAL cliDal = new ClienteDAL();
            Model.Cliente cli = cliDal.GetByID(item.IdCliente);

            string endereco = cli.Endereco + ", " + cli.Numero + ", " + cli.Bairro + " - " + cli.Cidade + " - " + cli.Estado;



            var locator = CrossGeolocator.Current;
            //Precisao do calculo de localizacao
            locator.DesiredAccuracy = 50;
            var position = await locator.GetPositionAsync();
            string origem = position.Latitude + ", " + position.Longitude;

            switch (Device.OS)
            {
                case TargetPlatform.iOS:
                    Device.OpenUri(new Uri(string.Format("http://maps.apple.com/?saddr={0}&daddr={1}&dirflg=d",
                        WebUtility.UrlEncode(origem), WebUtility.UrlDecode(endereco))));
                    break;
                case TargetPlatform.Android:
                    Device.OpenUri(new Uri(string.Format("google.navigation:q={0}&mode =d", WebUtility.UrlEncode(endereco))));
                    break;
            }
        }



    






    }
}