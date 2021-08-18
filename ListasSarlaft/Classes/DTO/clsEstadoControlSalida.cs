﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsEstadoControlSalida
    {
        private int _Id;
        private string _Nombre;
        private int _IdUsuario;
        private DateTime _FechaRegistro;

        public int intId
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public string strNombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        public int intIdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }

        public DateTime dtFechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }

        #region Constructors
        public clsEstadoControlSalida()
        {
        }

        public clsEstadoControlSalida(int intId, string strNombre, int intIdUsuario, DateTime dtFechaRegistro)
        {
            this.intId = intId;
            this.strNombre = strNombre;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }
        #endregion
    }
}