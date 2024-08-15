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
    public class EditUserModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public EditUserModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [BindProperty]
        public string SelectedRole { get; set; }

        public SelectList Roles { get; set; }

        public class InputModel
        {
            public string UserId { get; set; }

            [Required]
            [Display(Name = "Username")]
            public string UserName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Display(Name = "Current Password")]
            [DataType(DataType.Password)]
            public string CurrentPassword { get; set; }

            [Display(Name = "New Password")]
            [DataType(DataType.Password)]
            public string NewPassword { get; set; }

            [Display(Name = "Confirm New Password")]
            [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
            [DataType(DataType.Password)]
            public string ConfirmNewPassword { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            Input = new InputModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
            };

            Roles = new SelectList(await _roleManager.Roles.ToListAsync(), "Name", "Name");
            var userRoles = await _userManager.GetRolesAsync(user);
            SelectedRole = userRoles.FirstOrDefault();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Roles = new SelectList(await _roleManager.Roles.ToListAsync(), "Name", "Name");
                return Page();
            }

            var user = await _userManager.FindByIdAsync(Input.UserId);

            if (user == null)
            {
                return NotFound();
            }

            // Update password first, if provided
            if (!string.IsNullOrWhiteSpace(Input.CurrentPassword) && !string.IsNullOrWhiteSpace(Input.NewPassword))
            {
                var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.CurrentPassword, Input.NewPassword);
                if (!changePasswordResult.Succeeded)
                {
                    foreach (var error in changePasswordResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    Roles = new SelectList(await _roleManager.Roles.ToListAsync(), "Name", "Name");
                    return Page();
                }
            }

            // Update user properties
            user.UserName = Input.UserName;
            user.Email = Input.Email;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                // Update user roles
                var currentRoles = await _userManager.GetRolesAsync(user);
                if (!string.IsNullOrEmpty(SelectedRole) && !currentRoles.Contains(SelectedRole))
                {
                    await _userManager.RemoveFromRolesAsync(user, currentRoles);
                    await _userManager.AddToRoleAsync(user, SelectedRole);
                }

                return RedirectToPage("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            Roles = new SelectList(await _roleManager.Roles.ToListAsync(), "Name", "Name");
            return Page();
        }
    }
}
