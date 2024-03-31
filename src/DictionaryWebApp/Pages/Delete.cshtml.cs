using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DictionaryWebApp.Data;
using DictionaryWebApp.Models;

namespace DictionaryWebApp.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly DictionaryWebApp.Data.DefaultConnection _context;

        public DeleteModel(DictionaryWebApp.Data.DefaultConnection context)
        {
            _context = context;
        }

        [BindProperty]
        public DictionaryEntry DictionaryEntry { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dictionaryentry = await _context.DictionaryEntry.FirstOrDefaultAsync(m => m.Id == id);

            if (dictionaryentry == null)
            {
                return NotFound();
            }
            else
            {
                DictionaryEntry = dictionaryentry;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dictionaryentry = await _context.DictionaryEntry.FindAsync(id);
            if (dictionaryentry != null)
            {
                DictionaryEntry = dictionaryentry;
                _context.DictionaryEntry.Remove(DictionaryEntry);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}