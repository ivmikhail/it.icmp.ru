using System.Linq;
using ITCommunity.Db;
using System.Collections.Generic;
using ITCommunity.Core;
using System;

namespace ITCommunity.Db.Tables {

    public static class Comments {

        public static List<Comment> GetLastComments() {
            using (var db = Database.Connect()) {
                int count = Config.GetInt("LastCommentsCount");

                var result =
                    from comment in db.Comments
                    orderby comment.CreateDate descending
                    select comment;

                return result.Take(count).ToList();
            }
        }


        public static List<Comment> GetLasts(int count) {
            using (var db = Database.Connect()) {
                var comments =
                    from comment in db.Comments
                    orderby comment.CreateDate descending
                    select comment;

                return comments.Take(count).ToList();
            }
        }


        public static Comment Add(Comment comment) {
            using (var db = Database.Connect()) {

                comment.UserId = CurrentUser.User.Id;
                comment.Ip = CurrentUser.Ip;
                comment.CreateDate = DateTime.Now;

                db.Comments.InsertOnSubmit(comment);
                db.SubmitChanges();

                comment.Post.CommentsCount++;
                db.SubmitChanges();

                return comment;
            }
        }


    }
}
