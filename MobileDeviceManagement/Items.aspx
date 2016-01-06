<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Items.aspx.cs" Inherits="MobileDeviceManagement.Items" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-inline">
        <asp:TextBox ID="txtItem" runat="server" CssClass="form-control"></asp:TextBox>
        <asp:LinkButton ID="btnAdd" runat="server" CssClass="btn btn-primary-outline" OnClick="btnAdd_Click"><span class="icon icon-plus"></span> Add Item</asp:LinkButton>
    </div>
    <div class="table-responsive">
        <asp:GridView ID="gvItems" runat="server" GridLines="None" CssClass="table table-striped"
            AutoGenerateColumns="false" DataKeyNames="#" AllowPaging="true" PageSize="15"
            OnRowCommand="gvItems_RowCommand"
            OnPageIndexChanging="gvItems_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="#" HeaderText="#" />
                <asp:BoundField DataField="Item" HeaderText="Item" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnRemove" runat="server" CssClass="btn btn-xs btn-danger-outline"
                            CommandName="remove" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            OnClientClick="return confirm('Are you sure you want to remove this form?');"><span class="icon icon-cross"></span> Delete</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="pagination-ys" />
        </asp:GridView>
    </div>
</asp:Content>
