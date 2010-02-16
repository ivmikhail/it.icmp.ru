IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ratings]') AND type in (N'U'))
DROP TABLE [dbo].[ratings]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rating_logs]') AND type in (N'U'))
DROP TABLE [dbo].[rating_logs]

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ratings]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ratings](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[entity_id] [int] NOT NULL,
	[entity_type] [int] NOT NULL,
	[value] [int] NOT NULL,
 CONSTRAINT [PK_ratings] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rating_logs]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[rating_logs](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[entity_id] [int] NOT NULL,
	[entity_type] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[value] [int] NOT NULL,
	[cdate] [datetime] NOT NULL CONSTRAINT [DF_rating_logs_cdate]  DEFAULT (getdate()),
 CONSTRAINT [PK_rating_logs] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
