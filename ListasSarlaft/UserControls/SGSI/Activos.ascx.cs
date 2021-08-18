using ClosedXML.Excel;
using clsDTO;
using clsLogica;
using ListasSarlaft.Classes;
using ListasSarlaft.Classes.BLL.SGSI;
using ListasSarlaft.Classes.DTO.SGSI;
using Microsoft.Security.Application;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Excel = Microsoft.Office.Interop.Excel;
using ClosedXML;


namespace ListasSarlaft.UserControls.SGSI
{
    public partial class Activos : System.Web.UI.UserControl
    {
        string IdFormulario = "11009";
        cCuenta cCuenta = new cCuenta();
        cActivos cActivos = new cActivos();
        cParametrizacion Parametrizacion = new cParametrizacion();

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.Button6);
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
            {
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            }
            if (!Page.IsPostBack)
            {
                //inicializarValores();
                //infoGridActivos();
                //loadGridActivo();
                TablaActivos.Visible = true;
                inicializarValores();
                CargarDDLs();
                mtdLoadInfoGridPremisas();
                PopulateTreeView();
                string strErrMsg = string.Empty;
                if (mtdLoadActivos(ref strErrMsg) == false)
                    mtdMensaje(strErrMsg);

            }
            //if (string.IsNullOrEmpty(Session["IdUsuario"].ToString().Trim()))
            //{
            //    Response.Redirect("~/Formularios/Sitio/Login.aspx", false);
            //}
            //else
            //{

