using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_compi1
{
    class Fila
    {
        public String nombre;
        public List<String> columnas;
        public String tabla;
        public Fila(String n, List<String> c, String t)
        {
            this.nombre = n;
            this.columnas = c;
            this.tabla = t;
        }
    }
}
