<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Activos.ascx.cs" Inherits="ListasSarlaft.UserControls.SGSI.Activos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<style type="text/css">
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
<uc:OkMessageBox ID="omb" runat="server" />
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
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
        <table align="center" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label1" runat="server" ForeColor="White" Text="Activos"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large" Width="400px"></asp:Label>
                </td>
            </tr>
        </table>
        <tr id="TablaActivos" runat="server" bgcolor="#EEEEEE" visible="false">
            <td>
                <br />
                <table class="tablaSinBordes" align="center">
                    <tr align="center" bgcolor="#5D7B9D">
                        <td>
                            <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Small"
                                ForeColor="White" Text="Listado de activos"></asp:Label>
                        </td>
                    </tr>

                    <tr align="center">
                            <td>
                                <asp:Button ID="Button6" runat="server" Text="Exportar" ToolTip="Exportar a Excel" Font-Names="Calibri"
                                    Font-Size="Small" OnClick="Button6_Click" />
                            </td>
                        </tr>

                    <tr align="center">
                        <td>
                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="False" BorderStyle="Solid" CellPadding="4" Font-Names="Calibri"
                                Font-Size="Small" ForeColor="#333333" GridLines="Vertical" HorizontalAlign="Center"
                                Font-Bold="False" OnRowCommand="GridView1_RowCommand"
                                OnPageIndexChanging="GridView1_PageIndexChanging"
                                ShowHeaderWhenEmpty="True" DataKeyNames="IdActivos">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>

                                    <asp:BoundField DataField="NombreActivo" HeaderText="Nombre del Activo" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="TipoActivo" HeaderText="Tipo del Activo" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="ClasificacionActivo" HeaderText="Clasifcación del Activo" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="CadenaValor" HeaderText="Cadena Valor" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="MacroProceso" HeaderText="Macroproceso" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="Proceso" HeaderText="Proceso" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="SubProceso" HeaderText="Subproceso" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <%--<asp:BoundField DataField="Descripcion" HeaderText="Descripcion" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Wrap="true" ItemStyle-Width="150px"></asp:BoundField>--%>
                                    <asp:TemplateField HeaderText="Descripción" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 250px">
                                            <%--<asp:Label ID="lblDescripcion" Style="word-wrap: normal; word-break: break-all;" runat="server" ></asp:Label>--%>
                                            <asp:TextBox ID="txtDescripcion" TextMode="MultiLine"
           Rows="5"  runat="server" Text='<% # Bind("Descripcion")%>' Enabled="false"/>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                    <%--<asp:BoundField DataField="Ubicacion" HeaderText="Ubicacion" ItemStyle-HorizontalAlign="Center"></asp:BoundField>--%>
                                    <asp:TemplateField HeaderText="Ubicación" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 250px">
                                            <%--<asp:Label ID="lblDescripcion" Style="word-wrap: normal; word-break: break-all;" runat="server" ></asp:Label>--%>
                                            <asp:TextBox ID="txtUbicacion" TextMode="MultiLine"
           Rows="5"  runat="server" Text='<% # Bind("Ubicacion")%>' Enabled="false"/>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                    <asp:BoundField DataField="Criticidad" HeaderText="Criticidad" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="Propietario" HeaderText="Propietario" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/edit.png" Text="Modificar" HeaderText="Modificar"
                                        CommandName="Modificar" ItemStyle-HorizontalAlign="Center"></asp:ButtonField>
                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/delete.png" Text="Eliminar" HeaderText="Eliminar"
                                        CommandName="Eliminar" ItemStyle-HorizontalAlign="Center"></asp:ButtonField>

                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="gridViewHeader" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr align="right">
                        <td>
                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Imagenes/Icons/Add.png"
                                ToolTip="Insertar" OnClick="ImageButton3_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

        <%--FORMULARIO --%>

        <tr id="RegistrarActivos" runat="server" align="center" visible="false">
            <td bgcolor="#EEEEEE">
                <table class="tabla">
                    <tr align="center" bgcolor="#5D7B9D">
                        <td colspan="2">
                            <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Small"
                                ForeColor="White" Text="Registro de Activos"></asp:Label>
                        </td>
                        <tr align="left">
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label16" runat="server" CssClass="Apariencia" Text="Nombre del activo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNombreActivo" runat="server" Width="300px" CssClass="Apariencia"></asp:TextBox>
                            </td>
                        </tr>
                    <tr align="left">
                        <td align="left" bgcolor="#BBBBBB">
                            <asp:Label ID="Label17" runat="server" CssClass="Apariencia" Text="Tipo de Activo:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlObjetivo" runat="server" CssClass="Apariencia" Width="300px" DataTextField="Nombre" DataValueField="IdTipoActivo">
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr align="left">
                        <td align="left" bgcolor="#BBBBBB">
                            <asp:Label ID="lblClasificacionActivo" runat="server" CssClass="Apariencia" Text="Clasificación del Activo:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlClasificacionActivo" runat="server" CssClass="Apariencia" Width="300px" DataTextField="Nombre"
                                 DataValueField="IdClasificacionActivo">
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr align="left">
                        <td align="left" bgcolor="#BBBBBB">
                            <asp:Label ID="Label3" runat="server" CssClass="Apariencia" Text="Descripcion:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDescripcion" runat="server" Width="300px" CssClass="Apariencia"
                                Columns="100" Rows="8" TextMode="MultiLine" Font-Size="10pt" Font-Bold="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" bgcolor="#BBBBBB">
                            <asp:Label ID="lCadenaValor" runat="server" Text="Cadena de Valor:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCadenaValor" runat="server" Width="300px" CssClass="Apariencia" AutoPostBack="True" OnSelectedIndexChanged="DdlCadenaValor_SelectedIndexChanged"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlCadenaValor"
                                ForeColor="Red" ValidationGroup="Validacion" Display="Dynamic" InitialValue="0">*</asp:RequiredFieldValidator>
                        </td>

                    </tr>
                    <tr>
                        <td align="left" bgcolor="#BBBBBB">
                            <asp:Label ID="lMacroproceso" runat="server" Text="Macroproceso:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMacroproceso" runat="server" Width="300px" CssClass="Apariencia" AutoPostBack="True" OnSelectedIndexChanged="DdlMacroproceso_SelectedIndexChanged"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlMacroproceso"
                                ForeColor="Red" ValidationGroup="Validacion" Display="Dynamic" InitialValue="0">*</asp:RequiredFieldValidator>
                        </td>

                    </tr>
                    <tr>
                        <td align="left" bgcolor="#BBBBBB">
                            <asp:Label ID="lProceso" runat="server" Text="Proceso:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlProceso" runat="server" Width="300px" CssClass="Apariencia" AutoPostBack="True" OnSelectedIndexChanged="DdlProceso_SelectedIndexChanged"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlProceso"
                                ForeColor="Red" ValidationGroup="Validacion" Display="Dynamic" InitialValue="0">*</asp:RequiredFieldValidator>
                        </td>

                    </tr>
                    <tr>
                        <td align="left" bgcolor="#BBBBBB">
                            <asp:Label ID="lSubproceso" runat="server" Text="Subproceso:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSubproceso" runat="server" Width="300px" CssClass="Apariencia"></asp:DropDownList>
                        </td>

                    </tr>
                    <tr align="left">
                        <td align="left" bgcolor="#BBBBBB">
                            <asp:Label ID="Label4" runat="server" CssClass="Apariencia" Text="Ubicación:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server" Width="300px" CssClass="Apariencia"
                                Columns="100" Rows="8" TextMode="MultiLine" Font-Size="10pt" Font-Bold="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="left" bgcolor="#BBBBBB">
                            <asp:Label ID="Label5" runat="server" CssClass="Apariencia" Text="Confidencialidad:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlConfidencialidad" runat="server" CssClass="Apariencia" Width="300px"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlDisponibilidad_SelectedIndexChanged">
                                <asp:ListItem Value="0">--Seleccione una opción--</asp:ListItem>
                                <asp:ListItem Value="1">Alto</asp:ListItem>
                                <asp:ListItem Value="2">Medio</asp:ListItem>
                                <asp:ListItem Value="3">Bajo</asp:ListItem>
                                <asp:ListItem Value="4">No Aplica</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlConfidencialidad"
                                ForeColor="Red" ValidationGroup="Validacion" Display="Dynamic" InitialValue="0">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="left" bgcolor="#BBBBBB">
                            <asp:Label ID="Label6" runat="server" CssClass="Apariencia" Text="Justificación de confidencialidad:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtJustConfidencialidad" Width="300px" runat="server" CssClass="Apariencia"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtJustConfidencialidad"
                                ForeColor="Red" ValidationGroup="Validacion" Display="Dynamic" InitialValue="0">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="left" bgcolor="#BBBBBB">
                            <asp:Label ID="Label7" runat="server" CssClass="Apariencia" Text="Integridad:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlIntegridad" runat="server" CssClass="Apariencia" Width="300px"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlDisponibilidad_SelectedIndexChanged">
                                <asp:ListItem Value="0">--Seleccione una opción--</asp:ListItem>
                                <asp:ListItem Value="1">Alto</asp:ListItem>
                                <asp:ListItem Value="2">Medio</asp:ListItem>
                                <asp:ListItem Value="3">Bajo</asp:ListItem>
                                <asp:ListItem Value="4">No Aplica</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlIntegridad"
                                ForeColor="Red" ValidationGroup="Validacion" Display="Dynamic" InitialValue="0">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="left" bgcolor="#BBBBBB">
                            <asp:Label ID="Label9" runat="server" CssClass="Apariencia" Text="Justificación de Integridad:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtJustIntegridad" Width="300px" runat="server" CssClass="Apariencia"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtJustIntegridad"
                                ForeColor="Red" ValidationGroup="Validacion" Display="Dynamic" InitialValue="0">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="left" bgcolor="#BBBBBB">
                            <asp:Label ID="Label10" runat="server" CssClass="Apariencia" Text="Disponibilidad:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDisponibilidad" runat="server" CssClass="Apariencia" Width="300px"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlDisponibilidad_SelectedIndexChanged">
                                <asp:ListItem Value="0">--Seleccione una opción--</asp:ListItem>
                                <asp:ListItem Value="1">Alto</asp:ListItem>
                                <asp:ListItem Value="2">Medio</asp:ListItem>
                                <asp:ListItem Value="3">Bajo</asp:ListItem>
                                <asp:ListItem Value="4">No Aplica</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlDisponibilidad"
                                ForeColor="Red" ValidationGroup="Validacion" Display="Dynamic" InitialValue="0">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="left" bgcolor="#BBBBBB">
                            <asp:Label ID="Label11" runat="server" CssClass="Apariencia" Text="Justificación de Disponibilidad:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtJusDispobilidad" Width="300px" runat="server" CssClass="Apariencia"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtJusDispobilidad"
                                ForeColor="Red" ValidationGroup="Validacion" Display="Dynamic" InitialValue="0">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="left" bgcolor="#BBBBBB">
                            <asp:Label ID="Label12" runat="server" CssClass="Apariencia" Text="Criticidad:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox2" Width="300px" runat="server" CssClass="Apariencia" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="left" bgcolor="#BBBBBB">
                            <asp:Label ID="Label13" runat="server" CssClass="Apariencia" Text="Propietario:"></asp:Label>


                        </td>
                        <td>
                            <asp:TextBox ID="txtPropietarioActivo" runat="server" Width="300px" CssClass="Apariencia" ReadOnly="true" Rows="3" TextMode="MultiLine"></asp:TextBox>
                            <asp:Label ID="lblIdDependencia1" runat="server" Visible="False" Font-Names="Calibri"
                                Font-Size="Small"></asp:Label>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPropietarioActivo"
                                ForeColor="Red" ValidationGroup="Validacion" Display="Dynamic">*</asp:RequiredFieldValidator>
                            <asp:ImageButton ID="btnBorrarResponsables" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                ToolTip="Borrar Responsables Ejecución" OnClick="BtnBorrarResponsables_Click" />
                            <asp:ImageButton ID="imgDependencia1" runat="server" ImageUrl="~/Imagenes/Icons/Organization-Chart.png"
                                OnClientClick="return false;" />
                            <asp:PopupControlExtender ID="popupDependencia1" runat="server"
                                Enabled="True" TargetControlID="imgDependencia1" BehaviorID="popup1"
                                PopupControlID="pnlDependencia1" OffsetY="-200" OffsetX="30">
                            </asp:PopupControlExtender>
                            <asp:Panel ID="pnlDependencia1" runat="server" CssClass="popup" Width="32%" Style="display: none">
                                <table width="100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                                    <tr align="right" bgcolor="#5D7B9D">
                                        <td>
                                            <asp:ImageButton ID="btnClosepp1" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                                                OnClientClick="$find('popup1').hidePopup(); return false;" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TreeView ID="TreeView1" ExpandDepth="3" runat="server" Font-Names="Calibri"
                                                Font-Size="Small" LineImagesFolder="~/TreeLineImages" ForeColor="Black" ShowLines="True" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged"
                                                AutoGenerateDataBindings="False">
                                                <SelectedNodeStyle BackColor="Silver" BorderColor="#66CCFF" BorderStyle="Inset" />
                                            </asp:TreeView>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td>
                                            <asp:Button ID="BtnOk1" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popup1').hidePopup(); return false;" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>

                    </tr>
                    <tr align="left">
                        <td align="left" bgcolor="#BBBBBB">
                            <asp:Label ID="Label14" runat="server" CssClass="Apariencia" Text="Premisas:"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBoxList ID="cblPremisas" runat="server" Font-Names="Calibri" Font-Size="Small">
                            </asp:CheckBoxList>

                        </td>
                    </tr>

                    <tr align="left">
                        <td align="left" bgcolor="#BBBBBB">
                            <asp:Label ID="Label15" runat="server" CssClass="Apariencia" Text="Riesgo a asociar:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlRiesgoAsociado" runat="server" CssClass="Apariencia" Width="300px" DataTextField="Nombre"
                                 DataValueField="RiesgoAsociado">
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr align="center">
                        <td colspan="2">
                            <table class="tablaSinBordes">
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="btnImgInsertar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                            OnClick="btnImgInsertar_Click" ToolTip="Guardar" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnImgActualizar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                            Style="text-align: right" OnClick="btnImgActualizar_Click" Visible="false"
                                            ToolTip="Guardar" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnImgCancelarRegAvances" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                            OnClick="btnImgCancelarRegAvances_Click" ToolTip="Cancelar" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </ContentTemplate>
</asp:UpdatePanel>
