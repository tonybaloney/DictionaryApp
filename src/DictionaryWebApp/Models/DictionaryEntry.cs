namespace DictionaryWebApp.Models
{
    public class DictionaryEntry
    {
            public int Id { get; set; }
            public string? Word { get; set; }
            public string? Translation { get; set; }
            public string? Language { get; set; }
    }
}