IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CaptchaAnswersList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CaptchaAnswersList]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CaptchaAnswerDelete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CaptchaAnswerDelete]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CaptchaAnswerAdd]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CaptchaAnswerAdd]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PostAttachCategories]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PostAttachCategories]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FavoriteDel]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[FavoriteDel]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PostIsFavorite]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PostIsFavorite]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PollIsUserVoted]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PollIsUserVoted]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PollGetAnswers]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PollGetAnswers]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HeaderTextUpdateShowEndDate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[HeaderTextUpdateShowEndDate]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HeaderTextUpdateShowBeginDate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[HeaderTextUpdateShowBeginDate]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HeaderTextGetCurrents]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[HeaderTextGetCurrents]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HeaderTextGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[HeaderTextGet]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HeaderTextDel]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[HeaderTextDel]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HeaderTextAdd]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[HeaderTextAdd]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PostGetTopByRating]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PostGetTopByRating]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PollAdd]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PollAdd]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PollGetLast]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PollGetLast]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PollGetById]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PollGetById]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PollGetNext]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PollGetNext]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PollGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PollGet]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PollDel]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PollDel]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PollVote]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PollVote]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PostDel]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PostDel]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FavoriteAdd]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[FavoriteAdd]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FavoriteGetByUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[FavoriteGetByUser]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CommentAdd]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CommentAdd]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CommentDel]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CommentDel]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PostUpdateViews]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PostUpdateViews]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PostAdd]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PostAdd]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PostGetByAuthor]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PostGetByAuthor]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PostGetAttached]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PostGetAttached]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PostGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PostGet]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserGetLastTopPosters]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UserGetLastTopPosters]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserGetTopPosters]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UserGetTopPosters]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PostUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PostUpdate]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PostSearch]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PostSearch]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PostGetByCat]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PostGetByCat]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PostGetById]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PostGetById]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PostGetTopByViews]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PostGetTopByViews]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PostGetLast]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PostGetLast]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NotesGetById]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[NotesGetById]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NotesGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[NotesGet]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NotesDel]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[NotesDel]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NotesAdd]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[NotesAdd]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PostGetCategories]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PostGetCategories]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CategoryUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CategoryUpdate]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CategoryGetById]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CategoryGetById]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CategoryGetAll]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CategoryGetAll]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CategoryDel]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CategoryDel]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CategoryAdd]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CategoryAdd]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RatingGetByEntity]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RatingGetByEntity]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RatingAdd]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RatingAdd]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RatingUpdateValue]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RatingUpdateValue]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RatingLogGetByEntity]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RatingLogGetByEntity]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RatingLogAdd]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RatingLogAdd]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CommentGetLasts]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CommentGetLasts]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CommentGetByPost]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CommentGetByPost]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CommentGetByAuthor]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CommentGetByAuthor]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MessageMarkAsRead]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MessageMarkAsRead]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MessageGetNewCount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MessageGetNewCount]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MessageGetBySender]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MessageGetBySender]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MessageGetByReceiver]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MessageGetByReceiver]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MessageGetById]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MessageGetById]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MessageDelBySender]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MessageDelBySender]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MessageDelByReceiver]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MessageDelByReceiver]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MessageAdd]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MessageAdd]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MenuItemsUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MenuItemsUpdate]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MenuItemsGetById]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MenuItemsGetById]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MenuItemsGetAll]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MenuItemsGetAll]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MenuItemsDel]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MenuItemsDel]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MenuItemsAdd]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MenuItemsAdd]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MenuItemsGetByParent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MenuItemsGetByParent]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RecoveryPurgeOldTasks]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RecoveryPurgeOldTasks]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RecoveryGetByIdentifier]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RecoveryGetByIdentifier]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RecoveryDel]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RecoveryDel]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RecoveryAdd]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RecoveryAdd]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RfcSearchByTitle]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RfcSearchByTitle]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RfcGetByNum]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RfcGetByNum]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserGetStat]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UserGetStat]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserGetByLogin]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UserGetByLogin]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserGetById]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UserGetById]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserGetByEmail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UserGetByEmail]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserGetBlocked]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UserGetBlocked]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserDel]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UserDel]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserAdd]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UserAdd]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UserUpdate]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserGetLastRegistered]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UserGetLastRegistered]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserGetByRole]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UserGetByRole]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PollGetAnswerVoters]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PollGetAnswerVoters]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CaptchaGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CaptchaGet]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CaptchaDelete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CaptchaDelete]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CaptchaQuestionUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CaptchaQuestionUpdate]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CaptchaQuestionsList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CaptchaQuestionsList]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CaptchaQuestionGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CaptchaQuestionGet]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CaptchaQuestionAdd]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CaptchaQuestionAdd]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CaptchaAnswerUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CaptchaAnswerUpdate]

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PollGetLast]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PollGetLast]
AS
BEGIN
	SELECT * 
	FROM vote_polls 
	ORDER BY cdate DESC
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PollGetById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PollGetById]
	@id int
