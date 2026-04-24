using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace autok
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //autó rendszáma,a jeladás idejének óra, illetve perc értéke, valamint a jeladáskor mért sebesség, km / h mértékegységben
            List<adatok> AutokAdatai = new List<adatok>();

            foreach (var line in File.ReadLines("jeladas.txt"))
            {
                string[] darabolt = line.Split('\t');
                AutokAdatai.Add(new adatok(darabolt[0], int.Parse(darabolt[1]), int.Parse(darabolt[2]), int.Parse(darabolt[3])));

            }
            var utolsoJelOra = AutokAdatai.Max(x => x.Ora);
            var utolsoJelPerc = AutokAdatai.Where(x => x.Ora == utolsoJelOra).Max(x => x.Perc);
            var Autorendszam = AutokAdatai.Where(x => x.Ora == utolsoJelOra && x.Perc == utolsoJelPerc).Select(x => x.Rendszam).FirstOrDefault();
            Console.WriteLine($"Az utolsó jeladás időpontja {utolsoJelOra}:{utolsoJelPerc}, a jármű rendszáma {Autorendszam}");

            var elsoRendszam = AutokAdatai[0].Rendszam;
            var elsoRenszamIdoPontjai = AutokAdatai.Where(x => x.Rendszam == elsoRendszam).Select(x => new { x.Ora, x.Perc }).ToList();
            Console.Write($"Az első jármű: {elsoRendszam}\nJeladásainak időpontjai: ");

            foreach (var item in elsoRenszamIdoPontjai)
            {
                Console.Write($"{item.Ora}:{item.Perc} ");
            }

            Console.WriteLine("");
            Console.Write("Kérem adja meg az orát: ");
            string ora = Console.ReadLine();
            Console.Write("Kérem adja meg a percet: ");
            string perc = Console.ReadLine();
            var JeladasokSzama = AutokAdatai.Where(x => x.Ora == int.Parse(ora) && x.Perc == int.Parse(perc)).Count();
            Console.WriteLine($"A jeladások száma: {JeladasokSzama}");

            var legnagyobbSebesseg = AutokAdatai.Max(x => x.MertSebesseg);
            var legnagyobbSebessegRendszama = AutokAdatai.Where(x => x.MertSebesseg == legnagyobbSebesseg).Select(x => new { x.Rendszam }).ToList();
            Console.WriteLine($"A legnagyobb sebesség km/h: {legnagyobbSebesseg}");
            Console.Write("A járművek: ");
            foreach (var item in legnagyobbSebessegRendszama)
            {
                Console.Write($"{item.Rendszam} ");
            }
            Console.WriteLine("");
            Console.Write("Kérem, adja meg a rendszámot: ");
            string rendszam = Console.ReadLine();

            var tav = AutokAdatai.Where(x => x.Rendszam == rendszam).OrderBy(x => x.osszPerc).ToList();

            float osszTav = 0;
            if (tav.Count > 0)
            { 
                Console.WriteLine($"{tav[0].Ora}:{tav[0].Perc} 0,0 km");
                for (var i = 0; i < tav.Count - 1; i++)
                {
                    var idokulonbseg = tav[i + 1].osszPerc - tav[i].osszPerc;
                    var sebesseg = tav[i].MertSebesseg;
                    osszTav += (float)sebesseg * (idokulonbseg / 60.0f);
                    var ido = $"{tav[i + 1].Ora}:{tav[i + 1].Perc}";

                    Console.WriteLine($"{ido} {Math.Round(osszTav, 1)} km");
                }
            }
            else
            {
                Console.WriteLine("Nincs ilyen rendszámú jármű!");
            }


                var rendszamok = AutokAdatai.Select(x => x.Rendszam).Distinct().ToList();
            int elsoOra;
            int elsoPerc;
            int utolsoOra;
            int utolsoPerc;
            using (StreamWriter sw = new StreamWriter("ido.txt"))
            {
                foreach (var item in rendszamok)
                {
                    elsoOra = AutokAdatai.Where(x => x.Rendszam == item).Min(x => x.Ora);
                    elsoPerc = AutokAdatai.Where(x => x.Rendszam == item && x.Ora == elsoOra).Min(x => x.Perc);
                    utolsoOra = AutokAdatai.Where(x => x.Rendszam == item).Max(x => x.Ora);
                    utolsoPerc = AutokAdatai.Where(x => x.Rendszam == item && x.Ora == utolsoOra).Max(x => x.Perc);
                    sw.WriteLine($"{item} {elsoOra} {elsoPerc} {utolsoOra} {utolsoPerc}");
                }
            }

        }
    }
}
