<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="notfound.aspx.cs" Inherits="ITCommunity.NotFoundPage" Title="Ykt IT Community | �������� �� �������" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1 class="error-message">������������� �������� �� �������</h1>
    <p>
        ��������, ������� �� ������, ��� ��������� �� ����� �����, ���� ����������/�������. �������������� <a href="search.aspx" title="����� ������� ������� �� �����">�������</a> ����� ����� ��, ��� �� ������.       
    </p>    
    <asp:Literal ID="LiteralReferrerUrl" runat="server"/>    
    <p>
        ���� �� �������� ��� ����� ��������� ���������������, �� ������ �������� ������ �� ������ <a href="mailto:ykt.itcommunity@gmail.com">ykt.itcommunity@gmail.com</a>
    </p>

    <h1>��� ��������� ���������� ������ ��������� ���� ��������</h1>
        <ol style="list-style:a;">
	        <li><strong>���������� ��������</strong> � ����� ��������</li>
	        <li>������ ������, ������� ��� ��� ��� ������, <strong>��� �� �������� ����� �������� �����</strong></li>
	        <li>�� <strong>����������� ��� �������� ������</strong></li>
        </ol>

</asp:Content>

