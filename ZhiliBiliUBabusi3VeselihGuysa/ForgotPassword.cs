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
    public partial class ForgotPassword : Form
    {
        TextBox name, email;
        NumericUpDown vanus;
        public ForgotPassword()
        {
            Width = 200;
            Height = 150;
            TableLayoutPanel tlp = new TableLayoutPanel();
            tlp.Dock = DockStyle.Fill;
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            Label nameL = new Label();
            nameL.Text = "Nimi";
            nameL.AutoSize = true;
            Label emailL = new Label();
            emailL.Text = "Email";
            emailL.AutoSize = true;
            Label vanusL = new Label();
            vanusL.Text = "Vanus";
            vanusL.AutoSize = true;
            name = new TextBox
            {

            };
            email = new TextBox
            {

            };
            vanus = new NumericUpDown
            {

            };
            Button btn = new Button
            {
                Text = "Unustasin parooli",
                Dock = DockStyle.Fill,
                AutoSize = true,
            };
            tlp.Controls.Add(nameL, 0, 0);
            tlp.Controls.Add(emailL, 0, 1);
            tlp.Controls.Add(vanusL, 0, 2);
            tlp.Controls.Add(name, 1, 0);
            tlp.Controls.Add(email, 1, 1);
            tlp.Controls.Add(vanus, 1, 2);
            tlp.Controls.Add(btn, 0, 3);
            tlp.SetColumnSpan(btn, 2);
            btn.Click += (object s, EventArgs e) => User.ForgotPassword(name.Text, email.Text, (int)vanus.Value);
            Controls.Add(tlp);
        }
    }
}
