using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreak.BC
{
    public class TipoAmbientacion
    {
        #region Propiedades
        public int IdTipoAmbientacion { get; set; }
        public string Descripcion { get; set; }
        #endregion

        #region Constructor
        public TipoAmbientacion()
        {
            this.Init();
        }

        private void Init()
        {
            IdTipoAmbientacion = 0;
            Descripcion = string.Empty;
        }
        #endregion

        #region Customer
        public List<TipoAmbientacion> ReadAll()
        {
            //Crear una conexión al Entities
            BD.OnBreakEntities bd = new BD.OnBreakEntities();
            try
            {
                //Crear una lista de DATOS
                List<BD.TipoAmbientacion> listaDatos = bd.TipoAmbientacion.ToList();
                //Crear una lista de NEGOCIO
                List<TipoAmbientacion> listaNegocio = GenerarListado(listaDatos);
                return listaNegocio;
            }
            catch (Exception)
            {
                return new List<TipoAmbientacion>();
            }
        }

        private List<TipoAmbientacion> GenerarListado(List<BD.TipoAmbientacion> listaDatos)
        {
            List<TipoAmbientacion> listaNegocio = new List<TipoAmbientacion>();
            foreach (BD.TipoAmbientacion datos in listaDatos)
            {
                TipoAmbientacion negocio = new TipoAmbientacion();
                CommonBC.Syncronize(datos, negocio);
                listaNegocio.Add(negocio);
            }
            return listaNegocio;
        }
        #endregion
    }
}
