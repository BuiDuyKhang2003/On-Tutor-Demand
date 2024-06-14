using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class TutorArea
    {
        public int Id { get; set; }
        public int DistrictId { get; set; }
        public int TutorId { get; set; }

        public virtual District District { get; set; }
        public virtual Tutor Tutor { get; set; }
    }

}
