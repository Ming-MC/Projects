<%@ Page Title="Grocery List ODS Query" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GroceryListODSQuery.aspx.cs" Inherits="GroceryListWebApp.WebPages.GroceryListODSQuery" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Query: ODS</h1>
    <uc1:MessageUserControl runat="server" ID="MessageUserControl" /> 
    <br />
    <br />

    <div >
        <label>Select a category&nbsp;</label>
    
        <asp:ObjectDataSource ID="CategoryODS" runat="server" 
                              OldValuesParameterFormatString="original_{0}" 
                              SelectMethod="Categories_List" 
                              OnSelected="SelectCheckForException"
                              TypeName="GroceryListSystem.BLL.CategoryController">
        </asp:ObjectDataSource>

        <asp:DropDownList ID="CategoryList" runat="server" 
                          DataSourceID="CategoryODS" 
                          DataTextField="DisplayField" 
                          DataValueField="ValueField"
                          AppendDataBoundItems="true">
            <asp:ListItem Value="0">Select a category...</asp:ListItem>
        </asp:DropDownList>&nbsp;
        
        <asp:LinkButton ID="FetchProducts" runat="server" OnClick="FetchProducts_Click">Fetch</asp:LinkButton>
        <br />
        <br />
    </div>

    <asp:GridView ID="ProductsofCategoryList" runat="server" 
                  AutoGenerateColumns="False"
                  AllowPaging="true"
                  PageSize="5"
                  OnPageIndexChanging="ProductsofCategoryList_PageIndexChanging" 
                  HeaderStyle-HorizontalAlign="Center" 
                  HeaderStyle-Height="50px" HorizontalAlign="Center">
        <Columns>
            <asp:TemplateField HeaderText="ID" ItemStyle-Width="50px" ItemStyle-Height="30px" ItemStyle-HorizontalAlign="Center" >
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("ProductID") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Description" ItemStyle-Width="300px">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("Description") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Price" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Right" >
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("Price", "{0:N2}") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Disc" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Right" >
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("Discount", "{0:N2}") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Unit Size" ItemStyle-Width="110px" ItemStyle-HorizontalAlign="Right" >
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("UnitSize") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Taxable" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" >
                <ItemTemplate>
                    <asp:checkbox runat="server" checked='<%# Eval("Taxable") %>' Enabled="false" ></asp:checkbox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

        <EmptyDataTemplate>
            Category has no products file.
        </EmptyDataTemplate>

<HeaderStyle HorizontalAlign="Center" Height="35px" BackColor="#003366" ForeColor="White"></HeaderStyle>
    </asp:GridView>
</asp:Content>
