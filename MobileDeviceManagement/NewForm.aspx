<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NewForm.aspx.cs" Inherits="MobileDeviceManagement.NewForm" %>

<asp:Content ID="BodyContainer" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function AddItem() {
            var dpItems = document.getElementById('dpItems');
            var id = dpItems.value;
            var item = dpItems.options[dpItems.selectedIndex].text;
            var row = $("<tr>");
            row.append($("<td>" + id + "</td>"))
               .append($("<td>" + item + "</td>"))
               .append($('<td><button type="button" class="btn btn-xs btn-danger"><span class="icon icon-cross"></span></button></td>'));
            $(".table tbody").append(row);
            $('#itemModal').modal('toggle');
            $.ajax({
                type: "POST",
                url: "NewForm.aspx/insertItem",
                data: "{ id: '" + id + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            });
        }
        $( document ).ready(function() {
            $('.table').on('click', '.btn', function (e) {
                var id = $(this).closest('tr').find('td').first().text();
                $(this).closest('tr').remove();
                $.ajax({
                    type: "POST",
                    url: "NewForm.aspx/removeItem",
                    data: "{ id: '" + id + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"
                });
            })
        });
    </script>
    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>
            <div class="form-horizontal">
                <div class="form-group">
                    <label class="col-sm-2 control-label">Full Name</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtFullName" runat="server" class="form-control" placeholder="Full Name"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Witnessed By</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtWitnessed" runat="server" class="form-control" placeholder="Witnessed By"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Device Name</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtDeviceName" runat="server" class="form-control" placeholder="Device Name"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Manufacturer</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtMf" runat="server" class="form-control" placeholder="Manufacturer"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Model</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtModel" runat="server" class="form-control" placeholder="Model"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Type</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtType" runat="server" class="form-control" placeholder="Type"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Serial</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtSerial" runat="server" class="form-control" placeholder="Serial"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Asset Tag</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtAsset" runat="server" class="form-control" placeholder="Asset Tag"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Item Issued</label>
                    <div class="col-sm-10">
                        <div id="divItems" class="container">
                            <div class="row">
                                <table class="table table-condensed table-bordered col-sm-4" id="tbItems">
                                    <tr>
                                        <th>ID</th>
                                        <th>Item</th>
                                        <th>Action</th>
                                    </tr>
                                </table>
                            </div>
                            <div class="row">
                                <button class="btn btn-sm btn-primary-outline" type="button" data-toggle="modal"
                                    data-target="#itemModal">
                                    <span class="icon icon-plus"></span>
                                    Add Item
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Date Collected</label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server" ID="txtDate" CssClass="form-control" placeholder="Date Collected"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-10">
                        <asp:LinkButton ID="lbNext" runat="server" CssClass="btn btn-primary" OnClick="lbNext_Click"><span class="icon icon-arrow-bold-right"></span> Next</asp:LinkButton>
                        <asp:HyperLink ID="btnBack" runat="server" CssClass="btn btn-warning" NavigateUrl="~/Default.aspx"><span class="icon icon-back"></span> Back</asp:HyperLink>
                    </div>
                </div>
            </div>
            <div class="modal" id="itemModal">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span
                                    aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">Add Item</h4>
                        </div>
                        <div class="modal-body">
                            <select class="custom-select" id="dpItems">
                                <asp:Repeater runat="server" ID="rItems">
                                    <ItemTemplate>
                                        <option runat="server" value='<%# Eval("ID") %>'><%# Eval("Item") %></option>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </select>
                        </div>
                        <div class="modal-actions">
                            <button type="button" class="btn-link modal-action" data-dismiss="modal">Cancel</button>
                            <button type="button" class="btn-link modal-action" onclick="AddItem()">
                                <strong>Add</strong>
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
