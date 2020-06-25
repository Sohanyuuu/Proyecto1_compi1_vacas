using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_compi1
{
    class Analizador_201700471
    {
        String texto;
        List<Token> tokens;
        String palabra = "";
        int fila = 0;
        int columna = 0;
        String reservada = "";
        int estado = 0;
        String id = "";
        List<Error> erroreslexicos;
        public Analizador_201700471(String texto)
        {
            erroreslexicos = new List<Error>();
            this.texto = texto;
        }

        public List<Token> GetTokens()
        {
            return tokens;
        }

        public List<Error> GetErroreslexicos()
        {
            return erroreslexicos;
        }

        public void analizar()
        {
            
            String numero = "";
            String comentario = "";
            tokens = new List<Token>();

            for (int i = 0; i < texto.Length; i++)
            {

                switch (estado)
                {
                    case 0:
                        if (char.IsLetter(texto[i]))
                        {
                            palabra += texto[i].ToString();
                            estado = 1;
                            columna++;
                            continue;
                        }
                        else if (char.IsDigit(texto[i]))
                        {
                            numero = "";
                            numero += texto[i].ToString();
                            estado = 2;
                            columna++;
                            continue;
                        }
                        else if (texto[i] == '"')
                        {
                            palabra += texto[i].ToString();
                            estado = 12;
                            columna++;

                            continue;
                        }
                        else if (texto[i] == '-')
                        {
                            estado = 14;
                            columna++;
                            continue;
                        }
                        else if (texto[i].Equals((char)39))
                        {
                            numero += texto[i].ToString();
                            columna++;
                            estado = 7;
                            continue;
                        }
                        else if (texto[i] == '/')
                        {
                            comentario = "";
                            estado = 9;
                            columna++;
                            continue;
                        }
                        else if (texto[i] == '{')
                        {
                            estado = 0;
                            columna++;
                            tokens.Add(new Token(5, "llave a", "{", fila, columna));
                            continue;
                        }
                        else if (texto[i] == '*')
                        {
                            estado = 0;
                            columna++;
                            tokens.Add(new Token(40, "asterisco", "*", fila, columna));
                            continue;
                        }

                        else if (texto[i] == '!')
                        {
                            palabra = "";
                            numero = "";
                            estado = 25;
                            columna++;
                            continue;
                        }
                        else if (texto[i] == '}')
                        {
                            palabra = "";
                            numero = "";
                            estado = 0;
                            columna++;
                            tokens.Add(new Token(6, "llave c", "}", fila, columna));
                            continue;
                        }
                        else if (texto[i] == ')')
                        {
                            if (palabra != "") {
                                verificar(palabra);
                            }
                            reservada = "";
                            palabra = "";
                            numero = "";
                            estado = 0;
                            columna++;
                            tokens.Add(new Token(3, "parentesis c", ")", fila, columna));
                            continue;
                        }
                        else if (texto[i] == '(')
                        {

                            palabra = "";
                            numero = "";
                            estado = 0;
                            columna++;
                            tokens.Add(new Token(2, "parentesis a", "(", fila, columna));
                            continue;
                        }
                        else if (texto[i] == '>')
                        {
                            palabra = "";
                            numero = "";
                            estado = 26;
                            columna++;
                            continue;
                        }
                        else if (texto[i] == '<')
                        {
                            palabra = "";
                            numero = "";
                            estado = 27;
                            columna++;
                            continue;
                        }
                        else if (texto[i] == ',')
                        {
                            palabra = "";
                            numero = "";
                            estado = 0;
                            columna++;
                            tokens.Add(new Token(8, "coma", ",", fila, columna));
                            continue;
                        }
                        else if (texto[i] == '=')
                        {
                            palabra = "";
                            numero = "";
                            estado = 0;
                            columna++;
                            tokens.Add(new Token(12, "igual", "=", fila, columna));
                            continue;
                        }
                        else if (texto[i] == ';')
                        {
                            palabra = "";
                            estado = 0;
                            columna++;
                            tokens.Add(new Token(10, "punto y coma", ";", fila, columna));
                            continue;
                        }
                        else if (texto[i].Equals((char)10) || (texto[i].Equals("\n")))
                        {
                            palabra = "";
                            columna = 0;
                            fila++;
                            estado = 0;
                            continue;
                        }
                        else if (texto[i].Equals((char)32))
                        { 
                            palabra = "";
                            estado = 0;
                            columna++;
                            continue;
                        }
                        else
                        {
                            erroreslexicos.Add(new Error("Lexico", "No se admite el caracter"+ texto[i]+"en el alfabeto", fila, columna));
                        }
                        break;

                    case 1:
                        if (char.IsLetter(texto[i]))
                        {
                            palabra += texto[i].ToString();
                            estado = 1;
                            columna++;
                            continue;
                        }
                        else if (char.IsDigit(texto[i]))
                        {
                            palabra += texto[i].ToString();
                            estado = 1;
                            columna++;
                            continue;
                        }
                        else if (texto[i] == '{')
                        {
                            verificar(palabra);
                            palabra = "";
                            estado = 0;
                            columna++;
                            continue;
                        }
                        else if (texto[i] == '}')
                        {
                            verificar(palabra);
                            id = "";
                            palabra = "";
                            estado = 0;
                            columna++;
                            continue;
                        }
                        else if (texto[i] == '_')
                        {
                            palabra += texto[i].ToString();
                            estado = 0;
                            columna++;
                            continue;
                        }
                        else if (texto[i] == ',')
                        {
                            verificar(palabra);
                            id = "";
                            palabra = "";
                            estado = 0;
                            columna++;
                            tokens.Add(new Token(8, "coma", ",", fila, columna));
                            continue;
                        }
                        else if (texto[i] == ';')
                        {
                            verificar(palabra);
                            id = "";
                            palabra = "";
                            estado = 0;
                            columna++;
                            tokens.Add(new Token(10, "punto y coma", ";", fila, columna));
                            continue;
                        }
                        else if (texto[i] == '[')
                        {
                            verificar(palabra);
                            palabra = "";
                            estado = 0;
                            columna++;
                            continue;
                        }
                        else if (texto[i] == ']')
                        {
                            verificar(palabra);
                            id = "";
                            palabra = "";
                            estado = 0;
                            columna++;
                            continue;
                        }
                        else if (texto[i] == '(')
                        {
                            verificar(palabra);
                            id = "";
                            palabra = "";
                            numero = "";
                            estado = 0;
                            columna++;
                            tokens.Add(new Token(2, "parentesis a", "(", fila, columna));
                            continue;
                        }
                        else if (texto[i] == ')')
                        {
                            verificar(palabra);
                            id = "";
                            palabra = "";
                            reservada = "";
                            estado = 0;
                            columna++;
                            tokens.Add(new Token(3, "parentesis c", ")", fila, columna));
                            continue;
                        }
                        else if (texto[i] == '.')
                        {
                            tokens.Add(new Token(1, "id", palabra, fila, columna));
                            palabra = "";
                            estado = 29;
                            columna++;
                            tokens.Add(new Token(41, "punto", ".", fila, columna));
                            continue;
                        }
                        else if (texto[i] == '=')
                        {
                            verificar(palabra);
                            palabra = "";
                            numero = "";
                            estado = 0;
                            columna++;
                            tokens.Add(new Token(12, "igual", "=", fila, columna));
                            continue;
                        }
                        else if (texto[i] == '>')
                        {
                            verificar(palabra);
                            palabra = "";
                            numero = "";
                            estado = 26;
                            columna++;
                            continue;
                        }
                        else if (texto[i] == '<')
                        {
                            verificar(palabra);
                            palabra = "";
                            numero = "";
                            estado = 27;
                            columna++;
                            continue;
                        }
                        else if (texto[i] == '!')
                        {
                            verificar(palabra);
                            palabra = "";
                            numero = "";
                            estado = 25;
                            columna++;
                            continue;
                        }
                        else if (texto[i].Equals((char)32))
                        {
                            verificar(palabra);
                            palabra = "";
                            columna++;
                            estado = 0;
                            continue;
                        }

                        else if (texto[i].Equals((char)10))
                        {
                            verificar(palabra);
                            palabra = "";
                            columna = 0;
                            fila++;
                            estado = 0;
                            continue;
                        }
                        else if (texto[i].Equals((char)09))
                        {
                            palabra = "";
                            columna = columna + 8;
                            estado = 0;
                            continue;
                        }

                        else
                        {
                            erroreslexicos.Add(new Error("Lexico", "No se admite el caracter" + texto[i] + "en el alfabeto", fila, columna));
                        }
                        break;
                    case 2:
                        if (char.IsDigit(texto[i]))
                        {
                            numero += texto[i].ToString();
                            estado = 2;
                            columna++;
                            continue;
                        }
                        else if (texto[i] == '.')
                        {
                            numero += texto[i].ToString();
                            estado = 3;
                            columna++;
                            continue;
                        }
                        else if (texto[i].Equals((char)32))
                        {
                            tokens.Add(new Token(9, "entero", numero, fila, columna));
                            numero = "";
                            columna++;
                            estado = 0;
                            continue;
                        }
                        else if (texto[i] == ';')
                        {
                            tokens.Add(new Token(9, "entero", numero, fila, columna));
                            columna++;
                            tokens.Add(new Token(10, "punto y coma", ";", fila, columna));
                            numero = "";
                            estado = 0;
                           
                            continue;
                        }
                        else if (texto[i] == ',')
                        {
                            tokens.Add(new Token(9, "entero", numero, fila, columna));
                            columna++;
                            tokens.Add(new Token(8, "coma", ",", fila, columna));
                            numero = "";
                            estado = 0;
                            continue;
                        }
                        else if (texto[i] == ')')
                        {
                            estado = 0;
                            tokens.Add(new Token(9, "entero", numero, fila, columna));
                            columna++;
                            tokens.Add(new Token(3, "parentesis c", ")", fila, columna));
                            continue;
                        }
                        else
                        {
                            erroreslexicos.Add(new Error("Lexico", "No se admite el caracter" + texto[i] + "en el alfabeto", fila, columna));
                        }

                        break;
                    case 3:
                        if (char.IsDigit(texto[i]))
                        {
                            numero += texto[i].ToString();
                            estado = 3;
                            columna++;
                            continue;
                        }
                        else if (texto[i] == ';')
                        {
                            columna++;
                            tokens.Add(new Token(10, "punto y coma", ";", fila, columna));
                            numero = "";
                            estado = 0;

                            continue;
                        }
                        else if (texto[i] == ',')
                        {
                            tokens.Add(new Token(15, "flotante", numero, fila, columna));
                            numero = "";
                            estado = 0;
                            columna++;
                            tokens.Add(new Token(8, "coma", ",", fila, columna));
                            continue;
                        }
                        else if (texto[i].Equals((char)32))
                        {
                            numero = "";
                            estado = 0;
                            columna++;
                            continue;
                        }
                        else
                        {
                            erroreslexicos.Add(new Error("Lexico", "No se admite el caracter" + texto[i] + "en el alfabeto", fila, columna));
                        }

                        break;

                    case 4:
                        if (texto[i] != '"')
                        {
                            palabra += texto[i].ToString();
                            estado = 5;
                            columna++;
                            continue;
                        }
                        break;
                    case 5:
                        if (texto[i] != '"')
                        {
                            palabra += texto[i].ToString();
                            columna++;
                            estado = 5;
                            continue;
                        }
                        else if (texto[i] == '"')
                        {
                            palabra = "";
                            columna++;
                            estado = 6;
                            continue;
                        }
                        break;
                    case 6:
                        columna++;
                        tokens.Add(new Token(16, "cadena", palabra, fila, columna));
                        estado = 0;
                        palabra = "";
                        Console.WriteLine(palabra);
                        break;
                    case 7:
                        if(char.IsDigit(texto[i]))
                        {
                            numero += texto[i].ToString();
                            estado = 8;
                            columna++;
                            continue;
                        }else{
                            erroreslexicos.Add(new Error("Lexico", "No se admite el caracter" + texto[i] + "en el alfabeto", fila, columna));
                        }
                        break;
                    case 8:
                        if (char.IsDigit(texto[i]))
                        {
                            numero += texto[i].ToString();
                            estado = 16;
                            columna++;
                            continue;
                        }
                        else
                        {
                            erroreslexicos.Add(new Error("Lexico", "No se admite el caracter" + texto[i] + "en el alfabeto", fila, columna));
                        }
                        break;
                    case 9:
                        if (texto[i] == '*')
                        {
                            
                            estado = 10;
                            continue;
                        }
                        else
                        {
                            erroreslexicos.Add(new Error("Lexico", "No se admite el caracter" + texto[i] + "en el alfabeto", fila, columna));
                            continue;
                        }
                        break;
                    case 10:
                        if (texto[i] == '*')
                        {
                            estado = 11;
                            continue;
                        }
                        else
                        {
                            comentario += texto[i].ToString();
                            estado = 10;
                            continue;
                        }
                        break;
                    case 11:
                        if (texto[i] == '/')
                        {
                            tokens.Add(new Token(18, "comentario", comentario, fila, columna));
                            comentario = "";
                            estado = 0;
                            continue;
                        }
                        else
                        {
                            erroreslexicos.Add(new Error("Lexico", "No se admite el caracter" + texto[i] + "en el alfabeto", fila, columna));
                        }
                        break;
                    case 12:
                        if (texto[i] == '"')
                        {
                            palabra += texto[i].ToString();
                            tokens.Add(new Token(16, "cadena", palabra, fila, columna));
                            palabra = "";
                            estado = 0;
                            continue;
                        }
                        else
                        {
                            palabra += texto[i].ToString();
                            estado = 12;
                        }
                        break;
                    case 13:
                        palabra += texto[i].ToString();
                        tokens.Add(new Token(16, "cadena", palabra, fila, columna));
                        palabra = "";
                        estado = 0;
                        break;
                    case 14:
                        if (texto[i] == '-')
                        {
                            estado = 15;
                        }
                        else
                        {
                            erroreslexicos.Add(new Error("Lexico", "No se admite el caracter" + texto[i] + "en el alfabeto", fila, columna));

                        }
                        break;
                    case 15:
                        if (texto[i].Equals((char)10))
                        {
                            tokens.Add(new Token(18, "comentario", comentario, fila, columna));
                            comentario = "";
                            estado = 0;
                            continue;
                        }
                        else
                        {
                            comentario += texto[i].ToString();
                            continue;
                        }
                        break;
                    case 16:
                        if (texto[i] == '/')
                        {
                            numero += texto[i].ToString();
                            estado = 17;
                            continue;
                        }
                        else
                        {
                            erroreslexicos.Add(new Error("Lexico", "No se admite el caracter" + texto[i] + "en el alfabeto", fila, columna));
                            continue;
                        }
                        break;

                    case 17:
                        if (char.IsDigit(texto[i]))
                        {
                            numero += texto[i].ToString();
                            estado = 18;
                            continue;
                        }
                        else
                        {
                            erroreslexicos.Add(new Error("Lexico", "No se admite el caracter" + texto[i] + "en el alfabeto", fila, columna));
                            continue;
                        }
                        break;
                    case 18:
                        if (char.IsDigit(texto[i]))
                        {
                            numero += texto[i].ToString();
                            estado = 19;
                            continue;
                        }
                        else
                        {
                            erroreslexicos.Add(new Error("Lexico", "No se admite el caracter" + texto[i] + "en el alfabeto", fila, columna));
                            continue;
                        }
                        break;
                    case 19:
                        if (texto[i] == '/')
                        {
                            numero += texto[i].ToString();
                            estado = 20;
                            continue;
                        }
                        else
                        {
                            erroreslexicos.Add(new Error("Lexico", "No se admite el caracter" + texto[i] + "en el alfabeto", fila, columna));
                            continue;
                        }
                        break;
                    case 20:
                        if (char.IsDigit(texto[i]))
                        {
                            numero += texto[i].ToString();
                            estado = 21;
                            continue;
                        }
                        else
                        {
                            erroreslexicos.Add(new Error("Lexico", "No se admite el caracter" + texto[i] + "en el alfabeto", fila, columna));
                            continue;
                        }
                        break;
                    case 21:
                        if (char.IsDigit(texto[i]))
                        {
                            numero += texto[i].ToString();
                            estado = 22;
                            continue;
                        }
                        else
                        {
                            erroreslexicos.Add(new Error("Lexico", "No se admite el caracter" + texto[i] + "en el alfabeto", fila, columna));
                            continue;
                        }
                        break;
                    case 22:
                        if (char.IsDigit(texto[i]))
                        {
                            numero += texto[i].ToString();
                            estado = 23;
                            continue;
                        }
                        else
                        {
                            erroreslexicos.Add(new Error("Lexico", "No se admite el caracter" + texto[i] + "en el alfabeto", fila, columna));
                            continue;
                        }
                        break;
                    case 23:
                        if (char.IsDigit(texto[i]))
                        {
                            numero += texto[i].ToString();
                            estado = 24;
                            continue;
                        }
                        else
                        {
                            erroreslexicos.Add(new Error("Lexico", "No se admite el caracter" + texto[i] + "en el alfabeto", fila, columna));
                            continue;
                        }
                        break;
                    case 24:
                        if (texto[i].Equals((char)39))
                        {
                            numero += texto[i].ToString();
                            tokens.Add(new Token(20, "fecha", numero, fila, columna));
                            numero = "";
                            estado = 0;
                            continue;
                        }
                        else
                        {
                            erroreslexicos.Add(new Error("Lexico", "No se admite el caracter" + texto[i] + "en el alfabeto", fila, columna));
                            continue;
                        }
                        break;
                    
                    case 25:
                        if (texto[i] == '=')
                        {
                            tokens.Add(new Token(17, "diferente", "!=", fila, columna));
                            estado = 0; ;
                            continue;
                        }
                        else
                        {
                            erroreslexicos.Add(new Error("Lexico", "No se admite el caracter" + texto[i] + "en el alfabeto", fila, columna));
                            continue;
                        }
                        break;
                    case 26:
                        if (texto[i] == '=')
                        {
                            tokens.Add(new Token(14, "mayor o igual", ">=", fila, columna));
                            estado = 0;
                            continue;
                        }
                        else
                        {
                            tokens.Add(new Token(6, "mayor", ">", fila, columna));
                            estado = 0;
                            continue;
                        }
                        break;
                    case 27:
                        if (texto[i] == '=')
                        {
                            tokens.Add(new Token(13, "menor o igual", "<=", fila, columna));
                            estado = 0;
                            continue;
                        }
                        else
                        {
                            tokens.Add(new Token(7, "menor", "<", fila, columna));
                            estado = 0;
                            continue;
                        }
                        break;
                    case 28:
                        estado = 0;
                        break;
                    case 29:
                        if (char.IsLetter(texto[i]))
                        {
                            estado = 29;
                            palabra += texto[i].ToString();
                            continue;
                        }else if (texto[i] == '_')
                        {
                            palabra += texto[i].ToString();
                            estado = 29;
                            continue;
                        }
                        else if (texto[i] == '=')
                        {
                            tokens.Add(new Token(1, "id", palabra, fila, columna));
                            tokens.Add(new Token(12, "igual", "=", fila, columna));
                            estado = 0;
                            palabra = "";
                            continue;
                        }
                        else if (texto[i] == ';')
                        {
                            tokens.Add(new Token(1, "id", palabra, fila, columna));
                            palabra = "";
                            estado = 0;
                            columna++;
                            tokens.Add(new Token(10, "punto y coma", ";", fila, columna));
                            continue;
                        }
                        else
                        {
                            tokens.Add(new Token(1, "id", palabra, fila, columna));
                            estado = 0;
                            palabra = "";
                            continue;
                        }
                        break;
                }
            }

        }
        
            public void verificar(String p)
            {
            palabra = p.ToLower();
            switch (palabra)
                {
                    case "tabla":
                    tokens.Add(new Token(27, "reservada tabla", p, fila, columna));
                    palabra = "";
                    estado = 0;
                    break;
                    case "crear":
                    tokens.Add(new Token(28, "reservada crear", p, fila, columna));
                    palabra = "";
                    estado = 0;
                    break;
                    case "insertar":
                    tokens.Add(new Token(23, "reservada insertar", p, fila, columna));
                    palabra = "";
                    estado = 0;
                    break;
                    case "actualizar":
                    tokens.Add(new Token(33, "reservada actualizar", p, fila, columna));
                    palabra = "";
                    estado = 0;
                    break;
                    case "eliminar":
                    tokens.Add(new Token(29, "reservada eliminar", p, fila, columna));
                    palabra = "";
                    estado = 0;
                    break;
                    case "establecer":
                    tokens.Add(new Token(21, "reservada establecer", p, fila, columna));
                    palabra = "";
                    estado = 0;
                    break;
                    case "modificar":
                    tokens.Add(new Token(30, "reservada modificar", p, fila, columna));
                    palabra = "";
                    estado = 0;
                    break;
                    case "y":
                    tokens.Add(new Token(38, "reservada y", p, fila, columna));
                    palabra = "";
                    estado = 0;
                    break;
                    case "o":
                    tokens.Add(new Token(39, "reservada o", p, fila, columna));
                    palabra = "";
                    estado = 0;
                    break;
                    case "en":
                    tokens.Add(new Token(32, "reservada en", p, fila, columna));
                    palabra = "";
                    estado = 0;
                    break;
                    case "valores":
                    tokens.Add(new Token(24, "reservada valores", p, fila, columna));
                    palabra = "";
                    estado = 0;
                    break;
                    case "seleccionar":
                    tokens.Add(new Token(26, "reservada seleccionar", p, fila, columna));
                    palabra = "";
                    estado = 0;
                    break;
                    case "donde":
                    tokens.Add(new Token(31, "reservada donde", p, fila, columna));
                    palabra = "";
                    estado = 0;
                    break;
                    case "como":
                    tokens.Add(new Token(22, "reservada como", p, fila, columna));
                    palabra = "";
                    estado = 0;
                    break;
                case "de":
                    tokens.Add(new Token(25, "reservada de", p, fila, columna));
                    palabra = "";
                    estado = 0;
                    break;
                case "entero":
                    tokens.Add(new Token(34, "reservada entero", p, fila, columna));
                    palabra = "";
                    estado = 0;
                    break;
                    case "cadena":
                    tokens.Add(new Token(35, "reservada cadena", p, fila, columna));
                    palabra = "";
                    estado = 0;
                    break;
                    case "fecha":
                    tokens.Add(new Token(37, "reservada fecha", p, fila, columna));
                    palabra = "";
                    estado = 0;
                    break;
                    case "flotante":
                    tokens.Add(new Token(36, "reservada flotante", p, fila, columna));
                    palabra = "";
                    estado = 0;
                    break;
                    default:
                    tokens.Add(new Token(1, "id", p, fila, columna));
                    break;
            }
            }

        
    }
    
}
