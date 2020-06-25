using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_compi1
{
    class Error
    {
        public String tipo;
        public String descripcion;
        public int fila;
        public int columna;

        public Error(String t, String d, int f, int c)
        {
            this.tipo = t;
            this.descripcion = d;
            this.fila = f;
            this.columna = c;
        }
    }
}
