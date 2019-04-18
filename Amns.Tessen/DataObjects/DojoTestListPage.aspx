<%@ Register TagPrefix="cc1" Namespace="Amns.Tessen.Web.UI.WebControls" Assembly="Amns.Tessen" %>
<%@ Page language="c#" CodeFile="DojoTestListPage.aspx.cs" Inherits="TessenWeb.Administration.DojoTestListPage" trace="false" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Enterprise Services</title>
		<link href="./Themes/Admin.css" type="text/css" rel="stylesheet">
	</head>
	<body>
		<form id="Default" method="post" runat="server">
			<table id="MainPage" width="100%" border="0" cellspacing="0" cellpadding="2" height="100%">
				<tr>
					<td class="bodyLine" bgcolor="#ffffff">
						<table id="Table1" cellSpacing="0" cellPadding="3" width="100%" border="0" height="100%">
							<tr>
								<td class="bgTop" colSpan="2">
									<table id="Header" cellSpacing="0" cellPadding="3" width="100%" border="0">
										<tr><td><h1>Enterprise Services</h1></td></tr>
										<tr><td>Menu | Menu</td></tr>
									</table>
								</td>
							</tr>
							<tr>
								<td vAlign="top">
									<table id="LeftMenu" class="forumLine" cellSpacing="1" cellPadding="3" width="200" border="0">
										<tr><th></th></tr>
										<tr><td> can be accessed here</td></tr>
									</table>
								</td>
								<td vAlign="top" width="100%">
									<cc1:DojoTestListGrid id="DojoTestListGrid1" runat="server" Width="100%" BorderWidth="0px" Height="100%" 
										CellSpacing="1px" CellPadding="3px" CssClass="forumLine" HeaderCssClass="thHead" SubHeaderCssClass="catHead"
										HeaderRowCssClass="rowHead" AlternateRowCssClass="row2" SelectedRowCssClass="row3" DefaultRowCssClass="row1"
										Text=""
										></cc1:DojoTestListGrid>
									<cc1:DojoTestListEditor id="DojoTestListEditor1" runat="server" Width="100%" BorderWidth="0px"
										CellSpacing="1px" CellPadding="3px" CssClass="forumLine" HeaderCssClass="thHead" SubHeaderCssClass="catHead" Visible="false"
										Text=" Editor"
										></cc1:DojoTestListEditor>
									<cc1:DojoTestListView id="DojoTestListView1" runat="server" Width="100%" BorderWidth="0px"
										CellSpacing="1px" CellPadding="3px" CssClass="forumLine" HeaderCssClass="thHead" SubHeaderCssClass="catHead" Visible="false"
										Text=" View"
										></cc1:DojoTestListView>
								</td>
							</tr>
							<tr>
								<td colSpan="2" class="bgBottom" height="37" align="center">
									<p class="copyright">Copyright © 2006 Anyone Corp.</p>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
