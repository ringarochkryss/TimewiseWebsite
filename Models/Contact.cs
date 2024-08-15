using System.ComponentModel.DataAnnotations;

namespace Salto.Models
{
    public class Contact
    {
        public int ContactID { get; set; }
        
        [StringLength(20)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        
        [StringLength(50)]
        [Display(Name = "Family Name")]
        public string LastName { get; set; }
        
        [StringLength(30)]
        public string Organization { get; set; }
        
        [StringLength(30)]
        public string Email { get; set; }
        
        public string Phone { get; set; }
        
        [StringLength(300)]
        public string Message{ get; set; }

        [Display(Name = "Version, Demo or Meeting?")]
        public Order VersionDemoMeeting { get; set; }

        
        
        public enum Order
        {
            Starter,
            Plus,
            PerformingArts,
            Demo,
            Meeting
        }

    }
}
