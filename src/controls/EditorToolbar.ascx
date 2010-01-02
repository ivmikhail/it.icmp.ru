<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditorToolbar.ascx.cs" Inherits="ITCommunity.EditorToolbar" %>
<script type="text/javascript">        
        window.addEvent('domready', function(){
            var input = $('<asp:Literal Id="Input" runat="server" />');
         
            var editor = new SimpleEditor(input, $$('.editor-toolbar input'));

            editor.addCommands({
            	hr: {
		                shortcut: '1',
		                command: function(input){
			            input.insertAroundCursor({
			                before:'[hr]',
			                after:''
			            });
		            }
	            },
	            bold: {
		                shortcut: '2',
		                command: function(input){
			            input.insertAroundCursor({
			                before:'[b]',
			                after:'[/b]'
			            });
		            }
	            },
	            underline: {
		                shortcut: '3',
		                command: function(input){
			            input.insertAroundCursor({
			                before:'[u]',
			                after:'[/u]'
			            });
		            }
	            },
	            italic: {
		                shortcut: '4',
		                command: function(input){
			            input.insertAroundCursor({
			                before:'[i]',
			                after:'[/i]'
			            });
		            }
	            },
	            strike: {
		                shortcut: '5',
		                command: function(input){
			            input.insertAroundCursor({
			                before:'[s]',
			                after:'[/s]'
			            });
		            }
	            },
	            code: {
		                shortcut: '6',
		                command: function(input){
			            input.insertAroundCursor({
			                before:'[code]',
			                after:'[/code]'
			            });
		            }
	            },
	            quote: {
		                shortcut: '7',
		                command: function(input){
			            input.insertAroundCursor({
			                before:'[quote]',
			                after:'[/quote]'
			            });
		            }
	            },
	            bulllist: {
		                shortcut: '8',
		                command: function(input){
			            input.insertAroundCursor({
			                before:'[list]',
			                defaultMiddle: '[*]1 элемент[*]2 элемент[*]3 элемент',
			                after:'[/list]'
			            });
		            }
	            },
	            link: {
		                shortcut: '9',
		                command: function(input){
			            input.insertAroundCursor({
			                before:'[url]',
			                after:'[/url]'
			            });return false;
		            }
	            },
	            email: {
		                shortcut: '0',
		                command: function(input){
			            input.insertAroundCursor({
			                before:'[email]',
			                after:'[/email]'
			            });
		            }
	            },
	            img: {
		                shortcut: 'q',
		                command: function(input){
			            input.insertAroundCursor({
			                before:'[img]',
			                after:'[/img]'
			            });
		            }
	            },
	            video: {
		                shortcut: 'e',
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
    <input type="button" title="Разделитель (ctrl+1)"       rel="hr"         value="hr" />
    -
    <input type="button" title="Жирный (ctrl+2)"            rel="bold"      value="b" />
    <input type="button" title="Подчеркивание (ctrl+3)"     rel="underline" value="u" />
    <input type="button" title="Курсив (ctrl+4)"            rel="italic"    value="i" /> 
    <input type="button" title="Зачеркнутый текст (ctrl+5)" rel="strike"    value="s" /> 
    - 
    <input type="button" title="Блок кода (ctrl+6)" rel="code"  value="code"  />
    <input type="button" title="Цитата (ctrl+7)"    rel="quote" value="quote" />
    - 
    <input type="button" title="Маркированный список (ctrl+8)" rel="bulllist" value="list" /> 
    - 
    <input type="button" title="Ссылка (ctrl+9)"   rel="link"  value="link"  />
    <input type="button" title="email (ctrl+0)"    rel="email" value="email" />
    <input type="button" title="Картинка (ctrl+q)" rel="img"   value="img"   />
    <input type="button" title="Видео (ctrl+e)"    rel="video" value="video" />
</div>
