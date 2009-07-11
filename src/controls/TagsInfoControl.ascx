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
<p class="note">����� ������������ <a id="tags-info-link" href="#" title="������ ��� ����� ������������� �����">bbcode-����</a></p>
<div id="tags-info">
    <p style="text-align:right; width:100%;"><a id="tags-info-close" href="#" title="������ � ���� �����, ���� � �����">�������</a></p>
    <h3>���� ��� ��������������</h3>
    <table>
            <tr>
                <td style="padding-right:5px;">
                    <dl>
                        <dt>
                            [b]<b>������ �����</b>[/b]
                            <br />
                            [i]<i>������</i>[/i]
                            <br />
                            [u]<u>underline</u>[/u]
                            <br />
                            [s]<strike>����������� �����</strike>[/s]
                            <br />
                            [size=666px]������ ������[/size]
                        </dt>
                        <dd>
                            ������ ���������� ��� �������
                        </dd>
        
                        <dt>
                            [left][/left]
                            <br />
                            [right][/right]
                            <br />
                            [center][/center]
                        </dt>
                        <dd>
                            ���������������� ���������: ��������, ����� � �.�
                        </dd>
                         
                        <dt>
                            [float=left][/float]
                        </dt>
                        <dd>
                            ����������, �� ����� ������� ����� ������������� �������, ��� ���� ��������� �������� ����� �������� ��� � ������ ������
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
                            ������ ���� [code] ����� �������� ����������� ���(���������� ���������� ���������� �������������); ��� ��������� ����� ����������� [quote]
                        </dd>
        
                        <dt>
                            [img][/img]
                        </dt>
                        <dd>
                            ��� ������� ���� ��� ��������, �� ����� ��������. ������� �������������:
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
                            ������ ����� [url] � [link] ��������� ������, � ������ [email] ����� ����������� �����; 
                            ��� �� [url] ����� ������������ � ����:
                            <br />
                            [url=http://example.com]������[/url],
                            <br />
                            [url=http://test.ru][img]http://flickr.com/givemeimg.png[/img][/url]
                        </dd>  
                    </dl>
                </td>
            </tr>
    </table>   
</div>