AS
SELECT * FROM vote_polls WHERE id = @id;



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PollGetNext]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PollGetNext]
	@id int
AS
SELECT TOP(1) * 
FROM vote_polls 
WHERE id > @id 
ORDER BY id' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PollGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PollGet]
	@page int,
	@count int,
	@polls_count int out
AS
DECLARE @start int
DECLARE @end int
SET @start = (@page-1)*@count
SET @end = (@page*@count) + 1;
SET @polls_count = (SELECT COUNT(*) FROM vote_polls)

SELECT * FROM
(	SELECT ROW_NUMBER() OVER (ORDER BY id DESC) as row_num, *
	FROM vote_polls
) t
WHERE t.row_num > @start AND t.row_num < @end



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PollDel]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[PollDel]
	@poll_id int
AS
DELETE FROM vote_polls WHERE id = @poll_id' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PollAdd]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[PollAdd]
	@topic nvarchar(120),	
	@author_id int,
	@is_multiselect  int,	
	@is_open int,
	@answers nvarchar(1024) -- "answer1, answer2, answer3"
AS
BEGIN

DECLARE @poll_id int

INSERT INTO vote_polls(topic, is_multiselect, cdate, author_id, is_open, votes_count)
VALUES(@topic, @is_multiselect, GETDATE(), @author_id, @is_open, 0);

SET @poll_id = (SELECT id FROM vote_polls WHERE id = @@IDENTITY);

-- SplitString самописная функция, постарайтесь не юзать ее :)
-- Оказывается в MS SQL нету стандартной функции сплита текста!   
    
INSERT INTO vote_answers([text], poll_id, vote_count)
SELECT data AS [text], @poll_id AS poll_id, 0 AS vote_count FROM SplitString(@answers, '','');

SELECT * FROM vote_polls WHERE id = @poll_id;
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PollVote]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[PollVote]
	@poll_id int,
	@user_id int,
	@answers nvarchar(1024)	
AS

UPDATE vote_polls 
SET votes_count = votes_count + 1
WHERE id = @poll_id;
		  
EXEC(''UPDATE vote_answers SET vote_count = vote_count + 1 WHERE id IN ('' + @answers + '')'');

INSERT INTO vote_users([user_id], poll_id, answer_id, cdate)
SELECT @user_id, @poll_id, data as answer_id, GETDATE() FROM SplitString(@answers, '','');

/*
 SplitString самописная функция, постарайтесь не юзать ее :)
 Оказывается в MS SQL нету стандартной функции сплита текста! 

 Так же данная функция юзается в процедуре PollAdd
*/  


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PostDel]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PostDel]
	@id int
AS

UPDATE users
SET posts_count = posts_count - 1
WHERE id = (SELECT user_id FROM posts WHERE id = @id);

UPDATE users 
SET comments_count = comments_count - 1
WHERE id IN (SELECT user_id FROM comments WHERE post_id = @id);

DELETE FROM posts WHERE id = @id' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PostGetTopByRating]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PostGetTopByRating]
	@period int,
	@count int
AS

SELECT TOP(10) posts.*
FROM posts, ratings
WHERE 
	DATEDIFF(dd, cdate, GETDATE()) < @period AND
	posts.id = ratings.entity_id AND
	ratings.entity_type = 1 AND
	ratings.[value] > 0
