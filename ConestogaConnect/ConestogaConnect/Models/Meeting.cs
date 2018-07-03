//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConestogaConnect.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;

    public partial class Meeting
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Title")]
        public string MeetingTitle { get; set; }
        public string Description { get; set; }
        public string Subject { get; set; }
        [DisplayName("Date & Time")]
        public Nullable<System.DateTime> MeetingDateTime { get; set; }
        public string Location { get; set; }
        public Nullable<int> Program { get; set; }
        [Range(1,10)]
        public Nullable<int> Semester { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}