USE [itcommunity]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_header_texts_users]') AND parent_object_id = OBJECT_ID(N'[dbo].[header_texts]'))
ALTER TABLE [dbo].[header_texts] DROP CONSTRAINT [FK_header_texts_users]
GO
USE [itcommunity]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[header_texts]') AND type in (N'U'))
DROP TABLE [dbo].[header_texts]

USE [itcommunity]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[header_texts](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[text] [nvarchar](1024) COLLATE Cyrillic_General_CI_AS NOT NULL,
	[cdate] [datetime] NOT NULL,
	[show_begin_date] [datetime] NULL,
	[show_end_date] [datetime] NULL,
 CONSTRAINT [PK_header_texts] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
USE [itcommunity]
GO
ALTER TABLE [dbo].[header_texts]  WITH CHECK ADD  CONSTRAINT [FK_header_texts_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([id])
ON DELETE CASCADE