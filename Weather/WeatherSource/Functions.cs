using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Weather.WeatherSource
{
    class Functions
    {
        public static string deleteSymdols(string str)
        {
            Regex pattern = new Regex("[^\\d\\+\\-]");
            return pattern.Replace(str, ""); ;
        }
    }
}
