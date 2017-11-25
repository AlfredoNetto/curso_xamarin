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
    public class ClienteViewModel : INotifyPropertyChanged
    {
        //Mantem conexão direta com os controles (fica escutando o que mudou).
        public event PropertyChangedEventHandler PropertyChanged;

        //Captura qual foi o objeto atualizado.
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Propriedades
        private long? _clienteId;
        private string _nome;
        private string _telefone;
        private string _endereco;
        private string _numero;
        private string _bairro;
        private string _cidade;
        private string _estado;
        private string _titulo;
        #endregion

        #region Acesso_Propriedades
        public Opcoes Acao { get; set; }

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; OnPropertyChanged(); }
        }

        public string Telefone
        {
            get { return _telefone; }
            set { _telefone = value; OnPropertyChanged(); }
        }

        public string Titulo
        {
            get { return _titulo; }
            set { _titulo = value; OnPropertyChanged(); }
        }

        public string Endereco
        {
            get { return _endereco; }
            set { _endereco = value; OnPropertyChanged(); }
        }

        public string Numero
        {
            get { return _numero; }
            set { _numero = value; OnPropertyChanged(); }
        }

        public string Bairro
        {
            get { return _bairro; }
            set { _bairro = value; OnPropertyChanged(); }
        }

        public string Cidade
        {
            get { return _cidade; }
            set { _cidade = value; OnPropertyChanged(); }
        }

        public string Estado
        {
            get { return _estado; }
            set { _estado = value; OnPropertyChanged(); }
        }

        #endregion

        #region Métodos
        //Metodo construtor da classe que recebe o cliente para Inserir ou Alterar    
        public ClienteViewModel(Cliente cliente, Opcoes acao)
        {
            if (cliente != null)
            {
                this._clienteId = cliente.ClienteId;
                this._nome = cliente.Nome;
                this._telefone = cliente.Telefone;
                this._endereco = cliente.Endereco;
                this._numero = cliente.Numero;
                this._bairro = cliente.Bairro;
                this._cidade = cliente.Cidade;
                this._estado = cliente.Estado;
            }
            //Pega a ação selecionada pelo cliente (Cadastrar ou Alterar)
            this.Acao = acao;

            if (this.Acao == Opcoes.Inserir)
                this._titulo = "NOVO CLIENTE";
            else
                this._titulo = "ALTERAR CLIENTE";

        }

        public Cliente GetObjectFromView()
        {
            return new Cliente()
            {
                ClienteId = this._clienteId,
                Nome = this.Nome,
                Telefone = this.Telefone,
                Endereco = this.Endereco,
                Numero = this.Numero,
                Bairro = this.Bairro,
                Cidade = this.Cidade,
                Estado = this.Estado
            };

        }

        public ICommand Gravar
        {
            get
            {
                var clienteDal = new ClienteDAL();
                return new Command(() =>
                {

                    if (this.Acao == Opcoes.Inserir)
                    {
                        clienteDal.Add(GetObjectFromView());
                        App.Current.MainPage.DisplayAlert("Cliente",
                            "Cliente cadastrado com sucesso!", "Ok");
                    }
                    else
                    {
                        clienteDal.Update(GetObjectFromView());
                        App.Current.MainPage.DisplayAlert("Cliente",
                          "Cliente atualizado com sucesso!", "Ok");
                    }
                    //Fecha a janela de Cliente
                    App.Current.MainPage.Navigation.PopModalAsync();
                });
            }
        }
        #endregion

    }
}
