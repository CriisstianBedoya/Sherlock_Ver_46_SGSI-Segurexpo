using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clsDTO
{
    public class clsDTOPremisas
    {
        private string iDPremisa;
        private string codigo;
        private string nombre;
        private string descripcion;
        private int idusuario;
        public string IdPremisa
        {
            get { return iDPremisa; }
            set { iDPremisa = value; }
        }
        public string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        public int IdUsuario
        {
            get { return idusuario; }
            set { idusuario = value; }
        }
        public clsDTOPremisas(string iDPremisa, string codigo, string nombre, string descripcion, int idusuario)
        {
            this.IdPremisa = iDPremisa;
            this.Codigo = codigo;
            this.Nombre = nombre;
            this.Descripcion = descripcion;
            this.IdUsuario = idusuario;
        }
        public clsDTOPremisas(string iDPremisa, string codigo, string nombre, string descripcion)
        {
            this.IdPremisa = iDPremisa;
            this.Codigo = codigo;
            this.Nombre = nombre;
            this.Descripcion = descripcion;
        }
        public clsDTOPremisas()
        {
        }

    }
}
