using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Salto.Pages.RoleUserAdmin
{
    public class DeleteRoleModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public DeleteRoleModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [BindProperty]
        public IdentityRole Role { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Role = await _roleManager.FindByIdAsync(id);

            if (Role == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Role != null)
            {
                var result = await _roleManager.DeleteAsync(Role);

                if (result.Succeeded)
                {
                    return RedirectToPage("./Index");
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
