USE [itc2]
GO
/****** Object:  ForeignKey [FK_captcha_questions_captcha_questions]    Script Date: 04/12/2009 01:14:00 ******/
ALTER TABLE [dbo].[captcha_questions] DROP CONSTRAINT [FK_captcha_questions_captcha_questions]
GO
/****** Object:  StoredProcedure [dbo].[CaptchaGet]    Script Date: 04/12/2009 01:13:58 ******/
DROP PROCEDURE [dbo].[CaptchaGet]
GO
/****** Object:  Table [dbo].[captcha_questions]    Script Date: 04/12/2009 01:14:00 ******/
DROP TABLE [dbo].[captcha_questions]
GO
/****** Object:  Table [dbo].[captcha_answers]    Script Date: 04/12/2009 01:13:59 ******/
DROP TABLE [dbo].[captcha_answers]
GO
/****** Object:  Table [dbo].[captcha_questions]    Script Date: 04/12/2009 01:14:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[captcha_questions](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[text] [nvarchar](200) NOT NULL CONSTRAINT [DF_captchaAnswer_text]  DEFAULT (''),
 CONSTRAINT [PK_captcha_questions] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[captcha_answers]    Script Date: 04/12/2009 01:13:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[captcha_answers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[question_id] [int] NOT NULL,
	[text] [nvarchar](50) NOT NULL,
	[isRight] [tinyint] NOT NULL CONSTRAINT [DF_captcha_variants_isRight]  DEFAULT ((0)),
 CONSTRAINT [PK_captcha_answers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[CaptchaGet]    Script Date: 04/12/2009 01:13:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CaptchaGet]
AS
BEGIN
	SET NOCOUNT ON;

declare @id int
select top 1 @id = id from captcha_questions order by newid()

select -1 as o, text, 0 as isRight from captcha_questions where id = @id
union
select 1 as o, text, isRight as isRight from captcha_answers 
	where question_id = @id

END
GO
/****** Object:  ForeignKey [FK_captcha_questions_captcha_questions]    Script Date: 04/12/2009 01:14:00 ******/
ALTER TABLE [dbo].[captcha_questions]  WITH CHECK ADD  CONSTRAINT [FK_captcha_questions_captcha_questions] FOREIGN KEY([id])
REFERENCES [dbo].[captcha_questions] ([id])
GO
ALTER TABLE [dbo].[captcha_questions] CHECK CONSTRAINT [FK_captcha_questions_captcha_questions]
GO
