<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MobileDeviceManagement._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Forms
        <asp:HyperLink ID="btnNew" runat="server" CssClass="btn btn-primary pull-right" NavigateUrl="~/NewForm.aspx"><span class="icon icon-plus"></span> New Form</asp:HyperLink>
    </h1>
    <div class="form-inline">
        <div class="form-group">
            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:LinkButton ID="btnSearch" runat="server" CssClass="btn btn-primary-outline"
                OnClick="btnSearch_Click"><span class="icon icon-magnifying-glass"></span> Search</asp:LinkButton>
        </div>
    </div>
    <div class="table-responsive">
        <asp:GridView ID="gvForms" runat="server" CssClass="table table-striped" GridLines="None" OnRowDataBound="gvForms_RowDataBound" DataKeyNames="#"
            AllowPaging="true" PageSize="15" OnRowCommand="gvForms_RowCommand" AutoGenerateColumns="false" OnPageIndexChanging="gvForms_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="#" HeaderText="#" />
                <asp:BoundField DataField="Full Name" HeaderText="Full Name" />
                <asp:BoundField DataField="Witnessed By" HeaderText="Witnessed By" />
                <asp:BoundField DataField="Device Name" HeaderText="Device Name" />
                <asp:BoundField DataField="Manufacturer" HeaderText="Manufacturer" />
                <asp:BoundField DataField="Model" HeaderText="Model" />
                <asp:BoundField DataField="Type" HeaderText="Type" />
                <asp:BoundField DataField="Serial" HeaderText="Serial" />
                <asp:BoundField DataField="Asset Tag" HeaderText="Asset Tag" />
                <asp:BoundField DataField="Date Collected" HeaderText="Date Collected" />
                <asp:TemplateField HeaderText="Items Issued" ItemStyle-Width="20%">
                    <ItemTemplate>
                        <div>
                            <asp:Repeater ID="rFormItems" runat="server">
                                <ItemTemplate>
                                    <span class="label label-primary"><%# Eval("Item") %></span>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnView" runat="server" CssClass="btn btn-xs btn-primary-outline"
                            CommandName="view" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><span class="icon icon-eye"></span> View</asp:LinkButton>
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
