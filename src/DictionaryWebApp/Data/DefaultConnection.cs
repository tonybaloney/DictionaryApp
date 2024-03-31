using Microsoft.EntityFrameworkCore;

namespace DictionaryWebApp.Data
{
    public class DefaultConnection : DbContext
    {
        public DefaultConnection (DbContextOptions<DefaultConnection> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<DictionaryWebApp.Models.DictionaryEntry> DictionaryEntry { get; set; } = default!;
    }
}