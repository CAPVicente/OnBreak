using OnBreak.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreak.BC
{
    public class Cocktail
    {
        #region Propiedades
        public string Numero { get; set; }
        public int IdTipoAmbientacion { get; set; }
        public bool Ambientacion { get; set; }
        public bool MusicaAmbiental { get; set; }
        public bool MusicaCliente { get; set; }
        #endregion

        #region Constructores
        public Cocktail()
        {
            this.Init();
        }
        private void Init()
        {
            Numero = string.Empty;
            IdTipoAmbientacion = 0;
            Ambientacion = false;
            MusicaAmbiental = false;
            MusicaCliente = false;
        }
        #endregion

        #region CRUD
        public bool Create()
        {
            //Crear una conexión al Entities
            BD.OnBreakEntities bd = new BD.OnBreakEntities();
            BD.Cocktail cocktail = new BD.Cocktail();
            try
            {
                //sincronizo el contenido de las propiedades a la BD
                CommonBC.Syncronize(this, cocktail);
                bd.Cocktail.Add(cocktail);
                bd.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                bd.Cocktail.Remove(cocktail);
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
                BD.Cocktail cocktail =
                    bd.Cocktail.First(e => e.Numero.Equals(this.Numero));
                CommonBC.Syncronize(cocktail, this);

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
                BD.Cocktail cocktail =
                    bd.Cocktail.First(e => e.Numero.Equals(this.Numero));
                CommonBC.Syncronize(this, cocktail);
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
                BD.Cocktail cocktail =
                    bd.Cocktail.First(e => e.Numero.Equals(this.Numero));
                bd.Cocktail.Remove(cocktail);
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
