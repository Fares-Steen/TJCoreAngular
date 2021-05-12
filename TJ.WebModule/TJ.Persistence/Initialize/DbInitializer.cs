using Microsoft.EntityFrameworkCore;
using System.Linq;
using TJ.Interfaces.DbInterfaces;

namespace TJ.Persistence.Initialize
{
    public class DbInitializer : IDbInitializer
    {
        private readonly DevicesDbContext _context;

        public DbInitializer(DevicesDbContext context)
        {
            _context = context;
        }
        public void Initialize()
        {
            if (_context.Database.GetPendingMigrations().Count() > 0)
            {
                _context.Database.Migrate();
            }
        }

    }
}
