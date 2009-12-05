<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditorToolbar.ascx.cs" Inherits="ITCommunity.EditorToolbar" %>

 <script type="text/javascript">        
        window.addEvent('domready', function(){
            var input = $('<asp:Literal Id="input" runat="server" />');
            var editor = new SimpleEditor(input, $$('.editor-toolbar input'));
            editor.addCommands({
            	hr: {
		                shortcut: 'h',
		                command: function(input){
			            input.insertAroundCursor({
			                before:'[hr]',
			                after:''
			            });
		            }
	            },
	            bold: {
		                shortcut: 'b',
		                command: function(input){
			            input.insertAroundCursor({
			                before:'[b]',
			                after:'[/b]'
			            });
		            }
	            },
	            underline: {
		                shortcut: '_',
		                command: function(input){
			            input.insertAroundCursor({
			                before:'[u]',
			                after:'[/u]'
			            });
		            }
	            },
	            italic: {
		                shortcut: '/',
		                command: function(input){
			            input.insertAroundCursor({
			                before:'[i]',
			                after:'[/i]'
			            });
		            }
	            },
	            strike: {
		                shortcut: 's',
		                command: function(input){
			            input.insertAroundCursor({
			                before:'[s]',
			                after:'[/s]'
			            });
		            }
	            },
	            code: {
		                shortcut: 'c',
		                command: function(input){
			            input.insertAroundCursor({
			                before:'[code]',
			                after:'[/code]'
			            });
		            }
	            },
	            quote: {
		                shortcut: 'q',
		                command: function(input){
			            input.insertAroundCursor({
			                before:'[quote]',
			                after:'[/quote]'
			            });
		            }
	            },
	            bulllist: {
		                shortcut: '=',
		                command: function(input){
			            input.insertAroundCursor({
			                before:'[list]',
			                defaultMiddle: '[*]1 элемент[*]2 элемент[*]3 элемент',
			                after:'[/list]'
			            });
		            }
	            },
	            link: {
		                shortcut: 'l',
		                command: function(input){
			            input.insertAroundCursor({
			                before:'[url]',
			                after:'[/url]'
			            });return false;
		            }
	            },
	            email: {
		                shortcut: 'e',
		                command: function(input){
			            input.insertAroundCursor({
			                before:'[email]',
			                after:'[/email]'
			            });
		            }
	            },
	            img: {
		                shortcut: 'i',
		                command: function(input){
			            input.insertAroundCursor({
			                before:'[img]',
			                after:'[/img]'
			            });
		            }
	            },
	            video: {
		                shortcut: 'v',
		                command: function(input){
			            input.insertAroundCursor({
			                before:'[video]',
			                after:'[/video]'
			            });
		            }
	            }
            });
        });
 </script>


<div class="editor-toolbar">
    <input type="button" title="Разделитель (ctrl+h)" rel="hr" value="hr" />
    - 
    <input type="button" title="Жирный (ctrl+b)"            rel="bold"      value="b" />
    <input type="button" title="Подчеркивание (ctrl+_)"     rel="underline" value="u" />
    <input type="button" title="Курсив (ctrl+/)"            rel="italic"    value="i" /> 
    <input type="button" title="Зачеркнутый текст (ctrl+s)" rel="strike"    value="s" /> 
    - 
    <input type="button" title="Блок кода (ctrl+c)" rel="code"  value="code"  />
    <input type="button" title="Цитата (ctrl+q)"    rel="quote" value="quote" />
    - 
    <input type="button" title="Маркированный список (ctrl+=)" rel="bulllist" value="list" /> 
    - 
    <input type="button" title="Ссылка (ctrl+u)"   rel="link"  value="link"  />
    <input type="button" title="email (ctrl+e)"    rel="email" value="email" />
    <input type="button" title="Картинка (ctrl+i)" rel="img"   value="img"   />
    <input type="button" title="Видео (ctrl+v)"    rel="video" value="video" />
</div>
