using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
namespace Menu
{
    public partial class FrmPrincipalPacientes : Form
    {
        public FrmPrincipalPacientes()
        {
            InitializeComponent();
        }       
        public void CargarDatos()
        {
            MySqlConnection cnn = new MySqlConnection(Conexion.cad );
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT TblPacientes.Clave_Pacientes,TblPacientes.Nombre,TblPacientes.ApellidoPat,TblPacientes.ApellidoMat,TblPacientes.Domicilio,TblPacientes.Telefono,TblPacientes.Sexo,TblPacientes.fecha,TblPacientes.email FROM TblPacientes ", cnn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DgvDatos.DataSource = ds;
            DgvDatos.DataMember = ds.Tables[0].TableName;
        }
                        
        private void TsBtnNuevo_Click(object sender, EventArgs e)
        {
            FrmPacientes frmpacientes = new FrmPacientes();
            frmpacientes.ShowDialog();
            CargarDatos();
            frmpacientes.AutoClave();
            
            
        }
       
        private void TsBtnEditar_Click(object sender, EventArgs e)
        {
           
            FrmPacientes  n = new FrmPacientes();
            n.Text = "Editar registro de Paciente";
            n.TipoEstado = false;
            n.Clave = Convert.ToInt32(DgvDatos.SelectedRows[0].Cells[0].Value.ToString());
            n.ShowDialog();
            CargarDatos();
            DgvDatos.Sort(DgvDatos.Columns[0], ListSortDirection.Ascending);
        }

        private void FrmPrincipalPacientes_Load(object sender, EventArgs e)
        {
            CargarDatos();
           DgvDatos.Sort(DgvDatos.Columns[0], ListSortDirection.Ascending);
        }
       
        private void EliminarPaciente()
        {
            MySqlConnection conexion = new MySqlConnection(Conexion.cad);
            conexion.Open();
            MySqlCommand comando = new MySqlCommand("delete from TblPacientes where Clave_Pacientes = " + this.DgvDatos.SelectedRows[0].Cells[0].Value, conexion);
            comando.ExecuteNonQuery();

        }
        private void TsBtnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult respuesta;
            respuesta = MessageBox.Show("¿Seguro que deseas Eliminar el paciente?\n", "Eliminar...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                EliminarPaciente();
                CargarDatos();
                DgvDatos.Sort(DgvDatos.Columns[0], ListSortDirection.Ascending);
            }
        }

        private void TsBtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            String cad1;
            errorProvider1.Clear();
            try
            {
                foreach (DataGridViewRow fila in DgvDatos.Rows)
                {
                    TxtBuscar.Text = TxtBuscar.Text.ToUpper();
                    cad1 = fila.Cells[1].Value.ToString();
                    if (cad1.StartsWith(TxtBuscar.Text) == true)
                    {
                        fila.Selected = true;
                        break;
                    }
                }
                TxtBuscar.SelectionStart = TxtBuscar.Text.Length;
            }
            catch
            {
                errorProvider1.SetError(TxtBuscar,"No existe el Nombre...!!");
                TxtBuscar.Focus();
            }
         
        }

        private void TxtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
            base.OnKeyPress(e);
        }


      
       
            


       


    }
}
