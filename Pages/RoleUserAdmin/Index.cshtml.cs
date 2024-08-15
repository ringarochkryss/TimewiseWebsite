using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Salto.Pages.RoleUserAdmin
{
    public class IndexModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // Properties for displaying data in the view
        public IEnumerable<IdentityUser> Users { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }

        // New method to get role names for a user
        public async Task<IEnumerable<string>> GetUserRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                return roles;
            }
            return new List<string>();
        }

        public Task OnGet()
        {
            // Retrieve and set data for the view
            Users = _userManager.Users.ToList();
            Roles = _roleManager.Roles.ToList();
            return Task.CompletedTask;
        }
    }
}
