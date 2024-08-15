using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Salto.Pages.RoleUserAdmin
{
    public class CreateUserModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CreateUserModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            [DataType(DataType.Password)]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage = "Role is required.")]
            public string Role { get; set; }
        }

        public SelectList Roles { get; set; }

        public async Task OnGetAsync()
        {
            Roles = new SelectList(await _roleManager.Roles.ToListAsync(), "Name", "Name");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    // Assign role to the user
                    if (!string.IsNullOrEmpty(Input.Role))
                    {
                        await _userManager.AddToRoleAsync(user, Input.Role);
                    }

                    // Redirect to a success page or perform any other action
                    return RedirectToPage("Index"); // Change "Index" to the page you want to redirect to after successful creation
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay the form
            Roles = new SelectList(await _roleManager.Roles.ToListAsync(), "Name", "Name");

            return Page();
        }
    }
}
