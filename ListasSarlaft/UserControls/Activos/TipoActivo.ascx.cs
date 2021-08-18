using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ListasSarlaft.Classes;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.Activos
{
    public partial class TipoActivo : System.Web.UI.UserControl
    {
        cCuenta cCuenta = new cCuenta();
        cActivos cActivos = new cActivos();
        String IdFormulario = "1001";
        clsBLLRoles RolBLL = new clsBLLRoles();
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
                    inicializarValores();
                    loadGridTipoActivo();
                    infoGridTipoActivos();
                    
                    
                }
            }
        }

        private void inicializarValores()
        {
            PagIndexInfoGridActivos = 0;
        }

        private void infoGridTipoActivos()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cActivos.TipoActivosVer();
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridTiposActivos.Rows.Add(new Object[] 
                    {
                        dtInfo.Rows[rows]["IdTipoActivo"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreTipoActivo"].ToString().Trim()
                    }
                        );
                }
                //GridView1.PageIndex = PagIndexInfoGridActivos;
                GridView1.DataSource = InfoGridTiposActivos;
                GridView1.DataBind();
            }
        }

        private void loadGridTipoActivo()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdTipoActivo", typeof(string));
            grid.Columns.Add("NombreTipoActivo", typeof(string));
            InfoGridTiposActivos = grid;
            GridView1.DataSource = InfoGridTiposActivos;
            GridView1.DataBind();
        }

        #region Propierties
        private DataTable infoGridTiposActivos;
        private DataTable InfoGridTiposActivos
        {
            get
            {
                infoGridTiposActivos = (DataTable)ViewState["infoGridTiposActivos"];
                return infoGridTiposActivos;
            }
            set
            {
                infoGridTiposActivos = value;
                ViewState["infoGridTiposActivos"] = infoGridTiposActivos;
            }
        }

        private int rowGridTipoActivos;
        private int RowGridTipoActivos
        {
            get
            {
                rowGridTipoActivos = (int)ViewState["rowGridTipoActivos"];
                return rowGridTipoActivos;
            }
            set
            {
                rowGridTipoActivos = value;
                ViewState["rowGridTipoActivos"] = rowGridTipoActivos;
            }
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

        #endregion

        protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
        {
            resetValues();
        }

        private void resetValues()
        {
            trCampos.Visible = false;
            ImageButton2.Visible = false;
            ImageButton3.Visible = false;
            TextBox1.Text = "";
            infoGridTipoActivos();
            loadGridTipoActivo();
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
            }
            else
            {
                resetValues();
                trCampos.Visible = true;
                ImageButton2.Visible = true;
            }
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(IdFormulario) == "False")
                {
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                }
                else
                {
                    cActivos.agregarTipoActivo(Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()));
                    resetValues();
                    Mensaje("Registro agregado con éxito.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al agregar el registro. " + ex.Message);
            }
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                {
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                }
                else
                {
                    cActivos.actualizarTipoActivo(Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()), InfoGridTiposActivos.Rows[RowGridTipoActivos]["IdTipoActivo"].ToString().Trim());
                    resetValues();
                    Mensaje("Registro agregado con éxito.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al actualizar el registro. " + ex.Message);
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridTipoActivos = (Convert.ToInt16(GridView1.PageSize) * PagIndexInfoGridActivos) + Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Modificar":
                    resetValues();
                    detalleRegistro();
                    break;
                case "Eliminar":
                    if (cCuenta.permisosBorrar(IdFormulario) == "False")
                    {
                        lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                        mpeMsgBox.Show();
                    }
                    else
                    {
                        lblMsgBox.Text = "Desea eliminar la información de la Base de Datos?";
                        mpeMsgBox.Show();
                    }
                    break;
            }
        }

        protected void btnImgokEliminar_Click(object sender, EventArgs e)
        {
            mpeMsgBox.Hide();
            try
            {
                if (cCuenta.permisosBorrar(IdFormulario) == "False")
                {
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                }
                else
                {
                    cActivos.eliminarTipoActivo(InfoGridTiposActivos.Rows[RowGridTipoActivos]["IdTipoActivo"].ToString().Trim());
                    resetValues();
                    Mensaje("Registro Eliminado con éxito.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al actualizar el registro. " + ex.Message);
            }
        }

        private void detalleRegistro()
        {
            TextBox1.Text = InfoGridTiposActivos.Rows[RowGridTipoActivos]["NombreTipoActivo"].ToString().Trim();
            ImageButton3.Visible = true;
            trCampos.Visible = true;
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexInfoGridActivos = e.NewPageIndex;
            GridView1.PageIndex = PagIndexInfoGridActivos;
            GridView1.DataSource = InfoGridTiposActivos;
            GridView1.DataBind();
        }
    }
}