ORDER BY ratings.[value] DESC' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PostUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PostUpdate]
	@id int,
	@title nvarchar(128),
	@desc nvarchar(max),
	@text nvarchar(max),
	@attached tinyint,
	@source nvarchar(1024),
	@comments_count int
AS
	UPDATE posts
	SET title = @title,
		[description] = @desc,
		[text] = @text,
		attached = @attached,
		source = @source,
		comments_count = @comments_count
   WHERE id = @id' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PostSearch]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PostSearch]
	@query nvarchar(512),
	@page int,
	@count int,
	@posts_count int out
AS
DECLARE @start int
DECLARE @end int
SET @start = (@page-1)*@count
SET @end = (@page*@count) + 1;
SET @posts_count = (SELECT COUNT(*) 
					FROM posts 
					WHERE 
					title LIKE ''%'' + @query + ''%'');

SELECT * FROM
(	SELECT ROW_NUMBER() OVER (ORDER BY id DESC) as row_num, *
	FROM posts
	WHERE title LIKE ''%'' + @query + ''%''
) t
WHERE t.row_num > @start AND t.row_num < @end



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PostGetByCat]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PostGetByCat]
	@page int,
	@count int,
	@cat_id int,
	@posts_count int out
AS
DECLARE @start int
DECLARE @end int
SET @start = (@page-1)*@count
SET @end = (@page*@count) + 1;

SET @posts_count = (SELECT COUNT(*) 
					FROM posts 
					WHERE attached = 0 AND 
					            id IN (SELECT post_id 
									   FROM post_cat 
									   WHERE cat_id=@cat_id))
									   
SELECT 1 as row_num,* 
FROM posts 
WHERE attached = 1
	UNION
SELECT * 
FROM
	(	SELECT ROW_NUMBER() OVER (ORDER BY id DESC) as row_num, *
		FROM posts
		WHERE attached = 0 AND id IN (SELECT post_id 
									 FROM post_cat 
									 WHERE cat_id=@cat_id )
	) t
WHERE t.row_num > @start AND t.row_num < @end

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PostGetTopByViews]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PostGetTopByViews]
	@period int,
	@count int
AS

SELECT TOP(@count) * 
FROM posts 
WHERE DATEDIFF(dd, cdate, GETDATE()) < @period
ORDER BY [views] DESC' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PostGetLast]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PostGetLast]
	@count int
AS
SELECT TOP(@count) * 
FROM posts
ORDER BY cdate DESC



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FavoriteGetByUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[FavoriteGetByUser]
	@user_id int,
	@page int,
	@count int,
	@posts_count int out
AS
DECLARE @start int
DECLARE @end int
SET @start = (@page-1)*@count
SET @end = (@page*@count) + 1;
SET @posts_count = (SELECT COUNT(*) FROM favorites WHERE [user_id] = @user_id)

SELECT * FROM
(	SELECT ROW_NUMBER() OVER (ORDER BY favorites.cdate DESC) as row_num, posts.*
	FROM posts, favorites
	WHERE posts.id = favorites.post_id AND favorites.user_id = @user_id
) t
WHERE t.row_num > @start AND t.row_num < @end
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FavoriteAdd]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[FavoriteAdd]
	@user_id int,
	@post_id int
AS
INSERT INTO favorites([user_id], post_id, cdate)
VALUES(@user_id, @post_id, GETDATE())

SELECT * FROM posts WHERE id = @post_id' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CommentAdd]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[CommentAdd]
	@post_id int,
	@user_id int,
	@ip nvarchar(50),
	@text nvarchar(2048)
AS

UPDATE posts
SET comments_count = comments_count + 1
WHERE id = @post_id

UPDATE users
SET comments_count = comments_count + 1
WHERE id = @user_id

INSERT INTO comments(post_id, user_id, ip, cdate, text)
VALUES(@post_id, @user_id, @ip, GETDATE(), @text)

SELECT * FROM comments WHERE id = @@IDENTITY' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CommentDel]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[CommentDel]
	@id int
