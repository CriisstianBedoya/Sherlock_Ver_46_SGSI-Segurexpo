using System;
using System.Data;
using System.Data.OleDb;

namespace ListasSarlaft.Classes
{
    public class cActivos : cPropiedades
    {
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
#pragma warning disable CS0169 // El campo 'cActivos.parameters' nunca se usa
        private OleDbParameter[] parameters;
#pragma warning restore CS0169 // El campo 'cActivos.parameters' nunca se usa
#pragma warning disable CS0169 // El campo 'cActivos.parameter' nunca se usa
        private OleDbParameter parameter;
#pragma warning restore CS0169 // El campo 'cActivos.parameter' nunca se usa
        private cEncriptacion cEncrypt = new cEncriptacion();
        

        public void actualizarTipoActivo(string NombreTipoActivo, string IdTipoActivo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE SGSI.tipoActivo SET NombreTipoActivo = N'"+ NombreTipoActivo +"' WHERE IdTipoActivo = "+ IdTipoActivo +"");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
            }
        }

        public void agregarTipoActivo(string NombreTipoActivo, string IdUsuario)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT SGSI.tipoActivo ([NombreTipoActivo],[IdUsuario],[FechaRegistro]) VALUES ('"+ NombreTipoActivo +"',"+IdUsuario+",GETDATE())");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
            }
        }

        public void agregarClasificacionActivo(string NombreClasificacionActivo, string IdUsuario)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT SGSI.ClasificacionActivo ([NombreClasificacionActivo],[IdUsuario],[FechaRegistro]) " +
                    "VALUES ('" + NombreClasificacionActivo + "'," + IdUsuario + ",GETDATE())");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
            }
        }

        public void actualizarClasificacionActivo(string NombreClasificacionActivo, string IdClasificacionActivo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE SGSI.ClasificacionActivo SET NombreClasificacionActivo = N'" + NombreClasificacionActivo + "' " +
                    "WHERE IdClasificacionActivo = " + IdClasificacionActivo + "");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
            }
        }

        public DataTable TipoActivosVer()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT * FROM SGSI.tipoActivo");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable ClasificacionActivosVer()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT * FROM SGSI.ClasificacionActivo");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable RiesgosAsocVer()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("select * from Riesgos.Riesgo");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                dtInformacion.Rows.Clear();
                dtInformacion.Columns.Clear();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public void eliminarTipoActivo(string IdTipoActivo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("DELETE FROM SGSI.tipoActivo WHERE IdTipoActivo = " + IdTipoActivo + "");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
            }
        }


        public void updateUser(String IdRol, String NumeroDocumento, String Nombres, String Apellidos, String Usuario, 
            String Contrasena, String Bloqueado, String UsuarioId, String IdJerarquia)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Listas.Usuarios SET IdRol = " + IdRol.Trim() + ", NumeroDocumento = N'" + NumeroDocumento.Trim() + 
                    "', Nombres = N'" + Nombres.Trim() + "', Apellidos = N'" + Apellidos.Trim() + "', Usuario = N'" + Usuario.Trim() + 
                    "', Contrasena = N'" + Contrasena.Trim() + "', Bloqueado = " + Bloqueado.Trim() + ", IdJerarquia = " + IdJerarquia + 
                    ", FechaUltActualPassword = GETDATE() WHERE (IdUsuario = " + UsuarioId.ToString() + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
        }
        
        public void actualizarPermisos(String Consultar, String Agregar, String Actualizar, String Borrar, String IdPermiso)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Listas.Permisos SET Consultar = " + Consultar + ", Agregar = " + Agregar + ", Actualizar = " + Actualizar + ", Borrar = " + Borrar + " WHERE (IdPermiso = " + IdPermiso + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
            }
        }
    }
}