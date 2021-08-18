using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using clsLogica;
using clsDTO;
using ListasSarlaft.Classes;
using Microsoft.Security.Application;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.IO;

namespace ListasSarlaft.UserControls.SGSI
{
    public partial class Premisas : System.Web.UI.UserControl
    {
        string IdFormulario = "11011";
        clsCuenta cCuenta = new clsCuenta();
        cCuenta ccCuenta = new cCuenta();
        private int pagIndex;
        private DataTable infoGrid;
        private int rowGrid;
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        cAuditoria cAu = new cAuditoria();

        #region Properties
        private int PagIndex
        {
            get
            {
                pagIndex = (int)ViewState["pagIndex"];
                return pagIndex;
            }
            set
            {
                pagIndex = value;
                ViewState["pagIndex"] = pagIndex;
            }
        }

        private DataTable InfoGrid
        {
            get
            {
                infoGrid = (DataTable)ViewState["infGrid2"];
                return infoGrid;
            }
            set
            {
                infoGrid = value;
                ViewState["infGrid2"] = infoGrid;
            }
        }

        private int RowGrid
        {
            get
            {
                rowGrid = (int)ViewState["rowGrid"];
                return rowGrid;
            }
            set
            {
                rowGrid = value;
                ViewState["rowGrid"] = rowGrid;
            }
        }
        private void mtdMensaje(string Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }
        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            cCuenta cCuenta = new cCuenta();
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
            {
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            }
            if (!Page.IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(this.gvPremisas);
                mtdInicializarValoresPremisas();
                mtdLoadGridViewPremisas();
                mtdHideAll();
                //divGuardarCancelar.Visible = false;
                gvPremisas.Visible = true;
            }
        }

        protected void mtdShowAll()
        {
            divCampos.Visible = true;
        }

        protected void mtdHideAll()
        {
            divCampos.Visible = false;

        }

        protected void imgBtnInsertar_Click(object sender, ImageClickEventArgs e)
        {
            divInsertar.Visible = false;
            mtdShowAll();
            //divGuardarCancelar.Visible = true;
            btnImgActualizar.Visible = false;
            btnImgInsertar.Visible = true;
            dvConsultaPremisa.Visible = false;
        }

        #endregion

        #region Registrar premisa
        protected void btnImgInsertar_Click(object sender, ImageClickEventArgs e)
        {
            if (Codigo.Text != "")
            {

                if (Nombre.Text != "")
                {

                    if (Descripcion.Text != "")
                    {

                        DataTable dtInfo = new DataTable();
                        string strErrMsg = string.Empty;

                        try
                        {
                            try
                            {
                                strErrMsg = "Premisa creada exitosamente.";
                                gvPremisas.Visible = true;

                                mtdGuardarPremisa(string.Empty,
                                    Sanitizer.GetSafeHtmlFragment(Server.HtmlEncode(Codigo.Text.Trim())),
                                    Sanitizer.GetSafeHtmlFragment(Server.HtmlEncode(Nombre.Text.Trim())),
                                    Sanitizer.GetSafeHtmlFragment(Server.HtmlEncode(Descripcion.Text.Trim())),
                                    ref strErrMsg
                                    );
                                
                                mtdMensaje("Premisa creada exitosamente.");
                                mtdInicializarValoresPremisas();
                                mtdLoadGridViewPremisas();
                                mtdResetCampos();
                                dvConsultaPremisa.Visible = true;
                                divCampos.Visible = false;
                                divInsertar.Visible = true;
                            }
                            catch (Exception except)
                            {
                                mtdMensaje("Error al registrar el requerimiento." + except.Message.ToString());
                            }
                        }
                        catch (Exception except)
                        {
                            mtdMensaje("Error al registrar el requerimiento." + except.Message.ToString());
                        }
                    }
                    else
                    {
                        mtdMensaje("Debe proporcionar una descripción para la premisa.");
                    }
                }
                else
                {
                    mtdMensaje("La premisa debe llevar un nombre.");
                }
            }
            else
            {
                mtdMensaje("Debe proporcionar un código para la premisa.");
            }

        }

