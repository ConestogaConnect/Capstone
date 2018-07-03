using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConestogaConnect.ViewModels
{
    public class DiscussionsVM
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public DateTime Posted_Date { get; set; }
    }

    public class CommentsVM
    {
        public int Id { get; set; }
        public string CommentMessage { get; set; }
        public System.DateTime CommentDate { get; set; }
        public DiscussionsVM Discussions { get; set; }
        public UserVM Users { get; set; }
    }

    public class UserVM
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string imageProfile { get; set; }
    }

    public class SubCommentsVM
    {
        public int Id { get; set; }
        public string CommentMessage { get; set; }
        public DateTime CommentDate { get; set; }
        public CommentsVM Comment { get; set; }
        public UserVM User { get; set; }
    }


}