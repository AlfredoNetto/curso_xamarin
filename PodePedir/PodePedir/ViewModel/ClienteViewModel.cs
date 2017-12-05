using PodePedir.Dal;
using PodePedir.Model;
using PodePedir.Model.Enums;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace PodePedir.ViewModel
{
    public class ClienteViewModel : INotifyPropertyChanged
    {
        /* A interface INotifyPropertyChanged implementa o comportamento "PropertyChangedEventHandler", mantem conexão 
        direta com os controles e fica "escutando" qualquer alteração que ocorra e atualiza imediatamento. */
        public event PropertyChangedEventHandler PropertyChanged;

        //Sobrescrita do metodo PropertyChanged, que captura qual objeto foi atualizado.
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Propriedades
        //Propriedades privadas do Cliente, que poderão ser manipuladas apenas por essa classe aqui dentro.
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
        /*Aqui definimos quais propriedades poderão ser alteradas por uma classe externa (set), e quais 
        propriedades vão poder os valores de sua propriedade resgatados (get).
        Perceba que no set, a implementação do método OnPropertyChanged que é responsável por apontar alteração na propriedade. */
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
        //Método construtor da Classe, recebe objeto Cliente e qual ação a ser realizada (Inserir ou Alterar)
        public ClienteViewModel(Cliente cliente, Opcoes acao)
        {
            //Se diferete de nulo, passa para propriedades privadas os dados do cliente carregado.
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

            //Pega a ação selecionada, passando para a propriedade declarada no escopo global para ser visualizada por toda a classe.
            this.Acao = acao;

            //Se ação for inserir, exibe o Título "Novo Cliente" na tela, caso controrário exibe "Alterar Cliente".
            if (this.Acao == Opcoes.Inserir)
            {
                this._titulo = "NOVO CLIENTE";
            }
            else
            {
                this._titulo = "ALTERAR CLIENTE";
            }
        }

        //Pega o objeto que está na tela no tela, através das propriedades ligadas pelo Biding.
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

        //O botão Gravar, faz referenca em sua ligação Binding com esse método Gravar que é do tipo ICommand (comando de botão)
        public ICommand Gravar
        {
            get
            {
                var clienteDal = new ClienteDAL();
                return new Command(() =>
                {
                    //Se ação for Inserir então cadastra o novo cliente. Do contrário, atualiza os dados do cliente selecionado.
                    if (this.Acao == Opcoes.Inserir)
                    {
                        clienteDal.Add(GetObjectFromView());
                        App.Current.MainPage.DisplayAlert("Cliente", "Cliente Inserido com Sucesso!", "Ok");
                    }
                    else
                    {
                        clienteDal.Update(GetObjectFromView());
                        App.Current.MainPage.DisplayAlert("Cliente", "Cliente Atualizado com Sucesso!", "Ok");
                    }

                    /*Por fim, o App.Current.MainPage busca na árvore de Navegação (navigation) a View ativa e fecha 
                    ela (PopModalAsync), assim volta para a tela anterior que lista os dados do cliente para visualizarmos 
                    as alterações, como o foco voltará para a ClienteListView, o OnAppering da página irá regarregar a ListView com as novas alterações.
                    */
                    App.Current.MainPage.Navigation.PopModalAsync();
                });
            }
        }
        #endregion 
    }
}
