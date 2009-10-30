ALTER PROCEDURE [dbo].[CommentAdd]
	@post_id int,
	@user_id int,
	@ip nvarchar(50),
	@text nvarchar(2048)
AS

UPDATE posts
SET comments_count = comments_count + 1
WHERE id = @post_id

INSERT INTO comments(post_id, user_id, ip, cdate, text)
VALUES(@post_id, @user_id, @ip, GETDATE(), @text)

SELECT * FROM comments WHERE id = @@IDENTITY