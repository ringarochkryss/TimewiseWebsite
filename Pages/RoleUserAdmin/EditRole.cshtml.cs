using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Salto.Pages.RoleUserAdmin
{
    public class EditRoleModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        [BindProperty]
        public string RoleId { get; set; }

        [BindProperty]
        [Required]
        public string RoleName { get; set; }

        public EditRoleModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            RoleId = role.Id;
            RoleName = role.Name;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var role = await _roleManager.FindByIdAsync(RoleId);
            if (role == null)
            {
                return NotFound();
            }

            role.Name = RoleName;
            var result = await _roleManager.UpdateAsync(role);

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

