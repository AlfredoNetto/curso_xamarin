using PodePedir.Droid;
using PodePedir.Dal;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;
using System.IO;
using System;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection_Android))]
namespace PodePedir.Droid
{
    public class DatabaseConnection_Android : IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "bdPodePedir.db3";
            string documentsFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string path = Path.Combine(documentsFolder, dbName);
            var platform = new SQLitePlatformAndroid();
            return new SQLiteConnection(platform, path);
        }
    }
}