using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using caint.Data;
using caint.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace caintDashboard.Pages.Tenants
{
    public class BackupModel : dashboard_BasePageModel
    {

        public BackupModel(
            caintDBContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        [BindProperty]
        public Tenant Tenant { get; set; }

        public JsonResult OnGet(long? id)
        {
            var ownerId = UserManager.GetUserId(User);

            if (id == null)
            {
                return new JsonResult(null);//return RedirectToPage("../index");
            }

            Tenant = _context.tenants.FirstOrDefault(m => m.id == id);

            if (Tenant == null)
            {
                return new JsonResult(null);
            }
            if (Tenant.ownerId == ownerId)
            {
                return new JsonResult(getBackupComments(Tenant.ownerId));
            }
            else
            {
                return new JsonResult(null);
            }
            
        }

        public List<CommentDTO> getBackupComments(string id)
        {
            List<Comment> comments = _context.comments.Where(x => x.ownerId == id).ToList();
            List<CommentDTO> returnList = comments.Select(x => CommentToDTO(x)).ToList();
            /*List<CommentDTO> comments = new List<CommentDTO>{
                new CommentDTO{id=1, name = "Jack", body = "This is a test", threadId = 1},
                new CommentDTO{id=3, name = "John", body = "This is a different comment for the test", threadId = 1},
                new CommentDTO{id=13, name = "Jack", body = "Easily done", threadId = 2},
                new CommentDTO{id=665, name = "Thomas", body = "This looks great", threadId = 1},
                new CommentDTO{id=4578, name = "Cindy", body = "This is one more", threadId = 2}
            };*/

            return returnList;
        }

        private static CommentDTO CommentToDTO(Comment comment) =>
        new CommentDTO
            {
                id = comment.id,
                name = comment.name,
                body = comment.body,
                threadId = comment.threadId
            };
        }
}
