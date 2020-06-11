﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EscuelaDatos1
{
    public partial class Alumno : Form
    {
        //Variable contador
        int cont = 1;
        //Objeto de la clase Alumnos 
        Alumnos ObjAlumno = new Alumnos();
        StreamWriter objDatos;
        string almacenar = "";
        public Alumno()
        {
            //Tamaño del arreglo
            ObjAlumno.Materia = new string[102];
            ObjAlumno.Calificacion = new double[102];
            InitializeComponent();
        }

        private void tsbGuardar_Click(object sender, EventArgs e)
        {
            //Validación de la caja de texto número de control
            Regex reNumControl = new Regex("[0-9]{1,9}(/.[0-9]{0,2})?$", RegexOptions.Compiled);

            if (!reNumControl.IsMatch(txtNumControl.Text))
            {
                errorProvider1.SetError(txtNumControl, "Ingrese números en el campo número de control");
                txtNumControl.Focus();
                return;
            }
            errorProvider1.SetError(txtNumControl, "");

            //Validación de la caja de texto Carrera
            Regex reCarrera = new Regex("[a-zA-ZñÑ/s]", RegexOptions.Compiled);

            if (!reCarrera.IsMatch(txtCarrera.Text))
            {
                errorProvider1.SetError(txtCarrera, "Ingrese una carrera válida");
                txtCarrera.Focus();
                return;
            }
            errorProvider1.SetError(txtCarrera, "");

            //Validación de la caja de texto Materia
            Regex reMateria = new Regex("[a-zA-ZñÑ/s]{2,50}", RegexOptions.Compiled);

            if (!reMateria.IsMatch(txtMateria.Text))
            {
                errorProvider1.SetError(txtMateria, "Ingrese una materia válida");
                txtMateria.Focus();
                return;
            }
            errorProvider1.SetError(txtMateria, "");

            //Validación de la caja de texto calificación de la materia
            Regex reCalificacion = new Regex("[0-9]{1,9}(/.[0-9]{0,2})?$", RegexOptions.Compiled);

            if (txtCalMateria.Text == "")
            {
                errorProvider1.SetError(txtCalMateria, "Ingrese una calificación válida");
                txtCalMateria.Focus();
                return;
            }
            errorProvider1.SetError(txtCalMateria, "");

            //Condión que se cumplirá siempre y cuando, la longitud de los arreglos sea diferente de 102
            if (ObjAlumno.Materia.Length != 102)
            {
                ObjAlumno.NumControl = int.Parse(txtNumControl.Text);
                ObjAlumno.Carrera = txtCarrera.Text;

                //las propiedades tomaran la posición que vaya indicando el contador 
                ObjAlumno.Materia[cont] = txtMateria.Text;
                ObjAlumno.Calificacion[cont] = double.Parse(txtCalMateria.Text);

                //Método para limpiar el textbox
                txtMateria.Clear();
                txtCalMateria.Clear();
                //El contador incrementa de 1 en 1
                cont++;
            }
            //Sino se cumple la primera condición, entra a else
            else
            {
                ObjAlumno.NumControl = int.Parse(txtNumControl.Text);
                ObjAlumno.Carrera = txtCarrera.Text;
                //se incializa de nuevo el arreglo, pero ahora con un un tamaño de 100 y así, pueda entrar a la primera condición
                ObjAlumno.Materia = new string[100];
                //Se indica la posición del arreglo
                ObjAlumno.Materia[0] = txtMateria.Text;
                //se incializa de nuevo el arreglo, pero ahora con un tamaño de 100 y así, pueda entrar a la primera condición
                ObjAlumno.Calificacion = new double[100];
                //Se indica la posición del arreglo
                ObjAlumno.Calificacion[0] = double.Parse(txtCalMateria.Text);
                //Método para limpiar los textbox
                txtMateria.Clear();
                txtCalMateria.Clear();
            }
        }

        private void tsbImprimir_Click(object sender, EventArgs e)
        {
            //Método para que se limpien las filas de datagridview
            dgvAlumno.Rows.Clear();

            //Ciclo for que se cumplira siempre y cuando la variable i, sea menor a la 
            //Longitud del arreglo materia
            for (int i = 0; i < ObjAlumno.Materia.Length; i++)
            {
                //En caso de que el ciclo for se cumpla, entra a una condición que se cumplirá
                //siempre y cuando las posiciones del arreglo Materia no queden vacias y la posición del arreglo sea diferente de 0
                if (ObjAlumno.Materia[i] != "" && ObjAlumno.Calificacion[i] != 0)
                {
                    //Agrega la posicion de los arreglo al datagridview
                    dgvAlumno.Rows.Add(ObjAlumno.Materia[i], ObjAlumno.Calificacion[i]);
                }
                almacenar += ObjAlumno.NumControl + " ";
                almacenar += ObjAlumno.Carrera + " ";
                almacenar += ObjAlumno.Materia[i] + " ";
                almacenar += ObjAlumno.Calificacion[i] + " ";
                objDatos = new StreamWriter("Alumno1.txt");
                objDatos.Write(almacenar);
                objDatos.Close();
                Console.ReadLine();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            //Salir del form
            this.Close();
        }
    }
}
