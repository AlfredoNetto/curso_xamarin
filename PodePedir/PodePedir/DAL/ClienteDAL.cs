using PodePedir.Model;
using SQLite.Net;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;

namespace PodePedir.DAL
{
    public class ClienteDAL
    {
        private SQLiteConnection sqlConnection;

        public ClienteDAL()
        {
            //Captura com a conexão com o banco de dados.
            this.sqlConnection =
            DependencyService.Get<IDatabaseConnection>().DbConnection();

            //Cria uma tabela baseada no modelo Cliente.
            this.sqlConnection.CreateTable<Cliente>();
        }

        public void Add(Cliente cliente)
        {
            sqlConnection.Insert(cliente);
        }

        public void DeleteById(long id)
        {
            sqlConnection.Delete<Cliente>(id);
        }

        public void Update(Cliente cliente)
        {
            sqlConnection.Update(cliente);
        }

        public IEnumerable<Cliente> GetAll()
        {
            return (from t in sqlConnection.Table<Cliente>()
                    select t).OrderBy(i => i.Nome).ToList();
        }

        public Cliente GetByID(long? id)
        {
            return sqlConnection.Table<Cliente>().FirstOrDefault
                (c => c.ClienteId == id);
        }

    }
}
