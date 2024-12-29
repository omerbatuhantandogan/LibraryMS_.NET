﻿using System.ComponentModel.DataAnnotations;

namespace BLL.DAL
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Student name is required!")]
        [StringLength(20)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required!")]
        [StringLength(20, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string Surname { get; set; }
        public DateTime? BirthDate { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }

        public List<TeacherStudent> TeacherStudents { get; set; } = new List<TeacherStudent>();
    }
}
