using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.WeatherSource;
using System.IO;
using System.Threading;

namespace Weather
{
    class Program
    {
        static string TIME1 = "09:00";
        static string TIME2 = "15:00";
        static void Main(string[] args)
        {
            Timer t = new Timer(Iteration, null, 0, 60000);
            //StreamWriter sw = new StreamWriter(Directory.GetCurrentDirectory() + "\\db.db", true, System.Text.Encoding.Default, 2048);
            //Parse();
            //OpenWeatherMapParser.Parse(sw);
            Console.WriteLine("Нажмите любую кнопку");
            Console.ReadKey();
        }

        private static void Parse()
        {
            Params.GetParams();


            using (StreamWriter sw = new StreamWriter(Directory.GetCurrentDirectory() + "\\db.db", true, System.Text.Encoding.Default, 2048))
            {
                Console.WriteLine(@"[{0}]: Начало итерации", DateTime.Now.ToString());
                if (Params.Yandex == 1) YandexParser.Parse(sw);
                Console.Write("10%");
                if (Params.Mail == 1) MailParser.Parse(sw);
                Console.Write("20%");
                if (Params.AccuWeather == 1) AccuParser.Parse(sw);
                Console.Write("30%");
                if (Params.Weather == 1) WeatherParser.Parse(sw);
                Console.Write("40%");
                if (Params.GisMeteo == 1) GismeteoParser.Parse(sw);
                Console.Write("50%");
                if (Params.Fobos == 1) FobosParser.Parse(sw);
                Console.Write("60%");
                if (Params.Foreca == 1) ForecaParser.Parse(sw);
                Console.WriteLine("100%");
                Console.WriteLine(@"[{0}]: Итерация закончена", DateTime.Now.ToString());
            }

        }

        private static void Iteration(Object o)
        {
            string currentTime = DateTime.Now.ToString("HH:mm");
            if ((currentTime == TIME1) || (currentTime == TIME2))
            {
                Parse();
            }
        }
    }
}
