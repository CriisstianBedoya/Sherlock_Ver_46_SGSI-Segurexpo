using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using clsDTO;

namespace clsDatos
{
    public class clsDtPremisas
    {

        public DataTable mtdConsultaDatos(ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT IdPremisa, Codigo, Nombre, Descripcion,IdUsuario FROM SGSI.Premisas");
                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las premisas. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;

        }

        public DataTable mtdInsertarPremisa(clsDTOPremisas objPremisa, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [SGSI].[Premisas] ([Codigo], [Nombre]," +
                    " [Descripcion],[IdUsuario],[FechaRegistro]) VALUES ('{0}', '{1}', '{2}',{3}, GETDATE())", objPremisa.Codigo,
                    objPremisa.Nombre, objPremisa.Descripcion, objPremisa.IdUsuario);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el requerimiento. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }
            return dtInformacion;
        }

        public DataTable mtdActualizarPremisa(clsDTOPremisas objPremisa, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [SGSI].[Premisas] SET [Codigo]='{0}', [Nombre]='{1}'," +
                    " [Descripcion]='{2}' where IdPremisa = " + objPremisa.IdPremisa + "", objPremisa.Codigo,
                    objPremisa.Nombre, objPremisa.Descripcion);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el requerimiento. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }
            return dtInformacion;
        }

    }
}
