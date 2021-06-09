using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using caint.Data;
using caint.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace caintDashboard.Pages
{
    [AllowAnonymous]
    public class IndexModel : dashboard_BasePageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(
            caintDBContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        public IList<Comment> Comment { get;set; }
        public int unapprovedCount;

        public void OnGet()
        {
            var ownerId = UserManager.GetUserId(User);
            Comment = _context.comments.Where(x => x.ownerId == ownerId).ToList();

            unapprovedCount = _context.comments.Where(x => x.ownerId == ownerId && x.approved == false).ToList().Count;

            if (Comment.Count > 3)
            {
                Comment = Comment.Skip(Comment.Count - 3).ToList();
            }
        }
    }
}
