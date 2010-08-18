using System.Collections.Generic;
using System.Linq;

using ITCommunity.Core;


namespace ITCommunity.Db.Tables {

    public static class Comments {

        public static List<Comment> GetLasts() {
            var count = Config.GetInt("LastCommentsCount");

            return AppCache.Get("LastComments", () => GetLasts(count));
        }

        public static List<Comment> GetLasts(int count) {
            using (var db = Database.Connect()) {
                var comments =
                    from com in db.Comments
                    orderby com.CreateDate descending
                    select com;

                return comments.Take(count).ToList();
            }
        }

        public static Comment Add(Comment comment) {
            using (var db = Database.Connect()) {
                db.Comments.InsertOnSubmit(comment);
                db.SubmitChanges();

                comment.Post.CommentsCount++;
                comment.Author.CommentsCount++;
                db.SubmitChanges();

                return comment;
            }
        }

        public static void Update(Comment editedComment) {
            using (var db = Database.Connect()) {
                var comment = (
                    from com in db.Comments
                    where com.Id == editedComment.Id
                    select com
                ).Single();

                comment.Text = editedComment.Text;
                db.SubmitChanges();
            }
        }

        public static void Delete(int id) {
            using (var db = Database.Connect()) {
                var comment = (
                    from com in db.Comments
                    where com.Id == id
                    select com
                ).Single();

                comment.Post.CommentsCount--;
                comment.Author.CommentsCount--;
                db.SubmitChanges();

                db.Comments.DeleteOnSubmit(comment);
                db.SubmitChanges();
            }
        }

        public static Comment Get(int id) {
            using (var db = Database.Connect()) {
                var comment = (
                    from com in db.Comments
                    where com.Id == id
                    select com
                ).SingleOrDefault();

                return comment;
            }
        }

        public static List<Comment> GetPagedByUser(int authorId, int page, int count, ref int totalCount) {
            using (var db = Database.Connect()) {
                var comments =
                    from com in db.Comments
                    where com.AuthorId == authorId
                    orderby com.CreateDate descending
                    select com;

                return comments.Paged(page, count, ref totalCount);
            }
        }
    }
}
