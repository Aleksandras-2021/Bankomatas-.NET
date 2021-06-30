using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using System.Threading;

namespace Bankomatas_Klases
{



    class Tranzakcija
    {
        public static List<string> israsai = new List<string>();
        private string fileNameIsrasai = @"Israsai.txt";


        public void IssaugokSarasa(string fileNameIsrasai, List<string> israsas)
        {
            Console.WriteLine();
            File.WriteAllLines(fileNameIsrasai, israsas);
        }
        public void PridekIsemimoIsrasa(double suma)
        {

            israsai.Add($"- {suma} € {DateTime.Now}");
        }
        public void PridekInesimoIsrasa(double suma)
        {

            israsai.Add($"+ {suma} € {DateTime.Now}");
        }
        public void RodykIrasus(string text)
        {
            Console.Clear();
            if (israsai.Count > 0)
            {
                foreach (var item in israsai)
                {
                    Console.WriteLine(item);
                }
                Console.ReadLine();
                Console.Clear();

            }
            else// 
            {
                Console.WriteLine(text);// "Nėra išrašų"

                Thread.Sleep(1200);
                Console.Clear();

            }

        }
        public List<string> GetIsrasai()
        {
            return israsai;
        }
        public List<string> SetIsrasai(string fileNameIsrasai) => israsai = File.ReadAllLines(fileNameIsrasai).ToList();

        public string GetFilePathIsrasai()
        {
            return fileNameIsrasai;
        }






    }
}
//Papildyti bankomatą:

//-Sukurti klasę Tranzakcija, su laukais: Valiuta, Suma, MokejimoPaskirtis, Data.
//- Program.cs susikurti globalų tranzakcijų sąrašą.
//- Papildyti metodus "Įnešti pinigus" ir "Pasiimti pinigus" 
//kiekvienos operacijos metu sukurti po naują tranzakciją ir ją pridėtį į sąrašą.
//- Sukurti / papildyti metodą "Sąskaitos išrašas". Jis turi atspausdinti visas tranzakcijas iš tranzakcijų sąrašo.