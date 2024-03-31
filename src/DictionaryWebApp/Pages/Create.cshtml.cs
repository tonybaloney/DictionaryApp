using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DictionaryWebApp.Models;

namespace DictionaryWebApp.Pages
{
    public class CreateModel : PageModel
    {
        private readonly DictionaryWebApp.Data.DefaultConnection _context;

        public CreateModel(DictionaryWebApp.Data.DefaultConnection context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public DictionaryEntry DictionaryEntry { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.DictionaryEntry.Add(DictionaryEntry);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}