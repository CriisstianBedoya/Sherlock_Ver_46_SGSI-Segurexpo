using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using clsDatos;
using clsDTO;
using System.Data.SqlClient;

namespace clsLogica
{
    public class clsPremisas
    {

        // Trae las posiciones donde se guardan estos campos
        readonly string SenalAlertaPosTipoIden = System.Configuration.ConfigurationManager.AppSettings["SenalAlertaPosTipoIden"].ToString();
        readonly string SenalAlertaPosNumeroIden = System.Configuration.ConfigurationManager.AppSettings["SenalAlertaPosNumeroIden"].ToString();
        readonly string SenalAlertaPosNombre = System.Configuration.ConfigurationManager.AppSettings["SenalAlertaPosNombre"].ToString();
        private clsDatos.clsDatabase cDataBase = new clsDatos.clsDatabase();

        /// <summary>
        /// Metodo que consulta todos los perfiles configurados.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        

        public List<clsDTOPremisas> mtdCargarDatos(ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            clsDtPremisas cDtPrermisa = new clsDtPremisas();
            clsDTOPremisas objPremisa = new clsDTOPremisas();
            List<clsDTOPremisas> lstPremisas = new List<clsDTOPremisas>();
            #endregion Vars

            dtInfo = cDtPrermisa.mtdConsultaDatos(ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    #region Recorrido Info
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objPremisa = new clsDTOPremisas(
                            dr["IdPremisa"].ToString().Trim(),
                            dr["Codigo"].ToString().Trim(),
                            dr["Nombre"].ToString().Trim(),
                            dr["Descripcion"].ToString().Trim(),
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim())
                            );

                        lstPremisas.Add(objPremisa);
                    }
                    #endregion Recorrido Info
                }
                else
                {
                    lstPremisas = null;
                    strErrMsg = "No hay información de premisas..";
                }
            }
            else
                lstPremisas = null;
            return lstPremisas;
        }

        public void mtdGuardarPremisa(clsDTOPremisas objPremisa, ref string strErrMsg)
        {
            clsDtPremisas cDtPremisa = new clsDtPremisas();

            cDtPremisa.mtdInsertarPremisa(objPremisa, ref strErrMsg);
        }

        public void mtdActualizarPremisa(clsDTOPremisas objPremisa, ref string strErrMsg)
        {
            clsDtPremisas cDtPremisa = new clsDtPremisas();

            cDtPremisa.mtdActualizarPremisa(objPremisa, ref strErrMsg);
        }

    }
}