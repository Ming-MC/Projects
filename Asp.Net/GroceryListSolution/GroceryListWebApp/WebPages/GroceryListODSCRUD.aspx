<%@ Page Title="Grocery List ODS CRUD" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GroceryListODSCRUD.aspx.cs" Inherits="GroceryListWebApp.WebPages.GroceryListODSCRUD" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>CRUD: ODS</h1>

    <div>
        <uc1:MessageUserControl runat="server" ID="MessageUserControl" />

        <asp:ValidationSummary ID="ValidationSummaryEdit" runat="server"
                               HeaderText="Follwing are concerns with your data while editing."
                               ValidationGroup="egroup" />
        <asp:ValidationSummary ID="ValidationInsert" runat="server"
                               HeaderText="Follwing are concerns with your data while inserting."
                               ValidationGroup="igroup" />
    </div>

    <div class="row">
        <asp:ObjectDataSource ID="ProductListODS" runat="server"
                              DataObjectTypeName="GroceryListSystem.ViewModels.ProductItem"
                              SelectMethod="Products_List"
                              InsertMethod="Products_Add"
                              UpdateMethod="Products_Update"
                              DeleteMethod="Products_Delete"
                              OnSelected="SelectCheckForException"
                              OnInserted="InsertCheckForException"
                              OnUpdated="UpdateCheckForException"
                              OnDeleted="DeleteCheckForException"
                              OldValuesParameterFormatString="original_{0}"
                              TypeName="GroceryListSystem.BLL.ProductController">
        </asp:ObjectDataSource>

        <asp:ObjectDataSource ID="CategoryListODS" runat="server"
                              OldValuesParameterFormatString="original_{0}"
                              SelectMethod="Categories_List"
                              OnSelected="SelectCheckForException"
                              TypeName="GroceryListSystem.BLL.CategoryController">
        </asp:ObjectDataSource>

        <asp:ListView ID="ProductList" runat="server"
                      DataSourceID="ProductListODS"
                      InsertItemPosition="FirstItem"
                      DataKeyNames="ProductID" OnSelectedIndexChanged="ProductList_SelectedIndexChanged">

            <AlternatingItemTemplate>
                <tr style="background-color: #FFFFFF; color: #233b74; height:50px">
                    <td>
                        <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" />
                        <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                    </td>
                    <td style="text-align: center">
                        <asp:Label Text='<%# Eval("ProductID") %>' runat="server" ID="ProductIDLabel" Width="50px" />
                    </td>
                    <td>
                        <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" Width="300px" />
                    </td>
                    <td style="text-align: right">
                        <asp:Label Text='<%# Eval("Price", "{0:N2}") %>' runat="server" ID="PriceLabel" Width="60px" />
                    </td>
                    <td style="text-align: right">
                        <asp:Label Text='<%# Eval("Discount", "{0:N2}") %>' runat="server" ID="DiscountLabel" Width="90px" />
                    </td>
                    <td style="text-align: right">
                        <asp:Label Text='<%# Eval("UnitSize") %>' runat="server" ID="UnitSizeLabel" Width="100px" />
                    </td>
                    <td>
                        <%--<asp:Label Text='<%# Eval("CategoryID") %>' runat="server" ID="CategoryIDLabel" />--%>
                        <asp:DropDownList ID="CategoryList" runat="server"
                            DataSourceID="CategoryListODS"
                            DataTextField="DisplayField"
                            DataValueField="ValueField"
                            Enabled="false"
                            SelectedValue='<%# Eval("CategoryID") %>'>
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: center">
                        <asp:CheckBox Checked='<%# Eval("Taxable") %>' runat="server" ID="TaxableCheckBox" Enabled="false" Width="80px" />
                    </td>
                </tr>
            </AlternatingItemTemplate>
            <EditItemTemplate>

                <%-- Required Field Validator --%>
                <asp:RequiredFieldValidator ID="RequiredDescriptionE" runat="server"
                    ErrorMessage="Product description is required."
                    Display="None"
                    ControlToValidate="DescriptionTextBoxE"
                    ValidationGroup="egroup">
                </asp:RequiredFieldValidator>  
                <asp:RequiredFieldValidator ID="RequiredPriceTextBoxE" runat="server"
                    ErrorMessage="Price is required."
                    Display="None"
                    ControlToValidate="PriceTextBoxE"
                    ValidationGroup="egroup">
                </asp:RequiredFieldValidator>      
                <asp:RequiredFieldValidator ID="RequiredDiscountTextBoxE" runat="server"
                    ErrorMessage="Discount is required."
                    Display="None"
                    ControlToValidate="DiscountTextBoxE"
                    ValidationGroup="egroup">
                </asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RequiredUnitSizeE" runat="server"
                    ErrorMessage="Unit size is required."
                    Display="None"
                    ControlToValidate="UnitSizeTextBoxE"
                    ValidationGroup="egroup">
                </asp:RequiredFieldValidator>

                <%-- Regular Expression Validator --%>
                <asp:RegularExpressionValidator ID="RegExDescriptionE" runat="server"
                    ErrorMessage="Product description is limited to 100 characters."
                    Display="None"
                    ControlToValidate="DescriptionTextBoxE"
                    ValidationGroup="egroup"
                    ValidationExpression="^.{1,100}$">
                </asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegExUnitSizeE" runat="server"
                    ErrorMessage="Unit size is limited to 20 characters."
                    Display="None"
                    ControlToValidate="UnitSizeTextBoxE"
                    ValidationGroup="egroup"
                    ValidationExpression="^.{1,20}$">
                </asp:RegularExpressionValidator>

                <%-- Compare Validator --%>
                <asp:CompareValidator ID="CheckDiscountTextBoxE" runat="server" 
                    ErrorMessage="Discount should be a positive number."
                    Display="None"
                    ControlToValidate="DiscountTextBoxE"
                    type="Double"
                    Operator="GreaterThanEqual"
                    ValueToCompare="0.00"
                    ValidationGroup="egroup">
                </asp:CompareValidator>

                <asp:CompareValidator ID="ComparePriceTextBoxE" runat="server"          
                    ErrorMessage="Price should be a positive number and greater than discount."
                    Display="None"
                    ControlToValidate="PriceTextBoxE"
                    ControlToCompare="DiscountTextBoxE"
                    type="Double"
                    Operator="GreaterThanEqual"
                    ValidationGroup="egroup">
                </asp:CompareValidator>

                <tr style="background-color: #999999; height:50px">
                    <td>
                        <asp:Button runat="server" CommandName="Update" Text="Update" ID="UpdateButton" ValidationGroup="egroup" />
                        <asp:Button runat="server" CommandName="Cancel" Text="Cancel" ID="CancelButton" />
                    </td>
                    <td style="text-align: center">
                        <asp:TextBox Text='<%# Bind("ProductID") %>' runat="server" ID="ProductIDTextBox" Enabled="false" Width="50px" />
                    </td>
                    <td>
                        <asp:TextBox Text='<%# Bind("Description") %>' runat="server" ID="DescriptionTextBoxE" Width="300px" />
                    </td>
                    <td style="text-align: right">
                        <asp:TextBox Text='<%# Bind("Price", "{0:N2}") %>' runat="server" ID="PriceTextBoxE" Width="60px" />
                    </td>
                    <td style="text-align: right">
                        <asp:TextBox Text='<%# Bind("Discount", "{0:N2}") %>' runat="server" ID="DiscountTextBoxE" Width="90px" />
                    </td>
                    <td style="text-align: right">
                        <asp:TextBox Text='<%# Bind("UnitSize") %>' runat="server" ID="UnitSizeTextBoxE" Width="100px" />
                    </td>
                    <td>
                        <%--<asp:TextBox Text='<%# Bind("CategoryID") %>' runat="server" ID="CategoryIDTextBox" />--%>
                        <asp:DropDownList ID="CategoryList" runat="server"
                            DataSourceID="CategoryListODS"
                            DataTextField="DisplayField"
                            DataValueField="ValueField"
                            SelectedValue='<%# Bind("CategoryID") %>'>
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: center">
                        <asp:CheckBox Checked='<%# Bind("Taxable") %>' runat="server" ID="TaxableCheckBox" Width="80px" />
                    </td>
                </tr>
            </EditItemTemplate>
            <EmptyDataTemplate>
                <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                    <tr>
                        <td>No data was returned.</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <InsertItemTemplate>

                <%-- Required Field Validator --%>
                <asp:RequiredFieldValidator ID="RequiredDescriptionI" runat="server"
                    ErrorMessage="Product description is required."
                    Display="None"
                    ControlToValidate="DescriptionTextBoxI"
                    ValidationGroup="igroup">
                </asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RequiredPriceTextBoxI" runat="server"
                    ErrorMessage="Price is required."
                    Display="None"
                    ControlToValidate="PriceTextBoxI"
                    ValidationGroup="igroup">
                </asp:RequiredFieldValidator>     
                <asp:RequiredFieldValidator ID="RequiredDiscountTextBoxI" runat="server"
                    ErrorMessage="Discount is required."
                    Display="None"
                    ControlToValidate="DiscountTextBoxI"
                    ValidationGroup="igroup">
                </asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RequiredUnitSizeI" runat="server"
                    ErrorMessage="Unit size is required."
                    Display="None"
                    ControlToValidate="UnitSizeTextBoxI"
                    ValidationGroup="igroup">
                </asp:RequiredFieldValidator>

                <%-- Regular Expression Validator --%>
                <asp:RegularExpressionValidator ID="RegExDescriptionI" runat="server"
                    ErrorMessage="Product description is limited to 100 characters."
                    Display="None"
                    ControlToValidate="DescriptionTextBoxI"
                    ValidationGroup="igroup"
                    ValidationExpression="^.{1,100}$">
                </asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegExUnitSizeI" runat="server"
                    ErrorMessage="Unit size is limited to 20 characters."
                    Display="None"
                    ControlToValidate="UnitSizeTextBoxI"
                    ValidationGroup="igroup"
                    ValidationExpression="^.{1,20}$">
                </asp:RegularExpressionValidator>    

                <%-- Compare Validator --%>
                <asp:CompareValidator ID="CheckDiscountTextBoxI" runat="server" 
                    ErrorMessage="Discount should be a positive number."
                    Display="None"
                    ControlToValidate="DiscountTextBoxI"
                    type="Double"
                    Operator="GreaterThanEqual"
                    ValueToCompare="0.00"
                    ValidationGroup="igroup">
                </asp:CompareValidator>

                <asp:CompareValidator ID="ComparePriceTextBoxI" runat="server"          
                    ErrorMessage="Price should be a positive number and greater than discount."
                    Display="None"
                    ControlToValidate="PriceTextBoxI"
                    ControlToCompare="DiscountTextBoxI"
                    type="Double"
                    Operator="GreaterThanEqual"
                    ValidationGroup="igroup">
                </asp:CompareValidator>

                <tr style="height:50px" >
                    <td>
                        <asp:Button runat="server" CommandName="Insert" Text="Insert" ID="InsertButton" ValidationGroup="igroup" />
                        <asp:Button runat="server" CommandName="Cancel" Text="Clear" ID="CancelButton" />
                    </td>
                    <td style="text-align: center">
                        <asp:TextBox Text='<%# Bind("ProductID") %>' runat="server" ID="ProductIDTextBox" Enabled="false" Width="50px" />
                    </td>
                    <td>
                        <asp:TextBox Text='<%# Bind("Description") %>' runat="server" ID="DescriptionTextBoxI" Width="300px" />
                    </td>
                    <td style="text-align: right">
                        <asp:TextBox Text='<%# Bind("Price", "{0:N2}") %>' runat="server" ID="PriceTextBoxI" Width="60px" />
                    </td>
                    <td style="text-align: right">
                        <asp:TextBox Text='<%# Bind("Discount", "{0:N2}") %>' runat="server" ID="DiscountTextBoxI" Width="90px" />
                    </td>
                    <td style="text-align: right">
                        <asp:TextBox Text='<%# Bind("UnitSize") %>' runat="server" ID="UnitSizeTextBoxI" Width="100px" />
                    </td>
                    <td>
                        <%--<asp:TextBox Text='<%# Bind("CategoryID") %>' runat="server" ID="CategoryIDTextBox" />--%>
                        <asp:DropDownList ID="CategoryList" runat="server"
                            DataSourceID="CategoryListODS"
                            DataTextField="DisplayField"
                            DataValueField="ValueField"
                            SelectedValue='<%# Bind("CategoryID") %>'>
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: center">
                        <asp:CheckBox Checked='<%# Bind("Taxable") %>' runat="server" ID="TaxableCheckBox" Width="80px" />
                    </td>
                </tr>
            </InsertItemTemplate>
            <ItemTemplate>
                <tr style="background-color: #d2e7fa; color: #333333; height:50px">
                    <td>
                        <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" />
                        <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                    </td>
                    <td style="text-align: center">
                        <asp:Label Text='<%# Eval("ProductID") %>' runat="server" ID="ProductIDLabel" Width="50px" />
                    </td>
                    <td>
                        <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" Width="300px" />
                    </td>
                    <td style="text-align: right">
                        <asp:Label Text='<%# Eval("Price", "{0:N2}") %>' runat="server" ID="PriceLabel" Width="60px" />
                    </td>
                    <td style="text-align: right">
                        <asp:Label Text='<%# Eval("Discount", "{0:N2}") %>' runat="server" ID="DiscountLabel" Width="90px" />
                    </td>
                    <td style="text-align: right">
                        <asp:Label Text='<%# Eval("UnitSize") %>' runat="server" ID="UnitSizeLabel" WWidth="100px" />
                    </td>
                    <td>
                        <%--<asp:Label Text='<%# Eval("CategoryID") %>' runat="server" ID="CategoryIDLabel" />--%>
                        <asp:DropDownList ID="CategoryList" runat="server"
                            DataSourceID="CategoryListODS"
                            DataTextField="DisplayField"
                            DataValueField="ValueField"
                            Enabled="false"
                            SelectedValue='<%# Eval("CategoryID") %>'>
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: center">
                        <asp:CheckBox Checked='<%# Eval("Taxable") %>' runat="server" ID="TaxableCheckBox" Enabled="false" Width="80px" />
                    </td>
                </tr>
            </ItemTemplate>
            <LayoutTemplate>
                <table runat="server" style="width:100%">
                    <tr runat="server"  style="height:50px" >
                        <td runat="server">
                            <table runat="server" id="itemPlaceholderContainer"
                                   style="background-color: #FFFFFF; 
                                          border-collapse: collapse; 
                                          border-color: #999999; 
                                          border-style: none; 
                                          border-width: 1px; 
                                          font-family: Verdana, Arial, Helvetica, sans-serif;
                                          width:100%"
                                   border="1">
                                <tr runat="server" style="background-color: #003366; color: #FFFFFF; height: 50px">
                                    <th runat="server"></th>
                                    <th runat="server" style="text-align: center">ID</th>
                                    <th runat="server" style="text-align: center">Description</th>
                                    <th runat="server" style="text-align: center">Price</th>
                                    <th runat="server" style="text-align: center">Discount</th>
                                    <th runat="server" style="text-align: center">UnitSize</th>
                                    <th runat="server" style="text-align: center">Category</th>
                                    <th runat="server" style="text-align: center">Taxable</th>
                                </tr>
                                <tr runat="server" id="itemPlaceholder"></tr>
                            </table>
                        </td>
                    </tr>
                    <tr runat="server">
                        <td runat="server" 
                            style="text-align: center; 
                                   background-color: #FFFFFF; 
                                   font-family: Verdana, Arial, Helvetica, sans-serif; 
                                   color: #000000;
                                   height: 50px" >
                            <asp:DataPager runat="server" ID="DataPager1">
                                <Fields>
                                    <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                    <asp:NumericPagerField></asp:NumericPagerField>
                                    <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                </Fields>
                            </asp:DataPager>
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
            <SelectedItemTemplate>
                <tr style="background-color: #E2DED6; font-weight: bold; color: #333333; height:50px">
                    <td>
                        <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" />
                        <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                    </td>
                    <td style="text-align: center">
                        <asp:Label Text='<%# Eval("ProductID") %>' runat="server" ID="ProductIDLabel" Width="50px" />
                    </td>
                    <td>
                        <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" Width="
                            300px" />
                    </td>
                    <td style="text-align: right">
                        <asp:Label Text='<%# Eval("Price", "{0:N2}") %>' runat="server" ID="PriceLabel" Width="60px" />
                    </td>
                    <td style="text-align: right">
                        <asp:Label Text='<%# Eval("Discount", "{0:N2}") %>' runat="server" ID="DiscountLabel" Width="90px" />
                    </td>
                    <td>
                        <asp:Label Text='<%# Eval("UnitSize") %>' runat="server" ID="UnitSizeLabel" Width="100px" />
                    </td>
                    <td>
                        <%--<asp:Label Text='<%# Eval("CategoryID") %>' runat="server" ID="CategoryIDLabel" />--%>
                        <asp:DropDownList ID="CategoryList" runat="server"
                            DataSourceID="CategoryListODS"
                            DataTextField="DisplayField"
                            DataValueField="ValueField"
                            Enabled="false"
                            SelectedValue='<%# Eval("CategoryID") %>'>
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: center">
                        <asp:CheckBox Checked='<%# Eval("Taxable") %>' runat="server" ID="TaxableCheckBox" Enabled="false" Width="80px" />
                    </td>
                </tr>
            </SelectedItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>
