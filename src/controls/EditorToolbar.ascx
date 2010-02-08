<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditorToolbar.ascx.cs" Inherits="ITCommunity.EditorToolbar" %>
<script type="text/javascript">
	window.addEvent('domready', function() {
		var input = $('<asp:Literal Id="Input" runat="server" />');

		var editor = new SimpleEditor(input, $$('.editor-toolbar a'));

		editor.addCommands({
			hr: {
				shortcut: '1',
				command: function(input) {
					input.insertAroundCursor({
						before: '[hr]',
						after: ''
					});
				}
			},
			bold: {
				shortcut: '2',
				command: function(input) {
					input.insertAroundCursor({
						before: '[b]',
						after: '[/b]'
					});
				}
			},
			underline: {
				shortcut: '3',
				command: function(input) {
					input.insertAroundCursor({
						before: '[u]',
						after: '[/u]'
					});
				}
			},
			italic: {
				shortcut: '4',
				command: function(input) {
					input.insertAroundCursor({
						before: '[i]',
						after: '[/i]'
					});
				}
			},
			strike: {
				shortcut: '5',
				command: function(input) {
					input.insertAroundCursor({
						before: '[s]',
						after: '[/s]'
					});
				}
			},
			code: {
				shortcut: '6',
				command: function(input) {
					input.insertAroundCursor({
						before: '[code]',
						after: '[/code]'
					});
				}
			},
			quote: {
				shortcut: '7',
				command: function(input) {
					input.insertAroundCursor({
						before: '[quote]',
						after: '[/quote]'
					});
				}
			},
			bulllist: {
				shortcut: '8',
				command: function(input) {
					input.insertAroundCursor({
						before: '[list]',
						defaultMiddle: '[*]1 элемент[*]2 элемент[*]3 элемент',
						after: '[/list]'
					});
				}
			},
			link: {
				shortcut: '9',
				command: function(input) {
					input.insertAroundCursor({
						before: '[url]',
						after: '[/url]'
					}); return false;
				}
			},
			email: {
				shortcut: '0',
				command: function(input) {
					input.insertAroundCursor({
						before: '[email]',
						after: '[/email]'
					});
				}
			},
			img: {
				shortcut: 'q',
				command: function(input) {
					input.insertAroundCursor({
						before: '[img]',
						after: '[/img]'
					});
				}
			},
			video: {
				shortcut: 'e',
				command: function(input) {
					input.insertAroundCursor({
						before: '[video]',
						after: '[/video]'
					});
				}
			},
			table: {
				shortcut: 'e',
				command: function(input) {
					input.insertAroundCursor({
						before: '[table]',
						after: '[/table]'
					});
				}
			},
			tr: {
				shortcut: 'e',
				command: function(input) {
					input.insertAroundCursor({
						before: '[tr]',
						after: '[/tr]'
					});
				}
			},
			td: {
				shortcut: 'e',
				command: function(input) {
					input.insertAroundCursor({
						before: '[td]',
						after: '[/td]'
					});
				}
			}
		});
	});
</script>


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
