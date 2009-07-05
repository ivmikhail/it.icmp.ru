<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="profile.aspx.cs" Inherits="ITCommunity.ProfilePage" Title="Ykt IT Community | �������������� �������" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="profile_panel">
    <div id="profile_data">
        <ul class="list">
            <li>
                <h2>�������� email</h2> 
                <p class="note"> 
                    �� �����������
                </p>
                <label> 
                    <asp:TextBox ID="TextBoxEmail" runat="server" Width="100%" MaxLength="512" ValidationGroup="ValidateProfileData"/>
                </label>
            </li>        
            <li>
                 <h2>�������� ������</h2>   
                 <p class="note"> 
                    �� �����������, ���� ������ �� �������, �� ������ �� ���������
                 </p>                
                 <h3>����� ������</h3> 
                 <label>
                    <asp:TextBox ID="TextBoxNewPass" runat="server" Width="100%" TextMode="Password" MaxLength="512" ValidationGroup="ValidateProfileData"/>
                 </label>
                 <h3>��������� ����� ������</h3>
                 <label>
                    <asp:TextBox ID="TextBoxNewPassConf" runat="server" Width="100%" TextMode="Password" MaxLength="512" ValidationGroup="ValidateProfileData"/>
                </label>
            </li>            
            <li>
                <h2>������� ��� ������� ������ ��� �����������</h2>
                <p class="note">
                    ����� ��������� ��������� ������� ��� ������� ������
                </p>
                <label>
                    <asp:TextBox ID="TextBoxPassConf" runat="server" Width="100%"  TextMode="Password" MaxLength="512"/>
                </label>
            </li>
            <li style="text-align:right; padding: 10px 0 0 0;">            
                <asp:LinkButton ID="EditProfileButton" runat="server" OnClick="EditProfileButton_Click" ValidationGroup="ValidateProfileData">��������</asp:LinkButton>
            </li>
         </ul>         
     </div>
     <div class="profile_messages error-message">
         <asp:Literal ID="LiteralUpdatedMessage" runat="server" />
        <asp:ValidationSummary ID="ValidationSummaryProfile" runat="server" ValidationGroup="ValidateProfileData" DisplayMode="List"  />
            <asp:RequiredFieldValidator ID="RequiredEmail" 
                                        runat="server" 
                                        ControlToValidate="TextBoxEmail"
                                        ErrorMessage="������� e-mail." 
                                        ValidationGroup="ValidateProfileData"
                                        Display="None"  />
                                        
            <asp:RegularExpressionValidator ID="EmailValidator" 
                                        runat="server" 
                                        ControlToValidate="TextBoxEmail" 
                                        ErrorMessage="������� ���������� e-mail." 
                                        Display="None" 
                                        ValidationGroup="ValidateProfileData" 
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"/>
            <asp:CustomValidator ID="EmailExist" 
                                 runat="server" 
                                 Display="None" 
                                 ErrorMessage="������������ � ����� email'�� ��� ���������������. ��������� �� �������� � ����." 
                                 ValidationGroup="ValidateProfileData" />
                             
            <asp:CustomValidator ID="OldPassConfirm" 
                                 runat="server" 
                                 Display="None" 
                                 ErrorMessage="������ ������ �� �����, ��������� �� �������� � ����." 
                                 ValidationGroup="ValidateProfileData" />
                                        
            <asp:CompareValidator ID="ConfirmPassword" 
                                  runat="server" 
                                  Display="None"
                                  ControlToCompare="TextBoxNewPass" 
                                  ControlToValidate="TextBoxNewPassConf" 
                                  ErrorMessage="������ �� ���������." 
                                  ValidationGroup="ValidateProfileData" />
   
     </div>     
</div>
</asp:Content>

