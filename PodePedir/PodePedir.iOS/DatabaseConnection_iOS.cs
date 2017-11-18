using PodePedir.DAL;
using SQLite.Net;
using SQLite.Net.Platform.XamarinIOS;
using System.IO;
using PodePedir.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection_iOS))]
namespace PodePedir.iOS
{
    public class DatabaseConnection_iOS : IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {
            //Variavel para definir o nome do Banco de Dados.
            var dbName = "bdPodePedir.db3";

            //Pega a pasta pessoal do celular do usuário
            string personalFolder = System.Environment.GetFolderPath(
                System.Environment.SpecialFolder.Personal);


            //Concatena dois pontos a string de endereco do banco
            string libraryFolder = Path.Combine(personalFolder, "..", dbName);

            //Define onde o banco vai ser salvo.
            string path = Path.Combine(libraryFolder, dbName);


            //Define a plataforma em que o SQLite irá atuar.
            var platform = new SQLitePlatformIOS();

            //Retorna a plataforma e onde o banco está salvo.
            return new SQLiteConnection(platform, path);
        }
    }
}