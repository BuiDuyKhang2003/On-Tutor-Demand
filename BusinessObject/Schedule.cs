using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    [Table("Schedule", Schema = "dbo")]
    public class Schedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int RentalServiceId { get; set; }

        [Required]
        [StringLength(20)]
        public string DayOfWeek { get; set; } // Values: "Monday", "Tuesday", etc.

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        public virtual RentalService RentalService { get; set; }
    }

}
