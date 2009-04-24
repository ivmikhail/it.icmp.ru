<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddPost.aspx.cs" Inherits="AddPost" Title="Ykt IT Community | ���������� �������"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">

<div id="add_post">
    <ul class="list">
        <li>
            <h2>�������� ���������</h2>
            <label>
                <asp:DropDownList ID="DropDownListCats" runat="server" CssClass="input-text"/>
            </label>
        </li>
        <li>        
            <h2>
                <label>
                    ����������� <asp:CheckBox ID="CheckBoxAttached" runat="server" Enabled="false"/>
                </label>
            </h2>
        </li>
        <li>
            <h2>���������</h2>
            <label>
                <asp:TextBox ID="TextBoxTitle" runat="server" Columns="20" CssClass="input-text" MaxLength="32"/>  
             </label>
        </li>
        <li>
            <h2>������� ��������</h2>
            <label>               
                <asp:TextBox ID="TextBoxDesc" runat="server" TextMode="MultiLine" Rows="10" Width="100%" MaxLength="512"/>  
            </label>
        </li>        
        <li>
            <h2>����� �������</h2>      
            <asp:TextBox ID="TextBoxText" runat="server" TextMode="MultiLine" Rows="25" Width="100%" MaxLength="2048"/>  
        </li>        
        <li>
            <h2>��������(������ �������)</h2>
            <label>                
                <asp:TextBox ID="TextBoxSource" runat="server" CssClass="input-text" MaxLength="256"/> 
            </label>
        </li>            
        <li>
            <div class="error-message">
                <asp:Literal ID="AddPostMessages" runat="server" />
            </div>
        </li>
        <li style="text-align:right;">
            <asp:LinkButton ID="LinkButtonAdd" runat="server" OnClick="LinkButtonAdd_Click">��������</asp:LinkButton>
        </li>
        <li>
            <script type="text/javascript">
                function ReceiveServerData(rValue) 
                {   
                    if(rValue == "")
                    {
                        $('uploadImagesMessage').innerHTML = '�������� �� ������ ���������. �������� ������.';
                    } else
                    {
                        $('uploadImagesMessage').innerHTML = '';
                        CreateNestedElements(rValue); 
                    }
                } 
                function CreateNestedElements(newPath) 
                {
                    var imageObject = document.createElement('img'); 
                    imageObject.src = newPath;                  
                    $('uploadImagesList').appendChild(imageObject);   
                }
                function PopulateList(obj) 
                {   
                    CallServer(obj.value,'');    
                }
                  
            </script>
            <h2>��������</h2>
                <p class="note">
                        <asp:Literal ID="ImageOptions" runat="server"/>
                </p>
                <input id="list" type="hidden" runat="server"/>    
                <h3>��������:</h3> 
                <input id="image_upload" onchange="PopulateList(this)" name="image_upload" type="file" />

                <h3>�����������:</h3>                     
                <p class="note">                   
                        �� ����������� � ������� �������� ����� ������� �������������
                </p>
                <span id="uploadImagesMessage" class="error-message"></span>
                <div id="uploadImagesList"></div>
        </li>
    </ul>
</div>
</asp:Content>

