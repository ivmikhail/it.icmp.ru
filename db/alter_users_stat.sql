
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER TABLE [dbo].[users]
ADD [posts_count] [int] NOT NULL DEFAULT ((0))
GO
ALTER TABLE [dbo].[users]
ADD [comments_count] [int] NOT NULL DEFAULT ((0))
GO
update users
set posts_count = (select count(posts.id)    from posts    where posts.user_id    = users.id),
comments_count  = (select count(comments.id) from comments where comments.user_id = users.id)

