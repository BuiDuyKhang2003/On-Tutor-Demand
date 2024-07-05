using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    [Table("RentalService", Schema = "dbo")]
    public class RentalService
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [ForeignKey("Tutor")]
        public int TutorId { get; set; }

        public virtual Tutor? Tutor { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
        public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
    }

}
