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
    public partial class Menu : Form
    {
        public Menu()
        {
            Button btn1 = new Button
            {
                Text = "Picture thing",
                Dock = DockStyle.Fill
            };
            btn1.Click += (object sender, EventArgs e) => new Picture().Show();
            Button btn2 = new Button
            {
                Text = "Math thing",
                Dock = DockStyle.Fill
            };
            btn2.Click += (object sender, EventArgs e) => new Math().Show();
            Button btn3 = new Button
            {
                Text = "Match thing",
                Dock = DockStyle.Fill
            };
            btn3.Click += (object sender, EventArgs e) => new Match().Show();
            TableLayoutPanel tlp = new TableLayoutPanel();
            tlp.Dock = DockStyle.Fill;
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 33));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 33));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 33));
            tlp.Controls.Add(btn1, 0, 0);
            tlp.Controls.Add(btn2, 0, 1);
            tlp.Controls.Add(btn3, 0, 2);
            Controls.Add(tlp);
            //InitializeComponent();
        }
    }
}
