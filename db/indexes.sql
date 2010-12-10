/****** Object:  Index [idx_posts_isAttached_createDate_desc]    Script Date: 12/11/2010 02:33:49 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Posts]') AND name = N'idx_posts_isAttached_createDate_desc')
DROP INDEX [idx_posts_isAttached_createDate_desc] ON [dbo].[Posts] WITH ( ONLINE = OFF )
GO

/****** Object:  Index [idx_posts_isAttached_createDate_desc]    Script Date: 12/11/2010 02:33:49 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Posts]') AND name = N'idx_posts_isAttached_createDate_desc')
CREATE NONCLUSTERED INDEX [idx_posts_isAttached_createDate_desc] ON [dbo].[Posts] 
(
	[IsAttached] ASC,
	[CreateDate] DESC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

