using OnBreak.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreak.BC
{
    public class Contrato
    {
        #region Propiedades
        public string Numero { get; set; }
        public System.DateTime Creacion { get; set; }
        public System.DateTime Termino { get; set; }
        public string RutCliente { get; set; }
        public string IdModalidad { get; set; }
        public int IdTipoEvento { get; set; }
        public System.DateTime? FechaHoraInicio { get; set; }
        public System.DateTime? FechaHoraTermino { get; set; }
        public int Asistentes { get; set; }
        public int PersonalAdicional { get; set; }
        public bool Realizado { get; set; }
        public double ValorTotalContrato { get; set; }
        public string Observaciones { get; set; }
        #endregion

        #region Constructor
        public Contrato()
        {
            this.Init();
        }

        private void Init()
        {
            Numero = string.Empty;
            Creacion = new DateTime();
            Termino = new DateTime();
            RutCliente = string.Empty;
            IdModalidad = string.Empty;
            IdTipoEvento = 0;
            FechaHoraInicio = new DateTime();
            FechaHoraTermino = new DateTime();
            Asistentes = 0;
            PersonalAdicional = 0;
            Realizado = false;
            ValorTotalContrato = 0;
            Observaciones = string.Empty;
        }
        #endregion

        #region CRUD

        public bool Create()
        {
            //Crear una conexión al Entities
            BD.OnBreakEntities bd = new BD.OnBreakEntities();
            BD.Contrato contrato = new BD.Contrato();
            try
            {
                //sincronizo el contenido de las propiedades a la BD
                CommonBC.Syncronize(this, contrato);
                bd.Contrato.Add(contrato);
                bd.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                bd.Contrato.Remove(contrato);
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
                BD.Contrato contrato =
                    bd.Contrato.First(e => e.Numero.Equals(this.Numero));
                CommonBC.Syncronize(contrato, this);

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
                BD.Contrato contrato =
                    bd.Contrato.First(e => e.Numero.Equals(this.Numero));
                CommonBC.Syncronize(this, contrato);
                bd.SaveChanges();
                return true;
            }
            catch (Exception ex)
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
                BD.Contrato contrato =
                    bd.Contrato.First(e => e.Numero.Equals(this.Numero));
                bd.Contrato.Remove(contrato);
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
        public List<Contrato> ReadAll()
        {
            //Crear una conexión al Entities
            BD.OnBreakEntities bd = new BD.OnBreakEntities();
            try
            {
                //Crear una lista de DATOS
                List<BD.Contrato> listaDatos = bd.Contrato.ToList();
                //Crear una lista de NEGOCIO
                List<Contrato> listaNegocio = GenerarListado(listaDatos);
                return listaNegocio;
            }
            catch (Exception)
            {
                return new List<Contrato>();
            }
        }

        private List<Contrato> GenerarListado(List<BD.Contrato> listaDatos)
        {
            List<Contrato> listaNegocio = new List<Contrato>();
            foreach (BD.Contrato datos in listaDatos)
            {
                Contrato negocio = new Contrato();
                CommonBC.Syncronize(datos, negocio);
                listaNegocio.Add(negocio);
            }
            return listaNegocio;
        }
        #endregion

        public List<Contrato> ReadAllByNumber(string numero)
        {
            BD.OnBreakEntities bd = new BD.OnBreakEntities();
            try
            {
                //Crear una lista de DATOS
                List<BD.Contrato> listaDatos = bd.Contrato.Where(e => e.Numero.Equals(numero)).ToList<BD.Contrato>();
                //Crear una lista de NEGOCIO
                List<Contrato> listaNegocio = GenerarListado(listaDatos);
                return listaNegocio;
            }
            catch (Exception)
            {
                return new List<Contrato>();
            }
        }

        public List<Contrato> ReadAllByRutContrato(string rut)
        {
            BD.OnBreakEntities bd = new BD.OnBreakEntities();
            try
            {
                //Crear una lista de DATOS
                List<BD.Contrato> listaDatos = bd.Contrato.Where(e => e.RutCliente.Equals(rut)).ToList<BD.Contrato>();
                //Crear una lista de NEGOCIO
                List<Contrato> listaNegocio = GenerarListado(listaDatos);
                return listaNegocio;
            }
            catch (Exception)
            {
                return new List<Contrato>();
            }
        }

        public List<Contrato> ReadAllByTipoEvento(int idtipoevento)
        {
            BD.OnBreakEntities bd = new BD.OnBreakEntities();
            try
            {
                //Crear una lista de DATOS
                List<BD.Contrato> listaDatos = bd.Contrato.Where(e => e.IdTipoEvento == idtipoevento).ToList<BD.Contrato>();
                //Crear una lista de NEGOCIO
                List<Contrato> listaNegocio = GenerarListado(listaDatos);
                return listaNegocio;
            }
            catch (Exception)
            {
                return new List<Contrato>();
            }
        }

        public List<Contrato> ReadAllByModalidadServicio(string idmodalidad)
        {
            BD.OnBreakEntities bd = new BD.OnBreakEntities();
            try
            {
                //Crear una lista de DATOS
                List<BD.Contrato> listaDatos = bd.Contrato.Where(e => e.IdModalidad.Equals(idmodalidad)).ToList<BD.Contrato>();
                //Crear una lista de NEGOCIO
                List<Contrato> listaNegocio = GenerarListado(listaDatos);
                return listaNegocio;
            }
            catch (Exception)
            {
                return new List<Contrato>();
            }
        }
    }
}
