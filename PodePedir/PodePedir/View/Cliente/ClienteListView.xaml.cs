using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PodePedir.Model.Enum;
using PodePedir.DAL;

namespace PodePedir.View.Cliente
{
 
    public partial class ClienteListView : ContentPage
    {
        //Instancia que tem os metodos de manipulação do banco de dados.
        private ClienteDAL clienteDAL = new ClienteDAL();

        public ClienteListView()
        {
            InitializeComponent();

            Opcoes Acao = Opcoes.Selecionar;

            if(Acao == Opcoes.Selecionar)
            {
                lblTitulo.Text = "SELECIONAR CLIENTE";
            }
        }

        private async void BtnNovoItemClick(object sender, EventArgs e)
        {
            //Abre a tela de ClienteDetail por cima da atual.
            await Navigation.PushModalAsync(new ClienteDetailView(null));
        }

        private void OnAlterarClick(object sender, EventArgs e)
        {

        }
        private void OnRemoverClick(object sender, EventArgs e)
        {

        }

        private void OnItemTapped(object o, ItemTappedEventArgs e)
        {

        }

    }  
}