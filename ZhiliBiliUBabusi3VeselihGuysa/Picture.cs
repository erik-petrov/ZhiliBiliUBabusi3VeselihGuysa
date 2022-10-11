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
    public partial class Picture : Form
    {
        OpenFileDialog ofd;
        ColorDialog cd;
        PictureBox pb;
        CheckBox cb;
        public Picture()
        {
            Width = 900;
            Height = 900;
            TableLayoutPanel tlp = new TableLayoutPanel { Dock = DockStyle.Fill};
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 85));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 90));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 5));
            pb = new PictureBox { Dock = DockStyle.Fill, BorderStyle = BorderStyle.Fixed3D};
            tlp.Controls.Add(pb);
            tlp.SetColumnSpan(pb, 2);
            cb = new CheckBox
            {
                Text = "Venitada"
            };
            cb.CheckedChanged += Cb_CheckedChanged;
            FlowLayoutPanel flp = new FlowLayoutPanel { Dock = DockStyle.Fill};
            Button close = new Button {Text = "Sulge", AutoSize = true};
            close.Click += Close_Click;
            Button bg = new Button { Text = "Määrake taustavärv", AutoSize = true };
            bg.Click += Bg_Click;
            Button clr = new Button { Text = "Tühjenda pilt", AutoSize = true };
            clr.Click += Clr_Click;
            Button show = new Button { Text = "Näita pilti", AutoSize = true };
            show.Click += Show_Click;
            flp.Controls.Add(cb);
            flp.Controls.Add(close);
            flp.Controls.Add(bg);
            flp.Controls.Add(show);
            flp.Controls.Add(clr);
            tlp.Controls.Add(flp);

            ofd = new OpenFileDialog { Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp|All files (*.*)|*.*",
            Title = "Valige pildifail"
            };
            cd = new ColorDialog();
            Controls.Add(tlp);
            //InitializeComponent();
        }

        private void Cb_CheckedChanged(object sender, EventArgs e)
        {
            if (cb.Checked)
            {
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                pb.SizeMode = PictureBoxSizeMode.Normal;
            }
        }

        private void Show_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pb.Load(ofd.FileName);
            }
        }
        private void Clr_Click(object sender, EventArgs e) => pb.Image = null;
        private void Bg_Click(object sender, EventArgs e)
        {
            if (cd.ShowDialog() == DialogResult.OK)
                pb.BackColor = cd.Color;
        }
        private void Close_Click(object sender, EventArgs e) => Close();
    }
}
