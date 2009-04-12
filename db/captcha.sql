ALTER PROCEDURE [dbo].[CaptchaGet]
AS
BEGIN
	SET NOCOUNT ON;

declare @id int

select top 1 @id = id from captcha_questions order by newid()

select 1 as isAnswer, text, 0 as isRight, newid() as n from captcha_questions where id = @id
union
select 0 as isAnswer, text, isRight as isRight, newid() as n from captcha_answers 
	where question_id = @id
	order by n
END
