﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Grade
    {
        public int Id { get; set; }
        public string GradeName { get; set; }

        public virtual ICollection<TutorGrade> TutorGrades { get; set; }
    }

}
