using System;
using SQLite.Net;
using System.IO;
using SQLite.Net.Platform.XamarinIOS;
using PodePedir.DAL;
using PodePedir.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection_iOS))]
namespace PodePedir.iOS
{
    public class DatabaseConnection_iOS : IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "bdPodePedir.db3";
            string personalFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryFolder = Path.Combine(personalFolder, "..", "Library");
            var path = Path.Combine(libraryFolder, dbName);
            var platform = new SQLitePlatformIOS();
            return new SQLiteConnection(platform, path);
        }
    }
}