using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes.DTO.SGSI
{
    public class ActivosDTO
    {
        private int idActivos;
        private string nombreActivo;
        private int idTipoActivo;
        private string tipoActivo;
        private int idClasificacionActivo;
        private string clasificacionActivo;
        private string descripcion;
        private int idCadenaValor;
        private string cadenaValor;
        private int idMacroProceso;
        private string macroProceso;
        private int idProceso;
        private string proceso;
        private int idSubProceso;
        private string subProceso;
        private string ubicacion;
        private int idConfidencialidad;
        private string justificacionConfidencialidad;
        private int idIntegridad;
        private string justificacionIntegridad;
        private int idDisponibilidad;
        private string justificacionDisponibilidad;
        private string criticidad;
        private int idPropietario;
        private string propietario;
        private int idRiesgo;
        private string riesgo;
        private int idusuario;
        private string usuario;
        public int IdActivos
        {
            get { return idActivos; }
            set { idActivos = value; }
        }
        public string NombreActivo
        {
            get { return nombreActivo; }
            set { nombreActivo = value; }
        }
        public int IdTipoActivo
        {
            get { return idTipoActivo; }
            set { idTipoActivo = value; }
        }

        public string TipoActivo
        {
            get { return tipoActivo; }
            set { tipoActivo = value; }
        }

        public int IdClasificacionActivo
        {
            get { return idClasificacionActivo; }
            set { idClasificacionActivo = value; }
        }

        public string ClasificacionActivo
        {
            get { return clasificacionActivo; }
            set { clasificacionActivo = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        public int IdCadenaValor
        {
            get { return idCadenaValor; }
            set { idCadenaValor = value; }
        }
        public string CadenaValor
        {
            get { return cadenaValor; }
            set { cadenaValor = value; }
        }
        public int IdMacroProceso
        {
            get { return idMacroProceso; }
            set { idMacroProceso = value; }
        }
        public string MacroProceso
        {
            get { return macroProceso; }
            set { macroProceso = value; }
        }
        public int IdProceso
        {
            get { return idProceso; }
            set { idProceso = value; }
        }
        public string Proceso
        {
            get { return proceso; }
            set { proceso = value; }
        }
        public int IdSubProceso
        {
            get { return idSubProceso; }
            set { idSubProceso = value; }
        }
        public string SubProceso
        {
            get { return subProceso; }
            set { subProceso = value; }
        }
        public string Ubicacion
        {
            get { return ubicacion; }
            set { ubicacion = value; }
        }
        public int IdConfidencialidad
        {
            get { return idConfidencialidad; }
            set { idConfidencialidad = value; }
        }
        public string JustificacionConfidencialidad
        {
            get { return justificacionConfidencialidad; }
            set { justificacionConfidencialidad = value; }
        }
        public int IdIntegridad
        {
            get { return idIntegridad; }
            set { idIntegridad = value; }
        }
        public string JustificacionIntegridad
        {
            get { return justificacionIntegridad; }
            set { justificacionIntegridad = value; }
        }
        public int IdDisponibilidad
        {
            get { return idDisponibilidad; }
            set { idDisponibilidad = value; }
        }
        public string JustificacionDisponibilidad
        {
            get { return justificacionDisponibilidad; }
            set { justificacionDisponibilidad = value; }
        }
        public string Criticidad
        {
            get { return criticidad; }
            set { criticidad = value; }
        }
        public int IdPropietario
        {
            get { return idPropietario; }
            set { idPropietario = value; }
        }
        public string Propietario
        {
            get { return propietario; }
            set { propietario = value; }
        }
        public int Idusuario
        {
            get { return idusuario; }
            set { idusuario = value; }
        }
        public string Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        public int IdRiesgo
        {
            get { return idRiesgo; }
            set { idRiesgo = value; }
        }
        public string Riesgo
        {
            get { return riesgo; }
            set { riesgo = value; }
        }
        public ActivosDTO()
        {
        }
    }
}