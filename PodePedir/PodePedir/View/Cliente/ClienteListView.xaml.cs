using PodePedir.Dal;
using System;
using Xamarin.Forms;
using PodePedir.Model;
using PodePedir.Model.Enums;

namespace PodePedir.View.Cliente
{ 
    public partial class ClienteListView : ContentPage
    {
        //Instancia da classe DAL, onde temos os metodos de manipulacao dos dados no Banco de Dados.
        private ClienteDAL clienteDAL = new ClienteDAL();
        

        public ClienteListView()
        {
            InitializeComponent();

            
            if (Global.Acao == Opcoes.Selecionar)
            {
                lblTitulo.Text = "SELECIONAR CLIENTE";
                BtnNovoItem.IsVisible = false;
            }
               
        }

        //O evento OnAppearing é sempre disparado quando a tela recebe o foco.
        protected override void OnAppearing()
        {
            base.OnAppearing();

            //Carrega todos os clientes na ListView.
            lvClientes.ItemsSource = clienteDAL.GetAll();
        }

        private async void BtnNovoItemClick(object sender, EventArgs e)
        {
            //Para cadastrar novo cliente, chama ClienteDetailView passando um cliente em branco para a tela.
            await Navigation.PushModalAsync(new ClienteDetailView(null));
        }

        public async void OnAlterarClick(object sender, EventArgs e)
        {
            //Resgata o item selecionado
            var mi = ((MenuItem)sender);
            //Converte o objeto selecionado em um objeto Cliente.
            var cliente = mi.CommandParameter as Model.Cliente;
            /*Chama a View ClienteDetail e passa o cliente cliente para a tela para que os dados sejam exibidos.
             PushModal abre a View por cima da atual, empilhando ela na Navegação.
             */
            await Navigation.PushModalAsync(new ClienteDetailView(cliente));
        }

        public async void OnRemoverClick(object sender, EventArgs e)
        {
            //Resgata o item selecionado
            var mi = ((MenuItem)sender);
            //Converte o objeto selecionado em um objeto Cliente.
            var cliente = mi.CommandParameter as Model.Cliente;

            //Dispara uma mensagem com dois botões: Sim = true e Não = false. A variavél opcao pega o botao relecionado.
            var opcao = await DisplayAlert("Confirmação de exclusão",
                "Confirma excluir o cliente " + cliente.Nome.ToUpper() + "?", "Sim", "Não");

            //Se opcao = true, é porque o usuario clicou no botão Sim confirmand a exclusão.
            if (opcao)
            {
                //Remove cliente do banco pelo metodo criado na clase DAL que remove o cliente pelo ID.
                clienteDAL.DeleteById((long)cliente.ClienteId);
                //Regarrega a ListView para que uma nova lista seja gerada sem o item excluido.
                this.lvClientes.ItemsSource = clienteDAL.GetAll();
            }
        }



        //Seleciona cliente
        public async void OnItemTapped(object o, ItemTappedEventArgs e)
        {
            if(Global.Acao == Opcoes.Selecionar)
            {
                var item = (o as ListView).SelectedItem as Model.Cliente;
				
                Global.GCliente = item;
                await Navigation.PopAsync();
            }         
            
        }



       
    }
}