AS

UPDATE users
SET comments_count = comments_count - 1
WHERE id = (SELECT user_id from comments WHERE id = @id)

UPDATE posts 
SET comments_count = comments_count - 1
WHERE id = (SELECT post_id 
			FROM comments
		    WHERE id = @id)

DELETE FROM comments WHERE id = @id' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PostGetByAuthor]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PostGetByAuthor]
	@page int,
	@count int,
	@author_id int,
	@posts_count int out
AS
DECLARE @start int
DECLARE @end int
SET @start = (@page-1)*@count
SET @end = (@page*@count) + 1;

SET @posts_count = (SELECT COUNT(*) 
					FROM posts 
					WHERE user_id = @author_id)
									   
SELECT * 
FROM
	(	SELECT ROW_NUMBER() OVER (ORDER BY id DESC) as row_num, *
		FROM posts
		WHERE user_id = @author_id
	) t
WHERE t.row_num > @start AND t.row_num < @end

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PostGetAttached]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PostGetAttached]
AS
SELECT *
FROM posts
WHERE attached = 1
ORDER BY cdate DESC' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PostGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PostGet]
	@page int,
	@count int,
	@posts_count int out
AS
DECLARE @start int;
DECLARE @end int;

SET @start = (@page-1)*@count;
SET @end = (@page*@count) + 1;
SET @posts_count = (SELECT COUNT(*) FROM posts WHERE attached = 0);

SELECT 1 as row_num,* 
FROM posts 
WHERE attached = 1 
	UNION
SELECT * FROM
(	SELECT ROW_NUMBER() OVER (ORDER BY id DESC) as row_num, *
	FROM posts
	WHERE attached = 0
) t
WHERE t.row_num > @start AND t.row_num < @end



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserGetLastTopPosters]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UserGetLastTopPosters]
	@count int,
	@days int
AS
	SELECT TOP(@count) users.nick as usernick, count(posts.id) AS postcount
	FROM users INNER JOIN posts 
	ON
		users.id = posts.[user_id]
	WHERE 
		posts.cdate > DATEADD(dd, @days, GETDATE())
    GROUP BY users.nick ORDER BY postcount DESC
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PostUpdateViews]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PostUpdateViews]
	@id int
AS
	UPDATE posts
	SET [views] = [views] + 1 
   WHERE id = @id' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PostAdd]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PostAdd]
	@title nvarchar(128),
	@desc nvarchar(max),
	@text nvarchar(max),
	@attached tinyint,
	@source nvarchar(1024),
	@user_id int
AS
	UPDATE users
	SET posts_count = posts_count + 1
	WHERE id = @user_id

	INSERT INTO posts(title, [description], [text], cdate, [user_id], attached, [views], source, comments_count)
	VALUES(@title, @desc, @text, GETDATE(), @user_id, @attached, 0, @source, 0)

	SELECT * FROM posts WHERE id = @@IDENTITY ' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserGetTopPosters]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UserGetTopPosters]
	@count int
AS
	SELECT TOP(@count) users.nick as usernick, count(posts.id) AS postcount
	FROM users INNER JOIN posts ON
    users.id = posts.[user_id]
    GROUP BY users.nick ORDER BY postcount DESC
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PostGetById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PostGetById]
	@id int
AS
	SELECT * FROM posts WHERE id = @id' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NotesGetById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[NotesGetById]
	@id int
AS

SELECT * FROM notes WHERE id = @id




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NotesGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[NotesGet]
	@user_id int, 
	@page int,
	@count int,
	@posts_count int out
AS
DECLARE @start int
DECLARE @end int
SET @start = (@page-1)*@count
SET @end = (@page*@count) + 1;
SET @posts_count = (SELECT COUNT(*) FROM notes WHERE [user_id] = @user_id)

SELECT * FROM
(	SELECT ROW_NUMBER() OVER (ORDER BY id DESC) as row_num, *
	FROM notes
	WHERE [user_id] = @user_id
) t
WHERE t.row_num > @start AND t.row_num < @end




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NotesDel]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[NotesDel]
	@id int
