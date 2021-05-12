using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TJ.Domain.Entities;

namespace TJ.Persistence
{
    public class DevicesDbContext: DbContext
    {
        public DevicesDbContext(DbContextOptions<DevicesDbContext> options)
      : base(options)
        {
        }

        public virtual DbSet<Device> Device { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
    => options.UseSqlite("DataSource=DeviceDb.db");
        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DevicesDbContext>
        {
            public DevicesDbContext CreateDbContext(string[] args)
            {
                var builder = new DbContextOptionsBuilder<DevicesDbContext>();
                builder.UseSqlite("Data Source=DeviceDb.db");
                return new DevicesDbContext(builder.Options);
            }
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            var AddedEntities = ChangeTracker.Entries<IEntity>().Where(E => E.State == EntityState.Added).ToList();

            AddedEntities.ForEach(E =>
            {
                E.Entity.DateAdded = DateTime.Now;

            });

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
