namespace programmersdigest.MT940Parser.Parsing
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Resources;

    public static class DateParser
    {
        private static readonly ResourceManager _rm = new ResourceManager(typeof(AdditionalInfoParserResx));
        private static readonly CultureInfo _cultureInfo = CultureInfo.DefaultThreadCurrentCulture;

        public static DateTime Parse(string date)
        {
            if (date == null)
            {
                throw new ArgumentNullException(nameof(date), _rm.GetString("dateNotNull", _cultureInfo));
            }
            if (date.Length != 6)
            {
                throw new FormatException(_rm.GetString("dateFormat", _cultureInfo));
            }
            if (!date.All(char.IsNumber))
            {
                throw new FormatException(_rm.GetString("dateOnlyNumbers", _cultureInfo));
            }

            var year = int.Parse(date.Substring(0, 2));
            var month = int.Parse(date.Substring(2, 2));
            var day = int.Parse(date.Substring(4, 2));

            if (year > 79)
            {
                year += 1900;
            }
            else
            {
                year += 2000;
            }

            return new DateTime(year, month, day);
        }
    }
}
