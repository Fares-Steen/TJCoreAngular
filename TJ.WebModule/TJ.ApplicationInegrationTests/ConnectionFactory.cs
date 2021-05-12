using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TJ.Persistence;

namespace TJ.ApplicationInegrationTests
{
    public class ConnectionFactory
    {
        private bool disposedValue = false;
        private SqliteConnection connection = new SqliteConnection("DataSource=:memory:");

        public DevicesDbContext CreateContextForSQLite()
        {

            connection.Open();

            var option = new DbContextOptionsBuilder<DevicesDbContext>().UseSqlite(connection).Options;

            var context = new DevicesDbContext(option);

            return context;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    connection.Close();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }

   
}
