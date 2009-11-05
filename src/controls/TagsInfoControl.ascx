<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TagsInfoControl.ascx.cs" Inherits="ITCommunity.controls_TagsInfoControl" %>
<script type="text/javascript">
    function toggle_tags()
	{
	    $('tagsinfo').setStyle('display', $('tagsinfo').getStyle('display') == "none" ? "" : "none");    
	    return false;
	}
</script>
<p class="note">
    Можно использовать <a id="tags-info-link" href="#" onclick="javascript: return toggle_tags();" title="Узнать как можно форматировать текст">bbcode-теги</a>
</p>
<div id="tagsinfo" style="display:none;">
    <p class="tags-help-close">
        <a id="tags-info-close" href="#" onclick="javascript: return toggle_tags();" title="Убрать с глаз долой, сами с усами">закрыть</a>
    </p>
    <h3>Теги для форматирования</h3>
    <div class="tags">
          <div class="block-right">
                    <dl>
                        <dt>
                            [b]<b>жирный текст</b>[/b]
                            <br />
                            [i]<i>курсив</i>[/i]
                            <br />
                            [u]<u>underline</u>[/u]
                            <br />
                            [s]<s>зачеркнутый текст</s>[/s]
                            <br />
                            [size=666px]размер шрифта[/size]
                        </dt>
                        <dd>
                            всякое извращение над текстом
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
                            [float=left][/float]
                        </dt>
                        <dd>
                            определяет, по какой стороне будет выравниваться элемент, при этом остальные элементы будут обтекать его с других сторон
                        </dd>
                    </dl>
                    
                    <dt>
                            [url][/url]
                            <br />
                            [email][/email]
                    </dt>
                        <dd>
                            внутри тега [url] помещайте ссылки, а внутри [email] адрес электронной почты; 
                            так же [url] можно использовать в виде:
                            <br />
                            <br />
                            [url=http://example.com]пример[/url],
                            <br />
                            [url=http://test.ru][img]http://flickr.com/givemeimg.png[/img][/url]                            
                            <br />
                        </dd> 
          </div>
          <div class="block-left">
                     <dl>        
                        <dt>
                            [code][/code]
                            <br />
                            [quote][/quote]
                        </dt>
                        <dd>
                            внутри тега [code] можно помещать программный код(подстветка попытается включиться автоматически); для выделения цитат используйте [quote]
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
                            [img align=left]http://ya.ru/logo.png[/img],
                            <br />
                            [img=100x100px]http://ya.ru/logo.png[/img]
                            <br />
                            <br />
                            Пожалуйста загружайте картинки на наш сайт, либо вставляйте с бекбоновских ресурсов.
                        </dd>        
 
                        <dt>
                            [list][/list]
                        </dt>
                        <dd>
                             создаем списки(ul), каждый элемент пишется после [*]. 
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
                            [video][/video]
                        </dt> 
                        <dd>
                            Проигрывает видео, внутрь вставляем ссылки на видео, поддерживается <b>Play.Ykt.Ru</b> и <b>Tube.Abunda.Ru</b>
                        </dd>
                    </dl>                   
          </div>
    </div>
</div>