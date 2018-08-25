using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ktu10_2
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] tekstas =
                 System.IO.File.ReadAllLines(@"C:\Users\Andrius\Documents\Visual Studio 2017\ktu\ktu10-2\ktu10-2\duomenys.txt");
            int kiekis;
            //svorio intervalo pradzia
            double pradzia;
            //svorio intervalo pabaiga
            double pabaiga;
            double[] masyvas;

            skaitytiDuomenis(tekstas, out kiekis, out pradzia, out pabaiga, out masyvas);
            double bendrasSvorioVidurkis = vidurkioSkaiciavimas(masyvas);

            int atrinktuKiekis;
            List<double> atrinkti;
            double atrinktuSvorioVidurkis;

            vidurkioSkaiciavimas(out atrinktuKiekis, out atrinkti, out atrinktuSvorioVidurkis, masyvas, pradzia, pabaiga);
            spausdinimas(bendrasSvorioVidurkis, atrinktuKiekis, atrinktuSvorioVidurkis, atrinkti);
        }
         
        private static void skaitytiDuomenis(string[] tekstas, out int kiekis, out double pradzia, out double pabaiga, out double[] masyvas)
        {
            string[] pirma = tekstas[0].Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            string[] antra = tekstas[1].Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            kiekis = Convert.ToInt32(pirma[0]);
            pradzia = Convert.ToDouble(pirma[1]);
            pabaiga = Convert.ToDouble(pirma[2]);
            masyvas = new double[kiekis];
            for(int i = 0; i < kiekis; i++)
            {
                masyvas[i] = Convert.ToDouble(antra[i]);
            }
        }

        private static double vidurkioSkaiciavimas(double[] masyvas)
        {
            double vidurkis = masyvas.Sum() / masyvas.Length;
            return vidurkis;
        }

        private static void vidurkioSkaiciavimas(out int atrinktuKiekis, 
            out List<double> atrinkti, out double atrinktuSvorioVidurkis, double[] masyvas, double pradzia, double pabaiga)
        {
            atrinkti = new List<double>();
            atrinktuKiekis = 0;
            atrinktuSvorioVidurkis = 0;
            for (int i = 0; i < masyvas.Length; i++)
            {
                if(masyvas[i] > pradzia && masyvas[i] < pabaiga)
                {
                    atrinkti.Add(masyvas[i]);
                    atrinktuKiekis++;
                }
            }
            
            if(atrinktuKiekis > 0)
            {
                atrinktuSvorioVidurkis = atrinkti.Sum() / atrinktuKiekis;
            }            
        }

        private static void spausdinimas(double bendrasSvorioVidurkis, int atrinktuKiekis, double atrinktuSvorioVidurkis, List<double> atrinkti)
        {
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(@"C:\Users\Andrius\Documents\Visual Studio 2017\ktu\ktu10-2\ktu10-2\rezultatai.txt"))
            {
                file.WriteLine("Visų moliūgų vidutinis svoris: " + String.Format("{0:0.000}", bendrasSvorioVidurkis) + " kg.");
                if (atrinktuKiekis == 0)
                {
                    file.WriteLine("Į supirkimo punktą atrinktų moliūgų nėra.");
                }
                else
                {
                    file.Write("Į supirkimą atrinkta: " + atrinktuKiekis + " vnt.\nVidutinis atrinktų moliūgų svoris: "
                        + String.Format("{0:0.000}", atrinktuSvorioVidurkis) + " kg.\nAtrinktų moliūgų svorių sąrašas:");

                    foreach (var svoris in atrinkti)
                    {
                        file.Write(String.Format("{0: 0.000}", svoris) + " kg");
                    }
                    file.Write(".");
                }
            }
        }
    }
}
