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
        [HttpPost]
        public async Task<IActionResult> OnPostDeleteUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToPage("List");
                }
                else
                {
                    
                     return Page();
                }
            }
            return Page();

            //return RedirectToPage("List");
        }
        [HttpPost]

        public async Task<IActionResult> OnPostLockUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                
                user.LockoutEnd = DateTime.Now.AddDays(7);

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    
                    return RedirectToPage("List"); 
                }
                else
                {
                    
                    return Page();
                }
            }

            
            return RedirectToPage("List");
        }

        [HttpPost]

        public async Task<IActionResult> OnPostUnlock(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {

                user.LockoutEnd = DateTime.Now;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {

                    return RedirectToPage("List");
                }
                else
                {

                    return Page();
                }
            }


            return RedirectToPage("List");
        }
    }
}
