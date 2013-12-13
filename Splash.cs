using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Menu
{
    public partial class FrmSplash : Form
    {
        public FrmSplash()
        {
            InitializeComponent();
        }

        int z;
        FrmMenu frmmenu = new FrmMenu();

        private void FrmSplash_Load(object sender, EventArgs e)
        {
            this.TmrProgreso.Enabled = true;
            this.TmrProgreso.Interval = 70;
        }

        private void TmrProgreso_Tick(object sender, EventArgs e)
        {
            z += 2;
            if (z > 100)
            {
                this.Hide();
                this.TmrProgreso.Enabled = false;
                frmmenu.Show();
                return;
            }
            this.PgbCargando.Value = z;
            this.LblNumero.Text = PgbCargando.Value.ToString();  
        }
    }
}
