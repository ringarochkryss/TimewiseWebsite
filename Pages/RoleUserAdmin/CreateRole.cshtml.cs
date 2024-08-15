using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Salto.Pages.RoleUserAdmin
{
    public class CreateRoleModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        [BindProperty]
        [Required]
        public string RoleName { get; set; }

        public CreateRoleModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var role = new IdentityRole(RoleName);
            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                return RedirectToPage("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return Page();
        }
    }
}

