using PodePedir.DAL;
using PodePedir.Model;
using PodePedir.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PodePedir.ViewModel
{
    public class ProdutoViewModel : INotifyPropertyChanged
    {
        //Mantem conexão direta com os controles (fica escutando o que mudou).
        public event PropertyChangedEventHandler PropertyChanged;

        //Captura qual foi o objeto atualizado.
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Propriedades
        private long? _IdProduto;
        private string _Nome;
        private string _Categoria;
        private decimal _Preco;
        private string _Descricao;
        private Byte[] _Foto;
        private string _titulo;
        #endregion

        public Opcoes Acao { get; set; }

        public ProdutoViewModel(Produto produto, Opcoes acao)
        {
            if (produto != null)
            {
                this._IdProduto = produto.IdProduto;
                this._Nome = produto.Nome;
                this._Descricao = produto.Descricao;
                this._Preco = produto.Preco;
                this._Foto = produto.Foto;
                this._Categoria = produto.Categoria;
            }

            this.Acao = acao;

            if (acao == Opcoes.Inserir)
                this._titulo = "NOVO PRODUTO";
            else
                this._titulo = "ALTERAR PRODUTO";
        }

        public string Nome
        {
            get { return _Nome; }
            set { _Nome = value; OnPropertyChanged(); }
        }

        public string Categoria
        {
            get { return _Categoria; }
            set { _Categoria = value; OnPropertyChanged(); }
        }

        public string Titulo
        {
            get { return _titulo; }
            set { _titulo = value; OnPropertyChanged(); }
        }

        public long? IdProduto
        {
            get { return _IdProduto; }
            set { _IdProduto = value; OnPropertyChanged(); }
        }

        public Byte[] Foto
        {
            get { return _Foto; }
            set { _Foto = value; OnPropertyChanged(); }
        }

        public decimal Preco
        {
            get { return _Preco; }
            set { _Preco = value; OnPropertyChanged(); }
        }


        public ICommand Gravar
        {
            get
            {
                var produtoDal = new ProdutoDAL();
                return new Command(() =>
                {
                    if (this.Acao == Opcoes.Inserir)
                    {
                        produtoDal.Add(GetObjectFromView());
                        App.Current.MainPage.DisplayAlert("Produto",
                            "Produto cadastrado com sucesso!", "Ok");
                    }
                    else
                    {
                        produtoDal.Update(GetObjectFromView());
                        App.Current.MainPage.DisplayAlert("Produto",
                          "Produto atualizado com sucesso!", "Ok");
                    }
                    App.Current.MainPage.Navigation.PopModalAsync();
                });
            }
        }

        public Produto GetObjectFromView()
        {
            return new Produto()
            {
                IdProduto = this._IdProduto,
                Nome = this.Nome,
                Descricao = this._Descricao,
                Preco = this._Preco,
                Foto = this._Foto,
                Categoria = this._Categoria
            };
        }
    }
}
