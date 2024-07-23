using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    [Table("BookingSchedule", Schema = "dbo")]
    public class BookingSchedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ScheduleId { get; set; }

        [Required]
        public int AccountId { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }

        [ForeignKey("ScheduleId")]
        public virtual Schedule Schedule { get; set; }
    }
}