AS
DELETE FROM notes WHERE id = @id' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NotesAdd]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[NotesAdd]
	@user_id int,
	@title nvarchar(256),
	@text  nvarchar(1024),
	@cdate datetime
AS
INSERT INTO notes(title, [text], [user_id], cdate)
VALUES(@title, @text, @user_id,GETDATE())

SELECT * FROM notes WHERE id = @@IDENTITY
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PostGetCategories]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PostGetCategories]
	@post_id int
AS
SELECT * 
FROM categories 
WHERE id IN (SELECT cat_id 
			 FROM post_cat 
		     WHERE post_id = @post_id)' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CategoryUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[CategoryUpdate]
	@id int,
	@sort int,
	@name nvarchar(32)
AS
	UPDATE categories
	SET [name] = @name,
		sort = @sort
   WHERE id = @id' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CategoryGetById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[CategoryGetById]
	@id int
AS
SELECT * FROM categories WHERE id = @id' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CategoryGetAll]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[CategoryGetAll]
AS
SELECT * FROM categories ORDER BY sort' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CategoryDel]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[CategoryDel]
	@id int
AS
DELETE FROM categories WHERE id = @id' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CategoryAdd]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[CategoryAdd]
	@name nvarchar(32),
	@sort int
AS
INSERT INTO categories(name, sort)
VALUES(@name, @sort)

SELECT * FROM categories WHERE id = @@IDENTITY' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RatingGetByEntity]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[RatingGetByEntity]
	@entity_id int,
	@entity_type int
AS
SELECT * 
FROM ratings
WHERE entity_id = @entity_id AND entity_type = @entity_type
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RatingAdd]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[RatingAdd]
	@entity_id int,
	@entity_type int,
	@value int
AS
INSERT INTO ratings(entity_id, entity_type, value)
VALUES(@entity_id, @entity_type, @value)

SELECT * FROM ratings WHERE id = @@IDENTITY
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RatingUpdateValue]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[RatingUpdateValue]
	@id int,
	@value int
AS
	UPDATE [ratings]
	SET [value] = @value
	WHERE id = @id
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RatingLogGetByEntity]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[RatingLogGetByEntity]
	@entity_id int,
	@entity_type int,
	@user_id int
AS
SELECT * 
FROM rating_logs
WHERE entity_id = @entity_id AND entity_type = @entity_type AND user_id = @user_id

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RatingLogAdd]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[RatingLogAdd]
	@entity_id int,
	@entity_type int,
	@user_id int,
	@value int
AS
INSERT INTO rating_logs(entity_id, entity_type, user_id, value)
VALUES(@entity_id, @entity_type, @user_id, @value)

SELECT * FROM rating_logs WHERE id = @@IDENTITY
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CommentGetLasts]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[CommentGetLasts]
	@count int
AS
SELECT TOP(@count) *
FROM comments
ORDER BY cdate DESC' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CommentGetByPost]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[CommentGetByPost]
	@id int
AS
SELECT * FROM comments WHERE post_id = @id' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CommentGetByAuthor]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[CommentGetByAuthor]
	@author_id int,
	@page int,
	@count int,
	@total_records int out
AS
DECLARE @start int
DECLARE @end int
SET @start = (@page-1)*@count
SET @end = (@page*@count) + 1;
SET @total_records  = (SELECT COUNT(id) FROM comments WHERE user_id = @author_id)

SELECT * FROM
(	
	SELECT ROW_NUMBER() OVER (ORDER BY id DESC) as row_num, *
	FROM comments
	WHERE user_id = @author_id
) t
WHERE t.row_num > @start AND t.row_num < @end
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MessageMarkAsRead]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[MessageMarkAsRead]
	@id int
AS
UPDATE messages SET receiver_read = 1 WHERE id = @id' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MessageGetNewCount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[MessageGetNewCount]
	@receiver_id int
AS
SELECT COUNT(*) 
FROM messages
WHERE receiver_id = @receiver_id AND receiver_read = 0' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MessageGetBySender]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[MessageGetBySender]
	@sender_id int,
	@page int,
	@count int,
	@mess_count int out
