using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using caint.Data;
using caint.Models;

namespace caintDashboard.Pages.Threads
{
    public class CreateModel : PageModel
    {
        private readonly caint.Data.caintDBContext _context;

        public CreateModel(caint.Data.caintDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Thread Thread { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.threads.Add(Thread);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
