using PodePedir.DAL;
using PodePedir.Model;
using PodePedir.Model.Enum;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace PodePedir.View.Produto
{
    public partial class ProdutoListView : ContentPage
    {
        private ProdutoDal produtoDAL = new ProdutoDal();
        public ProdutoListView()
        {
            InitializeComponent();

            if (Global.Acao == Opcoes.Selecionar)
            {
                lblTitulo.Text = "SELECIONAR PRODUTO";
                BtnNovoItem.IsVisible = false;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ListaAgrupada("");
        }


        private async void ListaAgrupada(string texto)
        {
            try
            {
                //lvProdutos.ItemsSource = produtoDAL.GetAll();

                var produtos = produtoDAL.GetAll().Where(i => i.Nome.Contains(texto));

                var agrupamento = produtos.OrderBy(p => p.Categoria).GroupBy(p => p.Categoria.ToString()).Select(p => new AgrupamentoLista<string, Model.Produto>(p)).ToList();
                lvProdutos.ItemsSource = new ObservableCollection<AgrupamentoLista<string, Model.Produto>>(agrupamento);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops!", ex.Message, "Ok");
            }

        }



        private async void BtnNovoItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ProdutoDetailView(null, Opcoes.Inserir));
            ListaAgrupada("");

        }

        private async void OnAlterarClick(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var produto = mi.CommandParameter as Model.Produto;
            await Navigation.PushModalAsync(new ProdutoDetailView(produto, Opcoes.Alterar));
            ListaAgrupada("");
        }

        private async void OnRemoverClick(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var produto = mi.CommandParameter as Model.Produto;
            var resposta = await DisplayAlert("Confirmação de exclusão", "Confirma excluir o cliente " + produto.Nome.ToUpper() + "?", "Sim", "Não");
            if (resposta)
            {
                produtoDAL.DeleteById((long)produto.IdProduto);
                ListaAgrupada("");
            }
        }



        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            lvProdutos.BeginRefresh();
            ListaAgrupada(e.NewTextValue);
            lvProdutos.EndRefresh();
        }





        //Seleciona cliente
        public async void OnItemTapped(object o, ItemTappedEventArgs e)
        {
            if (Global.Acao == Opcoes.Selecionar)
            {
                var item = (o as ListView).SelectedItem as Model.Produto;
                Global.GProduto = item;
                await Navigation.PopAsync();
            }

        }   
    }
}