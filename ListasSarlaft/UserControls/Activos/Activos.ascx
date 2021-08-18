<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Activos.ascx.cs"
    Inherits="ListasSarlaft.UserControls.Activos.Activos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="center" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label1" runat="server" ForeColor="White" Text="Reporte de Activos"
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
                                ForeColor="White" Text="Tabla de activos"></asp:Label>
                        </td>
                    </tr>
                    <tr align="center">
                        <td>
                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="False" BorderStyle="Solid" CellPadding="4" Font-Names="Calibri"
                                Font-Size="Small" ForeColor="#333333" GridLines="Vertical" HorizontalAlign="Center"
                                Font-Bold="False" OnRowCommand="GridView1_RowCommand"
                                ShowHeaderWhenEmpty="True" DataKeyNames="IdPlanAccion,IdAuditoria,NombreAuditoria">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="IdActivo" HeaderText="Id Activo" SortExpression="IdAuditoria"
                                        ReadOnly="True"></asp:BoundField>
                                    <asp:BoundField DataField="NombreActivo" HeaderText="Nombre del Activo" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="DescripcionActivo" HeaderText="Descripcion" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="UbicacionActivo" HeaderText="Ubicacion" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="CriticidadActivo" HeaderText="Criticidad" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="PropietarioActivo" HeaderText="PropietarioActivo" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Editar" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandArgument="Editar"
                                                CommandName="Select" ImageUrl="~/Imagenes/Iconos/icono_modificar.gif" ToolTip="Recomendación" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
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
                                <asp:TextBox ID="txtNombreActivo" runat="server" CssClass="Apariencia"></asp:TextBox>
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
                            <asp:Label ID="Label3" runat="server" CssClass="Apariencia" Text="Descripcion:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDescripcion" runat="server" Width="800px" CssClass="Apariencia"
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
                        <td></td>
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
                        <td></td>
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
                        <td></td>
                    </tr>
                    <tr>
                        <td align="left" bgcolor="#BBBBBB">
                            <asp:Label ID="lSubproceso" runat="server" Text="Subproceso:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSubproceso" runat="server" Width="300px" CssClass="Apariencia"></asp:DropDownList>
                        </td>
                        <td></td>
                    </tr>
                    <tr align="left">
                        <td align="left" bgcolor="#BBBBBB">
                            <asp:Label ID="Label4" runat="server" CssClass="Apariencia" Text="Ubicación:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server" Width="800px" CssClass="Apariencia"
                                Columns="100" Rows="8" TextMode="MultiLine" Font-Size="10pt" Font-Bold="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="left" bgcolor="#BBBBBB">
                            <asp:Label ID="Label5" runat="server" CssClass="Apariencia" Text="Confidencialidad:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlConfidencialidad" runat="server" CssClass="Apariencia" Width="300px">
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
                            <asp:TextBox ID="txtJustConfidencialidad" runat="server" CssClass="Apariencia"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtJustConfidencialidad"
                                ForeColor="Red" ValidationGroup="Validacion" Display="Dynamic" InitialValue="0">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="left" bgcolor="#BBBBBB">
                            <asp:Label ID="Label7" runat="server" CssClass="Apariencia" Text="Integridad:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlIntegridad" runat="server" CssClass="Apariencia" Width="300px">
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
                            <asp:TextBox ID="txtJustIntegridad" runat="server" CssClass="Apariencia"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtJustIntegridad"
                                ForeColor="Red" ValidationGroup="Validacion" Display="Dynamic" InitialValue="0">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="left" bgcolor="#BBBBBB">
                            <asp:Label ID="Label10" runat="server" CssClass="Apariencia" Text="Confidencialidad:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDisponibilidad" runat="server" CssClass="Apariencia" Width="300px">
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
                            <asp:TextBox ID="txtJusDispobilidad" runat="server" CssClass="Apariencia"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtJusDispobilidad"
                                ForeColor="Red" ValidationGroup="Validacion" Display="Dynamic" InitialValue="0">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="left" bgcolor="#BBBBBB">
                            <asp:Label ID="Label12" runat="server" CssClass="Apariencia" Text="Criticidad:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox2" runat="server" CssClass="Apariencia" Enabled="true"></asp:TextBox>
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
                            <asp:PopupControlExtender ID="popupDependencia1" runat="server"
                                Enabled="True" TargetControlID="imgDependencia1" BehaviorID="popup1"
                                PopupControlID="pnlDependencia1" OffsetY="-200" OffsetX="30">
                            </asp:PopupControlExtender>
                            <asp:Panel ID="pnlDependencia1" runat="server" CssClass="popup" Width="50%" Style="display: none">
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
                        <td style="text-align: left">
                            <asp:ImageButton ID="btnBorrarResponsables" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                ToolTip="Borrar Responsables Ejecución" OnClick="BtnBorrarResponsables_Click" />
                            <asp:ImageButton ID="imgDependencia1" runat="server" ImageUrl="~/Imagenes/Icons/Organization-Chart.png"
                                OnClientClick="return false;" />
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="left" bgcolor="#BBBBBB">
                            <asp:Label ID="Label14" runat="server" CssClass="Apariencia" Text="Premisas:"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckAbuso" runat="server" Text="Abuso de Privilegios" /><br />
                            <asp:CheckBox ID="CheckApt" runat="server" Text="Apt's" /><br />
                            <asp:CheckBox ID="CheckCloud" runat="server" Text="Cloud Services" /><br />
                            <asp:CheckBox ID="CheckControl" runat="server" Text="Control de Accesos" /><br />
                            <asp:CheckBox ID="CheckCripto" runat="server" Text="Criptografía" /><br />
                            <asp:CheckBox ID="CheckDisp" runat="server" Text="Disponibilidad" /><br />
                            <asp:CheckBox ID="CheckMalware" runat="server" Text="Malware" /><br />
                            <asp:CheckBox ID="CheckVulnera" runat="server" Text="Vulnerabilidades" /><br />
                            <asp:CheckBox ID="CheckProveed" runat="server" Text="Proveedores" /><br />
                            <asp:CheckBox ID="CheckRedyTel" runat="server" Text="Redes y Telecomunicaciones" /><br />
                            <asp:CheckBox ID="CheckGathering" runat="server" Text="Información Gathering" /><br />
                            <asp:CheckBox ID="CheckSocial" runat="server" Text="Ingeniería Social" /><br />
                            <asp:CheckBox ID="CheckRemov" runat="server" Text="Medios Removibles" /><br />
                            <asp:CheckBox ID="CheckFuga" runat="server" Text="Fuga de Información" />
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
