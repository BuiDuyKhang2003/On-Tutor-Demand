using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class District
    {
        public int Id { get; set; }
        public string DistrictName { get; set; }

        public virtual ICollection<TutorArea> TutorAreas { get; set; }
    }

}
