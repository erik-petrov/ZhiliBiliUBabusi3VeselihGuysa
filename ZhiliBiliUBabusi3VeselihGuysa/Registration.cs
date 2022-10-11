using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZhiliBiliUBabusi3VeselihGuysa
{
    public partial class Registration : Form
    {
        TextBox name, email, password;
        NumericUpDown vanus;
        CheckBox male;
        public Registration()
        {
            Height = 200;
            Width = 200;
            TableLayoutPanel tlp = new TableLayoutPanel();
            tlp.Dock = DockStyle.Fill;
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 30));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            Label nameL = new Label();
            nameL.Text = "Nimi";
            nameL.AutoSize = true;
            Label emailL = new Label();
            emailL.Text = "Email";
            emailL.AutoSize = true;
            Label passwordL = new Label();
            passwordL.Text = "Parool";
            passwordL.AutoSize = true;
            Label vanusL = new Label();
            vanusL.Text = "Vanus";
            vanusL.AutoSize = true;
            Label maleL = new Label();
            maleL.Text = "Mees?";
            maleL.AutoSize = true;
            name = new TextBox
            {

            };
            email = new TextBox
            {

            };
            password = new TextBox
            {

            };
            vanus = new NumericUpDown
            {

            };
            male = new CheckBox();
            Button btn = new Button
            {
                Text = "Registratsioon",
                Dock = DockStyle.Fill,
            };
            tlp.Controls.Add(nameL, 0, 0);
            tlp.Controls.Add(emailL, 0, 1);
            tlp.Controls.Add(passwordL, 0, 2);
            tlp.Controls.Add(vanusL, 0, 3);
            tlp.Controls.Add(maleL, 0, 4);
            tlp.Controls.Add(name, 1, 0);
            tlp.Controls.Add(email, 1, 1);
            tlp.Controls.Add(password, 1, 2);
            tlp.Controls.Add(vanus, 1, 3);
            tlp.Controls.Add(male, 1, 4);
            tlp.Controls.Add(btn, 0, 5);
            tlp.SetColumnSpan(btn, 2);
            btn.Click += Btn_Click;
            Controls.Add(tlp);
        }
        private void Btn_Click(object sender, EventArgs e)
        {
            //int sugu;
            SuguEnum sugu;
            if (CheckEmpty())
            {
                if (male.Checked)
                    sugu = SuguEnum.Male;
                else
                    sugu = SuguEnum.Female;
                if (User.TryReg(name.Text, password.Text, email.Text, sugu, (int)vanus.Value))
                {
                    MessageBox.Show("Edukas registratsioon!");
                    ZhiliBiliUBabusi3VeselihGuysa.Menu.HideButtons();
                    this.Close();
                }
            }
        }
        private bool CheckEmpty()
        {
            if (name.Text != "" && email.Text != "" && password.Text != "" && vanus.Value != 0 && vanus.Value > 0)
                return true;
            MessageBox.Show("Täida kõik väljad!");
            return false;
        }
    }
}
