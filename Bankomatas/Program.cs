using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
namespace Bankomatas
{

    class Program
    {
        private static double limitas;
        private static string teisingasPIN;
        private static int kalba;// 1 - LT, 2- ENG, 3- RUS.
        private static double balansas;//€
        private static List<string> israsai = new List<string>();
        #region FilePaths
        private static string fileNamePin = @"PIN.txt";//Pradinis PIN 1234
        private static string fileNameBalance = @"Balansas.txt";
        private static string fileNameIsrasai = @"Israsai.txt";
        private static string fileNameLimit = @"Limit.txt";
        #endregion
        static void Main(string[] args)
        {
            Bankomatas();
            Console.ReadLine();
        }

        static void Bankomatas()
        {
            try
            {
                Console.OutputEncoding = Encoding.UTF8;
                Skaityk(fileNamePin);
                SkaitykBalansa(fileNameBalance);
                SkaitykIsrasa(fileNameIsrasai);
                SkaitykLimita(fileNameLimit);
                PasirinkKalba();
                Prisijungimas();
                Menu();

            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
                Thread.Sleep(2000);
                Console.Clear();
                Bankomatas();
            }
        }

        static int PasirinkKalba()
        {
            Console.WriteLine("1 - Lietuvių\n2 - English\n3 - Pусский");
            kalba = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            return kalba;
        }
        static void Prisijungimas()
        {
            int bandymai = 3;
            while (true)
            {
                Console.WriteLine(Tekstas(1));
                string prisijungimoPIN = Console.ReadLine();
                if (prisijungimoPIN == teisingasPIN && bandymai > 0)
                {
                    Console.Clear();
                    Console.WriteLine(Tekstas(0));
                    Thread.Sleep(890);
                    Console.Clear();
                    Menu();
                    break;
                }
                else if (prisijungimoPIN != teisingasPIN)
                {
                    bandymai--;
                    Console.WriteLine(Tekstas(2) + bandymai);
                    if (bandymai <= 0)
                    {
                        Console.Clear();
                        Console.WriteLine(Tekstas(3));

                        Thread.Sleep(2000);
                        Environment.Exit(0);
                    }
                }
                else
                {
                    Console.WriteLine("Ivyko klaida");
                }



            }
        }
        static string Tekstas(int fraze)
        {
            //Enum

            // Dictionary<string, string> LT_ENG = new Dictionary<string, string>();
            //LT_ENG.Add("Sveiki", "Hello");
            // LT_ENG.Add("Iveskite PIN", "Enter your PIN");

            //Console.WriteLine(LT_ENG.ge);

            List<string> lt = new List<string>()
            {
                "Sveiki",
                "Iveskite PIN",
                "Neteisingas PIN kodas, jūsų likusių bandymų skaičius: ",
                "Paskyra užblokuota",
                "1 - Keisti kalbą\n2 - Keisti PIN kodą\n3 - Sąskaitos likuti\n4 - Įnešti pinigus\n5 - Pasiimti pinigus\n6 - Sąskaitos israsas\n7 - Baigti darbą",
                "Iveskite nauja PIN: ",//5
                "Pin per trumpas",//6
                "PIN kodo apsaugos lygis yra silpnas",//7
                "PIN kodo apsaugos lygis yra vidutinis",//8
                "PIN kodo apsaugos lygis yra didelis",//9
                "Spauskit ENTER, kad grįžti atgal",//10
                "PIN sėkmingai pakeistas, naujas PIN {0}",//11
                "Ar tikrai norite sita PIN naudoti? 1-TAIP/0-NE",//12
                "Balansas: ",//13
                "Pavyko!",//14
                "Nėra išrašų",//15
                "Suma, kurią norite išimti(10)(20)(50)(100): ",//16
                "Suma, kurią norite įnešti(10)(20)(50)(100): ",//17
                "Viršijate savo limitą ",//18
                "Jūs tiek pinigų neturite ",//19
                "Bankomatas priėma tik 10€, 20€, 50€, 100€ banknotus",//20
                "PIN kodas privalo būti sudarytas tik iš skaičių",//21
                "Limitas: "//22

            };
            List<string> en = new List<string>()
            {
                "Hello",//0
                "Enter your PIN",//1
                "Incorrect PIN code, number of attempts you have left: ",//2
                "Account blocked",//3
                "1 - Change language\n2 - Change pin\n3 - Check Balance\n4 - Deposit Money\n5 - Withdraw Money\n6 - Bank statements\n7 - Log out",//4
                "Input your new PIN: ",//5
                "Your PIN is too short",//6
                "New PIN strength is weak",//7
                "New PIN strength is medium",//8
                "New PIN strength is high",//9
                "Press ENTER to go back",//10
                "PIN successfully changed, new PIN {0}",//11
                "Do you really want to use this PIN? 1-YES/0-NO",//12
                "Balance: ",//13
                "Success!",//14
                "There are no bank statements ",//15
                "Sum that you wish to withdraw(10)(20)(50)(100): ",//16
                "Sum, that you wish to deposit(10)(20)(50)(100): ",//17
                "You have exeeced your limit ",//18
                "You do not have that much money ",//19
                "The ATM accepts only 10€, 20€, 50€, 100€ banknotes ",//20
                "PIN code must only contain digits",//21
                "Limit: "
            };
            List<string> ru = new List<string>()
            {
                "Привет",
                "Введите свой PIN-код",
                "Неверный ПИН-код, количество оставшихся попыток:",
                "Аккаунт заблокирован",
                "1 - Изменение языка\n2 - Изменить пин-код\n3 - Остаток на счету\n4 - Депозит деньги\n5 - Снять деньги\n6 - Банковские выписки\n7 - Выйти",
                "Введите новый PIN-код:",//5
                "Ваш PIN-код слишком короткий ",//6
                "Новый PIN-код слабый",//7
                "Новый PIN-код имеет средний уровень надежности",//8
                "Новый PIN-код надежен",//9
                "Нажмите ENTER, чтобы вернуться",//10
                "PIN-код успешно изменен, новый PIN-код {0}",//11
                "Вы действительно хотите использовать этот PIN-код? 1-ДА / 0-НЕТ",//12
                "Баланс: ",//13
                "успех!",//14
                "Нет банковских выписок",//15
                "Сумма, которую вы хотите снять(10)(20)(50)(100): ",//16
                "Сумма, которую вы хотите внести(10)(20)(50)(100): ",//17
                "Вы превысили свой лимит ",//18
                "У вас не так много денег",//19
                "Банкомат принимает только банкноты номиналом 10€, 20€, 50€, 100€ ",//20
                "ПИН-код должен содержать только цифры",//21
                "Лимит: "
            };

            switch (kalba)
            {
                case 1:
                    return lt[fraze];
                case 2:
                    return en[fraze];
                case 3:
                    return ru[fraze];
                default:
                    return lt[fraze];
            }
        }
        static void PasirinkVeiksma()
        {
            string pasirinkimas = Console.ReadLine();
            while (true)
            {
                switch (pasirinkimas)
                {
                    case "1":
                        Console.Clear();
                        PasirinkKalba();
                        Thread.Sleep(1000);
                        Console.Clear();
                        Menu();
                        break;
                    case "2":
                        Console.Clear();
                        PakeiskPin();
                        break;
                    case "3":
                        SaskaitosLikutis();
                        Thread.Sleep(2000);
                        Console.Clear();
                        PasirinkVeiksma();
                        break;
                    case "4":
                        InestiPinigus();
                        break;
                    case "5":
                        PasiimtiPinigus();
                        break;
                    case "6":
                        RodykIrasus();
                        break;
                    case "7":
                        Exit();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Tokio pasirinkimo nera\n");
                        Menu();
                        break;

                }
            }

        }
        static void Menu()
        {

            Console.WriteLine(Tekstas(4));

            PasirinkVeiksma();
        }
        static bool Patvirtinimas()
        {
            bool patvirtinimas;
            Console.WriteLine(Tekstas(12));//AR TIKRAI NORITE NAUDOTI SITA PIN? 1-TAIP/0-NE
            string pasirinkimas = Console.ReadLine();
            if (pasirinkimas == "1" || pasirinkimas == "TAIP")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static void PakeiskPin()
        {
            string PIN;
            Console.Clear();

            bool arPakeite = false;
            while (!arPakeite)
            {
                Console.Write(Tekstas(5));//Iveskit nauja PIN KODA
                PIN = Console.ReadLine();
                if (PIN.All(char.IsDigit))//Patikrina ar tikrai visur yra skaiciai
                {
                    if (PIN.ToString().Length < 4)
                    {
                        Console.WriteLine(Tekstas(6));//PIN PER TRUMPAS
                        Thread.Sleep(999);
                        Console.Clear();
                    }
                    else if (PIN.Length == 4)
                    {
                        Console.WriteLine(Tekstas(7));//PIN kodo apsaugos lygis yra silpnas
                        arPakeite = Patvirtinimas();


                    }
                    else if (PIN.Length > 4 && PIN.ToString().Length <= 6)
                    {
                        Console.WriteLine(Tekstas(8));//PIN kodo apsaugos lygis yra vidutinis
                        arPakeite = Patvirtinimas();
                    }
                    else
                    {
                        Console.WriteLine(Tekstas(9));//PIN kodo apsaugos lygis yra didelis
                        arPakeite = Patvirtinimas();
                    }
                    if (arPakeite)
                    {
                        Console.WriteLine(Tekstas(11), PIN);
                        teisingasPIN = PIN;
                        RasykPin(fileNamePin, PIN);
                        Console.WriteLine(Tekstas(10));
                        Console.ReadLine();
                        Console.Clear();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(Tekstas(21));//PIN kodas privalo būti sudarytas tik iš skaičių
                    Thread.Sleep(2000);
                    Console.Clear();
                    PakeiskPin();
                }

            }
            Menu();
        }
        static void SaskaitosLikutis()
        {
            Console.Clear();
            Console.WriteLine(Tekstas(13) + balansas + " €");
            Console.WriteLine(Tekstas(10));
            Console.ReadLine();
            Console.Clear();
            Menu();
        }
        static void PasiimtiPinigus()
        {

            Console.Clear();
            Console.Write(Tekstas(16));
            double isemimoSuma = Convert.ToDouble(Console.ReadLine());

            if (isemimoSuma % 10 == 0 || isemimoSuma % 20 == 0 || isemimoSuma % 50 == 0 || isemimoSuma % 100 == 0)
            {
                if (isemimoSuma <= balansas && isemimoSuma <= limitas && isemimoSuma > 0)
                {

                    limitas -= isemimoSuma;
                    israsai.Add($"- {isemimoSuma} € {DateTime.Now}");
                    RasykBalansa(fileNameBalance, balansas - isemimoSuma);
                    balansas -= isemimoSuma;
                    IssaugokSarasa(fileNameIsrasai, israsai);
                    IssaugokLimita(fileNameLimit, limitas);
                    Console.WriteLine(Tekstas(22) + limitas + "\n");
                    Console.WriteLine(Tekstas(14) + " " + Tekstas(10));
                    Console.ReadLine();
                    Console.Clear();
                }
                else if (isemimoSuma > balansas)
                {
                    Console.Clear();
                    Console.WriteLine(Tekstas(19));//JUS TIEK PINIGU NETURIT
                    Thread.Sleep(2000);
                    Console.Clear();
                }
                else if (isemimoSuma > limitas || limitas <= 0)
                {
                    Console.Clear();
                    Console.WriteLine(Tekstas(18));//VIRSIJAT SAVO LIMITA
                    Thread.Sleep(2000);
                    Console.Clear();
                    PasiimtiPinigus();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine(Tekstas(20));
                Thread.Sleep(1002);
                Console.Clear();
            }
            Menu();
        }
        static void InestiPinigus()
        {
            double inesimoSuma;
            Console.Clear();
            Console.Write(Tekstas(17)); // "Suma, kurią norite įnešti: "
            inesimoSuma = Convert.ToDouble(Console.ReadLine());
            if (inesimoSuma % 10 == 0 || inesimoSuma % 20 == 0 || inesimoSuma % 50 == 0 || inesimoSuma % 100 == 0)
            {
                if (inesimoSuma > 0)
                {
                    RasykBalansa(fileNameBalance, balansas + inesimoSuma);
                    balansas += inesimoSuma;
                    israsai.Add($"+ {inesimoSuma} € {DateTime.Now}");

                    IssaugokSarasa(fileNameIsrasai, israsai);

                    Console.WriteLine(Tekstas(14) + " " + Tekstas(10));
                    Console.ReadLine();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine(Tekstas(20));
                Thread.Sleep(1001);
                Console.Clear();
                InestiPinigus();
            }

            Console.Clear();
            Menu();

        }
        static void RodykIrasus()
        {
            Console.Clear();
            if (israsai.Count > 0)
            {
                foreach (var item in israsai)
                {
                    Console.WriteLine(item);
                }
                Thread.Sleep(500);
                Console.WriteLine("\n" + Tekstas(10));
                Console.ReadLine();
                Console.Clear();
                Menu();
            }
            else// 
            {
                Console.WriteLine(Tekstas(15));// "Nėra išrašų"

                Thread.Sleep(1200);
                Console.Clear();
                Menu();
            }


        }
        static void Exit()
        {
            Console.Clear();
            switch (kalba)
            {
                case 1:
                    Console.WriteLine("Visogero");
                    break;

                case 2:
                    Console.WriteLine("Goodbye");
                    break;
                case 3:
                    Console.WriteLine("До свидания");
                    break;
                default:
                    Console.WriteLine("Iki, bet klaidingai");
                    break;

            }
            Thread.Sleep(1000);
            Environment.Exit(0);
        }
        #region FileStream
        static void RasykPin(string fileName, string newPin)
        {
            File.WriteAllText(fileName, newPin);
        }
        static void RasykBalansa(string fileName, double bal)
        {
            var Balansas = bal.ToString();
            File.WriteAllText(fileName, Balansas);
        }
        static void Skaityk(string fileName)
        {
            teisingasPIN = File.ReadAllText(fileName);

        }
        static void SkaitykBalansa(string fileNameBalance)
        {
            balansas = Convert.ToDouble(File.ReadAllText(fileNameBalance));
        }
        static void IssaugokSarasa(string fileNameIsrasai, List<string> israsas)
        {
            Console.WriteLine();
            File.WriteAllLines(fileNameIsrasai, israsas);
        }
        static void SkaitykIsrasa(string fileNameIsrasai)
        {
            //  string[] lines = File.ReadAllLines(fileNameIsrasai);

            israsai = File.ReadAllLines(fileNameIsrasai).ToList();//File yra STRING ToList() pavercia i LIST

        }
        static void SkaitykLimita(string fileNameLimit)
        {
            limitas = Convert.ToDouble(File.ReadAllText(fileNameLimit));

        }
        static void IssaugokLimita(string fileNameLimit, double limit)
        {
            var limitas = limit.ToString();

            File.WriteAllText(fileNameLimit, limitas);
        }


        #endregion
    }
}
//Bankomatas:

//1.Pasirinkti kalba; LT, EN, RUS
//2.Pasisveikinti pasirinkta kalba(naudoti Switch)
//3.Prašome įvesti PIN kodą. Įvedus 3 kartus neteisingai, užblokuoti kortelę. (naudoti ciklą)
//4.Parodyti meniu:
//-Keisti kalbą
//- Keisti PIN kodą(naujas pin kodas turi sutapti su senuoju, tik tada galima įvesti naują)
//-Sąskaitos likutis
//- Sąskaitos išrašas(sąrašas su operacijomis)
//- Įnešti pinigus(sąskaitos likutis padidėja)
//- Pasiimti pinigus(sąskaitos likutis sumažėja)
//- Baigti darbą