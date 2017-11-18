

using System;
using PodePedir.DAL;
using SQLite.Net;
using System.IO;
using SQLite.Net.Platform.XamarinAndroid;

namespace PodePedir.Droid
{
    public class DatabaseConnection_Android : IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {
            //Variavel para definir o nome do Banco de Dados.
            var dbName = "bdPodePedir.db3";
            
            //Pega a pasta pessoal do celular do usuário
            string documentsFolder = System.Environment.GetFolderPath(
                System.Environment.SpecialFolder.Personal);
            //Define aonde o arquivo será salvo.
            string path = Path.Combine(documentsFolder, dbName);

            //Define a plataforma em que o SQLite irá atuar.
            var platform = new SQLitePlatformAndroid();

            //Retorna a plataforma e onde o banco está salvo.
            return new SQLiteConnection(platform, path);
        }
    }
}