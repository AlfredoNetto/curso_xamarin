﻿using PodePedir.Model;
using PodePedir.View.Cliente;
using PodePedir.View.Pedido;
using PodePedir.View.Produto;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PodePedir.View
{
    public partial class MainView : MasterDetailPage
    {
        //Redireciona para a página principal da aplicação.
        public List<Menu> menuList { get; set; }

        public MainView()
        {
            InitializeComponent();

            //Remove tela de navegação padrão.
            NavigationPage.SetHasNavigationBar(this, false);

            //Chama o método que carrega os itens no Menu.
            CarregaMenu();
        }


        private void CarregaMenu()
        {
            //Instância a nossa lista que irá armazenar os itens do menu.
            this.menuList = new List<Menu>();

            // Cria as páginas de navegação, definindo Titulo e Icone.
            var clientes = new Menu() { Titulo = "CLIENTES", Icone = "food.png", TargetType = typeof(ClienteListView) };
            var produtos = new Menu() { Titulo = "PRODUTOS", Icone = "waiter.png", TargetType = typeof(ProdutoListView) };
            var pedidos = new Menu() { Titulo = "PEDIDOS", Icone = "delivery.png", TargetType = typeof(PedidoView) };


            // Adiciona o menu na lista de menus.
            menuList.Add(clientes);
            menuList.Add(produtos);
            menuList.Add(pedidos);

            // Carrega o ItemSource da ListView do menu na MainPage.xaml
            menu_navegacao.ItemsSource = menuList;

            // Inicia co menu lateral na Master e no Detail com a pagina inicial.
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(ProdutoListView)));
        }

        //Este Delegate (evento) é desparado sempre quando um item da Listview é clicado.
        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //Limpa variaveis globais ao clicar em outro menu.
            Global.Acao = Model.Enums.Opcoes.Nenhum;
            Global.GCliente = null;
            Global.GProduto = null;


            //a variavel "e" guarda o objeto-item clicado, assim fazemos um cast-convertermos para um objeto do tipo Menu 
            var item = (Menu)e.SelectedItem;

            //Ao converter do tipo menu, podemos resgatar o valor contido na propriedade TargetType que contém a página-View que deve ser aberta ao clicar no item do menu.
            Type page = item.TargetType;

            //Na area de Detail da MasterPage exibe a página referente ao item clicado.
            Detail = new NavigationPage((Page)Activator.CreateInstance(page));

            //não matém o menu aberto, ou seja, após clicar no item recolhe o menu.
            IsPresented = false;
        }


    }
}