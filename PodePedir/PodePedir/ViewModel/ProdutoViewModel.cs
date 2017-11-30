using PodePedir.DAL;
using PodePedir.Model;
using PodePedir.Model.Enum;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace PodePedir.ViewModel
{
    public class ProdutoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _titulo;
        private long? _produtoId;
        private string _nome;
        private decimal _preco;
        private string _descricao;
        private Byte[] _foto;
        private string _categoria;


        public ProdutoViewModel(Produto produto, Opcoes acao)
        {
            if (produto != null)
            {
                this._produtoId = produto.IdProduto;
                this._nome = produto.Nome;
                this._descricao = produto.Descricao;
                this._preco = produto.Preco;
                this._foto = produto.Foto;
                this._categoria = produto.Categoria;
            }

            this.Acao = acao;

            if (acao == Opcoes.Inserir)
                this._titulo = "NOVO PRODUTO";
            else
                this._titulo = "ALTERAR PRODUTO";
        }



        public Opcoes Acao { get; set; }

        public string Nome
        {
            get
            {
                return _nome;
            }
            set
            {
                _nome = value; OnPropertyChanged();
            }
        }

        public string Categoria
        {
            get
            {
                return _categoria;
            }
            set
            {
                _categoria = value; OnPropertyChanged();
            }
        }
        public string Titulo
        {
            get
            {
                return _titulo;
            }
            set
            {
                _titulo = value; OnPropertyChanged();
            }
        }

        public long? ProdutoId
        {
            get
            {
                return _produtoId;
            }
            set
            {
                _produtoId = value; OnPropertyChanged();
            }
        }


        public Byte[] Foto
        {
            get
            {
                return _foto;
            }
            set
            {
                _foto = value; OnPropertyChanged();
            }
        }

        public string Descricao
        {
            get
            {
                return _descricao;
            }
            set
            {
                _descricao = value; OnPropertyChanged();
            }
        }

        public decimal Preco
        {
            get
            {
                return _preco;
            }
            set
            {
                _preco = value; OnPropertyChanged();
            }
        }



        public ICommand Gravar
        {
            get
            {
                var produtoDal = new ProdutoDal();
                return new Command(() =>
                {
                    if (this.Acao == Opcoes.Inserir)
                    {
                        produtoDal.Add(GetObjectFromView());
                        App.Current.MainPage.DisplayAlert("Produto", "Produto Inserido com Sucesso!", "Ok");
                    }
                    else
                    {
                        produtoDal.Update(GetObjectFromView());
                        App.Current.MainPage.DisplayAlert("Produto", "Produto Atualizado com Sucesso!", "OK");
                    }

                    App.Current.MainPage.Navigation.PopModalAsync();
                });
            }
        }

        public Produto GetObjectFromView()
        {
            return new Produto()
            {
                IdProduto = this._produtoId,
                Nome = this._nome,
                Descricao = this._descricao,
                Preco = this._preco,
                Foto = this._foto,
                Categoria = this._categoria
            };
        }
    }
}
