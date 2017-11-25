using System;
using System.Collections.Generic;
using Xamarin.Forms;
using PodePedir.Model.Enum;
using PodePedir.ViewModel;

namespace PodePedir.View.Cliente
{
   
    public partial class ClienteDetailView : ContentPage
    {
        private ClienteViewModel clienteViewModel;
        private Opcoes acao;

        public ClienteDetailView(Model.Cliente cliente)
        {
            InitializeComponent();

            if(cliente == null)
            {
                this.acao = Opcoes.Inserir;
            }
            else
            {
                this.acao = Opcoes.Alterar;
            }

            //Carre no controle Picker os estados para selecionar.
            CarregarEstados();

            clienteViewModel = new ClienteViewModel(cliente, this.acao);
            //Define o contexto do Binding que é a classe que as alterações acontecem.
            BindingContext = clienteViewModel;

        }

        private void CarregarEstados()
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
            this.Navigation.PopModalAsync();
        }
    }
}