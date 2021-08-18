using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ListasSarlaft.Classes.DAL.SGSI;
using ListasSarlaft.Classes.DTO.SGSI;
namespace ListasSarlaft.Classes.BLL.SGSI
{
    public class clsActivosBLL
    {
        /// <summary>
        /// Metodo para insertar el registro de Activos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarActivos(ActivosDTO objActivos, ref string strErrMsg, List<int> lstPremisas)
        {
            bool booResult = false;
            clsActivosDAL clsData = new clsActivosDAL();

            booResult = clsData.mtdInsertarActivos(objActivos, ref strErrMsg, lstPremisas);

            return booResult;
        }
        /// <summary>
        /// Metodo para consultar y visualizar las Categorias de las Variables de Calificación Control
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de usuarios de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public void mtdConsultarActivos(ref List<ActivosDTO> lstActivos, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsActivosDAL cDtRegistro = new clsActivosDAL();
            #endregion Vars

            dtInfo = cDtRegistro.mtdConsultaActivos(strErrMsg);
            try
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            ActivosDTO objActivos = new ActivosDTO();
                            objActivos.IdActivos = Convert.ToInt32(dr["IdActivo"].ToString().Trim());
                            objActivos.NombreActivo = dr["NombreActivo"].ToString().Trim();
                            objActivos.IdTipoActivo = Convert.ToInt32(dr["IdTipoActivo"].ToString().Trim());
                            objActivos.TipoActivo = dr["NombreTipoActivo"].ToString();
                            objActivos.IdClasificacionActivo = Convert.ToInt32(dr["IdClasificacionActivo"].ToString().Trim());
                            objActivos.ClasificacionActivo = dr["NombreClasificacionActivo"].ToString();
                            objActivos.Descripcion = dr["Descripcion"].ToString().Trim();
                            objActivos.IdCadenaValor = Convert.ToInt32(dr["IdCadenaValor"].ToString().Trim());
                            objActivos.CadenaValor = dr["NombreCadenaValor"].ToString().Trim();
                            objActivos.IdMacroProceso = Convert.ToInt32(dr["IdMacroProceso"].ToString().Trim());
                            objActivos.MacroProceso = dr["NombreMacro"].ToString().Trim();
                            objActivos.IdProceso = Convert.ToInt32(dr["IdProceso"].ToString().Trim());
                            objActivos.Proceso = dr["NombreProceso"].ToString().Trim();
                            objActivos.IdSubProceso = Convert.ToInt32(dr["IdSubProceso"].ToString().Trim());
                            objActivos.SubProceso = dr["NombreSubproceso"].ToString().Trim();
                            objActivos.Ubicacion = dr["Ubicacion"].ToString().Trim();
                            objActivos.IdConfidencialidad = Convert.ToInt32(dr["IdConfidencialidad"].ToString().Trim());
                            objActivos.JustificacionConfidencialidad = dr["JustificacionConfidencialidad"].ToString();
                            objActivos.IdIntegridad = Convert.ToInt32(dr["IdIntegridad"].ToString().Trim());
                            objActivos.JustificacionIntegridad = dr["JustificacionIntegridad"].ToString();
                            objActivos.IdDisponibilidad = Convert.ToInt32(dr["IdDisponibilidad"].ToString().Trim());
                            objActivos.JustificacionDisponibilidad = dr["JustificacionDisponibilidad"].ToString();
                            objActivos.Criticidad = dr["Criticidad"].ToString();
                            objActivos.IdPropietario = Convert.ToInt32(dr["IdPropietario"].ToString());
                            objActivos.Propietario = dr["Propietario"].ToString();
                            objActivos.Idusuario = Convert.ToInt32(dr["IdUsuario"].ToString().Trim());
                            objActivos.Usuario = dr["Usuario"].ToString().Trim();

                            lstActivos.Add(objActivos);
                        }
                    }
                    else
                        lstActivos = null;
                }
                else
                    lstActivos = null;
            }
            catch (Exception ex)
            {
                strErrMsg = "Error en la consulta de Activos: " + ex.Message;
                lstActivos = null;
            }
        }
        public List<int> mtdConsultaActivoVsPremisas(ref string strErrMsg, int IdActivo)
        {
            #region Vars
            List<int> lstPremisas = new List<int>();
            DataTable dtInfo = new DataTable();
            clsActivosDAL cDtRegistro = new clsActivosDAL();
            #endregion Vars
            dtInfo = cDtRegistro.mtdConsultaActivosvsPremisas(ref strErrMsg, IdActivo);
            try
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            lstPremisas.Add(Convert.ToInt32(dr["IdPremisa"].ToString()));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strErrMsg = "Error en la consulta de premisas por activo: " +
                    ex.Message;
                lstPremisas = new List<int>();
            }

            return lstPremisas;
        }
        /// <summary>
        /// Metodo para Modificar registro de activos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarActivos(ActivosDTO objActivos, ref string strErrMsg, List<int> lstPremisas)
        {
            bool booResult = false;
            clsActivosDAL clsData = new clsActivosDAL();

            booResult = clsData.mtdActualizarActivos(objActivos, ref strErrMsg, lstPremisas);

            return booResult;
        }
        /// <summary>
        /// Metodo para Eliminar registro de activos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdEliminarActivos(ref string strErrMsg, int IdActivo)
        {
            bool booResult = false;
            clsActivosDAL clsData = new clsActivosDAL();

            booResult = clsData.mtdEliminarActivos(ref strErrMsg, IdActivo);

            return booResult;
        }
    }
}