using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class TutorSubject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public int TutorId { get; set; }

        public virtual Subject Subject { get; set; }
        public virtual Tutor Tutor { get; set; }
    }

}
