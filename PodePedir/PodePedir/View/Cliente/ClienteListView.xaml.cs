using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PodePedir.Model.Enum;
using PodePedir.DAL;
using PodePedir.Model;

namespace PodePedir.View.Cliente
{
 
    public partial class ClienteListView : ContentPage
    {
        //Instancia que tem os metodos de manipulação do banco de dados.
        private ClienteDAL clienteDAL = new ClienteDAL();

        public ClienteListView()
        {
            InitializeComponent();
            
            if(Global.Acao == Opcoes.Selecionar)
            {
                lblTitulo.Text = "SELECIONAR CLIENTE";
                BtnNovoItem.IsVisible = false;
            }
        }

        protected override void OnAppearing()
        {
            //é sempre disparado quando a tela recebo o foco.
            base.OnAppearing();
            
            lvClientes.ItemsSource = clienteDAL.GetAll();
        }

        private async void BtnNovoItemClick(object sender, EventArgs e)
        {
            //Abre a tela de ClienteDetail por cima da atual.
            await Navigation.PushModalAsync(new ClienteDetailView(null));
        }

        private async void OnAlterarClick(object sender, EventArgs e)
        {
            //Resgata o item selecionado
            var mi = ((MenuItem)sender);
            //Converte o item selecionado em um objeto Cliente
            var cliente = mi.CommandParameter as Model.Cliente;
            //Chama a tela de Detalhes do Cliente.
            await Navigation.PushModalAsync(new ClienteDetailView(cliente));

        }
        private async void OnRemoverClick(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var cliente = mi.CommandParameter as Model.Cliente;

            var opcao = await DisplayAlert("Confirmação!",
                "Deseja excluir o cliente " + cliente.Nome.ToUpper() + "?",
                "Sim", "Não");

            if(opcao)
            {
                clienteDAL.DeleteById((long)cliente.ClienteId);
                lvClientes.ItemsSource = clienteDAL.GetAll();
            }
        }

        private void OnItemTapped(object o, ItemTappedEventArgs e)
        {

        }

    }  
}