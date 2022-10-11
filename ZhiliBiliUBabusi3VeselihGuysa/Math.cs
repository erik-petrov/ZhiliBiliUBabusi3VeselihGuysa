using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZhiliBiliUBabusi3VeselihGuysa
{
    public partial class Math : Form
    {
        Random rnd = new Random();
        char[] symbols = new char[] { '+', '-', '*', '/' };
        int plusOne, plusTwo;
        int mulOne, mulTwo;
        int divOne, divTwo;
        int minOne, minTwo;
        int timeLeft;
        string loggedEmail;
        TableLayoutPanel tlp;
        Timer timer;
        Label lb;
        Button start;
        public Math(string email)
        {
            loggedEmail = email;
            Text = "Math Quiz";
            Width = 500;
            Height = 400;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
             lb = new Label {
                 Font = new Font(Font.FontFamily, 18),
                 AutoSize = false,
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(200, 30),
                Location = new Point(250, 10)
            };
            Label label = new Label
            {
                Font = new Font(Font.FontFamily, (float)15.75),
                Text = "Time Left",
                AutoSize = true,
                Location = new Point(140, 10)
            };
            start = new Button {
                Text = "Start the quiz",
                Font = new Font(Font.FontFamily, 14),
                AutoSize = true,
                TabIndex = 0,
                Location = new Point(150, 315),
            };
            start.Click += Start_Click;
             timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
             tlp = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
            };
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            for (int i = 0; i < 5; i++)
                tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            for (int i = 1; i < 5; i++)
            {
                Label num1 = new Label
                {
                    Font = new Font(FontFamily.GenericSansSerif, 18),
                    Text = "?",
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(60, 60),
                };
                Label sign = new Label
                {
                    Font = new Font(FontFamily.GenericSansSerif, 18),
                    Text = symbols[i-1].ToString(),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(60, 60),
                };
                Label num2 = new Label
                {
                    Font = new Font(FontFamily.GenericSansSerif, 18),
                    Text = "?",
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(60, 60),
                };
                Label equals = new Label
                {
                    Font = new Font(FontFamily.GenericSansSerif, 18),
                    Text = "=",
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(60, 60),
                };
                NumericUpDown N = new NumericUpDown
                {
                    Font = new Font(FontFamily.GenericSansSerif, 18),
                    Width = 100,
                    TabIndex = i+1,
                };
                tlp.Controls.Add(num1, 0 ,i);
                tlp.Controls.Add(sign, 1 ,i);
                tlp.Controls.Add(num2, 2 ,i);
                tlp.Controls.Add(equals, 3 ,i);
                tlp.Controls.Add(N, 4 ,i);
            }
            tlp.Controls.Add(lb, 3, 0);
            tlp.SetColumnSpan(label, 2);
            tlp.SetColumnSpan(lb, 2);
            tlp.Controls.Add(label, 1, 0);
            tlp.SetColumnSpan(start, 2);
            tlp.Controls.Add(start, 2, 5);
            Controls.Add(tlp);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            NumericUpDown N = (NumericUpDown)tlp.GetControlFromPosition(4, 1);
            NumericUpDown minN = (NumericUpDown)tlp.GetControlFromPosition(4, 2);
            NumericUpDown mulN = (NumericUpDown)tlp.GetControlFromPosition(4, 3);
            NumericUpDown divN = (NumericUpDown)tlp.GetControlFromPosition(4, 4);
            if (CheckTheAnswer())
            {
                timer.Stop();
                MessageBox.Show("Sa said kõik vastused õiged!", "Palju õnne");
                start.Enabled = true;
                HelperFunctions.SaveResults(loggedEmail, timeLeft, true);
            }
            else if (timeLeft > 0)
            {
                timeLeft = timeLeft - 1;
                lb.Text = timeLeft + " seconds";
            }
            else
            {
                timer.Stop();
                lb.Text = "Aeg on läbi!";
                MessageBox.Show("Sa ei lõpetanud õigeks ajaks.", "Vabandust!");
                N.Value = plusOne + plusTwo;
                minN.Value = minOne - minTwo;
                mulN.Value = mulOne * mulTwo;
                divN.Value = divOne / divTwo;
                start.Enabled = true;
            }
        }

        private void Start_Click(object sender, EventArgs e)
        {
            StartTheQuiz();
            start.Enabled = false;
        }

        private bool CheckTheAnswer()
        {
            NumericUpDown N = (NumericUpDown)tlp.GetControlFromPosition(4, 1);
            NumericUpDown minN = (NumericUpDown)tlp.GetControlFromPosition(4, 2);
            NumericUpDown mulN = (NumericUpDown)tlp.GetControlFromPosition(4, 3);
            NumericUpDown divN = (NumericUpDown)tlp.GetControlFromPosition(4, 4);
            if ((plusOne + plusTwo == N.Value)
                && (minOne - minTwo == minN.Value)
                && (mulOne * mulTwo == mulN.Value)
                && (divOne / divTwo == divN.Value))
                return true;
            else
                return false;
        }

        public void StartTheQuiz()
        {
            for (int row = 1; row < 5; row++)
            {
                Label num1 = (Label)tlp.GetControlFromPosition(0, row);
                Label symbol = (Label)tlp.GetControlFromPosition(1, row);
                Label num2 = (Label)tlp.GetControlFromPosition(2, row);
                NumericUpDown N = (NumericUpDown)tlp.GetControlFromPosition(4, row);
                int[] thing = getNums(symbol.Text);
                num1.Text = thing[0].ToString();
                num2.Text = thing[1].ToString();
                N.Value = 0;
            }
            timeLeft = 30;
            lb.Text = "30 seconds";
            timer.Start();
        }
        public int[] getNums(string sym)
        {
            int num1 = 0;
            int num2 = 0;
            switch (sym)
            {
                case "+":
                    num1 = rnd.Next(51);
                    num2 = rnd.Next(51);
                    plusOne = num1;
                    plusTwo = num2;
                    break;
                case "-":
                    num1 = rnd.Next(1, 101);
                    num2 = rnd.Next(1, num1);
                    minOne = num1;
                    minTwo = num2;
                    break;
                case "/":
                    num2 = rnd.Next(2, 11);
                    int temporaryQuotient = rnd.Next(2, 11);
                    num1 = num2 * temporaryQuotient;
                    divOne = num1;
                    divTwo = num2;
                    break;
                case "*":
                    num1 = rnd.Next(2, 11);
                    num2 = rnd.Next(2, 11);
                    mulOne = num1;
                    mulTwo = num2;
                    break;
            }
            return new int[2] { num1, num2 };
        }
    }
}