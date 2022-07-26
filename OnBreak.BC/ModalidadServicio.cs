using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreak.BC
{
    public class ModalidadServicio
    {
        #region Propiedades
        public string IdModalidad { get; set; }
        public int IdTipoEvento { get; set; }
        public string Nombre { get; set; }
        public double ValorBase { get; set; }
        public int PersonalBase { get; set; }
        #endregion

        #region Constructor
        public ModalidadServicio()
        {
            this.Init();
        }

        private void Init()
        {
            IdModalidad = string.Empty;
            IdTipoEvento = 0;
            Nombre = string.Empty;
            ValorBase = 0;
            PersonalBase = 0;

        }
        #endregion

        #region CRUD
        public bool Create()
        {
            //Crear una conexión al Entities
            BD.OnBreakEntities bd = new BD.OnBreakEntities();
            BD.ModalidadServicio modalidad = new BD.ModalidadServicio();
            try
            {
                //sincronizo el contenido de las propiedades a la BD
                CommonBC.Syncronize(this, modalidad);
                bd.ModalidadServicio.Add(modalidad);
                bd.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                bd.ModalidadServicio.Remove(modalidad);
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
                BD.ModalidadServicio modalidad =
                    bd.ModalidadServicio.First(e => e.IdModalidad.Equals(this.IdModalidad));
                CommonBC.Syncronize(modalidad, this);

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
                BD.ModalidadServicio modalidad =
                    bd.ModalidadServicio.First(e => e.IdModalidad.Equals(this.IdModalidad));
                CommonBC.Syncronize(this, modalidad);
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
                BD.ModalidadServicio modalidad =
                    bd.ModalidadServicio.First(e => e.IdModalidad.Equals(this.IdModalidad));
                bd.ModalidadServicio.Remove(modalidad);
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
        public double RetornaValorBase(string id)
        {
            double valorBase = 0;
            BD.OnBreakEntities bd = new BD.OnBreakEntities();
            try
            {
                var resultado =bd.ModalidadServicio.Where(i => i.IdModalidad.Equals(id));
                //Crear una lista de NEGOCIO
                var _res = GenerarListado(resultado.ToList());
               
                return _res[0].ValorBase;
            }
            catch (Exception ex)
            {
                return 0;
            }
            
        }
        public List<ModalidadServicio> ReadAll()
        {
            //Crear una conexión al Entities
            BD.OnBreakEntities bd = new BD.OnBreakEntities();
            try
            {
                //Crear una lista de DATOS
                List<BD.ModalidadServicio> listaDatos = bd.ModalidadServicio.ToList();
                //Crear una lista de NEGOCIO
                List<ModalidadServicio> listaNegocio = GenerarListado(listaDatos);
                return listaNegocio;
            }
            catch (Exception)
            {
                return new List<ModalidadServicio>();
            }
        }

        private List<ModalidadServicio> GenerarListado(List<BD.ModalidadServicio> listaDatos)
        {
            List<ModalidadServicio> listaNegocio = new List<ModalidadServicio>();
            foreach (BD.ModalidadServicio datos in listaDatos)
            {
                ModalidadServicio negocio = new ModalidadServicio();
                CommonBC.Syncronize(datos, negocio);
                listaNegocio.Add(negocio);
            }
            return listaNegocio;
        }

        public List<ModalidadServicio> ReadOnlyByTipoEvento(int idtipoevento)
        {
            BD.OnBreakEntities bd = new BD.OnBreakEntities();
            try
            {
                //Crear una lista de DATOS (esto es un select where)
                List<BD.ModalidadServicio> listaDatos = bd.ModalidadServicio.Where(e => e.IdTipoEvento == idtipoevento).ToList<BD.ModalidadServicio>();
                //Crear una lista de NEGOCIO
                List<ModalidadServicio> listaNegocio = GenerarListado(listaDatos);
                return listaNegocio;
            }
            catch (Exception)
            {
                return new List<ModalidadServicio>();
            }
        }

        #endregion

    }
}
