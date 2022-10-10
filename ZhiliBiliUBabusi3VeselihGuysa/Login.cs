using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZhiliBiliUBabusi3VeselihGuysa
{
    public partial class Login : Form
    {
        TextBox name, password;
        public Login()
        {
            TableLayoutPanel tlp = new TableLayoutPanel();
            tlp.Dock = DockStyle.Fill;
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 40));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 40));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            Label nameL = new Label();
            nameL.Text = "Nimi";
            Label emailL = new Label();
            emailL.Text = "Email";
             name = new TextBox
            {
                
            };
             password = new TextBox
            {

            };
            Button btn = new Button
            {
                Text = "Login",
                Dock = DockStyle.Fill,
            };
            tlp.Controls.Add(nameL, 0, 0);
            tlp.Controls.Add(emailL, 0, 1);
            tlp.Controls.Add(name, 1, 0);
            tlp.Controls.Add(password, 1, 1);
            tlp.Controls.Add(btn,2, 0);
            tlp.SetColumnSpan(btn, 2);
            btn.Click += Btn_Click;
            Controls.Add(tlp);
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            if(name.Text != "" && password.Text != "")
            {
                if (User.TryLogin(name.Text, password.Text))
                {
                    MessageBox.Show("Edukas login!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
        }
    }
}
