using Microsoft.EntityFrameworkCore;
using DictionaryWebApp.Data;
using DictionaryWebApp.Models;
using DictionaryWebApp.Pages;

namespace DictionaryWebApp.UnitTests
{
    public class DictionaryWebAppUnitTests
    {
        // Define the options for the database context
        private readonly DbContextOptions<DefaultConnection> options;
        public DictionaryWebAppUnitTests()
        {
            // Generate a unique database name for each test
            var databaseName = Guid.NewGuid().ToString();

            // Configure the database context to use an in-memory database
            options = new DbContextOptionsBuilder<DefaultConnection>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;
        }

        [Fact]
        public async Task Create_Entry()
        {
            // Create a new database context instance
            using var context = new DefaultConnection(options);
            // Create a new dictionary entry
            var entry = new DictionaryEntry { Word = "Hello", Translation = "Hola", Language = "Spanish" };

            // Add the entry to the context and save changes
            context.DictionaryEntry.Add(entry);
            await context.SaveChangesAsync();

            // Assert that the entry was added
            Assert.Equal(1, context.DictionaryEntry.Count());
        }

        [Fact]
        public async Task Update_Entry()
        {
            using var context = new DefaultConnection(options);
            var entry = new DictionaryEntry { Word = "Hello", Translation = "Hola", Language = "Spanish" };
            context.DictionaryEntry.Add(entry);
            await context.SaveChangesAsync();

            // Update the translation of the entry
            entry.Translation = "Bonjour";
            context.DictionaryEntry.Update(entry);
            await context.SaveChangesAsync();

            // Retrieve the updated entry
            var updatedEntry = context.DictionaryEntry.FirstOrDefault(e => e.Word == "Hello");

            // Assert that the entry was updated
            Assert.NotNull(updatedEntry);
            Assert.Equal("Bonjour", updatedEntry.Translation);
        }

        [Fact]
        public async Task Read_Entry()
        {
            using var context = new DefaultConnection(options);
            var entry = new DictionaryEntry { Word = "Hello", Translation = "Hola", Language = "Spanish" };
            context.DictionaryEntry.Add(entry);
            await context.SaveChangesAsync();

            // Retrieve the entry
            var retrievedEntry = context.DictionaryEntry.FirstOrDefault(e => e.Word == "Hello");

            // Assert that the entry was retrieved
            Assert.NotNull(retrievedEntry);
            Assert.Equal("Hola", retrievedEntry.Translation);
        }

        [Fact]
        public async Task OnGetAsync_ReturnsDictionaryEntries()
        {
            // Arrange
            // Insert seed data into the database using one instance of the context
            using (var context = new DefaultConnection(options))
            {
                context.DictionaryEntry.Add(new DictionaryEntry { Word = "Hello", Translation = "Hola", Language = "Spanish" });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new DefaultConnection(options))
            {
                var indexModel = new IndexModel(context);

                // Act
                await indexModel.OnGetAsync();

                // Assert
                Assert.IsType<List<DictionaryEntry>>(indexModel.DictionaryEntry);
                Assert.NotEmpty(indexModel.DictionaryEntry);
            }
        }

        [Fact]
        public async Task Delete_Entry()
        {
            using var context = new DefaultConnection(options);
            var entry = new DictionaryEntry { Word = "Hello", Translation = "Hola", Language = "Spanish" };
            context.DictionaryEntry.Add(entry);
            await context.SaveChangesAsync();

            // Remove the entry and save changes
            context.DictionaryEntry.Remove(entry);
            await context.SaveChangesAsync();

            // Assert that the entry was removed
            Assert.Equal(0, context.DictionaryEntry.Count());
        }
    }
}