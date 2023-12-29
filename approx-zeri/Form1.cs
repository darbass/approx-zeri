using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace approx_zeri
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        void plotfunzione(double x1, double x2, Funzione f)
        {
            double proporzionex = (x2-x1) / this.ClientSize.Width;
            double ymax = f.Valoreinx(x1);
            double ymin = f.Valoreinx(x1);
            double ciuccia = x1;

            for (int i = 1; i < this.ClientSize.Width; i++)
            {
                ciuccia += proporzionex;
                if(f.Valoreinx(x1)>ymax)
                {
                    ymax=f.Valoreinx(x1);
                }
                if(f.Valoreinx(x1)<ymin)
                {
                    ymin=f.Valoreinx(x1);
                }
            }
            double proprozioney = (ymax - ymin) / this.ClientSize.Height;

            Graphics g = this.CreateGraphics();
            SolidBrush pennelloCerchio = new SolidBrush(Color.Red);

            for (int i=0; i < this.ClientSize.Width;i++)
            {
                g.FillEllipse(pennelloCerchio, i, this.ClientSize.Height - (int)Math.Round((f.Valoreinx(x1)-ymin)), 5, 5);
                Console.WriteLine("({0},{1})", i, (int)Math.Round(f.Valoreinx(x1)));
                x1 += proporzionex;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Funzione funzione = new Funzione();
            funzione.FunzioneStr = "x+1";
            plotfunzione(-5, 30, funzione);
        }
    }
}
