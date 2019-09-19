using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.IO;

namespace Weather.WeatherSource
{
    class GismeteoParser
    {


        public static void Parse(StreamWriter sw)
        {
            try
            {

                string html = @"https://www.gismeteo.ru/weather-moscow-4368/10-days/";

                HtmlWeb web = new HtmlWeb();

                var htmlDoc = web.Load(html);
                DateTime now = DateTime.Now;
                DateTime dt = DateTime.Now;

                HtmlNode mainwidget = htmlDoc.DocumentNode.SelectSingleNode(".//div[contains(@class, 'widget__container')]");

                HtmlNode conditions = mainwidget.SelectSingleNode(".//div[contains(@class, 'widget__row_icon')]");
                HtmlNodeCollection condition = conditions.SelectNodes(".//span[contains(@class, 'tooltip')]");

                HtmlNode temperatures = mainwidget.SelectSingleNode(".//div[contains(@class, 'widget__row_temperature')]");
                HtmlNodeCollection maxTemp = temperatures.SelectNodes(".//div[contains(@class, 'maxt')]//span[contains(@class, 'unit_temperature_c')]");
                HtmlNodeCollection minTemp = temperatures.SelectNodes(".//div[contains(@class, 'mint')]//span[contains(@class, 'unit_temperature_c')]");
                //HtmlNodeCollection minTemp = temperatures.SelectNodes(".//div[contains(@class, 'mint')]");

                HtmlNode winds = htmlDoc.DocumentNode.SelectSingleNode(".//div[contains(@data-widget-id, 'wind')]");
                HtmlNodeCollection windSpeed = winds.SelectNodes(".//div[contains(@class, 'w_wind')]//span[contains(@class, 'unit_wind_m_s')]");
                HtmlNodeCollection windDirect = winds.SelectNodes(".//div[contains(@class, 'w_wind')]//div[contains(@class, 'w_wind__direction')]");

                HtmlNode pressures = htmlDoc.DocumentNode.SelectSingleNode(".//div[contains(@data-widget-id, 'pressure')]");
                HtmlNodeCollection pressure = pressures.SelectNodes(".//div[contains(@class, 'maxt')]//span[contains(@class, 'unit_pressure_mm_hg_atm')]");

                HtmlNode humidities = htmlDoc.DocumentNode.SelectSingleNode(".//div[contains(@class, 'widget__row_humidity')]");
                HtmlNodeCollection humidity = humidities.SelectNodes(".//div[contains(@class, 'w-humidity')]");

                //Console.WriteLine(windDirection[2].InnerText.Trim());
                //Console.WriteLine(humidity.Count);

                for (int i = 0; i < maxTemp.Count; i++)
                {
                    sw.WriteLine(@"{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};", "gismet", now.ToString(), dt.Date.ToShortDateString(),
                            "",
                            minTemp[i].InnerText,
                            maxTemp[i].InnerText,
                            condition[i].InnerText,
                            pressure[i].InnerText,
                            humidity[i].InnerText,
                            windSpeed[i].InnerText.Trim(),
                            windDirect[i].InnerText.Trim()
                            );
                    dt = dt.AddDays(1);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(@"[{0}]: Ошибка парсинга GisMeteo. {1}", DateTime.Now.ToString(), e.Message);
            }
        }
    }
}
