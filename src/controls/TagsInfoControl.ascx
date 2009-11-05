<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TagsInfoControl.ascx.cs" Inherits="ITCommunity.controls_TagsInfoControl" %>
<script type="text/javascript">
    function toggle_tags()
	{
	    $('tagsinfo').setStyle('display', $('tagsinfo').getStyle('display') == "none" ? "" : "none");    
	    return false;
	}
</script>
<p class="note">
    ����� ������������ <a id="tags-info-link" href="#" onclick="javascript: return toggle_tags();" title="������ ��� ����� ������������� �����">bbcode-����</a>
</p>
<div id="tagsinfo" style="display:none;">
    <p class="tags-help-close">
        <a id="tags-info-close" href="#" onclick="javascript: return toggle_tags();" title="������ � ���� �����, ���� � �����">�������</a>
    </p>
    <h3>���� ��� ��������������</h3>
    <div class="tags">
          <div class="block-right">
                    <dl>
                        <dt>
                            [b]<b>������ �����</b>[/b]
                            <br />
                            [i]<i>������</i>[/i]
                            <br />
                            [u]<u>underline</u>[/u]
                            <br />
                            [s]<s>����������� �����</s>[/s]
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
                    
                    <dt>
                            [url][/url]
                            <br />
                            [email][/email]
                    </dt>
                        <dd>
                            ������ ���� [url] ��������� ������, � ������ [email] ����� ����������� �����; 
                            ��� �� [url] ����� ������������ � ����:
                            <br />
                            <br />
                            [url=http://example.com]������[/url],
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
                            ������ ���� [code] ����� �������� ����������� ���(���������� ���������� ���������� �������������); ��� ��������� ����� ����������� [quote]
                        </dd>
        
                        <dt>
                            [img][/img]
                        </dt>
                        <dd>
                            ��� ��� ������� ���� ��� ��������, �� ����� ��������. ������� �������������:
                            <br />
                            <br />
                            [img]http://ya.ru/logo.png[/img],
                            <br />
                            [img align=left]http://ya.ru/logo.png[/img],
                            <br />
                            [img=100x100px]http://ya.ru/logo.png[/img]
                            <br />
                            <br />
                            ���������� ���������� �������� �� ��� ����, ���� ���������� � ������������ ��������.
                        </dd>        
 
                        <dt>
                            [list][/list]
                        </dt>
                        <dd>
                             ������� ������(ul), ������ ������� ������� ����� [*]. 
                             <br /> 
                             <br /> 
                             ����� ��������� ������ - [list=marker].
                             <br /> 
                             ��������� ������� <b>1</b>(decimal), <b>i</b>(lower-roman), <b>I</b>(upper-roman), <b>a</b>(lower-alpha), <b>A</b>(upper-alpha). �������:
                            <br />
                            <br />
                            [list][*]1 �������[*]2 �������[*]3 �������[/list]
                            <br /> 
                            [list=1][*]1 �������[*]2 �������[*]3 �������[/list]               
                            <br />                            
                            [list=A][*]1 �������[*]2 �������[*]3 �������[/list]                  
                            <br />
                        </dd> 
                        <dt>
                            [video][/video]
                        </dt> 
                        <dd>
                            ����������� �����, ������ ��������� ������ �� �����, �������������� <b>Play.Ykt.Ru</b> � <b>Tube.Abunda.Ru</b>
                        </dd>
                    </dl>                   
          </div>
    </div>
</div>