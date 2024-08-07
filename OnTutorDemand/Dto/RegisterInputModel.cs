﻿using BusinessObject;
using System.ComponentModel.DataAnnotations;

namespace OnTutorDemand.Dto
{
    public class RegisterInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Role { get; set; }

        // Tutor-specific fields
        public string Experience { get; set; }
        public string Education { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ít nhất một lớp dạy")]
        public IEnumerable<int> SelectedGrades { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ít nhất một môn học")]
        public IEnumerable<int> SelectedSubjects { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ít nhất một khu vực dạy")]
        public IEnumerable<int> SelectedAreas { get; set; }

    }
}
