using SQLite.Net;

namespace PodePedir.DAL
{
    public interface IDatabaseConnection
    {
        SQLiteConnection DbConnection();
    }
}
