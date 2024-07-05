using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Subject
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }

        public virtual ICollection<TutorSubject> TutorSubjects { get; set; }
    }

}
