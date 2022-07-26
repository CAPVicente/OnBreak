using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnBreak.BD;

namespace OnBreak.BC
{
     public class TipoEmpresa
    {
        #region Propiedades
        public int IdTipoEmpresa { get; set; }
        public string Descripcion { get; set; }
        #endregion

        #region Constructor
        public TipoEmpresa()
        {
            this.Init();
        }

        private void Init()
        {
            IdTipoEmpresa = 0;
            Descripcion = string.Empty;
        }

        #endregion

        #region CRUD
        public bool Create()
        {
            //Crear una conexión al Entities
            BD.OnBreakEntities bd = new BD.OnBreakEntities();
            BD.TipoEmpresa tipoEmpresa = new BD.TipoEmpresa();
            try
            {
                //sincronizo el contenido de las propiedades a la BD
                CommonBC.Syncronize(this, tipoEmpresa);
                bd.TipoEmpresa.Add(tipoEmpresa);
                bd.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                bd.TipoEmpresa.Remove(tipoEmpresa);
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
                BD.TipoEmpresa tipoEmpresa =
                    bd.TipoEmpresa.First(e => e.IdTipoEmpresa.Equals(this.IdTipoEmpresa));
                CommonBC.Syncronize(tipoEmpresa, this);

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
                BD.TipoEmpresa tipoEmpresa =
                    bd.TipoEmpresa.First(e => e.IdTipoEmpresa.Equals(this.IdTipoEmpresa));
                CommonBC.Syncronize(this, tipoEmpresa);
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
                BD.TipoEmpresa tipoEmpresa =
                    bd.TipoEmpresa.First(e => e.IdTipoEmpresa.Equals(this.IdTipoEmpresa));
                bd.TipoEmpresa.Remove(tipoEmpresa);
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
        public List<TipoEmpresa> ReadAll()
        {
            //Crear una conexión al Entities
            BD.OnBreakEntities bd = new BD.OnBreakEntities();
            try
            {
                //Crear una lista de DATOS
                List<BD.TipoEmpresa> listaDatos = bd.TipoEmpresa.ToList();
                //Crear una lista de NEGOCIO
                List<TipoEmpresa> listaNegocio = GenerarListado(listaDatos);
                return listaNegocio;
            }
            catch (Exception)
            {
                return new List<TipoEmpresa>();
            }
        }

        private List<TipoEmpresa> GenerarListado(List<BD.TipoEmpresa> listaDatos)
        {
            List<TipoEmpresa> listaNegocio = new List<TipoEmpresa>();
            foreach (BD.TipoEmpresa datos in listaDatos)
            {
                TipoEmpresa negocio = new TipoEmpresa();
                CommonBC.Syncronize(datos, negocio);
                listaNegocio.Add(negocio);
            }
            return listaNegocio;
        }
        #endregion
    }
}
