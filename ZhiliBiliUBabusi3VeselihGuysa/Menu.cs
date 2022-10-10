using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Windows.Forms;

namespace ZhiliBiliUBabusi3VeselihGuysa
{
    public partial class Menu : Form
    {
        static Button log, reg, logout;
        static Label loggedAs;
        public static string loggedEmail, loggedName;
        public Menu()
        {
            loggedEmail = "";
            loggedName = "";
            reg = new Button
            {
                Text = "Registratsioon",
                Dock = DockStyle.Fill
            };
            reg.Click += (object sender, EventArgs e) => new Registration().Show();
            log = new Button
            {
                Text = "Login",
                Dock = DockStyle.Fill
            };
            log.Click += (object sender, EventArgs e) => new Login().Show();
            logout = new Button
            {
                Text = "Logi välja",
                Dock = DockStyle.Fill,
            };
            loggedAs = new Label();
            loggedAs.Text = "Nimi: " + loggedName;
            logout.Click += (object s, EventArgs e) => { loggedEmail = ""; loggedName = ""; ShowButtons(); };
            Button btn1 = new Button
            {
                Text = "Pildi asi",
                Dock = DockStyle.Fill
            };
            btn1.Click += (object sender, EventArgs e) => new Picture().Show();
            Button btn2 = new Button
            {
                Text = "Matemaatika asi",
                Dock = DockStyle.Fill
            };
            btn2.Click += (object sender, EventArgs e) => new Math(loggedName).Show();
            Button btn3 = new Button
            {
                Text = "Matši asi",
                Dock = DockStyle.Fill
            };
            btn3.Click += (object sender, EventArgs e) => new Match(loggedName).Show();
            Button btn4 = new Button
            {
                Text = "Edetabelid",
                Dock = DockStyle.Fill
            };
            btn4.Click += (object sender, EventArgs e) => new Leaderboard(loggedName).Show();
            Button changeName = new Button
            {
                Text = "Nimeta ümber"
            };
            changeName.Click += ChangeName_Click;
            TableLayoutPanel tlp = new TableLayoutPanel();
            tlp.Dock = DockStyle.Fill;
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            tlp.Controls.Add(btn1, 0, 0);
            tlp.Controls.Add(btn2, 0, 1);
            tlp.Controls.Add(btn3, 0, 2);
            tlp.Controls.Add(btn4, 0, 3);
            tlp.Controls.Add(changeName, 2, 4);
            tlp.Controls.Add(log, 0, 4);
            tlp.Controls.Add(reg, 1, 4);
            loggedAs.Visible = false;
            logout.Visible = false;
            tlp.Controls.Add(loggedAs, 0, 4);
            tlp.Controls.Add(logout, 1, 4);
            tlp.SetColumnSpan(btn1, 3);
            tlp.SetColumnSpan(btn2, 3);
            tlp.SetColumnSpan(btn3, 3);
            tlp.SetColumnSpan(btn4, 3);
            Controls.Add(tlp);
            //InitializeComponent();
        }
        private void ChangeName_Click(object sender, EventArgs e)
        {
            if (LoggedIn())
            {
                string newName = Interaction.InputBox("Name", "Rename");
                try
                {
                    User.Rename(loggedName, newName, loggedEmail);
                    loggedName = newName;
                    loggedAs.Text = "Nimi: " + loggedName;
                }
                catch(Exception err)
                {
                    MessageBox.Show(err.Message, "Unable to rename user.");
                }
            }
            else
            {
                MessageBox.Show("Empty name field.");
            }
        }
        public static void ShowButtons()
        {
            logout.Visible = false;
            loggedAs.Visible = false;
            reg.Visible = true;
            log.Visible = true;
        }
        public static void HideButtons()
        {
            logout.Visible = true;
            loggedAs.Visible = true;
            loggedAs.Text = "Nimi: "+loggedName;
            reg.Visible = false;
            log.Visible = false;
        }
        private static bool LoggedIn()
        {
            if (loggedEmail != "" && loggedName != "")
                return true;
            else
                return false;
        }
    }
}
