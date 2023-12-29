using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace approx_zeri
{
    internal class funzione
    {
        public string FunzioneStr { get;set; }

        public double Valoreinx(double x)
        {
            string funcConVal = this.FunzioneStr.Replace("x", x.ToString());
            var result = new DataTable().Compute(funcConVal, null);
            return (double)result;
        }
        public double Derinx(double x) 
        {
            return (this.Valoreinx(x+0.000000001)+this.Valoreinx(x))/ 0.000000001;
        }
        public void stampafunzione(double x1, double x2) 
        { 
            
        }

    }
}
