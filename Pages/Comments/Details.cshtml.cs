using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using caint.Data;
using caint.Models;

namespace caintDashboard.Pages.Comments
{
    public class DetailsModel : PageModel
    {
        private readonly caint.Data.caintDBContext _context;

        public DetailsModel(caint.Data.caintDBContext context)
        {
            _context = context;
        }

        public Comment Comment { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Comment = await _context.comments.FirstOrDefaultAsync(m => m.id == id);

            if (Comment == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
