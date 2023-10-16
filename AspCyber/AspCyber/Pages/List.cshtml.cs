using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AspCyber.Pages
{
    [Authorize(Roles = "Admin")]
    public class ListModel : PageModel
    {
        public class UserRoleView
        {
            public string UserEmail { get; set; }
            public IList<string> Roles { get; set; }
        }

        [BindProperty]
        public IList<UserRoleView> model {get;set;} = new List<UserRoleView>();

        private readonly UserManager<IdentityUser> _userManager;

        public ListModel(UserManager<IdentityUser> userManager)
        {
            _userManager =  userManager;
        }

       
        public async Task<IActionResult> OnGetAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            foreach (IdentityUser user in users )
            {
                UserRoleView identityUser = new UserRoleView()
                {
                    UserEmail = user.Email,
                    Roles = await _userManager.GetRolesAsync(user)
                    
                };
                model.Add(identityUser);
            }
            return Page();
        
        }
    }
}
