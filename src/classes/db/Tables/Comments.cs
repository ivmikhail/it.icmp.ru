using System.Collections.Generic;
using System.Linq;

using ITCommunity.Core;


namespace ITCommunity.DB.Tables {

    public static class Comments {

        public const string LAST_CACHE_KEY = "LastComments";

        public static List<Comment> GetLast() {
            int count = Config.LastCommentsCount;

            return AppCache.Get(LAST_CACHE_KEY, () => GetLast(count));
        }

        public static List<Comment> GetLast(int count) {
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

                if (comment.AuthorId > 0) {
                    var author = (
                        from usr in db.Users
                        where usr.Id == comment.AuthorId
                        select usr
                        ).Single();

                    author.CommentsCount++;
                }

                db.SubmitChanges();

                AppCache.Remove(LAST_CACHE_KEY);

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

                AppCache.Remove(LAST_CACHE_KEY);
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

                if (comment.AuthorId > 0) {
                    var author = (
                        from usr in db.Users
                        where usr.Id == comment.AuthorId
                        select usr
                        ).Single();

                    author.CommentsCount--;
                }

                db.SubmitChanges();

                db.Comments.DeleteOnSubmit(comment);
                db.SubmitChanges();

                AppCache.Remove(LAST_CACHE_KEY);
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
