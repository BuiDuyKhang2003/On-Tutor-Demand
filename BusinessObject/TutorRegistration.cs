using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    [Table("TutorRegistration", Schema = "dbo")]
    public class TutorRegistration
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [StringLength(10)]
        public string Gender { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        public DateTime DateOfBirth { get; set; }

        [StringLength(15)]
        public string Phone { get; set; }

        [StringLength(500)]
        public string Experience { get; set; }

        [StringLength(500)]
        public string Education { get; set; }
        [StringLength(500)]
        public string Description { get; set; }

        public DateTime ApplicationDate { get; } = DateTime.Now;   

        public bool IsApproved { get; set; } = false;
    }

}


