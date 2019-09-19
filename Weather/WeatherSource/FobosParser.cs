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
    class FobosParser
    {        

        public static void Parse(StreamWriter sw)
        {
            try
            {

                //fobos = meteovesti
                string html = @"https://www.meteovesti.ru/pogoda_10/27612";

                HtmlWeb web = new HtmlWeb();

                var htmlDoc = web.Load(html);
                DateTime now = DateTime.Now;
                DateTime dt = DateTime.Now;
                HtmlNode table = htmlDoc.DocumentNode.SelectSingleNode(".//table[contains(@class, 'table')]");

                HtmlNodeCollection conditions = table.SelectNodes(".//span[contains(@class, 'forecast-icon')]");

                HtmlNodeCollection temperatures = table.SelectNodes(".//div[contains(@class, 'temper')]");
                //Console.WriteLine(@"{0};{1};", temperatures[0].GetAttributeValue("data-night", ""), temperatures[0].InnerText);

                for (int i = 0; i < temperatures.Count; i++)
                {
                    sw.WriteLine(@"{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};", "fobos", now.ToString(), dt.Date.ToShortDateString(),
                        "",
                        Functions.deleteSymdols(temperatures[i].GetAttributeValue("data-night", "")),
                        Functions.deleteSymdols(temperatures[i].InnerText),
                        Functions.deleteSymdols(conditions[i].InnerText),
                        "",
                        "",
                        "",
                        ""
                        );
                    dt = dt.AddDays(1);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(@"[{0}]: Ошибка парсинга Fobos. {1}", DateTime.Now.ToString(), e.Message);
            }
        }
    }
}
