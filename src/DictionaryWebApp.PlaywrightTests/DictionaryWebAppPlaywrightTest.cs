using Microsoft.Extensions.Configuration;

namespace DictionaryWebApp.PlaywrightTests
{
    [TestClass]
    public partial class DictionaryWebAppPlaywrightTest : PageTest
    {
        private string? BaseUrl { get; set; }

        public DictionaryWebAppPlaywrightTest()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json")
                .Build();

            BaseUrl = config["BaseUrl"];
        }

        [TestMethod]        
        public async Task EndToEndTest()
        {            
            if (BaseUrl == null)
            {
                throw new ArgumentNullException("BaseUrl");
            }
            else
            {
                await Page.GotoAsync(BaseUrl);

                // Expect a title "to contain" a substring.
                await Expect(Page).ToHaveTitleAsync(IndexRegex());

                // Check that the table of dictionary entries is present
                var dictionaryTable = Page.Locator("table");

                // Expect the table to be visible
                await Expect(dictionaryTable).ToBeVisibleAsync();

                // Check that the Create a new dictionary entry link is present
                var createLink = Page.Locator("text=Create a new dictionary entry");

                // Expect the link to be visible
                await Expect(createLink).ToBeVisibleAsync();

                // Create a new dictionary entry

                // Navigate to the Create page
                await Page.GotoAsync(BaseUrl + "Create");

                // Expect a title "to contain" a substring.
                await Expect(Page).ToHaveTitleAsync(CreateRegex());

                // Fill in the form fields
                await Page.FillAsync("input[name='DictionaryEntry.Word']", "Test Word");
                await Page.FillAsync("input[name='DictionaryEntry.Translation']", "Test Translation");
                await Page.FillAsync("input[name='DictionaryEntry.Language']", "Test Language");

                // Submit the form
                await Page.ClickAsync("input[type='submit']");

                // Expect the page to navigate to the Index page after form submission
                await Expect(Page).ToHaveURLAsync(BaseUrl);

                // Check that the new dictionary entry is displayed in the table

                // Navigate to the Details page for a specific dictionary entry
                await Page.GotoAsync(BaseUrl + "Details?id=1");

                // Expect a title "to contain" a substring.
                await Expect(Page).ToHaveTitleAsync(DetailsRegex());

                // Check that the details are displayed correctly
                // Replace "Test Word", "Test Translation", and "Test Language" with the expected details
                var word = await Page.Locator("dt:has-text('Word') + dd").InnerTextAsync();
                var translation = await Page.Locator("dt:has-text('Translation') + dd").InnerTextAsync();
                var language = await Page.Locator("dt:has-text('Language') + dd").InnerTextAsync();

                Assert.AreEqual("Test Word", word);
                Assert.AreEqual("Test Translation", translation);
                Assert.AreEqual("Test Language", language);

                // Check that the Edit and Delete pages are working

                // Navigate to the Edit page for a specific dictionary entry
                await Page.GotoAsync(BaseUrl + "Edit?id=1");

                // Expect a title "to contain" a substring.
                await Expect(Page).ToHaveTitleAsync(EditRegex());

                // Fill in the form fields with new values
                await Page.FillAsync("input[name='DictionaryEntry.Word']", "New Test Word");
                await Page.FillAsync("input[name='DictionaryEntry.Translation']", "New Test Translation");
                await Page.FillAsync("input[name='DictionaryEntry.Language']", "New Test Language");

                // Submit the form
                await Page.ClickAsync("input[type='submit']");

                // Expect the page to navigate to the Index page after form submission
                await Expect(Page).ToHaveURLAsync(BaseUrl);

                // Navigate to the Delete page for a specific dictionary entry
                await Page.GotoAsync(BaseUrl + "Delete?id=1");

                // Expect a title "to contain" a substring.
                await Expect(Page).ToHaveTitleAsync(DeleteRegex());

                // Click the Delete button
                await Page.ClickAsync("input[type='submit']");

                // Expect the page to navigate to the Index page after deletion
                await Expect(Page).ToHaveURLAsync(BaseUrl);
            }
        }

        [GeneratedRegex("Index")]
        private static partial Regex IndexRegex();
        [GeneratedRegex("Create")]
        private static partial Regex CreateRegex();
        [GeneratedRegex("Details")]
        private static partial Regex DetailsRegex();
        [GeneratedRegex("Edit")]
        private static partial Regex EditRegex();
        [GeneratedRegex("Delete")]
        private static partial Regex DeleteRegex();
    }
}