using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Weather.WeatherSource
{
    class OpenWeatherMapParser
    {
        public static void Parse(StreamWriter sw)
        {
            try
            {
                string html = @"https://openweathermap.org/city/524901";
                int i = 0;
                HtmlWeb web = new HtmlWeb();

                var htmlDoc = web.Load(html);
                DateTime now = DateTime.Now;
                DateTime dt = DateTime.Now;
                HtmlNodeCollection days = htmlDoc.DocumentNode.SelectNodes(".//tr[contains(@class, 'weather-forecast-list__items')]");

                Console.WriteLine(htmlDoc.DocumentNode.InnerHtml);
                /*HtmlNodeCollection daysInfo = htmlDoc.DocumentNode.SelectNodes(".//dd[contains(@class, 'forecast-details__day-info') and not(contains(@class,'jsadv'))]");
                foreach (HtmlNode day in days)
                {
                    HtmlNode dateDay = day.SelectSingleNode(".//strong[contains(@class,'forecast-details__day-number')]");
                    HtmlNode dateMonth = day.SelectSingleNode(".//span[contains(@class,'forecast-details__day-month')]");
                    HtmlNodeCollection rows = daysInfo[i].SelectNodes(".//tr[contains(@class,'weather-table__row')]");
                    foreach (var row in rows)
                    {
                        HtmlNodeCollection dayTemp = row.SelectNodes(".//span[contains(@class,'temp__value')]");
                        HtmlNode daypart = row.SelectSingleNode(".//div[contains(@class,'weather-table__daypart')]");

                        HtmlNode condition = row.SelectSingleNode(".//td[contains(@class,'weather-table__body-cell_type_condition')]");
                        HtmlNode pressure = row.SelectSingleNode(".//td[contains(@class,'weather-table__body-cell_type_air-pressure')]");
                        HtmlNode humidity = row.SelectSingleNode(".//td[contains(@class,'weather-table__body-cell_type_humidity')]");
                        HtmlNode windSpeed = row.SelectSingleNode(".//span[contains(@class,'wind-speed')]");
                        HtmlNode windDirect = row.SelectSingleNode(".//abbr[contains(@class,'icon-abbr')]");
                        sw.WriteLine(@"{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};", "yandex", now.ToString(), dt.Date.ToShortDateString(),
                            daypart.InnerText,
                            dayTemp[0].InnerText,
                            dayTemp[1].InnerText,
                            condition.InnerText,
                            pressure.InnerText,
                            humidity.InnerText,
                            windSpeed.InnerText,
                            windDirect.InnerHtml
                            );
                    }

                    dt = dt.AddDays(1);
                    i++;
                }
                */
            }
            catch (Exception e)
            {
                Console.WriteLine(@"[{0}]: Ошибка парсинга OpenWeatherMap. {1}", DateTime.Now.ToString(), e.Message);
            }
        }
    }
}
