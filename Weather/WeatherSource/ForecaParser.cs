using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.IO;

namespace Weather.WeatherSource
{
    class ForecaParser
    {
        public static void Parse(StreamWriter sw)
        {
            try
            {
                string html = @"https://www.foreca.ru/Russia/Moskva?tenday";
                int i = 0;
                HtmlWeb web = new HtmlWeb();

                var htmlDoc = web.Load(html);
                DateTime now = DateTime.Now;
                DateTime dt = DateTime.Now;
                HtmlNodeCollection days = htmlDoc.DocumentNode.SelectNodes(".//div[contains(@class, 'c1')]");

                foreach (HtmlNode day in days)
                {
                    HtmlNodeCollection imgs = day.SelectNodes(".//img");
                    HtmlNodeCollection strongs = day.SelectNodes(".//strong");

                    string condition = imgs[0].GetAttributeValue("alt", "");
                    string windDirect = imgs[1].GetAttributeValue("alt", "");

                    string maxTemp = Functions.deleteSymdols(strongs[0].InnerText);
                    string minTemp = Functions.deleteSymdols(strongs[1].InnerText);
                    string windSpeed = strongs[2].InnerText;

                    sw.WriteLine(@"{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};", "foreca", now.ToString(), dt.Date.ToShortDateString(),                    
                        "",
                        minTemp,
                        maxTemp,
                        condition,
                        "",
                        "",
                        windSpeed,
                        windDirect
                        );
                    dt = dt.AddDays(1);
                    i++;
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(@"[{0}]: Ошибка парсинга Foreca. {1}", DateTime.Now.ToString(), e.Message);
            }
        }
    }
}