AS
DECLARE @start int
DECLARE @end int
SET @start = (@page-1)*@count
SET @end = (@page*@count) + 1;
SET @mess_count = (SELECT COUNT(*) FROM messages WHERE sender_id = @sender_id AND sender_del = 0)

SELECT * FROM
(	
	SELECT ROW_NUMBER() OVER (ORDER BY id DESC) as row_num, *
	FROM messages
	WHERE sender_id = @sender_id AND sender_del = 0
) t
WHERE t.row_num > @start AND t.row_num < @end' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MessageGetByReceiver]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[MessageGetByReceiver]
	@rec_id int,
	@page int,
	@count int,
	@mess_count int out
AS
DECLARE @start int
DECLARE @end int
SET @start = (@page-1)*@count
SET @end = (@page*@count) + 1;
SET @mess_count = (SELECT COUNT(*) FROM messages WHERE receiver_id = @rec_id AND receiver_del = 0)

SELECT * FROM
(	
	SELECT ROW_NUMBER() OVER (ORDER BY id DESC) as row_num, *
	FROM messages
	WHERE receiver_id = @rec_id AND receiver_del = 0
) t
WHERE t.row_num > @start AND t.row_num < @end' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MessageGetById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[MessageGetById]
	@id int
AS
SELECT * 
FROM messages
WHERE id = @id' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MessageDelBySender]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[MessageDelBySender]
	@mess_id int
AS
UPDATE messages 
SET sender_del = 1 
WHERE id = @mess_id' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MessageDelByReceiver]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[MessageDelByReceiver]
	@mess_id int
AS
UPDATE messages 
SET receiver_del = 1 
WHERE id = @mess_id' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MessageAdd]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[MessageAdd]
	@receiver_id int,
	@sender_id int,
	@title nvarchar(64),
	@text nvarchar(1024)
AS
INSERT INTO messages(receiver_id, sender_id, title, [text], receiver_del, sender_del, cdate, receiver_read)
VALUES(@receiver_id, @sender_id, @title, @text, 0, 0, GETDATE(), 0)

SELECT * FROM messages WHERE id = @@IDENTITY' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MenuItemsUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[MenuItemsUpdate]
	@id int,
	@parent_id int,
	@url nvarchar(256),
	@sort int,
	@name nvarchar(32),
	@new_window tinyint
AS
	UPDATE menu_items
	SET parent_id = @parent_id,
		url = @url,
		sort = @sort,
		[name] = @name,
		new_window = @new_window
   WHERE id = @id' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MenuItemsGetById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[MenuItemsGetById]
	@id int
AS
	SELECT * FROM menu_items WHERE id = @id
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MenuItemsGetAll]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[MenuItemsGetAll]
AS
	SELECT * FROM menu_items
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MenuItemsDel]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[MenuItemsDel]
	@id int
AS
-- Меню двухуровневое!
	DECLARE @pid int
	SET @pid = (SELECT parent_id FROM menu_items WHERE id = @id)
 
	IF @pid = 0 
		BEGIN
			DELETE FROM menu_items WHERE parent_id = @id
		END

	DELETE FROM menu_items WHERE id = @id
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MenuItemsAdd]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[MenuItemsAdd]
	@parent_id int,
	@url nvarchar(256),
	@sort int,
	@name nvarchar(32),
	@new_window tinyint
AS
	INSERT INTO menu_items(parent_id, url, sort, name, new_window)
    VALUES   (@parent_id, @url, @sort, @name, @new_window)

	SELECT * FROM menu_items WHERE id = @@IDENTITY
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MenuItemsGetByParent]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[MenuItemsGetByParent]
	@parent_id int
AS
	SELECT * 
	FROM menu_items 
	WHERE parent_id = @parent_id
	ORDER BY sort
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RecoveryPurgeOldTasks]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[RecoveryPurgeOldTasks]
AS
BEGIN
	SET NOCOUNT ON;
	DELETE FROM pass_recovery WHERE DATEDIFF(d , cdate, GETDATE()) > 3
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RecoveryGetByIdentifier]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[RecoveryGetByIdentifier]
	@identifier nvarchar(1024)
