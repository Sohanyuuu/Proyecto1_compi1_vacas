using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_compi1
{
    class Nodo
    {
        public String nombre;
        public List<Nodo> lista;

        public Nodo(String n, List<Nodo> l)
        {
            this.nombre = n;
            this.lista = l;
        }
        

    }
}
