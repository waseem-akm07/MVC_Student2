//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mvc2_student2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Mapping
    {
        public int MapId { get; set; }
        public Nullable<int> MapStudentId { get; set; }
        public Nullable<int> MapSubjectId { get; set; }
    
        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
