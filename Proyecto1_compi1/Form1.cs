using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto1_compi1
{
    public partial class Form1 : Form
    {

        String palabra;
        String nombre;
        String comp;
        List<Token> tokens;
        List<Token> tokensconsulta;
        String lexemaactual = "";
        int posicion = 0;
        String parapintar = "";
        List<Tabla> tabla;
        RichTextBox aux = new RichTextBox();
        private string readText;
        String ruta = @"C:\Users\sohal\OneDrive\Escritorio";
        List<Error> erroreslexicos;
        List<Error> erroressintacticos;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OpenFileDialog archivo = new OpenFileDialog();
            archivo.Filter = "sals (*.sals)|*.sals";

            if (archivo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                nombre = Path.GetFileName(archivo.FileName);
                nombre = Path.GetFileNameWithoutExtension(archivo.FileName);

                comp = Path.GetFullPath(archivo.FileName);

                readText = File.ReadAllText(comp);
                Console.WriteLine(readText);

                richTextBox1.Text = readText;

            }
        }
        
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void ejecutarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Analizador_201700471 analizador = new Analizador_201700471(richTextBox1.Text);
            analizador.analizar();
            tokens = analizador.GetTokens();
            erroreslexicos = analizador.GetErroreslexicos();
            for (int i = 0; i<tokens.Count; i++)
            {
                Console.WriteLine(tokens[i].no + "\t"+tokens[i].tipo+"\t"+ tokens[i].lexema+"\t"+ tokens[i].fila+"\t"+ tokens[i].columna);
            }

            Sintactico_201700471 sintactico = new Sintactico_201700471(tokens);
            sintactico.analizar();
            erroressintacticos = sintactico.GetErroressintacticos();

            if (erroreslexicos.Count<=0 && erroressintacticos.Count<=0) {
                Ejecutar eje = new Ejecutar(tokens);
                eje.analizar();
                eje.imprimirtabla();
                tabla = eje.gettabla();
                pintar();
            }
           else
            {
               MessageBox.Show("El archivo contiene errores");
            }

            
        }


        public void pintar()
        {
            foreach (Token tkn in tokens)
            {
                if (tkn.tipo.Equals("id"))
                {
                    int index = 0;
                    while (index<=richTextBox1.Text.LastIndexOf(tkn.lexema))
                    {
                        richTextBox1.Find(tkn.lexema, index, richTextBox1.TextLength, RichTextBoxFinds.WholeWord);
                        richTextBox1.SelectionColor = Color.Brown;
                        index = richTextBox1.Text.IndexOf(tkn.lexema, index)+1;
                    }
                }
                continue;
            }

            foreach (Token tkn in tokens)
            {
                if (tkn.tipo.Equals("fecha"))
                {
                    int index = 0;
                    while (index <= richTextBox1.Text.LastIndexOf(tkn.lexema))
                    {
                        richTextBox1.Find(tkn.lexema, index, richTextBox1.TextLength, RichTextBoxFinds.WholeWord);
                        richTextBox1.SelectionColor = Color.Orange;
                        index = richTextBox1.Text.IndexOf(tkn.lexema, index) + 1;
                    }
                }
                continue;
            }

            foreach (Token tkn in tokens)
            {
                if (tkn.tipo.Equals("reservada crear") || tkn.tipo.Equals("tabla") || tkn.tipo.Equals("insertar") || tkn.tipo.Equals("eliminar") || tkn.tipo.Equals("actualizar"))
                {
                    int index = 0;
                    while (index <= richTextBox1.Text.LastIndexOf(tkn.lexema))
                    {
                        richTextBox1.Find(tkn.lexema, index, richTextBox1.TextLength, RichTextBoxFinds.WholeWord);
                        richTextBox1.SelectionColor = Color.Purple;
                        index = richTextBox1.Text.IndexOf(tkn.lexema, index) + 1;
                    }
                }
                continue;
            }

            foreach (Token tkn in tokens)
            {
                if (tkn.tipo.Equals("comentario"))
                {
                    int index = 0;
                    while (index <= richTextBox1.Text.LastIndexOf(tkn.lexema))
                    {
                        richTextBox1.Find(tkn.lexema, index, richTextBox1.TextLength, RichTextBoxFinds.WholeWord);
                        richTextBox1.SelectionColor = Color.LightGray;
                        index = richTextBox1.Text.IndexOf(tkn.lexema, index) + 1;
                    }
                }
                continue;
            }

            foreach (Token tkn in tokens)
            {
                if (tkn.tipo.Equals("entero") || tkn.tipo.Equals("flotante"))
                {
                    int index = 0;
                    while (index <= richTextBox1.Text.LastIndexOf(tkn.lexema))
                    {
                        richTextBox1.Find(tkn.lexema, index, richTextBox1.TextLength, RichTextBoxFinds.WholeWord);
                        richTextBox1.SelectionColor = Color.SkyBlue;
                        index = richTextBox1.Text.IndexOf(tkn.lexema, index) + 2;
                    }
                }
                continue;
            }

            foreach (Token tkn in tokens)
            {
                if (tkn.tipo.Equals("cadena"))
                {
                    int index = 0;
                    while (index <= richTextBox1.Text.LastIndexOf(tkn.lexema))
                    {
                        richTextBox1.Find(tkn.lexema, index, richTextBox1.TextLength, RichTextBoxFinds.WholeWord);
                        richTextBox1.SelectionColor = Color.Green;
                        index = richTextBox1.Text.IndexOf(tkn.lexema, index) + 1;
                    }
                }
                continue;
            }

            foreach (Token tkn in tokens)
            {
                if (tkn.tipo.Equals("mayor") || tkn.tipo.Equals("igual") || tkn.tipo.Equals("diferente") || tkn.tipo.Equals("mayor o igual") || tkn.tipo.Equals("menor o igual"))
                {
                    int index = 0;
                    while (index <= richTextBox1.Text.LastIndexOf(tkn.lexema))
                    {
                        richTextBox1.Find(tkn.lexema, index, richTextBox1.TextLength, RichTextBoxFinds.WholeWord);
                        richTextBox1.SelectionColor = Color.Red;
                        index = richTextBox1.Text.IndexOf(tkn.lexema, index) + 1;
                    }
                }
                continue;
            }


        }

        private void verTablasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            generarlista();
            Process.Start("C:\\Users\\sohal\\OneDrive\\Escritorio\\tokens.html");
        }

        public void generarlista()
        {
            StreamWriter vaciar = new StreamWriter(ruta + @"\tablas.html");
            vaciar.Write("");
            vaciar.Close();
            String filas = "";
            int columnas = 0;
            StreamWriter escribir = File.AppendText(ruta + @"\tokens.html");
            escribir.WriteLine("<html>\n<head>\n<title>Tablas</title>\n</head>\n<body>\n<center>");

            for (int i = 0; i<tabla.Count; i++)
            {
                escribir.WriteLine("\n<h1>"+tabla[i].nombre+"</h1>\n<table border=\"2px\">\n<tr>\n");
                for (int j = 0; j<tabla[i].filas.Count;j++)
                {
                     escribir.WriteLine("<td> " + tabla[i].filas[j].nombre + "</td>\n");
                    columnas = tabla[i].filas[j].columnas.Count;
                }
                escribir.WriteLine("</tr>");

                for (int j = 0; j < columnas; j++)
                {
                    escribir.WriteLine("<tr> ");
                    for (int k = 0; k<tabla[i].filas.Count;k++)
                    {
                        escribir.WriteLine("<td> " + tabla[i].filas[k].columnas[j] + " </td>\n");
                    }
                    escribir.WriteLine("</tr>");
                }
                escribir.WriteLine("</table>");
            }
            
            escribir.WriteLine("</center></body>\n</html>");
            escribir.Close();
        }


        private void richTextBox1_TextChanged(object sender, EventArgs e) 
        {
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            String h = richTextBox1.SelectedText;
            Console.WriteLine(h);
            Analizador_201700471 analizador = new Analizador_201700471(h);
            analizador.analizar();
            tokensconsulta = analizador.GetTokens();

            for (int i = 0; i < tokensconsulta.Count; i++)
            {
                Console.WriteLine(tokensconsulta[i].no + "\t" + tokensconsulta[i].tipo + "\t" + tokensconsulta[i].lexema + "\t" + tokensconsulta[i].fila + "\t" + tokensconsulta[i].columna);
            }

            Sintactico_201700471 sintactico = new Sintactico_201700471(tokensconsulta);
            sintactico.analizar();

            Ejecutar eje = new Ejecutar(tokens);
            eje.consulta(tokensconsulta, tabla);
            eje.analizarconsulta();
            tabla = eje.gettabla();
        }

        private void mostrarErroresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamWriter vaciar = new StreamWriter(ruta + @"\errores.html");
            vaciar.Write("");
            vaciar.Close();
            String filas = "";
            int columnas = 0;
            StreamWriter escribir = File.AppendText(ruta + @"\errores.html");
            escribir.WriteLine("<html>\n<head>\n<title>Errores</title>\n</head>\n<body>\n<center>\n<h1>Errores lexicos</h1>\n<table border=\"2px\">\n<tr><td>Tipo</td><td>Descripcion</td><td>Fila</td><td>Columna</td></tr>\n");

            for (int i = 0; i < erroreslexicos.Count; i++)
            {
                escribir.WriteLine("<tr> ");
                escribir.WriteLine("<td>"+erroreslexicos[i].tipo+"</td><td>" + erroreslexicos[i].descripcion + "</td><td>"+erroreslexicos[i].fila + "</td><td>"+erroreslexicos[i].columna + "</td>");
                escribir.WriteLine("</tr> ");
            }
            escribir.WriteLine("</table>");

            escribir.WriteLine("<h1>Errores Sintacticos</h1>\n<table border=\"2px\">\n<tr><td>Tipo</td><td>Descripcion</td><td>Fila</td><td>Columna</td></tr>\n");

            for (int i = 0; i < erroressintacticos.Count; i++)
            {
                escribir.WriteLine("<tr> ");
                escribir.WriteLine("<td>" + erroressintacticos[i].tipo + "</td><td>" + erroressintacticos[i].descripcion + "</td><td>" + erroressintacticos[i].fila + "</td><td>" + erroressintacticos[i].columna + "</td>");
                escribir.WriteLine("</tr> ");
            }
            escribir.WriteLine("</table>");
            escribir.WriteLine("</center></body>\n</html>");
            escribir.Close();
            Process.Start("C:\\Users\\sohal\\OneDrive\\Escritorio\\errores.html");
        }

        private void mostrarTokensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamWriter vaciar = new StreamWriter(ruta + @"\tokens.html");
            vaciar.Write("");
            vaciar.Close();
            String filas = "";
            int columnas = 0;
            StreamWriter escribir = File.AppendText(ruta + @"\tokens.html");
            escribir.WriteLine("<html>\n<head>\n<title>Tokens</title>\n</head>\n<body>\n<center>\n<h1>Tabla de tokens</h1>\n<table border=\"2px\">\n<tr><td>No.</td><td>Tipo</td><td>Lexema</td><td>Fila</td><td>Columna</td></tr>\n");

            for (int i = 0; i < tokens.Count; i++)
            {
                escribir.WriteLine("<tr> ");
                escribir.WriteLine("<td>" + tokens[i].no + "</td><td>" + tokens[i].tipo + "</td><td>" + tokens[i].lexema + "</td><td>" + tokens[i].fila + "</td><td>" + tokens[i].columna + "</td>");
                escribir.WriteLine("</tr> ");
            }
            escribir.WriteLine("</table>");
            escribir.WriteLine("</center></body>\n</html>");
            escribir.Close();
            Process.Start("C:\\Users\\sohal\\OneDrive\\Escritorio\\tokens.html");
        }

        private void mostrarArbolDeDerivaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("C:\\Users\\sohal\\OneDrive\\Escritorio\\arbol.jpg");
        }

        private void cargarTablasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String nombre = "";
            SaveFileDialog guardar = new SaveFileDialog();
            guardar.Filter = "sals|*.sals";
            guardar.Title = "sals";
            guardar.FileName = nombre + ".sals";
            var resultado = guardar.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                nombre = Path.GetFileNameWithoutExtension(guardar.FileName);
                ruta = Path.GetDirectoryName(guardar.FileName);
                StreamWriter escribir = new StreamWriter(guardar.FileName);
                escribir.WriteLine(richTextBox1.Text);
                escribir.Close();
            }
            }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void manualDeUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("C:\\Users\\sohal\\OneDrive\\Escritorio\\mu.pdf");
        }
        
        private void manualTecnicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("C:\\Users\\sohal\\OneDrive\\Escritorio\\mt.pdf");
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sohany Ayleen López Salazar 201700471");
        }
        
    }
}


