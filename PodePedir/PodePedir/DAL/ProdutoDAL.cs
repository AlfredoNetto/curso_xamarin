using PodePedir.Model;
using SQLite.Net;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;

namespace PodePedir.DAL
{
    public class ProdutoDAL
    {
        private SQLiteConnection sqlConnection;

        public ProdutoDAL()
        {
            //Captura com a conexão com o banco de dados.
            this.sqlConnection =
            DependencyService.Get<IDatabaseConnection>().DbConnection();

            //Cria uma tabela baseada no modelo Cliente.
            this.sqlConnection.CreateTable<Produto>();
        }


        public void Add(Produto produto)
        {
            sqlConnection.Insert(produto);
        }

        public void DeleteById(long id)
        {
            sqlConnection.Delete<Produto>(id);
        }

        public void Update(Produto produto)
        {
            sqlConnection.Update(produto);
        }

        public IEnumerable<Produto> GetAll()
        {
            return (from t in sqlConnection.Table<Produto>()
                    select t).OrderBy(i => i.Nome).ToList();
        }
       
        public Produto GetByID(long? id)
        {
            return sqlConnection.Table<Produto>().FirstOrDefault
                (c => c.IdProduto == id);
        }

        public IEnumerable<string> GetCategorias()
        {
            List<string> Categorias = new List<string>();
            Categorias.Add("Bebidas");
            Categorias.Add("Porções");
            Categorias.Add("Lanches");
            Categorias.Add("Massas");
            return Categorias;
        }

    }
}
