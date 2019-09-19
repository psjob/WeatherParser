using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.IO;

namespace Weather.WeatherSource
{
    class MailParser
    {        
        public static void Parse(StreamWriter sw)
        {
            try
            {
                string html = @"https://pogoda.mail.ru/prognoz/moskva/14dney/";
                int i = 0;
                HtmlWeb web = new HtmlWeb();

                var htmlDoc = web.Load(html);
                DateTime now = DateTime.Now;
                DateTime dt = DateTime.Now;
                HtmlNodeCollection days = htmlDoc.DocumentNode.SelectNodes(".//div[@class='block']");
                foreach (HtmlNode day in days)
                {

                    HtmlNodeCollection daypart = day.SelectNodes(".//div[contains(@class,'day__date')]");
                    HtmlNodeCollection temperature = day.SelectNodes(".//div[contains(@class,'day__temperature')]");
                    HtmlNodeCollection humidity = day.SelectNodes(".//span[contains(@title,'Влажность')]");
                    HtmlNodeCollection pressure = day.SelectNodes(".//span[contains(@title,'Давление')]");
                    HtmlNodeCollection description = day.SelectNodes(".//div[contains(@class,'day__description')]");

                    HtmlNodeCollection wind = day.SelectNodes(".//span[contains(@title,'Ветер')]");

                    for (int j = 0; j < 4; j++)
                    {
                        HtmlNode condition = description[j].SelectSingleNode(".//span");
                        string windStr = wind[j].InnerText.Trim();
                        string[] windArr = windStr.Split(' ');
                        string windSpeed = windArr[0];
                        string windDirect = windArr[2];
                        sw.WriteLine(@"{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};", "mail", now.ToString(), dt.Date.ToShortDateString(),
                            daypart[j].InnerText,
                            Functions.deleteSymdols(temperature[j].InnerText.ToString()),
                            Functions.deleteSymdols(temperature[j].InnerText.ToString()),
                            condition.InnerText.ToString(),
                            Functions.deleteSymdols(pressure[j].InnerText.ToString()),
                            Functions.deleteSymdols(humidity[j].InnerText.ToString()),
                            windSpeed,
                            windDirect
                            );
                    }
                    dt = dt.AddDays(1);
                    i++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(@"[{0}]: Ошибка парсинга Mail. {1}", DateTime.Now.ToString(), e.Message);
            }
        }
    }
}