        private void mtdGuardarPremisa(string iDPremisa, string codigo, string nombre, string descripcion, ref string strErrMsg)
        {
            clsPremisas cPremisa = new clsPremisas();
            clsDTOPremisas objPremisa = new clsDTOPremisas(iDPremisa, codigo, nombre, descripcion,Convert.ToInt32(Session["idUsuario"].ToString()));

            cPremisa.mtdGuardarPremisa(objPremisa, ref strErrMsg);
        }

        public void mtdCorregirCaracter()
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("update SGSI.Premisas set Descripcion = replace(replace(Descripcion, '&lt;', '<'), '&gt;', '>') ");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Actualizar premisa
        protected void btnImgActualizar_Click(object sender, ImageClickEventArgs e)
        {
            if (Codigo.Text != "")
            {

                if (Nombre.Text != "")
                {

                    if (Descripcion.Text != "")
                    {

                        DataTable dtInfo = new DataTable();
                        string strErrMsg = string.Empty;

                        try
                        {
                            try
                            {
                                strErrMsg = "Premisa actualizada con éxito.";
                                gvPremisas.Visible = true;

                                mtdActualizarPremisa(InfoGrid.Rows[RowGrid]["IdPremisa"].ToString().Trim(),
                                    Sanitizer.GetSafeHtmlFragment(Codigo.Text.Trim()),
                                    Sanitizer.GetSafeHtmlFragment(Nombre.Text.Trim()),
                                    Sanitizer.GetSafeHtmlFragment(Descripcion.Text.Trim()),
                                    ref strErrMsg
                                    );
                                
                                mtdMensaje("Premisa actualizada con éxito.");
                                mtdInicializarValoresPremisas();
                                mtdLoadGridViewPremisas();
                                dvConsultaPremisa.Visible = true;
                                divCampos.Visible = false;
                                divInsertar.Visible = true;
                            }
                            catch (Exception except)
                            {
                                mtdMensaje("Error al actualizar el requerimiento." + except.Message.ToString());
                            }
                        }
                        catch (Exception except)
                        {
                            mtdMensaje("Error al actualizar el requerimiento." + except.Message.ToString());
                        }
                    }
                    else
                    {
                        mtdMensaje("Debe proporcionar una descripción para la premisa.");
                    }
                }
                else
                {
                    mtdMensaje("La premisa debe llevar un nombre.");
                }
            }
            else
            {
                mtdMensaje("Debe proporcionar un código para la premisa.");
            }

        }

        private void mtdActualizarPremisa(string iDPremisa, string codigo, string nombre, string descripcion, ref string strErrMsg)
        {
            clsPremisas cPremisa = new clsPremisas();
            clsDTOPremisas objPremisa = new clsDTOPremisas(iDPremisa, codigo, nombre, descripcion);

            cPremisa.mtdActualizarPremisa(objPremisa, ref strErrMsg);
        }

        public void mtdCorregirUpdCaracter()
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("update SGSI.Premisas set Descripcion = replace(replace(Descripcion, '&lt;', '<'), '&gt;', '>') ");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Eliminar premisa
        

        public void borrarUsuario(string strIdPremisa)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM SGSI.Premisas WHERE (IdPremisa = " + strIdPremisa + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Reset campos
        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            divInsertar.Visible = true;
            mtdHideAll();
            mtdResetCampos();
            //divGuardarCancelar.Visible = false;
            btnImgActualizar.Visible = false;
            imgBtnInsertar.Visible = true;
            mtdInicializarValoresPremisas();
            mtdLoadGridViewPremisas();
            dvConsultaPremisa.Visible = true;
            divInsertar.Visible = true;
        }

        private void mtdResetCampos()
        {
            Codigo.Text = string.Empty;
            Nombre.Text = string.Empty;
            Descripcion.Text = string.Empty;
        }
        #endregion

        #region Cargar grid gvPremisas
        private void mtdLoadGridViewPremisas()
        {
            mtdLoadGridPremisas();
            mtdLoadInfoGridPremisas();
        }

