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
            //moliugu kiekis
            int kiekis;
            //superkamu moliugu svorio intervalo pradzia
            double pradzia;
            //superkamu moliugu svorio intervalo pabaiga
            double pabaiga;
            //saugome isaugintu moliugu svorius i masyva 
            double[] svoriai;
            //skaitome duomenu faila, priskiriame reiksmes
            SkaitytiDuomenis(tekstas, out kiekis, out pradzia, out pabaiga, out svoriai);

            double bendrasSvorioVidurkis = VidurkioSkaiciavimas(svoriai);

            int atrinktuKiekis;
            List<double> atrinkti;
            double atrinktuSvorioVidurkis;

            VidurkioSkaiciavimas(out atrinktuKiekis, out atrinkti, out atrinktuSvorioVidurkis, svoriai, pradzia, pabaiga);
            Spausdinimas(bendrasSvorioVidurkis, atrinktuKiekis, atrinktuSvorioVidurkis, atrinkti);
        }
         
        /// <summary>
        /// priskiriame kintamiesiems is failo nuskaitytas reiksmes, sukuriame masyva su moliugu svoriais
        /// </summary>
        /// <param name="tekstas"></param>
        /// <param name="kiekis"></param>
        /// <param name="pradzia"></param>
        /// <param name="pabaiga"></param>
        /// <param name="svoriai"></param>
        private static void SkaitytiDuomenis(string[] tekstas, out int kiekis, out double pradzia, out double pabaiga, out double[] svoriai)
        {
            string[] pirma = tekstas[0].Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            string[] antra = tekstas[1].Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            kiekis = Convert.ToInt32(pirma[0]);
            pradzia = Convert.ToDouble(pirma[1]);
            pabaiga = Convert.ToDouble(pirma[2]);
            svoriai = new double[kiekis];

            #region 

            for (int i = 0; i < kiekis; i++)
            {
                svoriai[i] = Convert.ToDouble(antra[i]);
            }
        }

        /// <summary>
        /// skaiciuojam visu moliugu svorio vidurki
        /// </summary>
        /// <param name="masyvas"></param>
        /// <returns></returns>
        private static double VidurkioSkaiciavimas(double[] masyvas)
        {
            double vidurkis = masyvas.Sum() / masyvas.Length;
            return vidurkis;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="atrinktuKiekis"></param>
        /// <param name="atrinkti"></param>
        /// <param name="atrinktuSvorioVidurkis"></param>
        /// <param name="masyvas"></param>
        /// <param name="pradzia"></param>
        /// <param name="pabaiga"></param>
        private static void VidurkioSkaiciavimas(out int atrinktuKiekis, 
            out List<double> atrinkti, out double atrinktuSvorioVidurkis, double[] masyvas, double pradzia, double pabaiga)
        {
            #region AtrinktuSarasas AtrinktuKiekis

            atrinkti = new List<double>();
            atrinktuKiekis = 0;                      
            for (int i = 0; i < masyvas.Length; i++)
            {
                if(masyvas[i] > pradzia && masyvas[i] < pabaiga)
                {
                    atrinkti.Add(masyvas[i]);
                    atrinktuKiekis++;
                }
            }

            #endregion

            #region Vidurkis

            atrinktuSvorioVidurkis = 0;
            if (atrinktuKiekis > 0)
            {
                atrinktuSvorioVidurkis = atrinkti.Sum() / atrinktuKiekis;
            }

            #endregion

        }

        /// <summary>
        /// spausdiname rezultatus i tekstini faila
        /// </summary>
        /// <param name="bendrasSvorioVidurkis"></param>
        /// <param name="atrinktuKiekis"></param>
        /// <param name="atrinktuSvorioVidurkis"></param>
        /// <param name="atrinkti"></param>
        private static void Spausdinimas(double bendrasSvorioVidurkis, int atrinktuKiekis, double atrinktuSvorioVidurkis, List<double> atrinkti)
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
