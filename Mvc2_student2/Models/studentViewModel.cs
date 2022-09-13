using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Mvc2_student2.Models
{
    public class StudentViewModel
    {
        //public tb2_student getStudent { get; set; }
        //public tb2_class getClass { get; set; }
        //public tb2_mapping getMapping { get; set; }
        //public tb2_subject getSubject { get; set; }


        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentAddress { get; set; }
        public string StudentPhone { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int MapId { get; set; }
        public int MapStudentId { get; set; }
        public int MapSubjectId { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public List<UserSubjectsModel> UserSubjects { get; set; }      
    }

    public class UserSubjectsModel
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
    }
}