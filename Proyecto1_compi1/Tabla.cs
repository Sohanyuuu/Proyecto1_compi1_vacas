using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_compi1
{
    class Tabla
    {
        public String nombre;
        public List<Fila> filas;

        public Tabla(String n, List<Fila> f)
        {
            this.nombre = n;
            this.filas = f;
        }
    }
}
