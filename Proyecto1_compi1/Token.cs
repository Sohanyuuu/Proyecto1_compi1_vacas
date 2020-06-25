using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_compi1
{
    class Token
    {
        public int no;
        public String tipo;
        public String lexema;
        public int fila;
        public int columna;

        public Token(int no, String tipo, String lexema, int fila, int columna)
        {
            this.no = no;
            this.tipo = tipo;
            this.lexema = lexema;
            this.fila = fila;
            this.columna = columna;
        }
        
        /*public int getno()
        {
            return no;
        }

        public String gettipo()
        {
            return tipo;
        }
        public String getlexema()
        {
            return lexema;
        }
        public int getfila()
        {
            return fila;
        }
        public int getcolumna()
        {
            return columna;
        }*/

    }
}
