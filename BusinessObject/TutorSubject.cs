using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class TutorSubject
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public int TutorId { get; set; }

        public virtual Subject Subject { get; set; }
        public virtual Tutor Tutor { get; set; }
    }

}
