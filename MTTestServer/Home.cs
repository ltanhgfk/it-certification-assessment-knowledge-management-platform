using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
using System.Windows.Forms;
using System.Globalization;


namespace ThiTracNghemUDCNTT
{
    public partial class Home : Form
    {
        //private Login login;

        public Home()
        {
            InitializeComponent();
        }
        /*
        public Home(Login login)
        {
            // TODO: Complete member initialization
            this.login = login;
        }*/

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.TopMost = true;
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.FormBorderStyle = System.Windows.Forms.
            this.WindowState = FormWindowState.Maximized;
            
            //this.Hide();
            //Login frm = new Login();
            //frm.Show();
        }

        private void btDethi_Click(object sender, EventArgs e)
        {
            LoginDialog frm = new LoginDialog();
            frm.ShowDialog();
        }

        private void btThisinh_Click(object sender, EventArgs e)
        {
            formThisinh frm = new formThisinh();
            frm.ShowDialog();
        }

        private void Home_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btAccount_Click(object sender, EventArgs e)
        {
            LoginDialogAccount frm = new LoginDialogAccount();
            frm.ShowDialog();
        }

        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        //private void Home_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    Application.Exit();
        //}

        //private void btKythi_Click(object sender, EventArgs e)
        //{
        //    //getImageFromWord frm = new getImageFromWord();
        //    //frm.ShowDialog();
        //    formKythi frm = new formKythi();
        //    frm.ShowDialog();
        //}

        //----------------------------------------------------------------------------------------------------------------//
    }
}
