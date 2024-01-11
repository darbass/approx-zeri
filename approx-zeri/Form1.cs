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
        public double x1 = 1;
        public double x2 = 4;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            PlotFunzione();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Refresh();
            PlotFunzione();
            ApprossimazioneBisezione(x1,x2);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Refresh();
            PlotFunzione();
            ApprossimazioneSecante(x1, x2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Refresh();
            PlotFunzione();
            ApprossimazioneTangente(x1);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            this.Refresh();
            PlotFunzione();
        }


        public static double funzione(double x)
        { return Math.Sin(x); }
        public double derivata(double x)
        {
            return (funzione(x + 0.000000001) + funzione(x)) / 0.000000001;
        }

        public void MaxMin(ref double max, ref double min)
        {
            double passo = (x2 - x1) / this.ClientSize.Width;
            for (double x = x1; x <= x2; x += passo)
            {
                if (funzione(x) > max)
                {
                    max = funzione(x);
                }
                if (funzione(x) < min)
                {
                    min = funzione(x);
                }
            }
        }
        public void Proporzionecoord(double x,  double y, ref double FormX, ref double FormY)
        {
            double maxY = 0;
            double minY = 0;
            MaxMin(ref maxY, ref minY);

            double proporzionex = (x2 - x1) / this.ClientSize.Width;
            double proporzioney = (maxY - minY) / this.ClientSize.Height;

            FormX = (x - x1) / proporzionex;
            FormY = (y - minY) / proporzioney;
        }
        public void PlotFunzione()
        {         
            Graphics g = this.CreateGraphics();
            SolidBrush pennelloCerchio2 = new SolidBrush(Color.Blue);
            Pen lineazero = new Pen(Color.Black);
            if((x1*x2)>0)
            g.DrawLine(lineazero, 0, this.ClientSize.Height / 2, this.ClientSize.Width, this.ClientSize.Height / 2);

            
            double maxY = 0;
            double minY = 0;
            MaxMin(ref maxY, ref minY);

            
            double proporzionex = (x2 - x1)/this.ClientSize.Width ;
            double proporzioney = (maxY - minY) / this.ClientSize.Height;

            
            for (double x = x1; x <= x2; x += proporzionex)
            {
                double y = funzione(x); 

                int formx = (int)((x - x1) / proporzionex);
                int formy = (int)((y - minY) / proporzioney);
                g.FillEllipse(pennelloCerchio2, formx, this.ClientSize.Height - formy, 3, 3);
            }
        }   
        public void ApprossimazioneSecante(double x1, double x2)
        {
            Graphics g = this.CreateGraphics();
            Pen pennello = new Pen(Color.Red,3);

            int iterazione = 0;
            double zero = funzione(x1);
            double controllo = 0;
            double FormX1 = 0;
            double FormY1 = 0;
            double FormX2 = 0;
            double FormY2 = 0;

            if (funzione(x1) * funzione(x2) < 0)
            {
                while ((zero != controllo) || iterazione < 5)
                {
                    controllo = zero;
                    zero = x2 - (funzione(x2) * (x2 - x1)) / (funzione(x2) - funzione(x1));
                    
                    Proporzionecoord(x1, funzione(x1), ref FormX1, ref FormY1);
                    Proporzionecoord(x2, funzione(x2), ref FormX2, ref FormY2);
                    g.DrawLine(pennello, (int)FormX1, (int)(this.ClientSize.Height-FormY1), (int)FormX2, (int)(this.ClientSize.Height-FormY2));


                    if (funzione(x1) * funzione(zero) > 0)
                        x1 = zero;
                    else
                        x2 = zero;
                    iterazione++;
                }
                Risultato.Text = string.Format("zero f(x)={0}", zero);
            }
        }
        public void ApprossimazioneBisezione(double x1, double x2)
        {
            Graphics g = this.CreateGraphics();
            Pen pennello = new Pen(Color.Red,3);

            int iterazione = 0;
            double zero = funzione(x1);
            double controllo = 0;
            double FormX2 = 0;
            double FormY2 = 0;

            if (funzione(x1) * funzione(x2) < 0)
            {
                while ((zero != controllo) || iterazione < 25)
                {
                    controllo = zero;
                    zero = (x1 + x2) / 2;
                    Proporzionecoord(zero, funzione(x2), ref FormX2, ref FormY2);
                    g.DrawLine(pennello, (int)Math.Round(FormX2), (int)(this.ClientSize.Height), (int)Math.Round(FormX2), (int)0);

                    if (funzione(zero) * funzione(x1) < 0)
                    {
                        x2 = zero;
                    }
                    else
                    {
                        x1 = zero;
                    }
                    Console.WriteLine(zero);
                    iterazione++;

                }
                Risultato.Text = string.Format("zero f(x)={0}", zero);
            }
        }
        public void ApprossimazioneTangente(double x1)
        {
            Graphics g = this.CreateGraphics();
            Pen pennello = new Pen(Color.Red, 3);
            double zero = x1;
            int iterazione = 0;
            double FormX1 = 0;
            double FormY1 = 0;
            double FormX2 = 0;
            double FormY2 = 0;
            if (funzione(x1) * funzione(x2) < 0 || iterazione > 5)
            {
                while ((zero != x1) || iterazione < 15)
                {
                    x1 = zero;
                    zero = zero - funzione(zero) / derivata(zero);


                    Proporzionecoord(zero, funzione(zero), ref FormX1, ref FormY1);
                    Proporzionecoord(x2, derivata(x2) * (x2) + funzione(x2) / (derivata(x2) * (x2)), ref FormX2, ref FormY2);
                    g.DrawLine(pennello, (int)FormX1, (int)(this.ClientSize.Height - FormY1), (int)FormX2, (int)(this.ClientSize.Height - FormY2));

                    Console.WriteLine(zero);

                    iterazione++;
                }
                Risultato.Text = string.Format("zero f(x)={0}", zero);
            }
        }

        
    }
}
