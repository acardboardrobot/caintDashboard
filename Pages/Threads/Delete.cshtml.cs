using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using caint.Data;
using caint.Models;

namespace caintDashboard.Pages.Threads
{
    public class DeleteModel : PageModel
    {
        private readonly caint.Data.caintDBContext _context;

        public DeleteModel(caint.Data.caintDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Thread Thread { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Thread = await _context.threads.FirstOrDefaultAsync(m => m.id == id);

            if (Thread == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Thread = await _context.threads.FindAsync(id);

            if (Thread != null)
            {
                _context.threads.Remove(Thread);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
