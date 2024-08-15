using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Salto.Pages.RoleUserAdmin
{
    public class DeleteUserModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public DeleteUserModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public IdentityUser User { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId)
        {
            User = await _userManager.FindByIdAsync(userId);

            if (User == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.FindByIdAsync(User.Id);

            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToPage("Index"); // Redirect to the user listing page
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
    }
}
