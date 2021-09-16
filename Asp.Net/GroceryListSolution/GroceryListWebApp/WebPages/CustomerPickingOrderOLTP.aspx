<%@ Page Title="Customer Picking Order OLTP" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerPickingOrderOLTP.aspx.cs" Inherits="GroceryListWebApp.WebPages.CustomerPickingOrderOLTP" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Customer Picking Order</h1>
        <br />
        <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    </div>

    <div class="container">
        <%-- Info --%>
        <div class="row">
            <div class="col-3">
                <asp:Label ID="Label1" runat="server" Text="Order No:"></asp:Label>
            </div>
            <div class="col-3">
                <asp:Label ID="PickerName" runat="server" Text="Picker:"></asp:Label>
            </div>
            <div class="col-6" style="text-align: right">
                <i class="fa fa-user" id="icon" runat="server" visible="false"></i>&nbsp;&nbsp;
               
                <asp:Label ID="CustomerName" runat="server"></asp:Label>
            </div>

            <div class="col-3">
                <asp:TextBox ID="OrderId" runat="server" Style="border-radius: 5px"></asp:TextBox>
            </div>
            <div class="col-3">
                <asp:TextBox ID="PickerId" runat="server" Style="border-radius: 5px"></asp:TextBox>
            </div>
            <div class="col-6" style="text-align: right">
                <asp:Label ID="CustomerPhone" runat="server"></asp:Label>
            </div>
        </div>
        <br />

        <%-- Buttons --%>
        <div class="row">
            <div class="col-6">
                <asp:Button ID="FetchBtn" runat="server" Text="Fetch" class="btn btn-primary" Width="100px" OnClick="FetchBtn_Click" />
            </div>
            <div class="col-6" style="text-align: right">
                <asp:Button ID="SaveBtn" runat="server" Text="Save" class="btn btn-primary" Width="100px" OnClick="SaveBtn_Click" Visible="false" />&nbsp;&nbsp;
               
                <asp:Button ID="ClearBtn" runat="server" Text="Clear" class="btn btn-primary" Width="100px" OnClick="ClearBtn_Click" Visible="false" />
            </div>
        </div>
        <br />

       
        <%-- List --%>
        <div id="OrderGridView" runat="server" visible="false">
            <h3>Order (Quantities and Concerns)</h3>

            <asp:GridView ID="OrderedItemList" runat="server"
                          AutoGenerateColumns="False"
                          HeaderStyle-HorizontalAlign="Center"
                          HeaderStyle-Height="40px"
                          AppendDataBoundItems="true"
                          Width="100%">
                <Columns>
                    <asp:TemplateField HeaderText="Product" ItemStyle-Height="40px" HeaderStyle-BackColor="#cccccc" Visible="false">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("OrderListID") %>' ID="OrderListID"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
            
                    <asp:TemplateField HeaderText="Product" ItemStyle-Height="40px" HeaderStyle-BackColor="#cccccc" Visible="false">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("ProductID") %>' ID="ProductID"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Product" ItemStyle-Height="40px" HeaderStyle-BackColor="#cccccc">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("Product") %>' Style="display: inline; margin: 10px" ID="Product"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Req Qty" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="110px" HeaderStyle-BackColor="#cccccc">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("QtyOrdered") %>' Style="display: inline; margin: 10px" ID="QtyOrdered"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Directions" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="150px" HeaderStyle-BackColor="#cccccc">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("CustomerComment") %>' Style="display: inline; margin: 10px" ID="CustomerComment"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Picked Qty" ItemStyle-Width="110px" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#cccccc">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="QtyPicked" Width="100px" Style="text-align: right; border-radius: 5px" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Picked Concerns" ItemStyle-Width="290px" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#cccccc">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="PickIssue" Width="290px" Style="text-align: right; border-radius: 5px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
