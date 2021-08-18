using ListasSarlaft.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ListasSarlaft.UserControls.Activos
{
    public partial class Activos : System.Web.UI.UserControl
    {
        string IdFormulario = "10001";
        cCuenta cCuenta = new cCuenta();
        cActivos cActivos = new cActivos();
        cParametrizacion Parametrizacion = new cParametrizacion();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["IdUsuario"].ToString().Trim()))
            {
                Response.Redirect("~/Formularios/Sitio/Login.aspx", false);
            }
            else
            {
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
                    CargarDDLs();
                }
            }
        }

        private void inicializarValores()
        {
            PagIndexInfoGridActivos = 0;
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
        private DataTable InfoGridActivo        {
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

        private void infoGridActivos()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdActivo", typeof(string));
            grid.Columns.Add("NombreActivo", typeof(string));
            InfoGridActivo = grid;
            GridView1.DataSource = InfoGridActivo;
            GridView1.DataBind();
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
        }

        protected void btnImgInsertar_Click(object sender, ImageClickEventArgs e)
        {
            RegistrarActivos.Visible = false;
            TablaActivos.Visible = true;
        }

        protected void btnImgActualizar_Click(object sender, ImageClickEventArgs e)
        {
            RegistrarActivos.Visible = false;
            TablaActivos.Visible = true;
        }

        protected void btnImgCancelarRegAvances_Click(object sender, ImageClickEventArgs e)
        {
            RegistrarActivos.Visible = false;
            TablaActivos.Visible = true;
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() == "Select")
            {
                RegistrarActivos.Visible = true;
                TablaActivos.Visible = false;
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
            }
            catch (Exception ex)
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
                ddlObjetivo.Items.Insert(0, new ListItem("---", "---"));
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    ddlObjetivo.Items.Insert(i, new ListItem(dtInfo.Rows[i]["NombreTipoActivo"].ToString().Trim(), dtInfo.Rows[i]["IdTipoActivo"].ToString()));
                }
                ddlObjetivo.SelectedIndex = ddlObjetivo.Items.Count;
            }
            catch (Exception ex)
            {
                //Mensaje("Error al cargar los tipos de registro. " + ex.Message);
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
    }  
}