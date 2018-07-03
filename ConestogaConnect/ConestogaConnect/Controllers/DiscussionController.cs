using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConestogaConnect.Models;
using ConestogaConnect.ViewModels;
using Microsoft.AspNet.Identity;

namespace ConestogaConnect.Controllers
{
    public class DiscussionController : Controller
    {

        private DiscussionsEntities db = new DiscussionsEntities();

        [HttpGet]
        public ActionResult GetUsers()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetPosts(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IQueryable<DiscussionsVM> Discussion = db.Discussions.
                                                 Where(p => p.Id == id)
                                                 .Select(p => new DiscussionsVM
                                                 {
                                                     Id = p.Id,
                                                     Topic = p.Topic,
                                                     Posted_Date = p.Posted_Date
                                                 }).AsQueryable();
                                                 
            //Discussion discussion = db.Discussions.Find(id);

            return View(Discussion);
        }


        public PartialViewResult GetComments(int postId)
        {
            Console.WriteLine(postId);
            IQueryable<CommentsVM> DiscussionComment = db.DiscussionComments.Where(c => c.Discussion.Id == postId)
                                     .Select(c => new CommentsVM
                                     {
                                         Id = c.Id,
                                         CommentDate = c.CommentDate,
                                         CommentMessage = c.CommentMessage,
                                         Users = new UserVM
                                         {
                                             UserId = c.AspNetUser.Id,
                                             UserName = c.AspNetUser.UserName,
                                             imageProfile = ""
                                         }
                                     }).AsQueryable();

            Console.WriteLine(DiscussionComment);

            return PartialView("~/Views/Discussion/_MyComments.cshtml", DiscussionComment);
        }

        [HttpPost]
        public ActionResult AddComment(string comment, int postId)
        {
            //bool result = false;  
            //DiscussionComment commentEntity = null;
            string userId = User.Identity.GetUserId();
            //Console.WriteLine(postId);

            //var user = db.AspNetUsers.FirstOrDefault(u => u.Id == userId);
            //var post = db.Discussions.FirstOrDefault(p => p.Id == postId);

            string commentMsg = Request.Form["commentMsg"];

            //Console.WriteLine(commentMsg);

            if (commentMsg != null)
            {

                var commentEntity = new DiscussionComment
                {
                    CommentMessage = commentMsg,
                    // CommentDate = comment.CommentDate,
                    CommentDate = DateTime.Now,
                    UserId = userId,
                    DiscussionId= postId
                };

                //Console.WriteLine(commentEntity);

                db.DiscussionComments.Add(commentEntity);
                
                    db.DiscussionComments.Add(commentEntity);
                    //db.SaveChangesAsync();
                    db.SaveChanges();
                

               // db.SaveChanges();


                /*if (user != null && post != null)
                {
                    //post.DiscussionComments.Add(commentEntity);
                    //user.DiscussionComments.Add(commentEntity);

                    db.DiscussionComments.Add(commentEntity);

                    db.SaveChanges();
                    //result = true;  
                }*/
            }

            return RedirectToAction("GetComments", "Discussion", new { postId = postId });
        }

        [HttpGet]
        public PartialViewResult GetSubComments(int ComID)
        {
            IQueryable<SubCommentsVM> subComments = db.SubComments.Where(sc => sc.DiscussionComment.Id == ComID)
                                       .Select(sc => new SubCommentsVM
                                       {
                                           Id = sc.Id,
                                           CommentMessage = sc.CommentMessage,
                                           CommentDate = sc.CommentedDate,
                                           User = new UserVM
                                           {
                                               UserId = sc.AspNetUser.Id,
                                               UserName = sc.AspNetUser.UserName,
                                               imageProfile = ""

                                            
                                           }
                                       }).AsQueryable();

            Console.WriteLine(subComments);

            return PartialView("~/Views/Discussion/_MySubComments.cshtml", subComments);

        }

        [HttpPost]
        public ActionResult AddSubComment(string subComment, int ComID)
        {
            
              // add sub comment
                string userId = User.Identity.GetUserId();

                string commentMsg = Request.Form["commentMsg"];

                Console.WriteLine(commentMsg+ "id is"+ userId+ ComID);

                if (commentMsg != null)
                {

                    var commentEntity = new SubComment
                    {
                        CommentMessage = commentMsg,
                        CommentedDate = DateTime.Now,
                        UserId = userId,
                        CommentId = ComID
                    };

                    //Console.WriteLine(commentEntity);

                    db.SubComments.Add(commentEntity);
                    //db.SaveChangesAsync();
                    db.SaveChanges();


                }

            return RedirectToAction("GetSubComments", "Discussion", new { ComID = ComID });

        }


        // GET: Meetings/Delete/5
        public ActionResult Delete(int id)
        {
            /* if (id == null)
             {
                 return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
             }
             if (comment == null)
             {
                 return HttpNotFound();
             }*/
            DiscussionComment comment = db.DiscussionComments.Find(id);

            return View("GetPosts");
            //return RedirectToAction("GetComments", "Discussion", new { postId = comment.DiscussionId });
            //return RedirectToAction("Delete", "Discussion");
            //return PartialView("~/Views/Discussion/GetComments.cshtml",  new { postId =comment.DiscussionId  });
        }

        // POST: Meetings/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var subComments = db.SubComments.Where(x => x.CommentId == id).ToList();
            for (var i = 0; i < subComments.Count(); i++)
            {
                db.SubComments.Remove(subComments[i]);
                db.SaveChanges();
            }

            DiscussionComment comment = db.DiscussionComments.Find(id);
            db.DiscussionComments.Remove(comment);
            db.SaveChanges();

            IQueryable<CommentsVM> DiscussionComment = db.DiscussionComments.Where(c => c.Discussion.Id == comment.DiscussionId)
                                    .Select(c => new CommentsVM
                                    {
                                        Id = c.Id,
                                        CommentDate = c.CommentDate,
                                        CommentMessage = c.CommentMessage,
                                        Users = new UserVM
                                        {
                                            UserId = c.AspNetUser.Id,
                                            UserName = c.AspNetUser.UserName,
                                            imageProfile = ""
                                        }
                                    }).AsQueryable();

            IQueryable<DiscussionsVM> Discussion = db.Discussions
                                                .Select(p => new DiscussionsVM
                                                {
                                                    Id = p.Id,
                                                    Topic = p.Topic,
                                                    Posted_Date = p.Posted_Date
                                                }).AsQueryable();
                                                
            //return View("~/Views/Discussion/GetPosts.cshtml", Discussion);
            // return View("GetPosts");
            //return RedirectToAction("GetComments", "Discussion", new { postId = comment.DiscussionId });
            return PartialView("~/Views/Discussion/_MyComments.cshtml", DiscussionComment);
            //return PartialView("~/Views/Discussion/_MySubComments.cshtml", subComments);
            //return View("GetPosts");
        }

        public ActionResult DeleteSubComment(int id)
        {
            //SubComment comment = db.SubComments.Where(x => x.Id == id).Single();

            var comment = db.SubComments.Single(x => x.Id == id);
            Console.WriteLine(comment);
            db.SubComments.Remove(comment);

            bool saveFailed;
            do
            {
                saveFailed = false;
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    // Update the values of the entity that failed to save from the store 
                    ex.Entries.Single().Reload();
                }
            } while (saveFailed);
            //return View("GetPosts");
            return RedirectToAction("GetSubComments", "Discussion", new { ComID = comment.CommentId });
            //return RedirectToAction("Delete", "Discussion");
            //return PartialView("~/Views/Discussion/GetComments.cshtml",  new { postId =comment.DiscussionId  });
        }

    }
}