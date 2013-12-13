using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
//using System.Drawing.Printing;
namespace Menu
{
    public partial class FrmVerCitas : Form
    {
        public FrmVerCitas()
        {
            InitializeComponent();
        }
        private void RdbMostrarTodo_CheckedChanged(object sender, EventArgs e)
        {
            
            LblSelectDoctor.Visible = false;
            CmbDoctores.Visible = false;
        }

        private void RdbMostraPorDoctor_CheckedChanged(object sender, EventArgs e)
        {
            LblSelectDoctor.Visible = true;
            CmbDoctores.Visible = true;
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void CargaDoctores()
        {
            MySqlConnection cnn = new MySqlConnection(Conexion.cad);
            MySqlDataAdapter da = new MySqlDataAdapter("select  Clave_Doctores,Nombre from TblDoctores", cnn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            CmbDoctores.DataSource = ds.Tables[0];
            CmbDoctores.DisplayMember = "Nombre";
            CmbDoctores.ValueMember = "Clave_Doctores";
        }
        private void CargarDatos()
        {
            MySqlConnection cnn = new MySqlConnection(Conexion.cad);
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT TblCitas.FolioCitas,TblCitas.Clave_Pacientes,TblCitas.Nombre,TblCitas.ApellidoPat,TblDoctores.Nombre as Doctor,TblCitas.Hora,TblCitas.Estatura,TblCitas.Peso,TblCitas.Temperatura,TblCitas.Precion,TblCitas.fecha FROM TblCitas INNER JOIN TblDoctores ON TblDoctores.Clave_Doctores=TblCitas.Clave_Doctores WHERE TblCitas.fecha BETWEEN '" + DtpDel.Text + "' AND '" + DtpAl.Text + "'", cnn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DgvDatos.DataSource = ds;
            DgvDatos.DataMember = ds.Tables[0].TableName;
        }
        private void CargarDatos2()
        {
            MySqlConnection cnn = new MySqlConnection(Conexion.cad);
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT TblCitas.FolioCitas,TblCitas.Clave_Pacientes,TblCitas.Nombre,TblCitas.ApellidoPat,TblDoctores.Nombre as Doctor,TblCitas.Hora,TblCitas.Estatura,TblCitas.Peso,TblCitas.Temperatura,TblCitas.Precion,TblCitas.fecha FROM TblCitas INNER JOIN TblDoctores ON TblDoctores.Clave_Doctores=TblCitas.Clave_Doctores WHERE TblCitas.Clave_Doctores='" + CmbDoctores.SelectedValue.ToString() + "'and TblCitas.fecha BETWEEN '" + DtpDel.Text + "' AND '" + DtpAl.Text + "'", cnn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DgvDatos.DataSource = ds;
            DgvDatos.DataMember = ds.Tables[0].TableName;
        }
        private void BtnMostrar_Click(object sender, EventArgs e)
        {
           
                if (DtpDel.Value <= DtpAl.Value)
            {

                if (RdbMostrarTodo.Checked == true)
                    CargarDatos();
                else
                    CargarDatos2();
            }
            else
            {
                MessageBox.Show("La fecha Inicial debe ser Menor o Igual a la fecha Final", "Ver citas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void FrmVerCitas_Load(object sender, EventArgs e)
        {
            CargaDoctores();
        }
       
    }
}
