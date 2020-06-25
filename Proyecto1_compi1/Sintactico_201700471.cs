using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_compi1
{

    class Sintactico_201700471
    {
        private List<Token> tokens;
        Token tokenactual;
        int contadortoken=0;
        int contadorlista = 0;
        private List<Nodo> arbol = new List<Nodo>();
        List<Error> erroressintacticos;

        public Sintactico_201700471(List<Token> t)
        {
            this.tokens = t;
        }

        public List<Error> GetErroressintacticos()
        {
            return erroressintacticos;
        }

        public void analizar()
        {
            Arbol arbol = new Arbol();
            erroressintacticos = new List<Error>();
            arbol.Analisis_sintactico(tokens);
            arbol.crear_arbol();
            contadortoken = 0;
            tokenactual = tokens[contadortoken];
            quitarcomentarios(0);
            inicio();
        }

        public void quitarcomentarios(int j)
        {
            for (int i = j; i<tokens.Count; i++)
            {
                if (tokens[i].tipo == "comentario")
                {
                    tokens.RemoveAt(i);
                    quitarcomentarios(i-1);
                }
            }
        }
        public void inicio()
        {
            try
            {
                if (tokenactual.tipo == "reservada crear")
                {
                    crear();
                }
                else if (tokenactual.tipo == "reservada insertar")
                {
                    insertar();
                }
                else if (tokenactual.tipo == "reservada actualizar")
                {
                    actualizar();
                }
                else if (tokenactual.tipo == "reservada eliminar")
                {
                    eliminar();
                }
                else if (tokenactual.tipo == "reservada seleccionar")
                {
                    seleccionar();
                }
                else if (tokenactual.tipo == "punto y coma")
                {
                    contadortoken++;
                    tokenactual = tokens[contadortoken];
                }
                else
                {
                    Console.WriteLine(tokenactual.lexema + "es el error");
                    erroressintacticos.Add(new Error("Sintactico", "Se esperaba la palabra reservada: crear, seleccionar, eliminar, actualizar o modificar", tokenactual.fila, tokenactual.columna));
                }
            }
            catch (Exception o)
            {

            }
            
        }

        public void seleccionar()
        {
            comparar("reservada seleccionar");
            if (tokenactual.tipo == "asterisco")
            {
                comparar("asterisco");
            }
            else
            {
                tablasseleccion();
            }

            if (tokenactual.tipo == "reservada de")
            {
                comparar("reservada de");
                de();
            }
            if (tokenactual.tipo == "reservada donde")
            {
                donde();
            }

            comparar("punto y coma");
            inicio();
        }
        public void de()
        {
            comparar("id");
            if (tokenactual.tipo=="coma")
            {
                comparar("coma");
                de();
            }
        }
        public void tablasseleccion()
        {
            comparar("id");
            comparar("punto");
            comparar("id");
            comparar("reservada como");
            comparar("id");

            if (tokenactual.tipo=="coma")
            {
                comparar("coma");
                tablasseleccion();
            }
            else
            {

            }
        }
        public void eliminar()
        {
            comparar("reservada eliminar");
            comparar("reservada de");
            comparar("id");
            if (tokenactual.tipo == "reservada donde")
            {
                donde();
            }
            comparar("punto y coma");
            inicio();
        }
        public void crear()
        {
            comparar("reservada crear");
            comparar("reservada tabla");
            comparar("id");
            comparar("parentesis a");
            declaracion();
            comparar("parentesis c");
            comparar("punto y coma");
            inicio();
        }

        public void insertar()
        {
            comparar("reservada insertar");
            comparar("reservada en");
            comparar("id");
            comparar("reservada valores");
            comparar("parentesis a");
            tipo_insertar();
            comparar("parentesis c");
            comparar("punto y coma");
            inicio();
        }
        public void declaracion()
        {
            comparar("id");
            tipo();
            if (tokenactual.tipo=="coma")
            {
                comparar("coma");
                declaracion();
            }

        }
        
        public void actualizar()
        {
            comparar("reservada actualizar");
            comparar("id");
            comparar("reservada establecer");
            comparar("parentesis a");
            contenido_actualizar();
            comparar("parentesis c");
            donde();
            comparar("punto y coma");
            inicio();
        }

        public void donde()
        {
            comparar("reservada donde");
            condicion();
        }

        public void condicion()
        {

            comparar("id");

            if (tokenactual.tipo=="punto")
            {
                comparar("punto");
                comparar("id");
            }
            comparador();

            if (tokenactual.tipo == "id")
            {
                comparar("id");
                if (tokenactual.tipo == "punto")
                {
                    comparar("punto");
                    comparar("id");
                }
            }
            else
            {
                tipovariable();
            }
            
            if (tokenactual.tipo == "reservada y")
            {
                comparar("reservada y");
                condicion();
            }
            else if (tokenactual.tipo == "reservada o")
            {
                comparar("reservada o");
                condicion();
            }
            else
            {

            }

        }

        public void comparador()
        {
            if (tokenactual.tipo == "diferente")
            {
                comparar("diferente");
            }
            else if (tokenactual.tipo == "mayor o igual")
            {
                comparar("mayor o igual");
            }
            else if (tokenactual.tipo == "menor o igual")
            {
                comparar("menor o igual");
            }
            else if (tokenactual.tipo == "igual")
            {
                comparar("igual");
            }
            else if (tokenactual.tipo == "menor")
            {
                comparar("menor");
            }
            else if (tokenactual.tipo == "mayor")
            {
                comparar("mayor");
            }
            else
            {
                Console.WriteLine("error sintactico ese esperaba ->" + "!=, <=, >= o =");
                erroressintacticos.Add(new Error("Sintactico", "Se esperaba :  !=, <=, >= o = y se obtuvo :  "+tokenactual.tipo, tokenactual.fila, tokenactual.columna));
                recuperar("punto y coma");
            }
            
        }
        public void contenido_actualizar()
        {
            comparar("id");
            comparar("igual");
            tipo_actualizar();
        }

        public void tipo_actualizar()
        {
            if (tokenactual.tipo == "id") {
                comparar("id");
            }
            else
            {
                tipovariable();
            }
            if (tokenactual.tipo == "coma")
            {
                comparar("coma");
                contenido_actualizar();
            }
        }
        public void tipo()
        {
            if (tokenactual.tipo == "reservada entero")
            {
                comparar("reservada entero");
            }else if (tokenactual.tipo == "reservada fecha")
            {
                comparar("reservada fecha");
            }else if (tokenactual.tipo == "reservada flotante")
            {
                comparar("reservada flotante");
            }else if (tokenactual.tipo == "reservada cadena")
            {
                comparar("reservada cadena");
            }
            else
            {
                Console.WriteLine("error sintactico ese esperaba ->" + "fecha, entero, cadena o floltante");
                erroressintacticos.Add(new Error("Sintactico", "Se esperaba un tipo de variable y se obtuvo   :  "+tokenactual.tipo, tokenactual.fila, tokenactual.columna));
                recuperar("parentesis c");
            }


        }

        public void tipo_insertar()
        {
            tipovariable();
            if (tokenactual.tipo == "coma")
            {
                comparar("coma");
                tipo_insertar();
            }
        }

        public void recuperar(String c)
        {
            try
            {
                Console.WriteLine("esta recuperando");
                if (tokenactual.tipo != c)
                {
                    contadortoken += 1;
                    tokenactual = tokens.ElementAt(contadortoken);
                    recuperar(c);

                }
                else
                {


                }
            }
            catch (Exception o)
            {

            }

        }

        public void tipovariable()
        {
            if (tokenactual.tipo == "entero")
            {
                comparar("entero");
            }
            else if (tokenactual.tipo == "fecha")
            {
                comparar("fecha");
            }
            else if (tokenactual.tipo == "flotante")
            {
                comparar("flotante");
            }
            else if (tokenactual.tipo == "cadena")
            {
                comparar("cadena");
            }
            else
            {
                Console.WriteLine("error sintactico ese esperaba ->" + "fecha, entero, cadena o floltante");
                erroressintacticos.Add(new Error("Sintactico", "Se esperaba un tipo de variable y se obtuvo:   "+ tokenactual.tipo, tokenactual.fila, tokenactual.columna));
                recuperar("parentesis c");
            }
        }
        public void comparar(String token)
        {
            if (tokenactual.tipo != token)
            {
                Console.WriteLine("error sintactico ese esperaba ->" + token);
                recuperar("punto y coma");
                erroressintacticos.Add(new Error("Sintactico", "Se esperaba:   "+token+"   y se obtuvo:   "+tokenactual.tipo, tokenactual.fila, tokenactual.columna));

            }

            if (contadortoken < tokens.Count-1)
            {
                Console.WriteLine("token analizado " + tokenactual.tipo);
                contadortoken += 1;
                tokenactual = tokens.ElementAt(contadortoken);
            }
            else
            {
                Console.WriteLine("token analizado " + tokenactual.tipo);
            }
        }
        
    }


}
