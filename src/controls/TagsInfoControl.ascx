<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TagsInfoControl.ascx.cs" Inherits="ITCommunity.controls_TagsInfoControl" %>
<script type="text/javascript">
    window.addEvent('domready', function(){ 
        var slider = new Fx.Slide('tags-info', {
             duration: 500,
             transition: Fx.Transitions.linear
        });
        
        slider.hide();
        
        $('tags-info-link').addEvent('click', function(e){            
            e = new Event(e);
            slider.toggle();
            e.stop();
	    });
	    $('tags-info-close').addEvent('click', function(e){            
            e = new Event(e);
            slider.toggle();
            e.stop();
	    });
	});		
</script>
<p class="note">ћожно использовать <a id="tags-info-link" href="#" title="”знать как можно форматировать текст">bbcode-теги</a></p>
<div id="tags-info">
    <p style="text-align:right; width:100%;"><a id="tags-info-close" href="#" title="”брать с глаз долой, сами с усами">закрыть</a></p>
    <h3>“еги дл€ форматировани€</h3>
    <table>
            <tr>
                <td style="padding-right:5px;">
                    <dl>
                        <dt>
                            [b]<b>жирный текст</b>[/b]
                            <br />
                            [i]<i>курсив</i>[/i]
                            <br />
                            [u]<u>underline</u>[/u]
                            <br />
                            [s]<strike>зачеркнутый текст</strike>[/s]
                            <br />
                            [size=666px]размер шрифта[/size]
                        </dt>
                        <dd>
                            вс€кое извращение над текстом
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
                            определ€ет, по какой стороне будет выравниватьс€ элемент, при этом остальные элементы будут обтекать его с других сторон
                        </dd>
                    </dl>
                </td>
                <td>
                    <dl>        
                        <dt>
                            [code][/code]
                            <br />
                            [quote][/quote]
                        </dt>
                        <dd>
                            внутри тега [code] можно помещать программный код(подстветка попытаетс€ включитьс€ автоматически); дл€ выделени€ цитат используйте [quote]
                        </dd>
        
                        <dt>
                            [img][/img]
                        </dt>
                        <dd>
                            дл€ вставки фото или картинок, мы любим картинки. ѕримеры использовани€:
                            <br />
                            [img]http://ya.ru/logo.png[/img],
                            <br />
                            [img align=left]http://ya.ru/logo.png[/img],
                            <br />
                            [img=100x100px]http://ya.ru/logo.png[/img]
                        </dd>
        
                        <dt>
                            [url][/url]
                            <br />
                            [email][/email]
                        </dt>
                        <dd>
                            внутри тегов [url] и [link] помещайте ссылки, а внутри [email] адрес электронной почты; 
                            так же [url] можно использовать в виде:
                            <br />
                            [url=http://example.com]пример[/url],
                            <br />
                            [url=http://test.ru][img]http://flickr.com/givemeimg.png[/img][/url]
                        </dd>  
                    </dl>
                </td>
            </tr>
    </table>   
</div>