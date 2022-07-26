using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnBreak.BD;

namespace OnBreak.BC
{
    public class Cliente
    {
        #region Propiedades
        public string RutCliente { get; set; }
        public string RazonSocial { get; set; }
        public string NombreContacto { get; set; }
        public string MailContacto { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int IdActividadEmpresa { get; set; }
        public int IdTipoEmpresa { get; set; }
        #endregion


        #region Constructor
        public Cliente()
        {
            this.Init();
        }

        private void Init()
        {
            RutCliente = string.Empty;
            RazonSocial = string.Empty;
            NombreContacto = string.Empty;
            MailContacto = string.Empty;
            Direccion = string.Empty;
            Telefono = string.Empty;
            IdActividadEmpresa = 0;
            IdTipoEmpresa = 0;
        }
        #endregion

        #region CRUD
        public bool Create()
        {
            //Crear una conexión al Entities
            BD.OnBreakEntities bd = new BD.OnBreakEntities();
            BD.Cliente cliente = new BD.Cliente();
            try
            {
                //sincronizo el contenido de las propiedades a la BD
                CommonBC.Syncronize(this, cliente);
                bd.Cliente.Add(cliente);
                bd.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                bd.Cliente.Remove(cliente);
                return false;
            }
        }

        public bool Read()
        {
            //Crear una conexión al Entities
            BD.OnBreakEntities bd = new BD.OnBreakEntities();
            try
            {
                //busco por el id el contenido de la entidad
                BD.Cliente cliente =
                    bd.Cliente.First(e => e.RutCliente.Equals(this.RutCliente));
                CommonBC.Syncronize(cliente, this);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update()
        {
            //Crear una conexión al Entities
            BD.OnBreakEntities bd = new BD.OnBreakEntities();

            try
            {
                //busco por el id el contenido de la entidad a modificar
                BD.Cliente cliente =
                    bd.Cliente.First(e => e.RutCliente.Equals(this.RutCliente));
                CommonBC.Syncronize(this, cliente);
                bd.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Delete()
        {
            //Crear una conexión al Entities
            BD.OnBreakEntities bd = new BD.OnBreakEntities();

            try
            {
                //busco por el id el contenido de la entidad a eliminar
                BD.Cliente cliente =
                    bd.Cliente.First(e => e.RutCliente.Equals(this.RutCliente));
                bd.Cliente.Remove(cliente);
                bd.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Customer

        private List<Cliente> GenerarListado(List<BD.Cliente> listaDatos)
        {
            List<Cliente> listaNegocio = new List<Cliente>();
            foreach (BD.Cliente datos in listaDatos)
            {
                Cliente negocio = new Cliente();
                CommonBC.Syncronize(datos, negocio);
                listaNegocio.Add(negocio);
            }
            return listaNegocio;
        }

        public List<Cliente> ReadAll()
        {
            //Crear una conexión al Entities
            BD.OnBreakEntities bd = new BD.OnBreakEntities();
            try
            {
                //Crear una lista de DATOS
                List<BD.Cliente> listaDatos = bd.Cliente.ToList();
                //Crear una lista de NEGOCIO
                List<Cliente> listaNegocio = GenerarListado(listaDatos);
                return listaNegocio;
            }
            catch (Exception)
            {
                return new List<Cliente>();
            }
        }

        public List<Cliente> ReadAllByRut(string rut)
        {
            //Crear una conexión al Entities
            BD.OnBreakEntities bd = new BD.OnBreakEntities();
            try
            {
                //Crear una lista de DATOS
                List<BD.Cliente> listaDatos = bd.Cliente.Where(e => e.RutCliente.Equals(rut)).ToList<BD.Cliente>();
                //Crear una lista de NEGOCIO
                List<Cliente> listaNegocio = GenerarListado(listaDatos);
                return listaNegocio;
            }
            catch (Exception)
            {
                return new List<Cliente>();
            }
        }

        public List<Cliente> ReadOnlyByTipoEmpresa(int tipoempresa)
        {
            BD.OnBreakEntities bd = new BD.OnBreakEntities();
            try
            {
                //Crear una lista de DATOS
                List<BD.Cliente> listaDatos = bd.Cliente.Where(e => e.IdTipoEmpresa == tipoempresa).ToList<BD.Cliente>();
                //Crear una lista de NEGOCIO
                List<Cliente> listaNegocio = GenerarListado(listaDatos);
                return listaNegocio;
            }
            catch (Exception)
            {
                return new List<Cliente>();
            }
        }

        public List<Cliente> ReadOnlyByActividadEmpresa(int actividadempresa)
        {
            BD.OnBreakEntities bd = new BD.OnBreakEntities();
            try
            {
                //Crear una lista de DATOS
                List<BD.Cliente> listaDatos = bd.Cliente.Where(e => e.IdActividadEmpresa == actividadempresa).ToList<BD.Cliente>();
                //Crear una lista de NEGOCIO
                List<Cliente> listaNegocio = GenerarListado(listaDatos);
                return listaNegocio;
            }
            catch (Exception)
            {
                return new List<Cliente>();
            }
        }
        #endregion
    }
}
