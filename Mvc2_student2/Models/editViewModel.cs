using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Mvc2_student2.Models
{
    public class EditViewModel
    {

        [Key]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Special Characters are not allowed.")]
        public string StudentName { get; set; }

        [Required(ErrorMessage = "Address is Required")]
        public string StudentAddress { get; set; }

        [Required(ErrorMessage = "Phone No is Required")]
        [RegularExpression(@"^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$", ErrorMessage = "Phone Number is not in correct formate")]
        public string StudentPhone { get; set; }

        [Required(ErrorMessage = "Class Name is Required")]
        public Nullable<int> StudentClassId { get; set; }
        public int ClassId { get; set; }

        [Required(ErrorMessage = "Class Name is Required")]
        public string ClassName { get; set; }
        public int MapId { get; set; }
        public Nullable<int> MapStudentId { get; set; }
        public Nullable<int> MapSubjectId { get; set; }
        public int SubjectId { get; set; }

        [Required(ErrorMessage = "This field can not be empty.")]
        public string SubjectName { get; set; }
        public bool IsChecked { get; internal set; } 
    }
}