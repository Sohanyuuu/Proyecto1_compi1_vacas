using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_compi1
{

    class Ejecutar
    {
        private List<Token> tokens;
        Token tokenactual;
        int contadortoken = 0;
        int contadorlista = 0;
        private List<Tabla> tablas = new List<Tabla>();
        int temporal =0;
        int fila = 0;
        int columna = 0;
        int contadoro = 0;
        List<Condicion> condiciones;
        List<Seleccion> seleccion;
        public List<Tabla> tablasaux;
        public List<Fila> filasconsulta;
        Boolean bandera = false;
        public Ejecutar(List<Token> t)
        {
            this.tokens = t;
        }

        public void consulta(List<Token> token, List<Tabla> tabla)
        {
            this.tokens = token;
            this.tablas = tabla;
        }

        public void analizarconsulta()
        {
            contadortoken = 0;
            tokenactual = tokens[contadortoken];
            inicio();
        }
        public void analizar()
        {
            contadortoken = 0;
            tokenactual = tokens[contadortoken];
            inicio();
        }
        public void inicio()
        {
            if (contadortoken<tokens.Count-1) {

                if (tokenactual.tipo == "reservada crear")
                {
                    crear();
                }
                else if (tokenactual.tipo == "reservada insertar")
                {
                    insertar();
                }
                else if (tokenactual.tipo == "reservada eliminar")
                {
                    eliminar();
                }
                else if (tokenactual.tipo == "reservada actualizar")
                {
                    actualizar();
                }
                else if (tokenactual.tipo == "reservada seleccionar")
                {
                    seleccionar();
                }
                else
                {
                    contadortoken++;
                    tokenactual = tokens[contadortoken];
                    inicio();
                }

            }
        }

        public void seleccionar()
        {

            filasconsulta = new List<Fila>();
            tablasaux = new List<Tabla>();
            if (tokenactual.tipo=="reservada seleccionar")
            {
                contadortoken++;
                tokenactual = tokens[contadortoken];
                if (tokenactual.tipo == "asterisco")
                {
                    bandera = true;
                    contadortoken++;
                    tokenactual = tokens[contadortoken];
                }
                else
                {
                    condicionseleccion();
                }

                if (tokenactual.tipo == "reservada de")
                {
                    tablasseleccion();
                }

                bandera = false;
            }

            Console.WriteLine("tablas aux_______________________________________");
            for (int i = 0; i < filasconsulta.Count; i++)
            {
                Console.WriteLine(filasconsulta[i].nombre);
                for (int j = 0; j < filasconsulta[i].columnas.Count; j++)
                {
                    Console.WriteLine(filasconsulta[i].columnas[j]);
                }

            }
        }


        public void agregartabla(Boolean bandera, String nombre)
        {
            if (bandera == true)
            {
                for (int i = 0; i < tablas.Count; i++)
                {
                    if (tablas[i].nombre == nombre)
                    {
                        List<Fila> n = new List<Fila>();
                        for (int j = 0; j < tablas[i].filas.Count; j++)
                            {
                                Console.WriteLine("entro a condicion agregar tabla");
                                n.Add(new Fila(tablas[i].filas[j].nombre, tablas[i].filas[j].columnas, tablas[i].nombre));
                                List<String> p = new List<string>();
                                filasconsulta.Add(new Fila(tablas[i].filas[j].nombre, p, tablas[i].nombre));
                            }
                        tablasaux.Add(new Tabla(nombre, n));
                    }
                }
            }
            
        }
        public void tablasseleccion()
        {
            contadortoken++;
            tokenactual = tokens[contadortoken];
            if (tokenactual.tipo == "id")
            {
                agregartabla(bandera, tokenactual.lexema);
                contadortoken++;
                tokenactual = tokens[contadortoken];
                if (tokenactual.tipo == "coma")
                {
                    tablasseleccion();
                }else if (tokenactual.tipo == "reservada donde")
                {
                   dondeseleccion();
                }
                else if (tokenactual.tipo == "punto y coma")
                {
                    agregaraux2();
                }
                else 
                {

                }

            }
        }

        public void dondeseleccion()
        {
            contadortoken++;
            tokenactual = tokens[contadortoken];
            String n = "";
            String c = "";
            String v = "";
            String s = "";
            String g = "";
            if (tokenactual.tipo == "id")
            {
                n = tokenactual.lexema;
                contadortoken++;
                tokenactual = tokens[contadortoken];
                if (tokenactual.tipo == "punto")
                {
                    c = tokenactual.lexema;
                    contadortoken++;
                    tokenactual = tokens[contadortoken];
                    if (tokenactual.tipo == "id")
                    {
                        c = tokenactual.lexema;
                        contadortoken++;
                        tokenactual = tokens[contadortoken];
                        if (tokenactual.tipo == "igual" || tokenactual.tipo == "mayor" || tokenactual.tipo == "menor" || tokenactual.tipo == "diferente" || tokenactual.tipo == "mayor o igual" || tokenactual.tipo == "menor o igual")
                        {
                            v = tokenactual.tipo;
                            contadortoken++;
                            tokenactual = tokens[contadortoken];
                            if (tokenactual.tipo == "id")
                            {
                                s = tokenactual.lexema;
                                contadortoken++;
                                tokenactual = tokens[contadortoken];
                                if (tokenactual.tipo == "punto")
                                {
                                    contadortoken++;
                                    tokenactual = tokens[contadortoken];
                                    if (tokenactual.tipo == "id")
                                    {
                                        g = tokenactual.lexema;
                                        contadortoken++;
                                        tokenactual = tokens[contadortoken];
                                        Console.WriteLine(n+"   "+c+"    "+v+"    "+s+"     "+g);
                                        campos(n,c,v,s,g);
                                        if (tokenactual.tipo == "reservada y")
                                        {
                                            dondeseleccion();
                                        }else if (tokenactual.tipo == "reservada o")
                                        {
                                            dondeseleccion();
                                        }
                                        else
                                        {

                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }
        }

        public void condicionseleccion()
        {
            String tabla = "";
            String atributo = "";
            String campo = "";
            if (tokenactual.tipo=="id")
            {
                tabla = tokenactual.lexema;
                contadortoken++;
                tokenactual = tokens[contadortoken];
                if (tokenactual.tipo == "punto")
                {
                    contadortoken++;
                    tokenactual = tokens[contadortoken];
                    if (tokenactual.tipo == "id")
                    {
                        atributo = tokenactual.lexema;
                        contadortoken++;
                        tokenactual = tokens[contadortoken];
                        if (tokenactual.tipo == "reservada como")
                        {
                            contadortoken++;
                            tokenactual = tokens[contadortoken];
                            if (tokenactual.tipo == "id")
                            {
                                campo = tokenactual.lexema;
                                tablaaux(tabla, atributo, campo);
                                contadortoken++;
                                tokenactual = tokens[contadortoken];
                                if (tokenactual.tipo == "coma")
                                {
                                    contadortoken++;
                                    tokenactual = tokens[contadortoken];
                                    condicionseleccion();
                                }
                                else
                                {

                                }
                            }
                        }
                    }
                }
            }
        }

        public void tablaaux(String tabla, String campo, String nuevo)
        {
            for (int i = 0; i<tablas.Count; i++)
            {
                if (tablas[i].nombre==tabla)
                {

                    for (int j = 0; j < tablas[i].filas.Count; j++)
                    {
                        if (tablas[i].filas[j].nombre==campo)
                        {
                            
                            List<Fila> n = new List<Fila>();
                            n.Add(new Fila(nuevo, tablas[i].filas[j].columnas, tablas[i].nombre));
                            tablasaux.Add(new Tabla(tabla, n));
                            List<String> p = new List<string>();
                            filasconsulta.Add(new Fila(nuevo, p, tablas[i].nombre));
                        }
                    }
                }
            }
            
        }

        public void campos(String tabla1, String id1, String com, String tabla2, String id2)
        {
            int campo1 = 0;
            int campo2 = 0;
            int campo3 = 0;
            int campo4 = 0;
            for (int i = 0; i<tablas.Count; i++)
            {
                if (tablas[i].nombre == tabla1)
                {
                    for (int j = 0; j<tablas[i].filas.Count;j++)
                    {
                        if (tablas[i].filas[j].nombre==id1)
                        {
                            campo1 = i;
                            campo2 = j;
                        }
                    }
                }
            }

            for (int i = 0; i < tablas.Count; i++)
            {
                if (tablas[i].nombre == tabla2)
                {
                    for (int j = 0; j < tablas[i].filas.Count; j++)
                    {
                        if (tablas[i].filas[j].nombre == id2)
                        {
                            Console.WriteLine(campo1 + "  " + campo2 + "   " + campo3 + "   " + campo4);
                            campo3 = i;
                            campo4 = j;
                            
                        }
                    }
                }
            }
            
            buscardato(campo1, campo2, campo3, campo4, com);
        }

        public void buscardato(int campo1, int campo2, int campo3, int campo4, String com)
        {
            
         if (com == "igual")
                        {
                            try
                            {
                    for (int i = 0; i<tablas[campo1].filas[campo2].columnas.Count; i++)
                    {
                        for (int j = 0; j < tablas[campo3].filas[campo4].columnas.Count; j++)
                        {
                            if (tablas[campo1].filas[campo2].columnas[i] == tablas[campo3].filas[campo4].columnas[j])
                            {
                                agregaraux(tablas[campo1].nombre, i);
                                agregaraux(tablas[campo3].nombre, j);
                            }
                            else
                            {

                            }
                        }
                    }
                    
                            }
                            catch (Exception o)
                            {

                            }
                        }
                        else if (com == "diferente")
                        {
                            try
                            {
                    for (int i = 0; i < tablas[campo1].filas[campo2].columnas.Count; i++)
                    {
                        for (int j = 0; j < tablas[campo3].filas[campo4].columnas.Count; j++)
                        {
                            if (tablas[campo1].filas[campo2].columnas[i] != tablas[campo3].filas[campo4].columnas[j])
                            {
                                agregaraux(tablas[campo1].nombre, i);
                                agregaraux(tablas[campo3].nombre, j);
                            }
                            else
                            {

                            }
                        }
                    }
                }
                            catch (Exception o)
                            {

                            }
                        }
                        else if (com == "mayor o igual")
                        {
                            try
                            {
                    for (int i = 0; i < tablas[campo1].filas[campo2].columnas.Count; i++)
                    {
                        for (int j = 0; j < tablas[campo3].filas[campo4].columnas.Count; j++)
                        {
                            if (Int32.Parse(tablas[campo1].filas[campo2].columnas[i]) >= Int32.Parse(tablas[campo3].filas[campo4].columnas[j]))
                            {
                                agregaraux(tablas[campo1].nombre, i);
                                agregaraux(tablas[campo3].nombre, j);
                            }
                            else
                            {

                            }
                        }
                    }

                }
                            catch (Exception o)
                            {

                            }
                        }
                        else if (com == "mayor")
                        {
                            try
                            {
                    for (int i = 0; i < tablas[campo1].filas[campo2].columnas.Count; i++)
                    {
                        for (int j = 0; j < tablas[campo3].filas[campo4].columnas.Count; j++)
                        {
                            if (Int32.Parse(tablas[campo1].filas[campo2].columnas[i]) > Int32.Parse(tablas[campo3].filas[campo4].columnas[j]))
                            {
                                agregaraux(tablas[campo1].nombre, i);
                                agregaraux(tablas[campo3].nombre, j);
                            }
                            else
                            {

                            }
                        }
                    }
                }
                            catch (Exception o)
                            {

                            }
                        }
                        else if (com == "menor o igual")
                        {
                            try
                            {
                    for (int i = 0; i < tablas[campo1].filas[campo2].columnas.Count; i++)
                    {
                        for (int j = 0; j < tablas[campo3].filas[campo4].columnas.Count; j++)
                        {
                            if (Int32.Parse(tablas[campo1].filas[campo2].columnas[i]) <= Int32.Parse(tablas[campo3].filas[campo4].columnas[j]))
                            {
                                agregaraux(tablas[campo1].nombre, i);
                                agregaraux(tablas[campo3].nombre, j);
                            }
                            else
                            {

                            }
                        }
                    }
                }
                            catch (Exception o)
                            {

                            }
                        }
                        else if (com == "menor")
                        {
                            try
                            {
                    for (int i = 0; i < tablas[campo1].filas[campo2].columnas.Count; i++)
                    {
                        for (int j = 0; j < tablas[campo3].filas[campo4].columnas.Count; j++)
                        {
                            if (Int32.Parse(tablas[campo1].filas[campo2].columnas[i]) < Int32.Parse(tablas[campo3].filas[campo4].columnas[j]))
                            {
                                agregaraux(tablas[campo1].nombre, i);
                                agregaraux(tablas[campo3].nombre, j);
                            }
                            else
                            {

                            }
                        }
                    }
                }
                            catch (Exception o)
                            {

                            }
                        }
           
        }

        public void agregaraux(String tabla, int columna)
        {
            for (int i = 0; i<tablasaux.Count;i++)
            {
                if (tablasaux[i].nombre==tabla)
                {
                    for (int j = 0; j<tablasaux[i].filas.Count; j++)
                    {
                        Console.WriteLine(tablasaux[i].nombre+"   "+ tablasaux[i].filas[j].nombre+"    "+tablasaux[i].filas[j].columnas[columna]);
                        for (int k = 0; k<filasconsulta.Count;k++)
                        {
                            if (filasconsulta[k].nombre == tablasaux[i].filas[j].nombre)
                            {
                                filasconsulta[k].columnas.Add(tablasaux[i].filas[j].columnas[columna]);
                            }
                        }
                    }
                }
            }
            
        }

        public void agregaraux2()
        {
            for (int i = 0; i < tablasaux.Count; i++)
            {
                for (int k = 0; k < filasconsulta.Count; k++)
                {
                    if (filasconsulta[k].tabla == tablasaux[i].nombre)
                    {
                        for (int j = 0; j < tablasaux[i].filas.Count; j++)
                        {
                            if (filasconsulta[k].nombre == tablasaux[i].filas[j].nombre)
                            {
                                for (int f = 0; f < tablasaux[i].filas[j].columnas.Count; f++)
                                {
                                    filasconsulta[k].columnas.Add(tablasaux[i].filas[j].columnas[f]);
                                }
                            }
                        }
                        }
                    }
                
            }

        }


        public void crear()
        {
            contadorlista++;
            contadortoken++;
            tokenactual = tokens[contadortoken];
            if (tokenactual.tipo == "reservada tabla")
            {
                contadortoken++;
                tokenactual = tokens[contadortoken];
                if (tokenactual.tipo == "id")
                {
                    List<Fila> n = new List<Fila>();
                    tablas.Add(new Tabla(tokenactual.lexema, n));
                    contadortoken++;
                    tokenactual = tokens[contadortoken];
                    if (tokenactual.tipo == "parentesis a")
                    {
                        contadortoken++;
                        tokenactual = tokens[contadortoken];
                        declaracion();
                        
                    }

                }

            }
            inicio();
        }

        public void declaracion()
        {

            List<String> n = new List<String>();
            if (tokenactual.tipo == "id")
            {
                tablas[contadorlista - 1].filas.Add(new Fila(tokenactual.lexema, n, tablas[contadorlista - 1].nombre));
                contadortoken++;
                tokenactual = tokens[contadortoken];
                if (tokenactual.tipo == "reservada entero" || tokenactual.tipo == "reservada cadena" || tokenactual.tipo == "reservada fecha" || tokenactual.tipo == "reservada flotante")
                {
                    contadortoken++;
                    tokenactual = tokens[contadortoken];
                    if (tokenactual.tipo == "coma")
                    {
                        contadortoken++;
                        tokenactual = tokens[contadortoken];
                        declaracion();
                    }
                    else
                    {

                    }
                }
            }

        }
        

        public void eliminar()
        {
            contadortoken++;
            tokenactual = tokens[contadortoken];
            if (tokenactual.tipo == "reservada de")
            {
                contadortoken++;
                tokenactual = tokens[contadortoken];
                if (tokenactual.tipo == "id")
                {
                    for (int i = 0; i < tablas.Count; i++)
                    {
                        if (tablas[i].nombre == tokenactual.lexema)
                        {
                            temporal = i;
                        }
                    }
                    contadortoken++;
                    tokenactual = tokens[contadortoken];

                    if (tokenactual.tipo == "reservada donde")
                    {
                        contadortoken++;
                        tokenactual = tokens[contadortoken];

                        borrardonde();

                    }else if (tokenactual.tipo == "punto y coma")
                    {
                        
                            for (int j = 0; j < tablas[temporal].filas.Count; j++)
                            {
                                for (int k = 0; k < tablas[temporal].filas[j].columnas.Count; k++)
                                {
                                tablas[temporal].filas[j].columnas = new List<string>();
                                }
                            }
                        
                    }


                }

            }

            inicio();
        }

        public void actualizar()
        {
            condiciones = new List<Condicion>();
            contadortoken++;
            tokenactual = tokens[contadortoken];
            if (tokenactual.tipo == "id")
            {
                contadortoken++;
                tokenactual = tokens[contadortoken];
                if (tokenactual.tipo == "reservada establecer")
                {
                    contadortoken++;
                    tokenactual = tokens[contadortoken];
                    if (tokenactual.tipo == "parentesis a") {

                        condi();
                    }
                }
            }
            inicio();
        }
        
        public void condi()
        {
            contadortoken++;
            tokenactual = tokens[contadortoken];
            String n = "";
            String c = "";
            String v = "";
            if (tokenactual.tipo == "id")
            {
                n = tokenactual.lexema;
                contadortoken++;
                tokenactual = tokens[contadortoken];
                if (tokenactual.tipo == "igual" )
                {
                    c = tokenactual.tipo;
                    contadortoken++;
                    tokenactual = tokens[contadortoken];
                    if (tokenactual.tipo == "entero" || tokenactual.tipo == "cadena" || tokenactual.tipo == "fecha" || tokenactual.tipo == "flotante")
                    {
                        v = tokenactual.lexema;
                        contadortoken++;
                        tokenactual = tokens[contadortoken];
                        if (tokenactual.tipo=="coma")
                        {
                            condiciones.Add(new Condicion(n, c, v));
                            condi();
                        }else if (tokenactual.tipo == "parentesis c")
                        {
                            condiciones.Add(new Condicion(n,c,v));
                            contadortoken++;
                            tokenactual = tokens[contadortoken];

                            if (tokenactual.tipo == "reservada donde")
                            {
                                contadortoken++;
                                tokenactual = tokens[contadortoken];
                                actualizarcampos();
                            }
                        }

                    }
                }
            }
        }

        public void actualizarcampos()
        {
            String lex = "";
            String fila = "";
            String comparador = "";
            Console.WriteLine("entro al y");
            if (tokenactual.tipo == "id")
            {
                lex = tokenactual.lexema;
                contadortoken++;
                tokenactual = tokens[contadortoken];

                if (tokenactual.tipo == "mayor o igual" || tokenactual.tipo == "menor o igual" || tokenactual.tipo == "igual" || tokenactual.tipo == "diferente" || tokenactual.tipo == "mayor" || tokenactual.tipo == "menor")
                {
                    comparador = tokenactual.tipo;
                    contadortoken++;
                    tokenactual = tokens[contadortoken];
                    if (tokenactual.tipo == "entero" || tokenactual.tipo == "cadena" || tokenactual.tipo == "fecha" || tokenactual.tipo == "flotante" || tokenactual.tipo == "id")
                    {
                        fila = tokenactual.lexema;
                        contadortoken++;
                        tokenactual = tokens[contadortoken];

                        if (tokenactual.tipo == "reservada y")
                        {
                            contadortoken++;
                            tokenactual = tokens[contadortoken];
                            if (tokenactual.tipo == "punto y coma")
                            {
                                cambiardato(lex, comparador, fila);
                            }
                            else
                            {
                                cambiardato(lex, comparador, fila);
                                actualizarcampos();
                            }
                        }
                        else if (tokenactual.tipo == "reservada o")
                        {
                            if (contadoro == 0)
                            {

                                contadortoken++;
                                tokenactual = tokens[contadortoken];
                                if (tokenactual.tipo == "punto y coma")
                                {
                                   cambiardato(lex, comparador, fila);
                                }
                                else
                                {
                                    cambiardato(lex, comparador, fila);
                                    actualizarcampos();
                                }
                            }
                        }
                        else if (tokenactual.tipo == "punto y coma")
                        {
                            cambiardato(lex, comparador, fila);
                        }
                    }
                    else
                    {

                    }
                }
            }
        }

        public void cambiardato(String lex, String com, String fi)
        {
            
            for (int i = 0; i < tablas[temporal].filas.Count; i++)
            {
                if (tablas[temporal].filas[i].nombre == lex)
                {
                    for (int j = 0; j <= tablas[temporal].filas[i].columnas.Count; j++)
                    {

                        if (com == "igual")
                        {
                            try
                            {
                                if (tablas[temporal].filas[i].columnas[j] == fi)
                                {
                                    for (int k = 0; k < condiciones.Count; k++)
                                    {
                                        
                                        for (int h = 0; h < tablas[temporal].filas.Count; h++)
                                        {
                                            if (tablas[temporal].filas[h].nombre==condiciones[k].campo)
                                            {
                                                tablas[temporal].filas[h].columnas[j] = condiciones[k].valor;
                                                
                                            }
                                        }

                                    }
                                }
                                else
                                {

                                }
                            }
                            catch (Exception o)
                            {

                            }
                        }
                        else if (com == "diferente")
                        {
                            try
                            {
                                if (tablas[temporal].filas[i].columnas[j] != fi)
                                {
                                    for (int k = 0; k < condiciones.Count; k++)
                                    {

                                        for (int h = 0; h < tablas[temporal].filas.Count; h++)
                                        {
                                            if (tablas[temporal].filas[h].nombre == condiciones[k].campo)
                                            {
                                                tablas[temporal].filas[h].columnas[j] = condiciones[k].valor;
                                            }
                                        }

                                    }
                                }
                            }
                            catch (Exception o)
                            {

                            }
                        }
                        else if (com == "mayor o igual")
                        {
                            try
                            {
                                if (Int32.Parse(tablas[temporal].filas[i].columnas[j]) >= Int32.Parse(fi))
                                {
                                    for (int k = 0; k < condiciones.Count; k++)
                                    {

                                        for (int h = 0; h < tablas[temporal].filas.Count; h++)
                                        {
                                            if (tablas[temporal].filas[h].nombre == condiciones[k].campo)
                                            {
                                                tablas[temporal].filas[h].columnas[j] = condiciones[k].valor;
                                            }
                                        }

                                    }
                                }
                            }
                            catch (Exception o)
                            {

                            }
                        }
                        else if (com == "mayor")
                        {
                            try
                            {
                                if (Int32.Parse(tablas[temporal].filas[i].columnas[j]) > Int32.Parse(fi))
                                {
                                    for (int k = 0; k < condiciones.Count; k++)
                                    {

                                        for (int h = 0; h < tablas[temporal].filas.Count; h++)
                                        {
                                            if (tablas[temporal].filas[h].nombre == condiciones[k].campo)
                                            {
                                                tablas[temporal].filas[h].columnas[j] = condiciones[k].valor;
                                            }
                                        }

                                    }
                                }
                            }
                            catch (Exception o)
                            {

                            }
                        }
                        else if (com == "menor o igual")
                        {
                            try
                            {
                                if (Int32.Parse(tablas[temporal].filas[i].columnas[j]) <= Int32.Parse(fi))
                                {
                                    for (int k = 0; k < condiciones.Count; k++)
                                    {

                                        for (int h = 0; h < tablas[temporal].filas.Count; h++)
                                        {
                                            if (tablas[temporal].filas[h].nombre == condiciones[k].campo)
                                            {
                                                tablas[temporal].filas[h].columnas[j] = condiciones[k].valor;
                                            }
                                        }

                                    }
                                }
                            }
                            catch (Exception o)
                            {

                            }
                        }
                        else if (com == "menor")
                        {
                            try
                            {
                                if (Int32.Parse(tablas[temporal].filas[i].columnas[j]) < Int32.Parse(fi))
                                {
                                    for (int k = 0; k < condiciones.Count; k++)
                                    {

                                        for (int h = 0; h < tablas[temporal].filas.Count; h++)
                                        {
                                            if (tablas[temporal].filas[h].nombre == condiciones[k].campo)
                                            {
                                                tablas[temporal].filas[h].columnas[j] = condiciones[k].valor;
                                            }
                                        }

                                    }
                                }
                            }
                            catch (Exception o)
                            {

                            }
                        }


                    }

                }
            }


        }
        public void borrardonde()
        {
            
            String lex = "";
            String fila = "";
            String comparador = "";
            if (tokenactual.tipo == "id")
            {
                lex = tokenactual.lexema;
                contadortoken++;
                tokenactual = tokens[contadortoken];
                
                if (tokenactual.tipo == "mayor o igual" || tokenactual.tipo == "menor o igual" || tokenactual.tipo == "igual" || tokenactual.tipo == "diferente" || tokenactual.tipo == "mayor" || tokenactual.tipo == "menor")
                {
                    comparador = tokenactual.tipo;
                    contadortoken++;
                    tokenactual = tokens[contadortoken];
                    if (tokenactual.tipo == "entero" || tokenactual.tipo == "cadena" || tokenactual.tipo == "fecha" || tokenactual.tipo == "flotante" || tokenactual.tipo == "id")
                    {
                        fila = tokenactual.lexema;
                        contadortoken++;
                        tokenactual = tokens[contadortoken];

                        if (tokenactual.tipo == "reservada y")
                        {
                            contadortoken++;
                            tokenactual = tokens[contadortoken];
                            if (tokenactual.tipo == "punto y coma")
                            {
                                eliminardato(lex, comparador, fila);
                            }
                            else
                            {
                                eliminardato(lex, comparador, fila);
                                borrardonde();
                            }
                        }
                        else if (tokenactual.tipo == "reservada o")
                        {
                            if (contadoro==0) {

                                contadortoken++;
                                tokenactual = tokens[contadortoken];
                                if (tokenactual.tipo == "punto y coma")
                                {
                                    eliminardato(lex, comparador, fila);
                                }
                                else
                                {
                                    eliminardato(lex, comparador, fila);
                                    borrardonde();
                                }
                            }
                        } 
                        else if (tokenactual.tipo == "punto y coma")
                        {
                            
                            eliminardato(lex, comparador, fila);
                        }
                    }
                    else
                    {

                    }
                }
            }
        }

        public void eliminardato(String lex, String com, String fi)
        {
            try
            {
                for (int i = 0; i < tablas[temporal].filas.Count; i++)
                {
                    if (tablas[temporal].filas[i].nombre == lex)
                    {
                        for (int j = 0; j <= tablas[temporal].filas[i].columnas.Count; j++)
                        {

                            if (com == "igual")
                            {
                                if (tablas[temporal].filas[i].columnas[j] == fi)
                                {
                                    for (int k = 0; k < tablas[temporal].filas.Count; k++)
                                    {
                                        tablas[temporal].filas[k].columnas.RemoveAt(j);

                                    }
                                }
                                else
                                {

                                }
                            }
                            else if (com == "diferente")
                            {
                                try
                                {
                                    if (tablas[temporal].filas[i].columnas[j] != fi)
                                    {
                                        //Console.WriteLine("entro a menor o igual  "+tablas[temporal].filas[i].columnas[j]);

                                        for (int k = 0; k < tablas[temporal].filas.Count; k++)
                                        {
                                            tablas[temporal].filas[k].columnas.RemoveAt(j);

                                        }
                                        eliminardato(lex, com, fi);
                                    }
                                }
                                catch (Exception o)
                                {

                                }
                            }
                            else if (com == "mayor o igual")
                            {
                                try
                                {
                                    if (Int32.Parse(tablas[temporal].filas[i].columnas[j]) >= Int32.Parse(fi))
                                    {
                                        //Console.WriteLine("entro a menor o igual  "+tablas[temporal].filas[i].columnas[j]);

                                        for (int k = 0; k < tablas[temporal].filas.Count; k++)
                                        {
                                            tablas[temporal].filas[k].columnas.RemoveAt(j);

                                        }
                                        eliminardato(lex, com, fi);
                                    }
                                }
                                catch (Exception o)
                                {

                                }
                            }
                            else if (com == "mayor")
                            {
                                if (Int32.Parse(tablas[temporal].filas[i].columnas[j]) > Int32.Parse(fi))
                                {

                                    for (int k = 0; k < tablas[temporal].filas.Count; k++)
                                    {
                                        tablas[temporal].filas[k].columnas.RemoveAt(j);

                                    }
                                    eliminardato(lex, com, fi);
                                }
                                else
                                {

                                }
                            }
                            else if (com == "menor o igual")
                            {
                                try
                                {
                                    if (Int32.Parse(tablas[temporal].filas[i].columnas[j]) <= Int32.Parse(fi))
                                    {
                                        //Console.WriteLine("entro a menor o igual  "+tablas[temporal].filas[i].columnas[j]);

                                        for (int k = 0; k < tablas[temporal].filas.Count; k++)
                                        {
                                            tablas[temporal].filas[k].columnas.RemoveAt(j);

                                        }
                                        eliminardato(lex, com, fi);
                                    }
                                }
                                catch (Exception o)
                                {

                                }
                            }
                            else if (com == "menor")
                            {
                                if (Int32.Parse(tablas[temporal].filas[i].columnas[j]) < Int32.Parse(fi))
                                {

                                    for (int k = 0; k < tablas[temporal].filas.Count; k++)
                                    {
                                        tablas[temporal].filas[k].columnas.RemoveAt(j);

                                    }
                                    eliminardato(lex, com, fi);
                                }
                                else
                                {

                                }
                            }


                        }

                    }
                }
            }
            catch (Exception o)
            {

            }
            
        }
        public void insertar()
        {
            contadortoken++;
            tokenactual = tokens[contadortoken];
            if (tokenactual.tipo == "reservada en")
            {
                contadortoken++;
                tokenactual = tokens[contadortoken];
                if (tokenactual.tipo == "id")
                {
                    for (int i = 0; i<tablas.Count; i++)
                    {
                        if (tablas[i].nombre == tokenactual.lexema)
                        {
                            temporal = i;
                        }
                    }
                    contadortoken++;
                    tokenactual = tokens[contadortoken];
                    if (tokenactual.tipo == "reservada valores")
                    {
                        contadortoken++;
                        tokenactual = tokens[contadortoken];

                        if (tokenactual.tipo == "parentesis a")
                        {
                            contadortoken++;
                            tokenactual = tokens[contadortoken];
                            insertando(0);

                        }

                    }

                }

            }
            inicio();
        }

        public void insertando(int i)
        {
            
                if (tokenactual.tipo == "entero" || tokenactual.tipo == "cadena" || tokenactual.tipo == "fecha" || tokenactual.tipo == "flotante")
                {
                    tablas[temporal].filas[i].columnas.Add(tokenactual.lexema);
                    contadortoken++;
                    tokenactual = tokens[contadortoken];
                    if (tokenactual.tipo == "coma")
                    {
                        contadortoken++;
                        tokenactual = tokens[contadortoken];
                        insertando(i+1);
                    }
                    else
                    {

                    }
                }
            
        }
        public List<Tabla> gettabla()
        {
            return tablas;
        }
        public void imprimirtabla()
        {
            Console.WriteLine("Tablas_____________________________________________________");

            for (int i = 0; i < tablas.Count; i++)
            {
                Console.WriteLine(tablas[i].nombre);
                for (int j = 0; j < tablas[i].filas.Count; j++)
                {
                    Console.WriteLine(tablas[i].filas[j].nombre);
                    for (int k = 0; k < tablas[i].filas[j].columnas.Count; k++)
                    {
                        Console.WriteLine(tablas[i].filas[j].columnas[k]);

                    }
                }
            }

        }
    }
}

internal class Condicion
{
    public String campo;
    public String signo;
    public String valor;
    public Condicion(String c, String s, String v)
    {
        this.campo = c;
        this.signo = s;
        this.valor = v;
    }
}

internal class Seleccion
{
    public String tabla;
    public String atributo;
    public String nuevo;
    public Seleccion(String c, String s, String v)
    {
        this.tabla = c;
        this.atributo = s;
        this.nuevo = v;
    }
}


