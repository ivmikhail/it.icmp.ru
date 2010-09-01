<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>

<div class="bbcode-info meta">
    можно использовать <a class="tags-info-link" href="#" title="Узнать как можно форматировать текст">bbcode-теги</a>

	<div class="tags-info hide">

		<dl class="left-block">
			<dt>
				[b]<b>жирный текст</b>[/b]
				<br />
				[i]<i>курсив</i>[/i]
				<br />
				[u]<u>underline</u>[/u]
				<br />
				[s]<s>зачеркнутый текст</s>[/s]
				<br />
				[size=20px]размер шрифта[/size]
			</dt>
			<dd>
				всякие изменения текста
			</dd>

			<dt>
				[left][/left]
				<br />
				[right][/right]
				<br />
				[center][/center]
			</dt>
			<dd>
				позиционирование элементов: картинки, текст и т.д
			</dd>

			<dt>
				[url][/url]
				<br />
				[email][/email]
			</dt>
			<dd>
				внутри тега [url] помещайте ссылки, а внутри [email] адрес электронной почты; 
				<br />
				так же [url] можно использовать в виде:
				<br />
				[url=http://example.com]пример[/url],
				<br />
				[url=http://test.ru][img]http://flickr.com/givemeimg.png[/img][/url]
			</dd>

			<dt>
				[code][/code]
				<br />
				[quote][/quote]
			</dt>
			<dd>
				внутри тега [code] можно помещать программный код (подстветка попытается включиться автоматически); для выделения цитат используйте [quote]
                <br />
                также можно напрямую указать язык [code=cpp]int i;[/code]
			</dd>
		</dl>

		<dl class="right-block">
			<dt>
				[list][/list]
			</dt>
			<dd>
				создаем списки, каждый элемент пишется после [*].
				<br />
				<br />
				Можно указывать маркер - [list=marker].
				<br />
				возможные маркеры <b>1</b>(decimal), <b>i</b>(lower-roman), <b>I</b>(upper-roman), <b>a</b>(lower-alpha), <b>A</b>(upper-alpha). Примеры:
				<br />
				<br />
				[list][*]1 элемент[*]2 элемент[*]3 элемент[/list]
				<br />
				[list=1][*]1 элемент[*]2 элемент[*]3 элемент[/list]
				<br />
				[list=A][*]1 элемент[*]2 элемент[*]3 элемент[/list]
				<br />
			</dd>

			<dt>
				[table][/table]
			</dt>
			<dd>
				оформляем таблицу, используя внутренние теги [tr] и [td].
				<br />
				[tr] - строка, [td] - поле в строке,
				<br />
				[table=100%] - можно задавать ширину в процентах, по-умолчанию ширина 100%
				<br />
				[td=2] - можно задавать сколько столбцов входит в это поле. Пример:
				<br />
				<br />
				[table=50%][tr][td]столбец 1[/td][td]столбец 2[/td][/tr][tr][td]значение 1[/td][td]значение 2[/td][/tr][tr][td=2]сразу 2 столбца[/td][/tr][/table]
			</dd>

			<dt>
				[img][/img]
			</dt>
			<dd>
				тег для вставки фото или картинок, мы любим картинки. Примеры использования:
				<br />
				<br />
				[img]http://ya.ru/logo.png[/img],
				<br />
				[img=100x100px]http://ya.ru/logo.png[/img]
				<br />
				открывать картинку в оригинальном размере:
				<br />
				[img=fullimg.url]thumbimg.url[/img],
				<br />
				Пожалуйста загружайте картинки на наш сайт, либо вставляйте с бекбоновских ресурсов.
			</dd>

			<dt>
				[video][/video]
			</dt>
			<dd>
				Проигрывает видео, внутрь вставляем ссылки на видео, поддерживается <i>Play.Ykt.Ru</i>(нужно вставить ссылку на страницу с видео) и <i>tv.ykt.ru</i>(нужно вставить ссылку на адрес файла)
			</dd>

		</dl>

		<div class="clear"></div>
	</div>
</div>
<%-- 
<div class="editor-toolbar">
	<a title="Разделитель (ctrl+1)"       rel="hr"        >hr</a>
	-
	<a title="Жирный (ctrl+2)"            rel="bold"      >b</a>
	<a title="Подчеркивание (ctrl+3)"     rel="underline" >u</a>
	<a title="Курсив (ctrl+4)"            rel="italic"    >i</a>
	<a title="Зачеркнутый текст (ctrl+5)" rel="strike"    >s</a>
	-
	<a title="Блок кода (ctrl+6)" rel="code"  >code</a>
	<a title="Цитата (ctrl+7)"    rel="quote" >quote</a>
	-
	<a title="Маркированный список (ctrl+8)" rel="bulllist" >list</a>
	-
	<a title="Ссылка (ctrl+9)"   rel="link"  >link</a>
	<a title="email (ctrl+0)"    rel="email" >email</a>
	<a title="Картинка (ctrl+q)" rel="img"   >img</a>
	<a title="Видео (ctrl+e)"    rel="video" >video</a>
	-
	<a title="Таблица (ctrl+y)"          rel="table" >table</a>
	<a title="Строка в таблице (ctrl+[)" rel="tr"    >tr</a>
	<a title="Поле таблицы (ctrl+])"     rel="td"    >td</a>
</div>
--%>