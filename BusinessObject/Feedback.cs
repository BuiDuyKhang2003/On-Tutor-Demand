using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int RentalServiceId { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public virtual Account Account { get; set; }
        public virtual RentalService RentalService { get; set; }
    }

}
