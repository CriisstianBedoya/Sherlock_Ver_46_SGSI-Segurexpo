using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using ListasSarlaft.Classes.DTO.SGSI;
namespace ListasSarlaft.Classes.DAL.SGSI
{

    public class clsActivosDAL
    {
        #region Variables Globales
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        private OleDbParameter[] parameters;
        private OleDbParameter parameter;
        #endregion Variables Globales
        public bool mtdInsertarActivos(ActivosDTO objActivos, ref string strErrMsg, List<int> lstPremisas)
        {
            string strConsulta = string.Empty, strTodosProcesos = string.Empty, strContrasenaEncriptada = string.Empty;
            bool booResult = true;

            try
            {
                #region Creacion Consulta
                parameters = new OleDbParameter[18];
                parameter = new OleDbParameter("@NombreActivo", OleDbType.VarChar);
                parameter.Value = objActivos.NombreActivo;
                parameters[0] = parameter;
                parameter = new OleDbParameter("@IdTipoActivo", OleDbType.Numeric);
                parameter.Value = objActivos.IdTipoActivo;
                parameters[1] = parameter;
                parameter = new OleDbParameter("@IdClasificacionActivo", OleDbType.Numeric);
                parameter.Value = objActivos.IdClasificacionActivo;
                parameters[2] = parameter;
                parameter = new OleDbParameter("@Descripcion", OleDbType.VarChar);
                parameter.Value = objActivos.Descripcion;
                parameters[3] = parameter;
                parameter = new OleDbParameter("@IdCadenaValor", OleDbType.Numeric);
                parameter.Value = objActivos.IdCadenaValor;
                parameters[4] = parameter;
                parameter = new OleDbParameter("@IdMacroProceso", OleDbType.Numeric);
                parameter.Value = objActivos.IdMacroProceso;
                parameters[5] = parameter;
                parameter = new OleDbParameter("@IdProceso", OleDbType.Numeric);
                parameter.Value = objActivos.IdProceso;
                parameters[6] = parameter;
                parameter = new OleDbParameter("@IdSubProceso", OleDbType.Numeric);
                parameter.Value = objActivos.IdSubProceso;
                parameters[7] = parameter;
                parameter = new OleDbParameter("@Ubicacion", OleDbType.VarChar);
                parameter.Value = objActivos.Ubicacion;
                parameters[8] = parameter;
                parameter = new OleDbParameter("@IdConfidencialidad", OleDbType.Numeric);
                parameter.Value = objActivos.IdConfidencialidad;
                parameters[9] = parameter;
                parameter = new OleDbParameter("@JustificacionConfidencialidad", OleDbType.VarChar);
                parameter.Value = objActivos.JustificacionConfidencialidad;
                parameters[10] = parameter;
                parameter = new OleDbParameter("@IdIntegridad", OleDbType.Numeric);
                parameter.Value = objActivos.IdIntegridad;
                parameters[11] = parameter;
                parameter = new OleDbParameter("@JustificacionIntegridad", OleDbType.VarChar);
                parameter.Value = objActivos.JustificacionIntegridad;
                parameters[12] = parameter;
                parameter = new OleDbParameter("@IdDisponibilidad", OleDbType.Numeric);
                parameter.Value = objActivos.IdDisponibilidad;
                parameters[13] = parameter;
                parameter = new OleDbParameter("@JustificacionDisponibilidad", OleDbType.VarChar);
                parameter.Value = objActivos.JustificacionDisponibilidad;
                parameters[14] = parameter;
                parameter = new OleDbParameter("@Criticidad", OleDbType.VarChar);
                parameter.Value = objActivos.Criticidad;
                parameters[15] = parameter;
                parameter = new OleDbParameter("@IdPropietario", OleDbType.Numeric);
                parameter.Value = objActivos.IdPropietario;
                parameters[16] = parameter;
                parameter = new OleDbParameter("@IdUsuario", OleDbType.Numeric);
                parameter.Value = objActivos.Idusuario;
                parameters[17] = parameter;
                #endregion Creacion Consulta

                cDataBase.conectar();
                cDataBase.ejecutarSPParametros("SGSI.InsertActivos", parameters);
                cDataBase.desconectar();


                DataTable dtInformacion = new DataTable();

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT MAX(IdActivo) AS LastIdActivo FROM [SGSI].[Activos]");
                cDataBase.desconectar();

                int IdActivo = Convert.ToInt32(dtInformacion.Rows[0]["LastIdActivo"].ToString());

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("INSERT INTO SGSI.RiesgosvsActivos (IdRiesgo, IdActivo, IdUsuario, FechaRegistro) " +
                    "VALUES (" + objActivos.IdRiesgo + ", " + IdActivo + ", " + objActivos.Idusuario + ", GETDATE())");
                cDataBase.desconectar();

                foreach (int idPremisa in lstPremisas)
                {
                    parameters = new OleDbParameter[3];
                    parameter = new OleDbParameter("@IdActivo", OleDbType.Numeric);
                    parameter.Value = IdActivo;
                    parameters[0] = parameter;
                    parameter = new OleDbParameter("@IdPremisa", OleDbType.Numeric);
                    parameter.Value = idPremisa;
                    parameters[1] = parameter;
                    parameter = new OleDbParameter("@IdUsuario", OleDbType.Numeric);
                    parameter.Value = objActivos.Idusuario;
                    parameters[2] = parameter;

                    cDataBase.conectar();
                    cDataBase.ejecutarSPParametros("SGSI.InsertActivosvsPremisas", parameters);
                    cDataBase.desconectar();
                }
                booResult = true;
            }
            catch (Exception ex)
            {
                booResult = false;
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                strErrMsg = string.Format("Error al insertar el activo. [{0}]", ex.Message);
            }


            return booResult;
        }
        public DataTable mtdConsultaActivos(string strErrMsg)
        {
            DataTable dtInfo = new DataTable();
            try
            {
                string query = "SELECT [IdActivo],[NombreActivo],[Descripcion],[IdCadenaValor],[NombreCadenaValor]" + "\n" +
            ",[IdMacroProceso],[NombreMacro],[IdProceso],[NombreProceso],[IdSubProceso]" + "\n" +
            ",[NombreSubproceso],[Ubicacion],[IdConfidencialidad],[JustificacionConfidencialidad]" + "\n" +
            ",[IdIntegridad],[JustificacionIntegridad],[IdDisponibilidad],[JustificacionDisponibilidad]" + "\n" +
            ",[Criticidad],[IdPropietario],[Propietario],[IdUsuario],[Usuario],[IdTipoActivo],[NombreTipoActivo],[IdClasificacionActivo],[NombreClasificacionActivo] " + "\n" +
            "FROM [SGSI].[vwActivos]";

                cDataBase.conectar();
                dtInfo = cDataBase.ejecutarConsulta(query);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                strErrMsg = string.Format("Error al consultar los activos. [{0}]", ex.Message);
            }
            return dtInfo;
        }
        public DataTable mtdConsultaActivosvsPremisas(ref string strErrMsg, int IdActivo)
        {
            DataTable dtInfo = new DataTable();
            try
            {
                string query = "SELECT [IdActivosvsPremisa],[IdActivo],[IdPremisa] "+"\n"+
                "FROM [SGSI].[ActivosvsPremisas]  "+"\n"+
                "where IdActivo = "+IdActivo;

                cDataBase.conectar();
                dtInfo = cDataBase.ejecutarConsulta(query);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                strErrMsg = string.Format("Error al consultar los activos. [{0}]", ex.Message);
            }
            return dtInfo;
        }
        public bool mtdActualizarActivos(ActivosDTO objActivos, ref string strErrMsg, List<int> lstPremisas)
        {
            string strConsulta = string.Empty, strTodosProcesos = string.Empty, strContrasenaEncriptada = string.Empty;
            bool booResult = true;

            try
            {
                #region Creacion Consulta
                parameters = new OleDbParameter[19];
                parameter = new OleDbParameter("@NombreActivo", OleDbType.VarChar);
                parameter.Value = objActivos.NombreActivo;
                parameters[0] = parameter;
                parameter = new OleDbParameter("@IdTipoActivo", OleDbType.Numeric);
                parameter.Value = objActivos.IdTipoActivo;
                parameters[1] = parameter;
                parameter = new OleDbParameter("@IdClasificacionActivo", OleDbType.Numeric);
                parameter.Value = objActivos.IdClasificacionActivo;
                parameters[2] = parameter;
                parameter = new OleDbParameter("@Descripcion", OleDbType.VarChar);
                parameter.Value = objActivos.Descripcion;
                parameters[3] = parameter;
                parameter = new OleDbParameter("@IdCadenaValor", OleDbType.Numeric);
                parameter.Value = objActivos.IdCadenaValor;
                parameters[4] = parameter;
                parameter = new OleDbParameter("@IdMacroProceso", OleDbType.Numeric);
                parameter.Value = objActivos.IdMacroProceso;
                parameters[5] = parameter;
                parameter = new OleDbParameter("@IdProceso", OleDbType.Numeric);
                parameter.Value = objActivos.IdProceso;
                parameters[6] = parameter;
                parameter = new OleDbParameter("@IdSubProceso", OleDbType.Numeric);
                parameter.Value = objActivos.IdSubProceso;
                parameters[7] = parameter;
                parameter = new OleDbParameter("@Ubicacion", OleDbType.VarChar);
                parameter.Value = objActivos.Ubicacion;
                parameters[8] = parameter;
                parameter = new OleDbParameter("@IdConfidencialidad", OleDbType.Numeric);
                parameter.Value = objActivos.IdConfidencialidad;
                parameters[9] = parameter;
                parameter = new OleDbParameter("@JustificacionConfidencialidad", OleDbType.VarChar);
                parameter.Value = objActivos.JustificacionConfidencialidad;
                parameters[10] = parameter;
                parameter = new OleDbParameter("@IdIntegridad", OleDbType.Numeric);
                parameter.Value = objActivos.IdIntegridad;
                parameters[11] = parameter;
                parameter = new OleDbParameter("@JustificacionIntegridad", OleDbType.VarChar);
                parameter.Value = objActivos.JustificacionIntegridad;
                parameters[12] = parameter;
                parameter = new OleDbParameter("@IdDisponibilidad", OleDbType.Numeric);
                parameter.Value = objActivos.IdDisponibilidad;
                parameters[13] = parameter;
                parameter = new OleDbParameter("@JustificacionDisponibilidad", OleDbType.VarChar);
                parameter.Value = objActivos.JustificacionDisponibilidad;
                parameters[14] = parameter;
                parameter = new OleDbParameter("@Criticidad", OleDbType.VarChar);
                parameter.Value = objActivos.Criticidad;
                parameters[15] = parameter;
                parameter = new OleDbParameter("@IdPropietario", OleDbType.Numeric);
                parameter.Value = objActivos.IdPropietario;
                parameters[16] = parameter;
                parameter = new OleDbParameter("@IdUsuario", OleDbType.Numeric);
                parameter.Value = objActivos.Idusuario;
                parameters[17] = parameter;
                parameter = new OleDbParameter("@IdActivo", OleDbType.Numeric);
                parameter.Value = objActivos.IdActivos;
                parameters[18] = parameter;
                #endregion Creacion Consulta

                cDataBase.conectar();
                cDataBase.ejecutarSPParametros("SGSI.UpdateActivos", parameters);
                cDataBase.desconectar();


                DataTable dtInformacion = new DataTable();
                
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT MAX(IdActivo) AS LastIdActivo FROM [SGSI].[Activos]");
                cDataBase.desconectar();

                int IdActivo = Convert.ToInt32(dtInformacion.Rows[0]["LastIdActivo"].ToString());

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("INSERT INTO SGSI.RiesgosvsActivos (IdRiesgo, IdActivo, IdUsuario, FechaRegistro) " +
                    "VALUES (" + objActivos.IdRiesgo + ", " + IdActivo + ", " + objActivos.Idusuario + ", GETDATE())");
                cDataBase.desconectar();


                //int IdActivo = Convert.ToInt32(dtInformacion.Rows[0]["LastIdActivo"].ToString());

                foreach (int idPremisa in lstPremisas)
                {
                    parameters = new OleDbParameter[3];
                    parameter = new OleDbParameter("@IdActivo", OleDbType.Numeric);
                    parameter.Value = objActivos.IdActivos;
                    parameters[0] = parameter;
                    parameter = new OleDbParameter("@IdPremisa", OleDbType.Numeric);
                    parameter.Value = idPremisa;
                    parameters[1] = parameter;
                    parameter = new OleDbParameter("@IdUsuario", OleDbType.Numeric);
                    parameter.Value = objActivos.Idusuario;
                    parameters[2] = parameter;

                    cDataBase.conectar();
                    cDataBase.ejecutarSPParametros("SGSI.InsertActivosvsPremisas", parameters);
                    cDataBase.desconectar();
                }
                booResult = true;
            }
            catch (Exception ex)
            {
                booResult = false;
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                strErrMsg = string.Format("Error al insertar el activo. [{0}]", ex.Message);
            }


            return booResult;
        }
        public bool mtdEliminarActivos(ref string strErrMsg, int IdActivo)
        {
            DataTable dtInfo = new DataTable();
            Boolean flag = false;
            try
            {
                string query = "DELETE FROM [SGSI].[Activos] WHERE IdActivo = " + IdActivo;

                cDataBase.conectar();
                dtInfo = cDataBase.ejecutarConsulta(query);
                cDataBase.desconectar();

                DataTable dtInformacion = new DataTable();

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("DELETE FROM [SGSI].[ActivosvsPremisas] Where IdActivo = " + IdActivo);
                cDataBase.desconectar();
                flag = true;
            }
            catch (Exception ex)
            {
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                strErrMsg = string.Format("Error al consultar los activos. [{0}]", ex.Message);
                flag = false;
            }
            return flag;
        }
    }
}