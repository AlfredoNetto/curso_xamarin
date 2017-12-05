using PodePedir.Model;
using SQLite.Net;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace PodePedir.Dal
{
    public class ProdutoDal
    {


        private SQLiteConnection sqlConnection;

        public ProdutoDal()
        {
            this.sqlConnection = DependencyService.Get<IDatabaseConnection>().DbConnection();
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
            return (from t in sqlConnection.Table<Produto>() select t).OrderBy(i => i.Nome).ToList();
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
