using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.IO;

namespace Weather.WeatherSource
{
    class WeatherParser
    {
        public static void Parse(StreamWriter sw)
        {
            try
            {
                string html = @"https://weather.com/ru-RU/weather/tenday/l/aad6cfff41f8ff8ba6f7f704388aca9ef8ec099f20666c32d00f240a6f1b9d9f";
                int i = 0;
                HtmlWeb web = new HtmlWeb();

                var htmlDoc = web.Load(html);
                DateTime now = DateTime.Now;
                DateTime dt = DateTime.Now;

                //Console.WriteLine(htmlDoc.DocumentNode.InnerHtml);
                HtmlNodeCollection days = htmlDoc.DocumentNode.SelectNodes(".//tr[contains(@class, 'clickable') and contains(@class,'closed')]");
                foreach (HtmlNode day in days)
                {

                    HtmlNode temperature = day.SelectSingleNode(".//td[contains(@class,'temp')]");
                    HtmlNodeCollection spans = temperature.SelectNodes(".//span");
                    string dayTemperature = spans[0].InnerText;
                    string nightTemperature = spans[2].InnerText;
                    HtmlNode condition = day.SelectSingleNode(".//td[contains(@class,'description')]/span");
                    HtmlNode humidity = day.SelectSingleNode(".//td[contains(@class,'humidity')]/span/span");
                    HtmlNode wind = day.SelectSingleNode(".//td[contains(@class,'wind')]");
                    string windSpeed = wind.InnerText.Split(' ')[1];
                    string windDirect = wind.InnerText.Split(' ')[0];

                    sw.WriteLine(@"{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};", "weather", now.ToString(), dt.Date.ToShortDateString(),
                        "",
                        Functions.deleteSymdols(nightTemperature),
                        Functions.deleteSymdols(dayTemperature),
                        condition.InnerText,
                        "", //давление
                        Functions.deleteSymdols(humidity.InnerText), //влажность
                        windSpeed,  //скорость ветра
                        windDirect  //направление ветра
                        );                    

                    dt = dt.AddDays(1);
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(@"[{0}]: Ошибка парсинга Weather. {1}", DateTime.Now.ToString(), e.Message);
            }
        }
    }
}
