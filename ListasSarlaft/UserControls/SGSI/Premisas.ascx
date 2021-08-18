<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Premisas.ascx.cs" Inherits="ListasSarlaft.UserControls.SGSI.Premisas" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<style type="text/css">
    .ajax__html_editor_extender_texteditor {
        background-color: White;
        font-size: small;
        font-weight: lighter;
        font-family: Calibri;
        font-style: normal;
    }
    .gridViewHeader a:link {
        text-decoration: none;
    }
    .FondoAplicacion {
        background-color: Gray;
        filter: alpha(opacity=80);
        opacity: 0.8;
    }
    .scrollingControlContainer {
        overflow-x: hidden;
        overflow-y: scroll;
    }
    .scrollingCheckBoxList {
        border: 1px #808080 solid;
        margin: 10px 10px 10px 10px;
        height: 200px;
    }
    .popup {
        border: Silver 1px solid;
        color: #060F40;
        background: #ffffff;
    }
    .no-visible {
        display: none
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <br />
        <table align="center" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label61" runat="server" ForeColor="White" Text="Premisas"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large" Width="400px"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <div id="dvConsultaPremisa" runat="server" class="ColumnStyle">
    <table align="center">
        <tr align="center" >
            <td style="margin-left: 40px" >
                <asp:GridView ID="gvPremisas" runat="server" CellPadding="4" EnableModelValidation="True" 
                    ForeColor="#333333" GridLines="Vertical" AutoGenerateColumns="False" AllowPaging="False"
                    OnPageIndexChanging="gvPremisas_PageIndexChanging" OnRowCommand="gvPremisas_RowCommand" 
                    ShowHeaderWhenEmpty="True" BorderStyle="Solid"
                    
                    HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small" >
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                    <Columns>
                        <asp:BoundField DataField="IdPremisa" HeaderText="IdPremisa" Visible="False"></asp:BoundField>
                        <asp:BoundField DataField="Codigo" HeaderText="Código" ItemStyle-HorizontalAlign="Center" ></asp:BoundField>
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" ItemStyle-HorizontalAlign="Center" ></asp:BoundField>
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" ItemStyle-HorizontalAlign="Center" ></asp:BoundField>
                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/edit.png" Text="Modificar" HeaderText="Modificar"
                            CommandName="Modificar" ItemStyle-HorizontalAlign="Center"></asp:ButtonField>
                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/delete.png" Text="Eliminar" HeaderText="Eliminar"
                            CommandName="Eliminar" ItemStyle-HorizontalAlign="Center"></asp:ButtonField>
                    </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White"></FooterStyle>
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="gridViewHeader"></HeaderStyle>
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left"></RowStyle>
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                        <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                        <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                        <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                </asp:GridView>
            </td>
        </tr>
    </table>
            </div>
        <br />
        <div id="divInsertar" runat="server" class="ColumnStyle">
        <table align="center">
            <tr>
                <td >
                    <asp:ImageButton ID="imgBtnInsertar" runat="server" CausesValidation="False" CommandName="Insert"
                        ImageUrl="~/Imagenes/Icons/Add.png" Text="Insert"
                        OnClick="imgBtnInsertar_Click" ToolTip="Insertar" />
                </td>
            </tr>
        </table>
        </div>
        <br />
    <div id="divCampos" runat="server"  class="ColumnStyle">
        <table align="center" >
            <tr>
                <td align="center" bgcolor="#BBBBBB">
                    <asp:Label ID="lblCodigo" runat="server" BackColor="Silver" CssClass="Apariencia" Text="Código:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="Codigo" runat="server" Width="300px" CssClass="Apariencia"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" bgcolor="#BBBBBB">
                    <asp:Label ID="lblNombre" runat="server" BackColor="Silver" CssClass="Apariencia" Text="Nombre:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="Nombre" runat="server" Width="300px" CssClass="Apariencia"></asp:TextBox>
                </td>
            </tr>
            <tr>
                     <td align="Center" bgcolor="#BBBBBB">
                         <asp:Label ID="lblDescripcion" runat="server" Text="Descripción:" CssClass="Apariencia"  align="center"></asp:Label>
                     </td>
                     <td>
                         <asp:TextBox ID="Descripcion" runat="server" Width="300px" CssClass="Apariencia" Height="80px"
                             Columns="100" Rows="8" TextMode="MultiLine" Font-Size="10pt" Font-Bold="False" MaxLength="500"></asp:TextBox>
                     </td>
                 </tr> 
            <tr>
                         <td colspan="2" align="Center">
                            <asp:ImageButton ID="btnImgInsertar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                OnClick="btnImgInsertar_Click" ToolTip="Guardar" ValidationGroup="iPerfil" />
                        
                            <asp:ImageButton ID="btnImgActualizar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                OnClick="btnImgActualizar_Click" ToolTip="Actualizar" ValidationGroup="iPerfil" />
                        
                            <asp:ImageButton ID="btnImgCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                OnClick="btnImgCancelar_Click" ToolTip="Cancelar" />
                         </td>
                     </tr>
            <caption>
            </caption>
         </table>
        
    </div>
        <br />
        <%--<div id="divGuardarCancelar" runat="server" visible="true" class="ColumnStyle">
        <table align="center">
        <tr align="center">
             <td>
                 <table class="tablaSinBordes">
                     
                 </table>
             </td>
        </tr>
            <caption>
                <br />
                </table>
            </div>--%>
        <br />







        
        <asp:ModalPopupExtender ID="mpeMsgBox" runat="server" TargetControlID="btndummy"
            PopupControlID="pnlMsgBox" OkControlID="btnAceptar" BackgroundCssClass="FondoAplicacion"
            Enabled="True" DropShadow="true">
        </asp:ModalPopupExtender>
        <asp:Button ID="btndummy" runat="server" Text="Button" Style="display: none" />
        <asp:Panel ID="pnlMsgBox" runat="server" Width="400px" Style="display: none;" BorderColor="#575757"
            BackColor="#FFFFFF" BorderStyle="Solid">
            <table width="100%">
                <tr class="topHandle" style="background-color: #5D7B9D">
                    <td colspan="2" align="center" runat="server" id="tdCaption">&nbsp;
                        <asp:Label ID="lblCaption" runat="server" Text="Atención" Font-Names="Calibri" Font-Size="Small"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px" valign="middle" align="center">
                        <asp:Image ID="imgInfo" runat="server" ImageUrl="~/Imagenes/Icons/icontexto-webdev-about.png" />
                    </td>
                    <td valign="middle" align="left">
                        <asp:Label ID="lblMsgBox" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnImgokEliminar" runat="server" Text="Eliminar" OnClick="btnImgokEliminar_Click" Visible="false" />
                        <asp:Button ID="btnAceptar" runat="server" Text="Ok" Font-Names="Calibri" Font-Size="Small" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>