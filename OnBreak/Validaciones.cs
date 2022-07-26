using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreak
{
    public static class Validaciones
    {
        public static int ObtenerRutParaBusqueda(string rut)
        {
            if (string.IsNullOrEmpty(rut)) return 0;

            if (string.IsNullOrWhiteSpace(rut)) return 0;

            if (!int.TryParse(rut, out int numero))
            {
                return -1;
            }
            if (numero < 1)
            {
                return -1;
            }
            return Convert.ToInt32(rut);
        }
    }
}
