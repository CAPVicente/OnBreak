using OnBreak.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreak.BC
{
    public class Cenas
    {
        #region Propiedades

        public string Numero { get; set; }
        public int IdTipoAmbientacion { get; set; }
        public bool MusicaAmbiental { get; set; }
        public bool LocalOnBreak { get; set; }
        public bool OtroLocalOnBreak { get; set; }
        public double ValorArriendo { get; set; }
        #endregion

        #region Constructores
        public Cenas()
        {
            this.Init();
        }

        private void Init()
        {
            Numero = string.Empty;
            IdTipoAmbientacion = 0;
            MusicaAmbiental = false;
            LocalOnBreak = false;
            OtroLocalOnBreak = false;
            ValorArriendo = 0;
        }
        #endregion

        #region CRUD
        public bool Create()
        {
            //Crear una conexión al Entities
            BD.OnBreakEntities bd = new BD.OnBreakEntities();
            BD.Cenas cenas = new BD.Cenas();
            try
            {
                //sincronizo el contenido de las propiedades a la BD
                CommonBC.Syncronize(this, cenas);
                bd.Cenas.Add(cenas);
                bd.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                bd.Cenas.Remove(cenas);
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
                BD.Cenas cenas =
                    bd.Cenas.First(e => e.Numero.Equals(this.Numero));
                CommonBC.Syncronize(cenas, this);

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
                BD.Cenas cenas =
                    bd.Cenas.First(e => e.Numero.Equals(this.Numero));
                CommonBC.Syncronize(this, cenas);
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
                BD.Cenas cenas =
                    bd.Cenas.First(e => e.Numero.Equals(this.Numero));
                bd.Cenas.Remove(cenas);
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

        private List<Cenas> GenerarListado(List<BD.Cenas> listaDatos)
        {
            List<Cenas> listaNegocio = new List<Cenas>();
            foreach (BD.Cenas datos in listaDatos)
            {
                Cenas negocio = new Cenas();
                CommonBC.Syncronize(datos, negocio);
                listaNegocio.Add(negocio);
            }
            return listaNegocio;
        }

        public List<Cenas> ReadAll()
        {
            //Crear una conexión al Entities
            BD.OnBreakEntities bd = new BD.OnBreakEntities();
            try
            {
                //Crear una lista de DATOS
                List<BD.Cenas> listaDatos = bd.Cenas.ToList();
                //Crear una lista de NEGOCIO
                List<Cenas> listaNegocio = GenerarListado(listaDatos);
                return listaNegocio;
            }
            catch (Exception)
            {
                return new List<Cenas>();
            }
        }

        public List<Cenas> ObtenerValorArriendo(string nro_contrato)
        {
            //Crear una conexión al Entities
            BD.OnBreakEntities bd = new BD.OnBreakEntities();
            try
            {
                //Crear una lista de DATOS
                List<BD.Cenas> listaDatos = bd.Cenas.Where(e => e.Numero.Equals(nro_contrato)).ToList<BD.Cenas>();
                //Crear una lista de NEGOCIO
                List<Cenas> listaNegocio = GenerarListado(listaDatos);
                return listaNegocio;
            }
            catch (Exception)
            {
                return new List<Cenas>();
            }
        }



        #endregion
    }
}