        private void mtdLoadGridPremisas()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("IdPremisa", typeof(string));
            grid.Columns.Add("Codigo", typeof(string));
            grid.Columns.Add("Nombre", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));

            gvPremisas.DataSource = grid;
            gvPremisas.DataBind();
            InfoGrid = grid;
        }

        private void mtdLoadInfoGridPremisas()
        {
            string strErrMsg = string.Empty;
            clsPremisas cConsulReq = new clsPremisas();
            List<clsDTOPremisas> lstEvidencias = new List<clsDTOPremisas>();

            lstEvidencias = cConsulReq.mtdCargarDatos(ref strErrMsg);

            if (lstEvidencias != null)
            {
                mtdLoadInfoGrid(lstEvidencias);
                gvPremisas.DataSource = lstEvidencias;
                gvPremisas.DataBind();
            }
        }

        private void mtdLoadInfoGrid(List<clsDTOPremisas> lstEvidencias)
        {
            foreach (clsDTOPremisas objRegReq in lstEvidencias)
            {
                InfoGrid.Rows.Add(new Object[] {
                    objRegReq.IdPremisa.ToString().Trim(),
                    objRegReq.Codigo.ToString().Trim(),
                    objRegReq.Nombre.ToString().Trim(),
                    objRegReq.Descripcion.ToString().Trim()
                    });
            }
        }

        protected void gvPremisas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            gvPremisas.PageIndex = PagIndex;
            gvPremisas.DataSource = InfoGrid;
            gvPremisas.DataBind();

            //mtdLoadGridViewPremisas();
        }

        protected void gvPremisas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string strErrMsg = string.Empty;

            RowGrid = (Convert.ToInt16(gvPremisas.PageSize) * PagIndex) + Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Modificar":
                    mtdLoadUpdateData();
                    break;
                case "Eliminar":
                    btnImgokEliminar.Visible = true;
                    mtdMensaje("¿Seguro que desea eliminar la información de la premisa?");
                    break;
            }
        }

        private void mtdLoadUpdateData()
        {
            //Codigo.Text = InfoGrid.Rows[PagIndex]["Codigo"].ToString().Trim();
            //Nombre.Text = InfoGrid.Rows[PagIndex]["Nombre"].ToString().Trim();
            //Descripcion.Text = InfoGrid.Rows[PagIndex]["Descripcion"].ToString().Trim();
            Codigo.Text = InfoGrid.Rows[RowGrid]["Codigo"].ToString().Trim();
            Nombre.Text = InfoGrid.Rows[RowGrid]["Nombre"].ToString().Trim();
            Descripcion.Text = InfoGrid.Rows[RowGrid]["Descripcion"].ToString().Trim();
            dvConsultaPremisa.Visible = false;
            divInsertar.Visible = false;
            btnImgInsertar.Visible = false;
            divCampos.Visible = true;
            btnImgActualizar.Visible = true;
        }

        private void BtnModificarUsuario_Click(ref string strErrMsg)
        {
            imgBtnInsertar.Visible = false;
            //divGuardarCancelar.Visible = true;
            btnImgInsertar.Visible = false;
            divCampos.Visible = true;
            btnImgActualizar.Visible = true;
        }


        private void mtdInicializarValoresPremisas()
        {
            PagIndex = 0;
        }

        private void mtdMensajePremisas(string Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }
        #endregion

        protected void btnImgokEliminar_Click(object sender, EventArgs e)
        {
            string strIdPremisa = InfoGrid.Rows[RowGrid]["IdPremisa"].ToString().Trim();

            try
            {
                borrarUsuario(
                    strIdPremisa
                    );
                mtdResetCampos();
                btnImgokEliminar.Visible = false;
                mtdMensaje("Premisa eliminada con éxito.");
                divInsertar.Visible = true;
            }
            catch (Exception ex)
            {
                mtdMensaje("Error eliminando la premisa." + ex.Message);
            }
            mtdLoadGridViewPremisas();
        }
    }
}