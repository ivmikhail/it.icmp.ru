SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER TABLE [dbo].[users]
ADD [can_add_header_text] [tinyint] NOT NULL DEFAULT ((1))
GO
ALTER TABLE [dbo].[users]
ADD [header_text_counter] [int] NOT NULL DEFAULT ((0))

