using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.IO;

namespace Weather.WeatherSource
{
    class AccuParser
    {

        //todo: возможно нужно сделать парсер со страницы 
        //http://mprod.accuweather.com/ru/ru/moscow/294021/hourly-weather-forecast/294021
        //там больше информации, но грузится будет дольше
        static string url = @"http://mprod.accuweather.com/ru/ru/moscow/294021/daily-weather-forecast/294021?day=";

        private static string trimSymdols(string str)
        {
            if (str.IndexOf("&") != -1)
            {
                return str.Substring(0, str.IndexOf("&"));
            }
            else
            {
                return str;
            }
        }

        public static void Parse(StreamWriter sw)
        {
            try
            {
                DateTime dt = DateTime.Now;
                for (int i = 0; i < 10; i++)
                {
                    string html = url + i;
                    HtmlWeb web = new HtmlWeb();
                    var htmlDoc = web.Load(html);
                    DateTime now = DateTime.Now;
                    HtmlNode mainpanel = htmlDoc.DocumentNode.SelectSingleNode(".//ul[contains(@class,'group')]");
                    HtmlNode detailspanel = htmlDoc.DocumentNode.SelectSingleNode(".//div[contains(@id,'details')]");

                    HtmlNode day = mainpanel.SelectSingleNode(".//li[contains(@class,'day')]");
                    HtmlNode dayTemperature = day.SelectSingleNode(".//dl/dd/strong");
                    HtmlNode dayCondition = day.SelectSingleNode(".//dl/dd/p[contains(@class,'desc')]");

                    HtmlNode night = mainpanel.SelectSingleNode(".//li[contains(@class,'night')]");
                    HtmlNode nightTemperature = night.SelectSingleNode(".//dl/dd/strong");
                    HtmlNode nightCondition = night.SelectSingleNode(".//dl/dd/p[contains(@class,'desc')]");

                    sw.WriteLine(@"{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};", "accu", now.ToString(), dt.Date.ToShortDateString(),
                            "День",
                            trimSymdols(dayTemperature.InnerText),
                            trimSymdols(dayTemperature.InnerText),
                            dayCondition.InnerText,
                            "", //давление
                            "", //влажность
                            "",  //скорость ветра
                            ""  //направление ветра
                            );
                    sw.WriteLine(@"{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};", "accu", now.ToString(), dt.Date.ToShortDateString(),
                            "Ночь",
                            trimSymdols(nightTemperature.InnerText),
                            trimSymdols(nightTemperature.InnerText),
                            nightCondition.InnerText,
                            "", //давление
                            "", //влажность
                            "",  //скорость ветра
                            ""  //направление ветра
                            );

                    dt = dt.AddDays(1);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(@"[{0}]: Ошибка парсинга AccuWeather. {1}", DateTime.Now.ToString(), e.Message);
            }
        }
    }
}