            //}
        }

        private void inicializarValores()
        {
            PagIndexInfoGridActivos = 0;
            RowGridActivo = 0;
        }

        private int pagIndexInfoGridActivos;
        private int PagIndexInfoGridActivos
        {
            get
            {
                pagIndexInfoGridActivos = (int)ViewState["pagIndexInfoGridActivos"];
                return pagIndexInfoGridActivos;
            }
            set
            {
                pagIndexInfoGridActivos = value;
                ViewState["pagIndexInfoGridActivos"] = pagIndexInfoGridActivos;
            }
        }

        private DataTable infoGridActivo;
        private DataTable InfoGridActivo
        {
            get
            {
                infoGridActivo = (DataTable)ViewState["infoGridActivo"];
                return infoGridActivo;
            }
            set
            {
                infoGridActivo = value;
                ViewState["infoGridActivo"] = infoGridActivo;
            }
        }

        private int rowGridActivo;
        private int RowGridActivo
        {
            get
            {
                rowGridActivo = (int)ViewState["rowGridActivo"];
                return rowGridActivo;
            }
            set
            {
                rowGridActivo = value;
                ViewState["rowGridActivo"] = rowGridActivo;
            }
        }
        private bool mtdLoadActivos(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            List<ActivosDTO> lstActivos = new List<ActivosDTO>();
            clsActivosBLL cData = new clsActivosBLL();
            #endregion Vars
            cData.mtdConsultarActivos(ref lstActivos, ref strErrMsg);

            if (lstActivos != null)
            {
                mtdLoadActivos();
                mtdLoadActivos(lstActivos);
                GridView1.DataSource = lstActivos;
                GridView1.PageIndex = pagIndexInfoGridActivos;
                GridView1.DataBind();
                booResult = true;
            }
            else
            {
                strErrMsg = "No hay variables registradas";
            }

            return booResult;
        }
        private void mtdLoadActivos()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdActivos", typeof(string));
            grid.Columns.Add("NombreActivo", typeof(string));
            grid.Columns.Add("IdTipoActivo", typeof(string));
            grid.Columns.Add("TipoActivo", typeof(string));
            grid.Columns.Add("IdClasificacionActivo", typeof(string));
            grid.Columns.Add("ClasificacionActivo", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));
            grid.Columns.Add("IdCadenaValor", typeof(string));
            grid.Columns.Add("CadenaValor", typeof(string));
            grid.Columns.Add("IdMacroProceso", typeof(string));
            grid.Columns.Add("MacroProceso", typeof(string));
            grid.Columns.Add("IdProceso", typeof(string));
            grid.Columns.Add("Proceso", typeof(string));
            grid.Columns.Add("IdSubProceso", typeof(string));
            grid.Columns.Add("SubProceso", typeof(string));
            grid.Columns.Add("Ubicacion", typeof(string));
            grid.Columns.Add("IdConfidencialidad", typeof(string));
            grid.Columns.Add("JustificacionConfidencialidad", typeof(string));
            grid.Columns.Add("IdIntegridad", typeof(string));
            grid.Columns.Add("JustificacionIntegridad", typeof(string));
            grid.Columns.Add("IdDisponibilidad", typeof(string));
            grid.Columns.Add("JustificacionDisponibilidad", typeof(string));
            grid.Columns.Add("Criticidad", typeof(string));
            grid.Columns.Add("IdPropietario", typeof(string));
            grid.Columns.Add("Propietario", typeof(string));
            grid.Columns.Add("Idusuario", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));

            InfoGridActivo = grid;
            GridView1.DataSource = InfoGridActivo;
            GridView1.DataBind();
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstActivos">Lista con los Indicadores</param>
        private void mtdLoadActivos(List<ActivosDTO> lstActivos)
        {
            string strErrMsg = String.Empty;
            //clsControlInfraestructuraBLL cCrlInfra = new clsControlInfraestructuraBLL();

            foreach (ActivosDTO objActivos in lstActivos)
            {

                InfoGridActivo.Rows.Add(new Object[] {
                    objActivos.IdActivos,
objActivos.NombreActivo,
objActivos.IdTipoActivo,
objActivos.TipoActivo,
objActivos.IdClasificacionActivo,
objActivos.ClasificacionActivo,
objActivos.Descripcion,
objActivos.IdCadenaValor,
objActivos.CadenaValor,
objActivos.IdMacroProceso,
objActivos.MacroProceso,
objActivos.IdProceso,
objActivos.Proceso,
objActivos.IdSubProceso,
objActivos.SubProceso,
objActivos.Ubicacion,
objActivos.IdConfidencialidad,
objActivos.JustificacionConfidencialidad,
objActivos.IdIntegridad,
objActivos.JustificacionIntegridad,
objActivos.IdDisponibilidad,
objActivos.JustificacionDisponibilidad,
objActivos.Criticidad,
objActivos.IdPropietario,
objActivos.Propietario,
objActivos.Idusuario,
objActivos.Usuario
                    });
            }
        }

        private void loadGridActivo()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cActivos.TipoActivosVer();
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridActivo.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdActivo"].ToString().Trim(),
                                                               dtInfo.Rows[rows]["NombreActivo"].ToString().Trim()
                                                               });
                }
                GridView1.PageIndex = PagIndexInfoGridActivos;
                GridView1.DataSource = InfoGridActivo;
                GridView1.DataBind();
            }
        }


        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            RegistrarActivos.Visible = true;
            TablaActivos.Visible = false;
            btnImgInsertar.Visible = true;
            btnImgActualizar.Visible = false;
        }

        protected void btnImgInsertar_Click(object sender, ImageClickEventArgs e)
        {
            ActivosDTO objActivos = new ActivosDTO();
            clsActivosBLL clsData = new clsActivosBLL();
            List<int> lstPremisas = new List<int>();
            try
            {
                objActivos.NombreActivo = Sanitizer.GetSafeHtmlFragment(Server.HtmlEncode(txtNombreActivo.Text));
                objActivos.IdTipoActivo = Convert.ToInt32(ddlObjetivo.SelectedValue);
                objActivos.IdClasificacionActivo = Convert.ToInt32(ddlClasificacionActivo.SelectedValue);
                objActivos.Descripcion = Sanitizer.GetSafeHtmlFragment(Server.HtmlEncode(txtDescripcion.Text));
                objActivos.IdCadenaValor = Convert.ToInt32(ddlCadenaValor.SelectedValue);
                objActivos.IdMacroProceso = Convert.ToInt32(ddlMacroproceso.SelectedValue);
                objActivos.IdProceso = Convert.ToInt32(ddlProceso.SelectedValue);
                if (Convert.ToInt32(ddlSubproceso.SelectedValue) != 0)
                    objActivos.IdSubProceso = Convert.ToInt32(ddlSubproceso.SelectedValue);
                objActivos.Ubicacion = Sanitizer.GetSafeHtmlFragment(Server.HtmlEncode(TextBox1.Text));
                objActivos.IdConfidencialidad = Convert.ToInt32(ddlConfidencialidad.SelectedValue);
                objActivos.JustificacionConfidencialidad = Sanitizer.GetSafeHtmlFragment(Server.HtmlEncode(txtJustConfidencialidad.Text));
                objActivos.IdIntegridad = Convert.ToInt32(ddlIntegridad.SelectedValue);
                objActivos.JustificacionIntegridad = Sanitizer.GetSafeHtmlFragment(Server.HtmlEncode(txtJustIntegridad.Text));
                objActivos.IdDisponibilidad = Convert.ToInt32(ddlDisponibilidad.SelectedValue);
                objActivos.JustificacionDisponibilidad = Sanitizer.GetSafeHtmlFragment(Server.HtmlEncode(txtJusDispobilidad.Text));
                objActivos.Criticidad = Sanitizer.GetSafeHtmlFragment(Server.HtmlEncode(TextBox2.Text));
                objActivos.IdPropietario = Convert.ToInt32(lblIdDependencia1.Text);
                objActivos.IdRiesgo = Convert.ToInt32(ddlRiesgoAsociado.SelectedValue);
                objActivos.Idusuario = Convert.ToInt32(Session["IdUsuario"].ToString());
                for (int i = 0; i < cblPremisas.Items.Count; i++)
                {
                    if (cblPremisas.Items[i].Selected)
                    {
                        lstPremisas.Add(Convert.ToInt32(cblPremisas.Items[i].Value.ToString().Trim()));
                    }
                }
            }
            catch (Exception ex)
            {
                mtdMensaje("Error en la información: " + ex.Message);
            }
            try
            {
                string strErrMsg = string.Empty;
                clsData.mtdInsertarActivos(objActivos, ref strErrMsg, lstPremisas);
                if (strErrMsg == string.Empty)
                {
                    mtdMensaje("Registro de Activo realizado satisfactoriamente");
                    mdtCleanFields();
                    
                    if (mtdLoadActivos(ref strErrMsg) == false)
                        mtdMensaje(strErrMsg);
                }

            }
            catch (Exception ex)
            {
                mtdMensaje("Error registrado la información: " + ex.Message);
            }
            RegistrarActivos.Visible = false;
            TablaActivos.Visible = true;
        }

        protected void btnImgActualizar_Click(object sender, ImageClickEventArgs e)
        {
            ActivosDTO objActivos = new ActivosDTO();
            clsActivosBLL clsData = new clsActivosBLL();
            List<int> lstPremisas = new List<int>();
            try
            {
                objActivos.IdActivos = Convert.ToInt32( InfoGridActivo.Rows[RowGridActivo]["IdActivos"].ToString().Trim());
                objActivos.NombreActivo = Sanitizer.GetSafeHtmlFragment(Server.HtmlEncode(txtNombreActivo.Text));
                objActivos.IdTipoActivo = Convert.ToInt32(ddlObjetivo.SelectedValue);
                objActivos.IdClasificacionActivo = Convert.ToInt32(ddlClasificacionActivo.SelectedValue);
                objActivos.Descripcion = Sanitizer.GetSafeHtmlFragment(Server.HtmlEncode(txtDescripcion.Text));
                objActivos.IdCadenaValor = Convert.ToInt32(ddlCadenaValor.SelectedValue);
                
                objActivos.IdMacroProceso = Convert.ToInt32(ddlMacroproceso.SelectedValue);
                
                objActivos.IdProceso = Convert.ToInt32(ddlProceso.SelectedValue);
                
                if (Convert.ToInt32(ddlSubproceso.SelectedValue) != 0)
                    objActivos.IdSubProceso = Convert.ToInt32(ddlSubproceso.SelectedValue);
                objActivos.Ubicacion = Sanitizer.GetSafeHtmlFragment(Server.HtmlEncode(TextBox1.Text));
                objActivos.IdConfidencialidad = Convert.ToInt32(ddlConfidencialidad.SelectedValue);
                objActivos.JustificacionConfidencialidad = Sanitizer.GetSafeHtmlFragment(Server.HtmlEncode(txtJustConfidencialidad.Text));
                objActivos.IdIntegridad = Convert.ToInt32(ddlIntegridad.SelectedValue);
                objActivos.JustificacionIntegridad = Sanitizer.GetSafeHtmlFragment(Server.HtmlEncode(txtJustIntegridad.Text));
                objActivos.IdDisponibilidad = Convert.ToInt32(ddlDisponibilidad.SelectedValue);
                objActivos.JustificacionDisponibilidad = Sanitizer.GetSafeHtmlFragment(Server.HtmlEncode(txtJusDispobilidad.Text));
                objActivos.Criticidad = Sanitizer.GetSafeHtmlFragment(Server.HtmlEncode(TextBox2.Text));
                objActivos.IdPropietario = Convert.ToInt32(lblIdDependencia1.Text);
                objActivos.IdRiesgo = Convert.ToInt32(ddlRiesgoAsociado.SelectedValue);
                objActivos.Idusuario = Convert.ToInt32(Session["IdUsuario"].ToString());
                for (int i = 0; i < cblPremisas.Items.Count; i++)
                {
                    if (cblPremisas.Items[i].Selected)
                    {
                        lstPremisas.Add(Convert.ToInt32(cblPremisas.Items[i].Value.ToString().Trim()));
                    }
                }
            }
            catch (Exception ex)
            {
                mtdMensaje("Error en la información: " + ex.Message);
                return;
            }
            try
            {
                string strErrMsg = string.Empty;
                clsData.mtdActualizarActivos(objActivos, ref strErrMsg, lstPremisas);
                if (strErrMsg == string.Empty)
                {
                    mtdMensaje("Actualización de Activo realizado satisfactoriamente");
                    mdtCleanFields();
                    
                    if (mtdLoadActivos(ref strErrMsg) == false)
                        mtdMensaje(strErrMsg);
                }

            }
            catch (Exception ex)
            {
                mtdMensaje("Error actualizando la información: " + ex.Message);
            }
            RegistrarActivos.Visible = false;
            TablaActivos.Visible = true;
        }

        protected void btnImgCancelarRegAvances_Click(object sender, ImageClickEventArgs e)
        {
            mdtCleanFields();
            RegistrarActivos.Visible = false;
            TablaActivos.Visible = true;
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = (Convert.ToInt16(GridView1.PageSize) * PagIndexInfoGridActivos) + Convert.ToInt16(e.CommandArgument);
            RowGridActivo = rowIndex;
            switch (e.CommandName)
            {
                case "Modificar":
                    RegistrarActivos.Visible = true;
                    TablaActivos.Visible = false;
                    btnImgInsertar.Visible = false;
                    btnImgActualizar.Visible = true;
                    mtdShowUpdate(rowIndex);
                    break;
                case "Eliminar":
                    btnImgokEliminar.Visible = true;
                    mtdMensaje("¿Seguro que desea eliminar la información del activo?");
                    break;
            }
            
        }

        private void mtdShowUpdate(int rowIndex)
        {
            clsActivosBLL cData = new clsActivosBLL();
            int idActivo = 0;
            try
            {
                idActivo = (int)this.GridView1.DataKeys[rowIndex]["IdActivos"];
                txtNombreActivo.Text = InfoGridActivo.Rows[rowIndex]["NombreActivo"].ToString().Trim();
                ddlObjetivo.SelectedValue = InfoGridActivo.Rows[rowIndex]["IdTipoActivo"].ToString();
                ddlClasificacionActivo.SelectedValue = InfoGridActivo.Rows[rowIndex]["IdClasificacionActivo"].ToString();
                txtDescripcion.Text = InfoGridActivo.Rows[rowIndex]["Descripcion"].ToString().Trim();
                ddlCadenaValor.SelectedValue = InfoGridActivo.Rows[rowIndex]["IdCadenaValor"].ToString().Trim();
                DdlCadenaValor_SelectedIndexChanged(null, null);
                ddlMacroproceso.SelectedValue = InfoGridActivo.Rows[rowIndex]["IdMacroproceso"].ToString().Trim();
                DdlMacroproceso_SelectedIndexChanged(null, null);
                ddlProceso.SelectedValue = InfoGridActivo.Rows[rowIndex]["IdProceso"].ToString().Trim();
                DdlProceso_SelectedIndexChanged(null, null);
                ddlSubproceso.SelectedValue = InfoGridActivo.Rows[rowIndex]["IdSubproceso"].ToString().Trim();
                TextBox1.Text = InfoGridActivo.Rows[rowIndex]["Ubicacion"].ToString().Trim();
                ddlConfidencialidad.SelectedValue = InfoGridActivo.Rows[rowIndex]["IdConfidencialidad"].ToString().Trim();
                txtJustConfidencialidad.Text = InfoGridActivo.Rows[rowIndex]["JustificacionConfidencialidad"].ToString().Trim();
                ddlIntegridad.SelectedValue = InfoGridActivo.Rows[rowIndex]["IdIntegridad"].ToString().Trim();
                txtJustIntegridad.Text = InfoGridActivo.Rows[rowIndex]["JustificacionIntegridad"].ToString().Trim();
                ddlDisponibilidad.SelectedValue = InfoGridActivo.Rows[rowIndex]["IdDisponibilidad"].ToString().Trim();
                txtJusDispobilidad.Text = InfoGridActivo.Rows[rowIndex]["JustificacionDisponibilidad"].ToString().Trim();
                TextBox2.Text = InfoGridActivo.Rows[rowIndex]["Criticidad"].ToString().Trim();
                txtPropietarioActivo.Text = InfoGridActivo.Rows[rowIndex]["Propietario"].ToString().Trim();
                lblIdDependencia1.Text = InfoGridActivo.Rows[rowIndex]["IdPropietario"].ToString().Trim();
            }
            catch(Exception ex)
            {
                mtdMensaje("Error en el cargue de la información: "+
                    ex.Message);
                return;
            }
            
            List<int> lstPremisas = new List<int>();
            string strErrMsg = string.Empty;
            lstPremisas = cData.mtdConsultaActivoVsPremisas(ref strErrMsg, idActivo);
            if (strErrMsg == string.Empty)
            {
                for(int i = 0; i < lstPremisas.Count;i++)
                {
                    for (int j = 0; j < cblPremisas.Items.Count; j++)
                    {
                        if(cblPremisas.Items[j].Value == lstPremisas[i].ToString())
                            cblPremisas.Items[j].Selected = true;

                    }
                        
                }
                
            }
            else
                mtdMensaje(strErrMsg);
        }

        #region Treeview
        private void PopulateTreeView()
        {
            DataTable treeViewData = GetTreeViewData();
            AddTopTreeViewNodes(treeViewData);
            TreeView1.ExpandAll();
        }

        private DataTable GetTreeViewData()
        {
            string selectCommand = "SELECT Parametrizacion.JerarquiaOrganizacional.IdHijo, Parametrizacion.JerarquiaOrganizacional.IdPadre, Parametrizacion.JerarquiaOrganizacional.NombreHijo, Parametrizacion.DetalleJerarquiaOrg.NombreResponsable, Parametrizacion.DetalleJerarquiaOrg.CorreoResponsable FROM Parametrizacion.JerarquiaOrganizacional LEFT JOIN Parametrizacion.DetalleJerarquiaOrg ON Parametrizacion.JerarquiaOrganizacional.idHijo = Parametrizacion.DetalleJerarquiaOrg.idHijo";
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
            DataTable dtblDiscuss = new DataTable();
            dad.Fill(dtblDiscuss);
            return dtblDiscuss;
        }

        private void AddTopTreeViewNodes(DataTable treeViewData)
        {
            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = -1";
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString().Trim(), row["IdHijo"].ToString());
                TreeView1.Nodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }

        }

        private void AddChildTreeViewNodes(DataTable treeViewData, TreeNode parentTreeViewNode)
        {
            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = " + parentTreeViewNode.Value;
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString().Trim(), row["IdHijo"].ToString());
                newNode.ToolTip = "Nombre: " + row["NombreResponsable"].ToString() + "\rCorreo: " + row["CorreoResponsable"].ToString().Trim();
                parentTreeViewNode.ChildNodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (txtPropietarioActivo.Text.Length > 0)
            {
                txtPropietarioActivo.Text += $", {TreeView1.SelectedNode.Text}";
                lblIdDependencia1.Text += $",{TreeView1.SelectedNode.Value}";
            }
            else
            {
                txtPropietarioActivo.Text += $"{TreeView1.SelectedNode.Text}";
                lblIdDependencia1.Text += $"{TreeView1.SelectedNode.Value}";
            }
        }
        #endregion Treeview

        protected void BtnBorrarResponsables_Click(object sender, ImageClickEventArgs e)
        {
            txtPropietarioActivo.Text = string.Empty;
            lblIdDependencia1.Text = string.Empty;
        }

        private void CargarDDLs()
        {
            try
            {
                string strErrMsg = string.Empty;
                LoadDDLCadenaValor(ref strErrMsg);
                LoadDDLTipoActivo();
                LoadDDLClasificacionActivo();
                LoadDDLRiesgoAsociar();
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                //omb.ShowMessage($"Error al cargar la información. {ex.Message}", 1, "Atención");
            }
        }

        private bool LoadDDLCadenaValor(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            List<clsCadenaValor> lstCadenaValor = new List<clsCadenaValor>();
            clsCadenaValorBLL cCadenaValor = new clsCadenaValorBLL();
            #endregion Vars

            try
            {
                lstCadenaValor = cCadenaValor.mtdConsultarCadenaValor(true, ref strErrMsg);

                ddlCadenaValor.Items.Clear();
                ddlCadenaValor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));

                if (string.IsNullOrEmpty(strErrMsg))
                {
                    if (lstCadenaValor != null)
                    {
                        int intCounter = 1;

                        foreach (clsCadenaValor objCadenaValor in lstCadenaValor)
                        {
                            ddlCadenaValor.Items.Insert(intCounter, new System.Web.UI.WebControls.ListItem(objCadenaValor.strNombreCadenaValor, objCadenaValor.intId.ToString()));
                            intCounter++;
                        }
                        booResult = false;
                    }
                    else
                        booResult = true;
                }
                else
                    booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de las cadenas de valor. [{0}]", ex.Message);
                booResult = true;
                throw ex;
            }

            return booResult;
        }


        private void LoadDDLTipoActivo()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cActivos.TipoActivosVer();
                ddlObjetivo.Items.Insert(0, new ListItem("--Seleccione--", "0"));
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    ddlObjetivo.Items.Insert(i, new ListItem(dtInfo.Rows[i]["NombreTipoActivo"].ToString().Trim(), dtInfo.Rows[i]["IdTipoActivo"].ToString()));
                }
                ddlObjetivo.SelectedValue = "0";
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                mtdMensaje("Error al cargar los tipos de registro. " + ex.Message);
            }
        }

        private void LoadDDLClasificacionActivo()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cActivos.ClasificacionActivosVer();
                ddlClasificacionActivo.Items.Insert(0, new ListItem("--Seleccione--", "0"));
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    ddlClasificacionActivo.Items.Insert(i, new ListItem(dtInfo.Rows[i]["NombreClasificacionActivo"].ToString().Trim(), dtInfo.Rows[i]["IdClasificacionActivo"].ToString()));
                }
                ddlClasificacionActivo.SelectedValue = "0";
            }
            catch (Exception ex)
            {
                mtdMensaje("Error al cargar las clasificaciones del activo. " + ex.Message);
            }
        }

        private void LoadDDLRiesgoAsociar()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cActivos.RiesgosAsocVer();
                ddlRiesgoAsociado.Items.Insert(0, new ListItem("--Seleccione--", "0"));
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    ddlRiesgoAsociado.Items.Insert(i, new ListItem(dtInfo.Rows[i]["Nombre"].ToString().Trim(), dtInfo.Rows[i]["IdRiesgo"].ToString()));
                }
                ddlRiesgoAsociado.SelectedValue = "0";
            }
            catch (Exception ex)
            {
                mtdMensaje("Error al cargar los riesgos. " + ex.Message);
            }
        }



        protected void DdlCadenaValor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            ddlMacroproceso.Items.Clear();

            if (LoadDDLMacroProceso(Convert.ToInt32(ddlCadenaValor.SelectedValue)))
            {
                ddlMacroproceso.ClearSelection();
                ddlProceso.Items.Clear();
                ddlSubproceso.Items.Clear();
            };
        }

        private bool LoadDDLMacroProceso(int IdCadenaValor)
        {
            #region Vars
            bool booResult = false;
            List<clsMacroproceso> lstMacroproceso = new List<clsMacroproceso>();
            clsMacroProcesoBLL cMacroproceso = new clsMacroProcesoBLL();
            #endregion Vars

            try
            {
                DataTable dt = cMacroproceso.ConsultarMacroProcesos(IdCadenaValor);
                ddlMacroproceso.Items.Clear();
                ddlMacroproceso.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow Row in dt.Rows)
                    {
                        int intCounter = 1;
                        ddlMacroproceso.Items.Insert(intCounter, new System.Web.UI.WebControls.ListItem(Row["Nombre"].ToString(), Row["IdMacroProceso"].ToString()));
                        intCounter++;
                    }
                }
                booResult = true;
            }
            catch (Exception ex)
            {
                booResult = false;
                throw ex;
            }

            return booResult;
        }

        protected void DdlMacroproceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            ddlProceso.Items.Clear();

            if (LoadDDLProceso(Convert.ToInt32(ddlMacroproceso.SelectedValue)))
            {
                ddlProceso.ClearSelection();
                ddlSubproceso.Items.Clear();
            }
        }

        private bool LoadDDLProceso(int IdMacroproceso)
        {
            #region Vars
            bool booResult = false;
            clsMacroproceso objMProceso = new clsMacroproceso();
            List<clsProceso> lstProceso = new List<clsProceso>();
            clsProcesoBLL cProceso = new clsProcesoBLL();
            #endregion Vars

            try
            {

                DataTable dt = cProceso.ConsultarProcesos(IdMacroproceso);
                ddlProceso.Items.Clear();
                ddlProceso.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));

                if (dt != null && dt.Rows.Count > 0)
                {
                    int intCounter = 1;
                    foreach (DataRow Row in dt.Rows)
                    {
                        ddlProceso.Items.Insert(intCounter, new System.Web.UI.WebControls.ListItem(Row["Nombre"].ToString(), Row["IdProceso"].ToString()));
                        intCounter++;
                    }
                }
                booResult = true;
            }
            catch (Exception ex)
            {
                booResult = true;
                throw ex;
            }

            return booResult;
        }

        protected void DdlProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LoadDDLSubproceso(Convert.ToInt32(ddlProceso.SelectedValue)))
            {
                ddlSubproceso.ClearSelection();
                if (ddlSubproceso.Items.Count == 1)
                {
                    //omb.ShowMessage("No hay información de Subprocesos", 2, "Atención");
                }
            }
        }

        private bool LoadDDLSubproceso(int? idProceso)
        {
            #region Vars
            bool booResult = false;
            clsProceso objProceso = new clsProceso();
            List<clsSubproceso> lstSubproceso = new List<clsSubproceso>();
            clsSubprocesoBLL cSubproceso = new clsSubprocesoBLL();
            #endregion Vars

            try
            {
                DataTable dt = cSubproceso.ConsultarSubprocesos(idProceso);

                ddlSubproceso.Items.Clear();
                ddlSubproceso.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));

                if (dt != null && dt.Rows.Count > 0)
                {
                    int intCounter = 1;
                    foreach (DataRow Row in dt.Rows)
                    {
                        ddlSubproceso.Items.Insert(intCounter, new System.Web.UI.WebControls.ListItem(Row["Nombre"].ToString(), Row["IdSubproceso"].ToString()));
                        intCounter++;
                    }
                }
                booResult = true;
            }
            catch (Exception ex)
            {
                booResult = false;
                throw ex;
            }

            return booResult;
        }
        private void mtdLoadInfoGridPremisas()
        {
            string strErrMsg = string.Empty;
            clsPremisas cConsulReq = new clsPremisas();
            List<clsDTOPremisas> lstEvidencias = new List<clsDTOPremisas>();
            cblPremisas.Items.Clear();
            lstEvidencias = cConsulReq.mtdCargarDatos(ref strErrMsg);

            if (lstEvidencias != null)
            {
                try
                {
                    int iteracion = 0;
                    foreach (clsDTOPremisas objRegReq in lstEvidencias)
                    {
                        cblPremisas.Items.Insert(iteracion, new ListItem(objRegReq.Nombre, objRegReq.IdPremisa));
                        //cblPremisas.Items.Insert(iteracion, new ListItem(objRegReq.Nombre, objRegReq.IdPremisa));
                    }
                }
                catch (Exception ex)
                {
                    mtdMensaje("Error al cargar las premisas. " + ex.Message);
                }
            }
        }
        private void mtdMensaje(string Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        protected void ddlDisponibilidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Confidencialidad = Convert.ToInt32(ddlConfidencialidad.SelectedValue);
            int Integridad = Convert.ToInt32(ddlIntegridad.SelectedValue);
            int Disponibilidad = Convert.ToInt32(ddlDisponibilidad.SelectedValue);
            int sumCriticidad = Confidencialidad + Integridad + Disponibilidad;
            if (sumCriticidad <= 3)
            {
                TextBox2.Text = "Alta";
            }
            if (sumCriticidad > 3 && sumCriticidad <= 8)
            {
                TextBox2.Text = "Media";
            }
            if (sumCriticidad == 9)
            {
                TextBox2.Text = "Baja";
            }
        }
        private void mdtCleanFields()
        {
            txtNombreActivo.Text = string.Empty;
            ddlObjetivo.ClearSelection();
            txtDescripcion.Text = string.Empty;
            ddlCadenaValor.ClearSelection();
            ddlMacroproceso.ClearSelection();
            ddlProceso.ClearSelection();
            ddlSubproceso.ClearSelection();
            TextBox1.Text = string.Empty;
            ddlConfidencialidad.ClearSelection();
            txtJustConfidencialidad.Text = string.Empty;
            ddlIntegridad.ClearSelection();
            txtJustIntegridad.Text = string.Empty;
            ddlDisponibilidad.ClearSelection();
            txtJusDispobilidad.Text = string.Empty;
            TextBox2.Text = string.Empty;
            txtPropietarioActivo.Text = string.Empty;
            lblIdDependencia1.Text = string.Empty;
            cblPremisas.ClearSelection();
        }

        protected void btnImgokEliminar_Click(object sender, EventArgs e)
        {
           int IdActivo =  Convert.ToInt32(InfoGridActivo.Rows[RowGridActivo]["IdActivos"].ToString().Trim());
            try
            {
                clsActivosBLL cData = new clsActivosBLL();
                string strErrMsg = string.Empty;
                if(cData.mtdEliminarActivos(ref strErrMsg, IdActivo))
                {
                    btnImgokEliminar.Visible = false;
                    mtdMensaje("Activo Eliminado satisfactoriamente");
                    mdtCleanFields();

                    if (mtdLoadActivos(ref strErrMsg) == false)
                        mtdMensaje(strErrMsg);
                }
            }catch(Exception ex)
            {
                mtdMensaje("Error en el borrado: "+
                    ex.Message);
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexInfoGridActivos = e.NewPageIndex;
            GridView1.PageIndex = PagIndexInfoGridActivos;
            GridView1.DataSource = InfoGridActivo;
            GridView1.DataBind();
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            exportExcel(InfoGridActivo, Response, "Reporte Activos");
        }

        public static void exportExcel(DataTable dt, HttpResponse Response, string filename)
        {

            // Create the workbook
            XLWorkbook workbook = new XLWorkbook();
            //workbook.Worksheets.Add("Sample").Cell(1, 1).SetValue("Hello World");
            workbook.Worksheets.Add(dt);
            // Prepare the response
            HttpResponse httpResponse = Response;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpResponse.AddHeader("content-disposition", "attachment;filename=\"" + filename + ".xlsx\"");

            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                workbook.SaveAs(memoryStream);
                memoryStream.WriteTo(httpResponse.OutputStream);
                memoryStream.Close();
            }

            httpResponse.End();
        }

    }
}