﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{    
    [Table("Tutor", Schema = "dbo")]
    public class Tutor
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Account")]
        public int AccountId { get; set; }

        public string Experience { get; set; }
        public string Education { get; set; }
        public string Description { get; set; }

        public virtual Account Account { get; set; }
        public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
        public virtual ICollection<TutorSubject> TutorSubjects { get; set; } = new List<TutorSubject>();
        public virtual ICollection<TutorGrade> TutorGrades { get; set; } = new List<TutorGrade>();
        public virtual ICollection<TutorArea> TutorAreas { get; set; } = new List<TutorArea>();
        public virtual ICollection<RentalService> RentalServices { get; set; } = new List<RentalService>();

        public string GetGrade() {
            string gradeList = "";
            foreach (var item in TutorGrades)
            {
                gradeList = gradeList + item.Grade.GradeName + ", ";
            }
            return gradeList; 
        }
        public string GetDistrict()
        {
            string districtList = "";
            foreach (var item in TutorAreas)
            {
                districtList = districtList + item.District.DistrictName + ", ";
            }
            return districtList;
        }

        public string GetFullName()
        {
            return Account.FullName;
        }
    }
}
