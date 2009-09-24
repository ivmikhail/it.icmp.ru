<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="structure.aspx.cs" Inherits="ITCommunity.StructurePage" Title="Ykt IT Community | Управлять категориями/меню" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
	    window.addEvent('domready', init);
	    function init() {
		    myTabs = new TabSwapper({
                        selectedClass: 'on',
                        deselectedClass: 'off',
                        tabs: $$('#structure-tabs li'),
                        clickers: $$('#structure-tabs li a'),
                        sections: $$('div.panelSet div.panel'),
                        cookieName: 'structure-tabs',
                        smooth: true,
	                    smoothSize: true
                     });
	        }
    </script>
    
    <h2>Список категорий/меню</h2>    
    <p>
        Внимание после каких-либо изменений, рекомендуется 
        <asp:LinkButton ID="LinkButtonDropCache" runat="server" OnClick="LinkButtonDropCache_Click">сбросить кеш</asp:LinkButton>
    </p>
    <p class="error-message">
        <asp:Literal ID="LiteralCacheMessage" runat="server"/>
    </p>
        <div id="structure-tabs">
	        <ul class="tabSet">
		        <li class="off"><a>Категории</a></li>
		        <li class="off"><a>Меню</a></li>
	        </ul>
	    
            <div class="panelSet">
	            <div class="panel">
	                <div id="category_add">	                
	                    <h2>Добавить категорию</h2>
	                    <table cellspacing="5px">
	                        <tr>
	                            <td>
	                                <h3>Название</h3>   
	                            </td>
	                            <td colspan="2">
                                    <h3>Сортировка(int)</h3>
                                </td>
                            </tr>
                            <tr>
	                            <td>
                                    <asp:TextBox ID="TextBoxCatName" runat="server" />
	                            </td>
	                            <td>
                                    <asp:TextBox ID="TextBoxCatSort" runat="server" Width="50px"/>
                                </td>
                                <td>                            
                                    <asp:LinkButton ID="LinkButtonAddCat" runat="server" OnClick="LinkButtonAddCat_Click">добавить</asp:LinkButton>                                    
                                </td>
                            </tr>
                         </table>
	                </div>
	                <br />
	            	<asp:GridView ID="gv_categories" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSourceCategories" PageSize="20" EnableSortingAndPagingCallbacks="True" CellSpacing="5">
                            <Columns>         
                                <asp:BoundField DataField="id"   HeaderText="id"   SortExpression="id" ReadOnly="True"/>         
                                <asp:BoundField DataField="name" HeaderText="name" SortExpression="name"/>
                                <asp:BoundField DataField="sort" HeaderText="sort(int)" SortExpression="sort"/>
                                              
                                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" EditText="edit" CancelText="cancel" DeleteText="del" UpdateText="upd" />
                            </Columns>
                       </asp:GridView>
                       <asp:SqlDataSource ID="SqlDataSourceCategories" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                 ProviderName="<%$ ConnectionStrings:ConnectionString.providerName%>"
                                 SelectCommandType="StoredProcedure"
                                 SelectCommand="CategoryGetAll"
                
                                 DeleteCommandType="StoredProcedure"
                                 DeleteCommand="CategoryDel"
                     
                                 UpdateCommandType="StoredProcedure"
                                 UpdateCommand="CategoryUpdate">
                           <DeleteParameters>
                               <asp:Parameter Name="id" Type="Int32"  Direction="Input" DefaultValue="-1"/>
                           </DeleteParameters>
                           <UpdateParameters>
                               <asp:Parameter Name="id"   Type="Int32"  Direction="Input" DefaultValue="-1" />
                               <asp:Parameter Name="name" Type="String" Direction="Input" DefaultValue="null"/>
                               <asp:Parameter Name="sort" Type="Int32"  Direction="Input" DefaultValue="0" />
                           </UpdateParameters>
                       </asp:SqlDataSource>   
	            </div>
	            <div class="panel">
	            
	            	    <h2>Добавить пункт в меню</h2>
	                    <table cellspacing="5px">
	                        <tr>
	                            <td>
	                                <h3>parent_id(int)</h3>	                                
                                    <asp:TextBox ID="TextBoxMenuParent" runat="server" />
	                            </td>	                            
	                            <td> 
                                    <h3>name</h3>
                                    <asp:TextBox ID="TextBoxMenuName" runat="server"/>
                                </td>  
	                            <td>
                                    <h3>открывать в новом окне(1 или 0)</h3>
                                    <asp:TextBox ID="TextBoxMenuWindow" runat="server"/>
                                </td>
	                        </tr>
                            <tr>
                                <td>
	                                <h3>url</h3>   
                                    <asp:TextBox ID="TextBoxMenuUrl" runat="server" />
	                            </td>
                                <td>
                                    <h3>sort(int)</h3>
                                    <asp:TextBox ID="TextBoxMenuSort" runat="server"/>
                                </td>
                                <td>                                
                                    <asp:LinkButton ID="LinkButtonAddMenu" runat="server" OnClick="LinkButtonAddMenu_Click">добавить</asp:LinkButton>   
                                </td>
                            </tr>
                         </table>
                         
                         <br />
                         
	            	    <asp:GridView ID="gv_menuitems" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="SqlDataSourceMenu" PageSize="20" EnableSortingAndPagingCallbacks="True" CellSpacing="5">
                            <Columns>                  
                                <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" ReadOnly="True"
                                    SortExpression="id" />
                                <asp:BoundField DataField="parent_id" HeaderText="parent_id" SortExpression="parent_id" />
                                <asp:BoundField DataField="url" HeaderText="url" SortExpression="url" />
                                <asp:BoundField DataField="sort" HeaderText="sort" SortExpression="sort" />
                                <asp:BoundField DataField="name" HeaderText="name" SortExpression="name" />
                                <asp:BoundField DataField="new_window" HeaderText="new_window" SortExpression="new_window" />
                                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" EditText="edit" CancelText="cancel" DeleteText="del" UpdateText="upd" />
                
                            </Columns>
                       </asp:GridView>
                       <asp:SqlDataSource ID="SqlDataSourceMenu" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                 ProviderName="<%$ ConnectionStrings:ConnectionString.providerName%>"
                                 SelectCommandType="StoredProcedure"
                                 SelectCommand="MenuItemsGetAll"
                
                                 DeleteCommandType="StoredProcedure"
                                 DeleteCommand="MenuItemsDel"
                     
                                 UpdateCommandType="StoredProcedure"
                                 UpdateCommand="MenuItemsUpdate">
                           <DeleteParameters>
                               <asp:Parameter Name="id" Type="Int32"  Direction="Input" DefaultValue="-1"/>
                           </DeleteParameters>
                           <UpdateParameters>
                               <asp:Parameter Name="id"         Type="Int32"  Direction="Input" DefaultValue="-1" />
                               <asp:Parameter Name="parent_id"  Type="Int32"  Direction="Input" DefaultValue="0"/>
                               <asp:Parameter Name="name"       Type="String" Direction="Input" DefaultValue="0" />                               
                               <asp:Parameter Name="url"        Type="String" Direction="Input" DefaultValue="-1" />
                               <asp:Parameter Name="new_window" Type="Int32"  Direction="Input" DefaultValue="1"/>
                               <asp:Parameter Name="sort"       Type="Int32"  Direction="Input" DefaultValue="0" />
                           </UpdateParameters>
                       </asp:SqlDataSource>   
	            </div>
            </div> 
        </div>
</asp:Content>

