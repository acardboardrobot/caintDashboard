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
    public class IndexModel : PageModel
    {
        private readonly caintDBContext _context;

        public IndexModel(caintDBContext context)
        {
            _context = context;
        }

        public IList<Comment> Comment { get;set; }
        public Comment singleComment {get; set; }

        public async Task OnGetAsync()
        {
            Comment = await _context.comments.ToListAsync();
        }

        public async Task OnGetThreadViewAsync(long? id)
        {
            Comment = await _context.comments.Where(x => x.threadId == id).ToListAsync();
        }

        public async Task<IActionResult> OnGetApproveCommentAsync(long? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            singleComment = await _context.comments.FindAsync(id);

            if (singleComment != null)
            {
                singleComment.approved = true;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnGetDeleteCommentAsync(long? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            singleComment = await _context.comments.FindAsync(id);

            if (singleComment != null)
            {
                _context.Remove(singleComment);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
