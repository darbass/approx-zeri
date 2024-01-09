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
            
            double ymax = f.Valoreinx(x1);
            double ymin = f.Valoreinx(x1);
            double ciuccia = x1;
            double proporzionex = this.ClientSize.Width / (x2 - x1);
            double proprozioney = (ymax - ymin) / this.ClientSize.Height;

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
            

            Graphics g = this.CreateGraphics();
            SolidBrush pennelloCerchio = new SolidBrush(Color.Red);

            for (int i=0; i < this.ClientSize.Width;i++)
            {
                g.FillEllipse(pennelloCerchio, i, this.ClientSize.Height - (int)Math.Round((ymax-f.Valoreinx(x1))/ proprozioney), 5, 5);
                Console.WriteLine("({0},{1})", i, (int)Math.Round(f.Valoreinx(x1)));
                x1 += proporzionex;
            }
        }

        public void esperimento()
        {
            double startX = -25;
            double endX = 20;
            

            Graphics g = this.CreateGraphics();
            SolidBrush pennelloCerchio = new SolidBrush(Color.Red);
            SolidBrush pennelloCerchio2 = new SolidBrush(Color.Blue);
            
            // Trova il massimo e il minimo della tua funzione
            double maxY = 1.76;
            double minY = -1.76;

            // Calcola i fattori di scala per x e y
            double proporzionex = (endX - startX)/this.ClientSize.Width ;
            double proporzioney = (maxY - minY) / this.ClientSize.Height;

            // Stampa i punti sulla console
            for (double x = startX; x <= endX; x += proporzionex)
            {
                double y = Math.Sin(x)+Math.Sin(0.5*x); // Sostituisci con la tua funzione

                int screenx1 = (int)((x - startX) /proporzionex);
                int screeny1 = (int)((y-minY) / proporzioney);
                g.FillEllipse(pennelloCerchio2, screenx1, this.ClientSize.Height - screeny1, 5, 5);
            }
        }

        public static double funzione(double x)
        { return Math.Sin(x); }
        public void approssimazionesec(double x1, double x2)
        {
            double zero = funzione(x1);
            double controllo = 0;
            Graphics g = this.CreateGraphics();
            Pen pennello = new Pen(Color.Blue);

            if (funzione(x1) * funzione(x2) < 0)
            {
                while (zero != controllo)
                {
                    controllo = zero;
                    zero = x2 - (funzione(x2) * (x2 - x1)) / (funzione(x2) - funzione(x1));
                    g.DrawLine(pennello, 10, 10, 10, 10);


                    if (funzione(x1) * funzione(zero) > 0)
                    {
                        x1 = zero;
                    }
                    else
                        x2 = zero;
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Funzione funzione = new Funzione();
            funzione.FunzioneStr = "x+1";
            //plotfunzione(-5, 30, funzione);
            esperimento();
        }
    }
}
