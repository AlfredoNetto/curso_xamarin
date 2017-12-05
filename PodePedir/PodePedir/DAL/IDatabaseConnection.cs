using SQLite.Net;

namespace PodePedir.Dal
{
    public interface IDatabaseConnection
    {
        SQLiteConnection DbConnection();
    }
}
