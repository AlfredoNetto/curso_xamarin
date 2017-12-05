using PodePedir.Model.Enums;
using PodePedir.ViewModel;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PodePedir.View.Cliente
{
    public partial class ClienteDetailView : ContentPage
    {
        private ClienteViewModel clienteViewModel;
        private Opcoes acao;

        public ClienteDetailView(Model.Cliente cliente)
        {
            InitializeComponent();

            //Verifica a ação selecionada, e repassa para a váriavel Ação declarada no escopo global da classe.
            if (cliente == null)
            {
                this.acao = Opcoes.Inserir;
            }
            else
            {
                this.acao = Opcoes.Alterar;
            }

            //Este método carrega no controle Picker os estados.
            CarregaEstados();

            //Atribui para a instancia da clase ViewModel o Cliente e Ação selecionada
            clienteViewModel = new ClienteViewModel(cliente, this.acao);
            //Define o contexto do Binding, que é a classe que as alteração serão redirecionadas.
            BindingContext = clienteViewModel;
        }


        private void CarregaEstados()
        {
            List<string> Estados = new List<string>();
            Estados.Add("SP");
            Estados.Add("RJ");
            Estados.Add("BH");
            Estados.Add("MG");

            pkEstados.ItemsSource = Estados;
        }

        private void BtnCancelar_Clicked(object sender, System.EventArgs e)
        {
            /*Quando clicado no Botão Cancelar (definido no XAML do botão, na propriedade "Clicked"), 
            a tela atual é removida da pilha de navegação (pop), voltando para a página anterior. */
            this.Navigation.PopModalAsync();
        }



    }
}