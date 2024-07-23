using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class TutorGrade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int TutorId { get; set; }
        public int GradeId { get; set; }

        public virtual Tutor Tutor { get; set; }
        public virtual Grade Grade { get; set; }
    }

}
