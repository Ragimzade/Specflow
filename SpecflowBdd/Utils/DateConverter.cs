using System;
using System.Linq;
using Framework.BaseClasses;
using static Framework.Utils.StringUtils;

namespace Bdd.Utils
{
    public class DateConverter : BaseEntity
    {
        private static readonly string[] Months =
        {
            "янв", "фев", "мар", "апр", "мая", "июн", "июн", "июл", "авг", "сен", "окт",
            "ноя", "дек"
        };

        private static readonly string[] Hours =
        {
            "назад", "вчера"
        };

        protected internal static string ConvertDate(string stringValue)
        {
            DateTime currentDate;
            var hoursString = Hours.FirstOrDefault(stringValue.Contains);

            switch (hoursString)
            {
                case "вчера":
                    currentDate = DateTime.Now.AddHours(-24);
                    break;
                case "назад":
                    var hours = int.Parse(CutNonDigitCharacters(stringValue));
                    currentDate = DateTime.Now.AddHours(-hours);
                    break;
                default:
                    currentDate = Replace(stringValue);
                    break;
            }

            return currentDate.ToShortDateString();
        }

        private static DateTime Replace(string stringValue)
        {
            string result;
            var dt = DateTime.Now;
            var month = dt.Month;
            var year = DateTime.Now.ToString("yy");
            var monthsString = Months.FirstOrDefault(stringValue.Contains);

            switch (monthsString)
            {
                case "янв":
                    result = stringValue.Replace("янв", $".01.{year}");
                    break;
                case "фев":
                    result = stringValue.Replace("фев", $".02.{year}");
                    break;
                case "мар":
                    result = stringValue.Replace("мар", $".03.{year}");
                    break;
                case "апр":
                    result = stringValue.Replace("апр", $".04.{year}");
                    break;
                case "мая":
                    result = stringValue.Replace("мая", $".05.{year}");
                    break;
                case "июн":
                    result = stringValue.Replace("июн", $".06.{year}");
                    break;
                case "июл":
                    result = stringValue.Replace("июл", $".07.{year}");
                    break;
                case "авг":
                    result = stringValue.Replace("авг", $".08.{year}");
                    break;
                case "сен":
                    result = stringValue.Replace("сен", $".09.{year}");
                    break;
                case "окт":
                    result = stringValue.Replace("окт", $".10.{year}");
                    break;
                case "ноя":
                    result = stringValue.Replace("ноя", $".11.{year}");
                    break;
                case "дек":
                    result = stringValue.Replace("дек", $".12.{year}");
                    break;
                default:
                    result = "";
                    break;
            }

            if (result == "")
            {
                return dt;
            }
            
            var date = DateTime.ParseExact(result.Replace(" ", string.Empty), "d.MM.yy",
                null);
            
            if (date.Month > month)
            {
                date = date.AddYears(-1);
            }

            return date;
        }
    }
}