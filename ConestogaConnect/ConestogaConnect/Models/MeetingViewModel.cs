using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ConestogaConnect.Models
{
    public class MeetingViewModel
    {
        public int Id { get; set; }
       
        [DisplayName("Title")]
        public string MeetingTitle { get; set; }
        public string Description { get; set; }
        public string Subject { get; set; }
        [DisplayName("Date & Time")]
        public Nullable<System.DateTime> MeetingDateTime { get; set; }
        public string Location { get; set; }
        public Nullable<int> Program { get; set; }
        public string ProgramName { get; set; }
        
        public Nullable<int> Semester { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public bool IsAccepted { get; set; }
    }
}