AS

SELECT * FROM pass_recovery WHERE UPPER(identifier) = UPPER(@identifier) ' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RecoveryDel]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[RecoveryDel]
	@identifier nvarchar(1024)
AS

DELETE FROM pass_recovery WHERE UPPER(identifier) = UPPER(@identifier) ' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RecoveryAdd]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[RecoveryAdd]
	@identifier nvarchar(1024),
	@user_id int
AS

INSERT INTO pass_recovery(identifier, [user_id], cdate)
VALUES(@identifier, @user_id, GETDATE())
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RfcSearchByTitle]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[RfcSearchByTitle]
	@query nvarchar(512)
AS
SELECT * 
FROM rfc 
WHERE title LIKE ''%'' + @query + ''%''
 ' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RfcGetByNum]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[RfcGetByNum]
	@num nvarchar(16)
AS
SELECT * FROM rfc WHERE number = @num;
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserGetStat]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UserGetStat]
AS
	SELECT ''users'' as [key], COUNT(*) as value
	FROM users
	UNION
	SELECT ''posters'' as [key], COUNT(*) as value
	FROM users WHERE role = 2 
	UNION
	SELECT ''admins'' as [key], COUNT(*) as value
	FROM users WHERE role = 1' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserGetByLogin]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UserGetByLogin]
	@login nvarchar(32)
AS
	SELECT * FROM Users WHERE nick = @login' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserGetById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UserGetById]
	@userId int
AS
	SELECT * FROM Users WHERE id = @userId' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserGetByEmail]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UserGetByEmail]
	@email nvarchar(512)
AS
	SELECT * FROM Users WHERE UPPER(email) = UPPER(@email)' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserGetBlocked]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UserGetBlocked]
AS
	SELECT * FROM Users WHERE can_add_header_text = 0
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserAdd]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UserAdd]
	@nick nvarchar(32),
	@pass nvarchar(1024),
	@role tinyint,
	@email nvarchar(32)
AS
	INSERT INTO users(nick, pass, cdate, [role], email)
    VALUES   (@nick, @pass, GetDate(), @role, @email)

	SELECT * FROM users WHERE id = @@IDENTITY
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UserUpdate]
	@user_id	int,
	@pass nvarchar(1024),
	@role tinyint,
	@email nvarchar(32),
	@can_add_header_text tinyint,
	@header_text_counter int
AS

UPDATE users
SET pass   = @pass,
    [role] = @role,
	email  = @email,
	can_add_header_text = @can_add_header_text,
	header_text_counter = @header_text_counter
WHERE id = @user_id;

SELECT * 
FROM users 
WHERE id = @user_id;
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserGetLastRegistered]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UserGetLastRegistered]
	@count int
AS
	SELECT TOP(@count) * FROM Users ORDER BY cdate DESC' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserDel]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UserDel]
	@userId int
AS
	DELETE FROM Users WHERE id = @userId' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserGetByRole]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UserGetByRole]
	@role int
AS
	SELECT * FROM Users WHERE role = @role' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PollGetAnswerVoters]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PollGetAnswerVoters]
	@answer_id int
AS
SELECT * 
FROM users 
WHERE id IN (
			 SELECT [user_id]
			 FROM vote_users 
			 WHERE answer_id = @answer_id);



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CaptchaGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[CaptchaGet]
AS
BEGIN
	SET NOCOUNT ON;

declare @id int

select top 1 @id = id from captcha_questions order by newid()

select 1 as isAnswer, text, 0 as isRight, newid() as n from captcha_questions where id = @id
union
select 0 as isAnswer, text, isRight as isRight, newid() as n from captcha_answers 
	where question_id = @id
	order by isAnswer desc, n
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CaptchaDelete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[CaptchaDelete]
	@id int
AS
BEGIN
	SET NOCOUNT ON;
	delete from captcha_answers where question_id=@id
	delete from captcha_questions where id=@id
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CaptchaQuestionUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[CaptchaQuestionUpdate]
	@id int,
	@text nvarchar(200)

