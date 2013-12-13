using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Drawing.Printing;
namespace Menu
{
    public partial class FrmVerHistorial : Form
    {
        public FrmVerHistorial()
        {
            InitializeComponent();
        }
        public void CargarDatos()
        {
            MySqlConnection cnn = new MySqlConnection(Conexion.cad);
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT TblConsultas.Folio,TblConsultas.Fecha,TblConsultas.Clave_Pacientes,TblPacientes.Nombre,TblPacientes.ApellidoPat, TblPacientes.ApellidoMat, TblPacientes.Domicilio,TblDoctores.Nombre as Doctor,TblConsultas.Hora, TblConsultas.Estatura, TblConsultas.Peso, TblConsultas.Temperatura, TblConsultas.Precion,TblConsultas.Sintomas,TblConsultas.Enfermedad FROM TblConsultas INNER JOIN TblDoctores ON TblDoctores.Clave_Doctores=TblConsultas.Clave_Doctores INNER JOIN TblPacientes ON TblPacientes.Clave_Pacientes=TblConsultas.Clave_Pacientes WHERE TblConsultas.Clave_Pacientes='" + CmbPacientes.SelectedValue.ToString() + "'", cnn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DgvDatos.DataSource = ds;
            DgvDatos.DataMember = ds.Tables[0].TableName;
        }

        public void CargarDatos2()
        {
            MySqlConnection cnn = new MySqlConnection(Conexion.cad);
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT TblConsultas.Folio,TblConsultas.Fecha,TblConsultas.Clave_Pacientes,TblPacientes.Nombre,TblPacientes.ApellidoPat, TblPacientes.ApellidoMat, TblPacientes.Domicilio,TblDoctores.Nombre as Doctor,TblConsultas.Hora, TblConsultas.Estatura, TblConsultas.Peso, TblConsultas.Temperatura, TblConsultas.Precion,TblConsultas.Sintomas,TblConsultas.Enfermedad FROM TblConsultas INNER JOIN TblDoctores ON TblDoctores.Clave_Doctores=TblConsultas.Clave_Doctores INNER JOIN TblPacientes ON TblPacientes.Clave_Pacientes=TblConsultas.Clave_Pacientes WHERE TblConsultas.Clave_Pacientes='" + CmbPacientes.SelectedValue.ToString() + "'and TblConsultas.fecha BETWEEN '" + DtpDel.Text + "' AND '" + DtpAl.Text + "'", cnn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DgvDatos.DataSource = ds;
            DgvDatos.DataMember = ds.Tables[0].TableName;
        }
        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RdbMostrarPeriodos_CheckedChanged(object sender, EventArgs e)
        {
            this.LblAl.Visible = true;
            this.LblDel.Visible = true;
            this.DtpDel.Visible = true;
            this.DtpAl.Visible = true;
        }

        private void RdbMostrarTodos_CheckedChanged(object sender, EventArgs e)
        {
            this.LblAl.Visible = false;
            this.LblDel.Visible = false;
            this.DtpDel.Visible = false;
            this.DtpAl.Visible = false;
        }

        private void CargaPacientes()
        {
            MySqlConnection cnn = new MySqlConnection(Conexion.cad);
            MySqlDataAdapter da = new MySqlDataAdapter("select  Clave_Pacientes,Nombre from TblPacientes", cnn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            CmbPacientes.DataSource = ds.Tables[0];
            CmbPacientes.DisplayMember = "Nombre";
            CmbPacientes.ValueMember = "Clave_Pacientes";
        }


        private void FrmVerHistorial_Load(object sender, EventArgs e)
        {
            CargaPacientes();
        }

        private void BtnMostrar_Click(object sender, EventArgs e)
        {
             if (RdbMostrarTodos.Checked == true)
            {
                 CargarDatos();
            }
          
            else 
            {
                if (DtpDel.Value <= DtpAl.Value)
                {
              
                    CargarDatos2();
                }
                else
                {
                    MessageBox.Show("La fecha Inicial debe ser Menor o Igual a la fecha Final", "Ver Historial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

    }
}
