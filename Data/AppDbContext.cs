using AspNetCoreLoggingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreLoggingAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        public DbSet<DataRecord> DataRecords { get; set; }
        public DbSet<LogRecord> LogRecords { get; set; }
    }
}