AS
BEGIN
	SET NOCOUNT ON;
	update captcha_questions set text=@text where id=@id
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CaptchaQuestionsList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[CaptchaQuestionsList]
AS
BEGIN
	SET NOCOUNT ON;
	select id, text from captcha_questions order by text
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CaptchaQuestionGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[CaptchaQuestionGet]
	 @id int
AS

BEGIN
	SET NOCOUNT ON;
	select text from captcha_questions where id=@id
end

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CaptchaQuestionAdd]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[CaptchaQuestionAdd]
AS
BEGIN
	SET NOCOUNT ON;
	insert into captcha_questions (text) values(''Новый вопрос'') 
	select @@identity
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CaptchaAnswerUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[CaptchaAnswerUpdate]
	@id int,
	@text nvarchar(50),
	@isRight tinyint
AS
BEGIN
	SET NOCOUNT ON;
	update captcha_answers set text=@text, isRight=@isRight where id=@id
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CaptchaAnswersList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[CaptchaAnswersList]
@id int
AS
BEGIN
	SET NOCOUNT ON;

select id, text, isRight as isRight from captcha_answers 
	where question_id = @id
	order by text
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CaptchaAnswerDelete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROCEDURE [dbo].[CaptchaAnswerDelete]
	@id int
AS
BEGIN
	SET NOCOUNT ON;
	delete from captcha_answers where id=@id
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CaptchaAnswerAdd]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[CaptchaAnswerAdd]
@question_id int
AS
BEGIN
	SET NOCOUNT ON;
	insert into captcha_answers (question_id) values(@question_id) 
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PostAttachCategories]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PostAttachCategories]
		@post_id int,
		@query nvarchar(1024)
AS

DELETE FROM post_cat WHERE post_id = @post_id; -- чтобы всякой хрени не возникло
EXEC(@query)
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FavoriteDel]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[FavoriteDel]
	@post_id int,
	@user_id int
AS
DELETE FROM favorites WHERE post_id = @post_id AND [user_id] = @user_id' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PostIsFavorite]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PostIsFavorite]
	@user_id int,
	@post_id int
AS
	SELECT id FROM favorites WHERE [user_id] = @user_id AND post_id = @post_id' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PollIsUserVoted]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PollIsUserVoted]
	@user_id int,
	@poll_id int
AS
SELECT COUNT(id) 
FROM vote_users 
WHERE poll_id = @poll_id AND [user_id] = @user_id 



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PollGetAnswers]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PollGetAnswers]
	@poll_id int
AS
BEGIN
	SELECT * 
	FROM vote_answers
	WHERE poll_id = @poll_id 
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HeaderTextUpdateShowEndDate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[HeaderTextUpdateShowEndDate]
	@id int
AS
	UPDATE header_texts
	SET show_end_date = GETDATE()
	WHERE id = @id
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HeaderTextUpdateShowBeginDate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[HeaderTextUpdateShowBeginDate]
	@id int
AS
	UPDATE header_texts
	SET show_begin_date = GETDATE()
	WHERE id = @id
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HeaderTextGetCurrents]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[HeaderTextGetCurrents]
AS
select *
from header_texts
where show_end_date is null
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HeaderTextGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[HeaderTextGet]
	@page int,
	@count int,
	@records_count int out
AS
DECLARE @start int
DECLARE @end int
SET @start = (@page-1)*@count
SET @end = (@page*@count) + 1;
SET @records_count = (SELECT COUNT(*) FROM header_texts)

SELECT * FROM
(	SELECT ROW_NUMBER() OVER (ORDER BY id DESC) as row_num, *
	FROM header_texts
) t
WHERE t.row_num > @start AND t.row_num < @end
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HeaderTextDel]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[HeaderTextDel]
	@id int
AS
DELETE FROM header_texts WHERE id = @id
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HeaderTextAdd]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[HeaderTextAdd]
	@user_id int,
	@text nvarchar(128)
AS
INSERT INTO header_texts([text], [user_id], cdate)
VALUES(@text, @user_id, GETDATE())

SELECT * FROM header_texts WHERE id = @@IDENTITY
' 
END
