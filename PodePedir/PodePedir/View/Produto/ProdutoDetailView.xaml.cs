using PCLStorage;
using Plugin.Media;
using PodePedir.Dal;
using PodePedir.Model.Enums;
using PodePedir.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

namespace PodePedir.View.Produto
{
    public partial class ProdutoDetailView : ContentPage
    {
        private ProdutoViewModel produtoViewModel;
        Model.Produto produto_selecionado = null;

        private Byte[] bytesFoto;
        Opcoes Acao;

        public ProdutoDetailView(Model.Produto produto, Opcoes acao)
        {
            InitializeComponent();

            this.Acao = acao;
            this.produto_selecionado = produto;

            if (produto != null)
            {
                fotoAlbum.Source = ImageSource.FromStream(() => new MemoryStream(produto.Foto));
                bytesFoto = produto.Foto;
            }

            CarregaCategorias();

            produtoViewModel = new ProdutoViewModel(this.produto_selecionado, acao);
            BindingContext = produtoViewModel;
        }

        private void CarregaCategorias()
        {
            List<string> Categorias = new List<string>();
            Categorias.Add("Bebidas");
            Categorias.Add("Porções");
            Categorias.Add("Lanches");
            Categorias.Add("Massas");

            pkCategoria.ItemsSource = Categorias;

        }

        public async void OnAlbumClicked(object sender, EventArgs e)
        {
            try
            {
                //Inicialização do plugin de interação com a câmera e álbum.
                await CrossMedia.Current.Initialize();

                //Verifica se a câmera está disponível para tirar foto.
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await App.Current.MainPage.DisplayAlert("Álbum não suportado", "Não existe permissão para acessar o álbum de fotos", "OK");
                    return;
                }

                /* Método que habilita a câmera, informando a pasta onde a foto deverá ser armazenada, o nome a ser dado ao arquivo e se 
                é ou não para armazenar a foto também no álbum */
                var file = await CrossMedia.Current.PickPhotoAsync();

                // Caso o usuário não tenha tirado a foto, clicando no botão cancelar 
                if (file == null)
                    return;

                // Recupera a foto e a atribui para o controle visual
                var stream = file.GetStream();
                var memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);
                
                fotoAlbum.Source = ImageSource.FromStream(() =>
                {
                    var s = file.GetStream();
                    file.Dispose();
                    return s;
                });

                this.bytesFoto = memoryStream.ToArray();

                PegaFoto();

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Ops", ex.Message, "OK");
            }
        }



    



        private void PegaFoto()
        {
            if (this.produto_selecionado == null)
            {
                this.produto_selecionado = new Model.Produto();
            }

            this.produto_selecionado.Foto = bytesFoto;
        }


        private async void Salvar()
        {
            try
            {

                var dal = new ProdutoDal();
                if (this.produto_selecionado == null)
                {
                    this.produto_selecionado = new Model.Produto();
                }
                this.produto_selecionado.Nome = txtNome.Text.Trim();
                this.produto_selecionado.Descricao = txtDescricao.Text.ToString();
                this.produto_selecionado.Preco = Convert.ToDecimal(txtPreco.Text.Trim());
                this.produto_selecionado.Categoria = pkCategoria.SelectedItem.ToString();
                this.produto_selecionado.Foto = bytesFoto;

                if (this.produto_selecionado.IdProduto == null)
                {
                    dal.Add(this.produto_selecionado);
                    await App.Current.MainPage.DisplayAlert("Inserção de Produto", "Produto inserido com sucesso", "Ok");
                    await Navigation.PopModalAsync();
                }
                else
                {
                    dal.Update(this.produto_selecionado);
                    await App.Current.MainPage.DisplayAlert("Atualização de Produto", "Produto atualizado com sucesso", "Ok");
                    await Navigation.PopModalAsync();
                }

            }
            catch (Exception erro)
            {
                await App.Current.MainPage.DisplayAlert("Ops!", erro.Message, "Ok");

            }
        }

        private void btnSalvar_Clicked(object sender, EventArgs e)
        {
            Salvar();
        }

        private async void OnBotaoCamera(object sender, EventArgs e)
        {
            try
            {

                //Inicialização do plugin de interação com a câmera e álbum.
                await CrossMedia.Current.Initialize();

                //Verifica se a câmera está disponível para tirar foto.
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("Ops!", "A câmera não está disponível.", "OK");
                    return;
                }

                /* Método que habilita a câmera, informando a pasta onde a foto deverá ser armazenada, o nome a ser dado ao arquivo e se 
                é ou não para armazenar a foto também no álbum */
                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = FileSystem.Current.LocalStorage.Name,
                    Name = "tipoitem_" + DateTime.Today.ToString() + "_" + DateTime.Now.ToString() + ".jpg"
                });

                // Caso o usuário não tenha tirado a foto, clicando no botão cancelar 
                if (file == null)
                    return;

                var stream = file.GetStream();
                var memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);

                fotoAlbum.Source = ImageSource.FromStream(() =>
                {
                    var s = file.GetStream();
                    file.Dispose();
                    return s;
                });

                this.bytesFoto = memoryStream.ToArray();

                PegaFoto();

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}