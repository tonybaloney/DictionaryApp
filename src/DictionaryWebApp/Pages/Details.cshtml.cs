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
    public class DetailsModel : PageModel
    {
        private readonly DictionaryWebApp.Data.DefaultConnection _context;

        public DetailsModel(DictionaryWebApp.Data.DefaultConnection context)
        {
            _context = context;
        }

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
    }
}
