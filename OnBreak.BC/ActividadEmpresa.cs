using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnBreak.BD;


namespace OnBreak.BC
{
    public class ActividadEmpresa
    {
        #region Propiedades
        public int IdActividadEmpresa { get; set; }
        public string Descripcion { get; set; }
        #endregion

        #region Constructor
        public ActividadEmpresa()
        {
            this.Init();

        }

        private void Init()
        {
            IdActividadEmpresa = 0;
            Descripcion = string.Empty;
        }
        #endregion

        #region Customer
        public List<ActividadEmpresa> ReadAll()
        {
            //Crear una conexión al Entities
            BD.OnBreakEntities bd = new BD.OnBreakEntities();
            try
            {
                //Crear una lista de DATOS
                List<BD.ActividadEmpresa> listaDatos = bd.ActividadEmpresa.ToList();
                //Crear una lista de NEGOCIO
                List<ActividadEmpresa> listaNegocio = GenerarListado(listaDatos);
                return listaNegocio;
            }
            catch (Exception)
            {
                return new List<ActividadEmpresa>();
            }
        }

        private List<ActividadEmpresa> GenerarListado(List<BD.ActividadEmpresa> listaDatos)
        {
            List<ActividadEmpresa> listaNegocio = new List<ActividadEmpresa>();
            foreach (BD.ActividadEmpresa datos in listaDatos)
            {
                ActividadEmpresa negocio = new ActividadEmpresa();
                CommonBC.Syncronize(datos, negocio);
                listaNegocio.Add(negocio);
            }
            return listaNegocio;
        }
        #endregion

        #region CRUD

        public bool Create()
        {
            //Crear una conexión al Entities
            BD.OnBreakEntities bd = new BD.OnBreakEntities();
            BD.ActividadEmpresa actividadEmpresa = new BD.ActividadEmpresa();
            try
            {
                //sincronizo el contenido de las propiedades a la BD
                CommonBC.Syncronize(this, actividadEmpresa);
                bd.ActividadEmpresa.Add(actividadEmpresa);
                bd.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                bd.ActividadEmpresa.Remove(actividadEmpresa);
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
                BD.ActividadEmpresa actividadEmpresa =
                    bd.ActividadEmpresa.First(e => e.IdActividadEmpresa.Equals(this.IdActividadEmpresa));
                CommonBC.Syncronize(actividadEmpresa, this);

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
                BD.ActividadEmpresa actividadEmpresa =
                    bd.ActividadEmpresa.First(e => e.IdActividadEmpresa.Equals(this.IdActividadEmpresa));
                CommonBC.Syncronize(this, actividadEmpresa);
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
                BD.ActividadEmpresa actividadEmpresa =
                    bd.ActividadEmpresa.First(e => e.IdActividadEmpresa.Equals(this.IdActividadEmpresa));
                bd.ActividadEmpresa.Remove(actividadEmpresa);
                bd.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
    }
}
