<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form.aspx.cs"
    Inherits="MobileDeviceManagement.Form" %>


<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title><%: Page.Title %></title>

    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>
    <link href="favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />

</head>
<body>
    <form runat="server">
        <asp:ScriptManager runat="server">
            <Scripts>
                <%--To learn more about bundling scripts in ScriptManager see http://go.microsoft.com/fwlink/?LinkID=301884 --%>
                <%--Framework Scripts--%>
                <asp:ScriptReference Name="MsAjaxBundle" />
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="respond" />
                <asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
                <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
                <asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Scripts/WebForms/GridView.js" />
                <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
                <asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
                <asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />
                <asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />
                <asp:ScriptReference Name="WebFormsBundle" />
                <%--Site Scripts--%>
            </Scripts>
        </asp:ScriptManager>
        <div class="container body-content" style="font-size: 16px;">
            <div class="page-header">
                <div class="row">
                    <div class="container">
                        <h3>RICOH (SINGAPORE) PTE LTD <small class="pull-right p-r-lg">Form No: ISMS-POL-05-F1-02</small>
                        </h3>
                    </div>
                </div>
                <div class="row">
                    <div class="container">
                        <h3 class="text-center">DEVICE ISSUE FORM <small class="pull-right p-r-lg">Rev:00
                    <br />
                            Page 1 of 1</small></h3>
                    </div>
                </div>
            </div>
            <div class="container">
                <p>The following equipment is entrusted to <b><asp:Label ID="lblFullName1" runat="server" Text=""></asp:Label></b> on <u>
                    <asp:Label ID="lblDate1" runat="server" Text=""></asp:Label></u>.</p>
                <p>
                    The above mentioned user is responsible for maintaining it in good condition.
                </p>
                <p>
                    The user is responsible that the use of this mobile device is in consistence with
                    the Company Policies such as the
                    <br />
                    <strong>Acceptable use of IT Resources policy and Mobile
                        Computing Device Usage Policy (ISMS-POL-05).</strong>
                </p>
                <p>
                    NO unlicensed software applications are to be installed on the mobile device, and
                    are not authorized to change any setting unless authorized by the IT department.
                    No changing / adding / re assigning /dispose of this device is allowed unless being
                    authorised by the IT department.
                </p>
                <p>
                    The user must ensure that every due care is being taken to ensure only sufficiently
                    trained personnel operate the equipment and in the right conditions. He is responsible
                    for reporting any defects and in the case of theft of damage to the IT department.
                </p>
                <p>
                    In the event of any damage, loss due to theft or other cost incurred to repair due
                    to the user’s negligence, the user will be responsible for bearing the cost of repair
                    or in a worse case scenario the cost of replacing the unit.
                </p>
                <p>This equipment: </p>
                <div class="row">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Device Name</label>
                            <div class="col-sm-10">
                                <Label ID="lblDeviceName" runat="server" class="control-label"></Label>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Manufacturer</label>
                            <div class="col-sm-10">
                                <Label ID="lblMf" runat="server" class="control-label"></Label>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Model</label>
                            <div class="col-sm-10">
                                <Label ID="lblModel" runat="server" class="control-label"></Label>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Type</label>
                            <div class="col-sm-10">
                                <Label ID="lblType" runat="server" class="control-label"></Label>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Serial</label>
                            <div class="col-sm-10">
                                <Label ID="lblSerial" runat="server" class="control-label"></Label>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Asset Tag</label>
                            <div class="col-sm-10">
                                <Label ID="lblAsset" runat="server" class="control-label"></Label>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Item Issued</label>
                            <div class="col-sm-10">
                                <asp:Repeater ID="rItems" runat="server">
                                    <ItemTemplate>
                                        <label class="control-label"><span class="label label-primary"><%# Eval("Item") %></span></label>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                    <div class="form-horizontal">
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Full Name</label>
                                    <div class="col-sm-6">
                                        <Label ID="lblFullName2" runat="server" class="control-label"></Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Sign</label>
                                    <div class="col-sm-6">
                                        <label class="control-label">_________________________________</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Witnessed By</label>
                                    <div class="col-sm-6">
                                        <Label ID="lblWitnessed" runat="server" class="control-label"></Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Sign</label>
                                    <div class="col-sm-6">
                                        <label class="control-label">_________________________________</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">Date Collected</label>
                                    <div class="col-sm-6">
                                        <Label ID="lblDate2" runat="server" class="control-label"></Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="container text-center">
                <asp:LinkButton ID="btnPDF" runat="server" OnClick="btnPDF_Click" CssClass="btn btn-primary">Download PDF</asp:LinkButton>
                <asp:HyperLink ID="btnBack" runat="server" NavigateUrl="~/Default.aspx" CssClass="btn btn-warning">Back</asp:HyperLink>
            </div>
        </div>
    </form>
</body>
</html>
