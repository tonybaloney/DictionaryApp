using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DictionaryWebApp.Models;

namespace DictionaryWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly DictionaryWebApp.Data.DefaultConnection _context;

        public IndexModel(DictionaryWebApp.Data.DefaultConnection context)
        {
            _context = context;
        }

        public IList<DictionaryEntry> DictionaryEntry { get;set; } = default!;

        public async Task OnGetAsync()
        {
            DictionaryEntry = await _context.DictionaryEntry.ToListAsync();
        }
    }
}