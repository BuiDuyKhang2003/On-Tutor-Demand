using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class TutorGrade
    {
        public int Id { get; set; }
        public int TutorId { get; set; }
        public int GradeId { get; set; }

        public virtual Tutor Tutor { get; set; }
        public virtual Grade Grade { get; set; }
    }

}
