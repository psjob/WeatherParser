using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ini;
using System.IO;

namespace Weather.WeatherSource
{
    class Params
    {
        public static byte Yandex = 1;
        public static byte Fobos = 1;
        public static byte Mail = 1;
        public static byte GisMeteo = 1;
        public static byte Weather = 1;
        public static byte AccuWeather = 1;
        public static byte Foreca = 1; 

        public static void GetParams()
        {
            //Создание объекта, для работы с файлом
            IniFile manager = new IniFile(Directory.GetCurrentDirectory() + "\\weather.ini");

            string value;
            value = manager.GetPrivateString("Settings", "Yandex");
            Yandex = (byte)(value == "" ? 0 : Int16.Parse(value));

            value = manager.GetPrivateString("Settings", "Fobos");
            Fobos = (byte)(value == "" ? 0 : Int16.Parse(value));

            value = manager.GetPrivateString("Settings", "Mail");
            Mail = (byte)(value == "" ? 0 : Int16.Parse(value));

            value = manager.GetPrivateString("Settings", "GisMeteo");
            GisMeteo = (byte)(value == "" ? 0 : Int16.Parse(value));

            value = manager.GetPrivateString("Settings", "Weather");
            Weather = (byte)(value == "" ? 0 : Int16.Parse(value));

            value = manager.GetPrivateString("Settings", "AccuWeather");
            AccuWeather = (byte)(value == "" ? 0 : Int16.Parse(value));

            value = manager.GetPrivateString("Settings", "Foreca");
            Foreca = (byte)(value == "" ? 0 : Int16.Parse(value));
        }

    }
}
