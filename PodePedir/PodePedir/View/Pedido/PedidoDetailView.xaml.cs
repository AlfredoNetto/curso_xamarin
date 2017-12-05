using PodePedir.Model;
using PodePedir.Model.Enums;
using PodePedir.View.Cliente;
using PodePedir.View.Produto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

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

            //Se for direferente de nulo é porque tem cliente para ser exibido.
            if (Global.GCliente != null)
            {
                txtCliente.Text = Global.GCliente.Nome;
            }

            if (Global.GProduto != null)
            {
                txtProduto.Text = Global.GProduto.Nome;
            }
        }

        private async void OnSelecionarCliente(object sender, EventArgs args)
        {
            Global.Acao = Opcoes.Selecionar;
            await Navigation.PushAsync(new ClienteListView());
        }


        private async Task<dynamic> CadastrarPedido()
        {
            try
            {
                int?
                    idPedido = await RegistraPedido();

                Uri url = new Uri("http://unespapp.96.lt/podepedir/NovoItemPedido.php");

                var postData = new List<KeyValuePair<string, string>>();

                foreach (var item in this.Lista_pedidos)
                {                   
                    postData.Add(new KeyValuePair<string, string>("IdPedido", idPedido.ToString()));
                    postData.Add(new KeyValuePair<string, string>("IdProduto", item.IdProduto.ToString()));
                    postData.Add(new KeyValuePair<string, string>("Quantidade", item.Quantidade.ToString()));
                    postData.Add(new KeyValuePair<string, string>("ValorTotal", item.ValorTotal.ToString("C")));

                    var content = new FormUrlEncodedContent(postData);

                    var response = await this.client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        //Limpa lista.
                        postData.Clear();
                    }
                }

                //Dando tudo certo exibe mensagem de Pedido Registrado na tela.
                await DisplayAlert("Pedido Registrado!", null, "Ok");

            }
            catch (HttpRequestException e)
            {
                await DisplayAlert("Ops!", "Algo aconteceu de errado, detalhes: " + e.Message, "Ok");
            }

            return "";

        }

        private async void OnSelecionarProduto(object sender, EventArgs args)
        {
            Global.Acao = Opcoes.Selecionar;
            await Navigation.PushAsync(new ProdutoListView());
        }

        private async void OnAddProduto(object sender, EventArgs args)
        {

            Model.ItemPedido novo_item_pedido = new Model.ItemPedido();
            novo_item_pedido.Foto = Global.GProduto.Foto;
            novo_item_pedido.Nome = Global.GProduto.Nome;
            novo_item_pedido.ValorTotal = Global.GProduto.Preco * Convert.ToDecimal(txtQuantidade.Text);
            novo_item_pedido.Descricao = "Pedido em " + DateTime.Today.ToString("dd/MM/yyyy") + " às " + DateTime.Now.ToString("HH:mm");
            novo_item_pedido.IdProduto = Global.GProduto.IdProduto;
            novo_item_pedido.Quantidade = Convert.ToInt32(txtQuantidade.Text);

            this.Lista_pedidos.Add(novo_item_pedido);

            AtualizaLista();

        }

        private void AtualizaLista()
        {
            lvProdutos.ItemsSource = null;
            lvProdutos.ItemsSource = this.Lista_pedidos;

            txtValor.Text = "TOTAL: R$ " + this.Lista_pedidos.Sum(x => x.ValorTotal).ToString("C");

        }

        void OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            txtQuantidade.Text = e.NewValue.ToString();
        }


        async void OnDelete(object sender, EventArgs args)
        {
            var mi = ((MenuItem)sender);
            var answer = await DisplayAlert("Removerv", null, "Sim", "Não");

            if (answer)
            {
                this.Lista_pedidos.Remove((Model.ItemPedido)mi.CommandParameter);
            }

            AtualizaLista();
        }

        private async void btnSalvar_Clicked(object sender, EventArgs e)
        {
            var opcao = await DisplayAlert("Pedido", "Fechar o Pedido?", "Sim", "Não");

            if (opcao)
            {
                await CadastrarPedido();
            }
        }


        public async Task<int?> RegistraPedido()
        {
            try
            {

                Uri url = new Uri("http://unespapp.96.lt/podepedir/NovoPedido.php");    
            
                var postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("Data", DateTime.Today.ToString("dd/MM/yyyy")));
                postData.Add(new KeyValuePair<string, string>("Hora", DateTime.Now.ToString("HH:mm")));
                postData.Add(new KeyValuePair<string, string>("IdCliente", Global.GCliente.ClienteId.ToString()));
                postData.Add(new KeyValuePair<string, string>("ValorTotalPedido", this.Lista_pedidos.Sum(x => x.ValorTotal).ToString("C")));

                var content = new FormUrlEncodedContent(postData);

                var response = await this.client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    //pega o conteudo retornado pelo post que deve ser o id gerado para o pedido.
                    string retorno = await response.Content.ReadAsStringAsync();

                    //Retorna o ID gerado para o pedido.
                    return Convert.ToInt32(retorno);
                }
                
            }
            catch (HttpRequestException e)
            {
                await DisplayAlert("Ops!", e.Message, "Ok");
            }

            return null;
        }


    }
}