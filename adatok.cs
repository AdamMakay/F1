using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace autok
{
    public class adatok
    {
        private List<string> adat;
        private int ora;
        private int perc;
        private string rendszam;
        private int mertSebesseg;

        public adatok(string Rendszam, int Ora,int Perc,int MertSebesseg) 
        {
          
                rendszam = Rendszam;
                ora = Ora;
                perc = Perc;
                mertSebesseg = MertSebesseg;
        }

        public int Ora { get => ora;}
        public int Perc { get => perc;}
        public string Rendszam { get => rendszam;}
        public int MertSebesseg { get => mertSebesseg;}

        public int osszPerc => ora * 60 + perc;
    }
}
