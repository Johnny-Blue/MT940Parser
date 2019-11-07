namespace programmersdigest.MT940Parser.Parsing
{
    using programmersdigest.MT940Parser.Model;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Resources;

    public class BalanceParser
    {
        private StringReader _reader;
        private ResourceManager _rm = new ResourceManager(typeof(BalanceParserResx));
        private readonly CultureInfo _cultureInfo = CultureInfo.DefaultThreadCurrentCulture;

        public Balance ReadBalance(string buffer, BalanceType type)
        {
            _reader = new StringReader(buffer);

            var balance = new Balance
            {
                Type = type
            };

            ReadDebitCreditMark(ref balance);

            return balance;
        }

        private void ReadDebitCreditMark(ref Balance balance)
        {
            var value = _reader.Read(1);
            if (value.Length < 1)
            {
                throw new InvalidDataException(_rm.GetString("endUnexpectedlycrdb", _cultureInfo));
            }

            switch (value)
            {
                case "C":
                    balance.Mark = DebitCreditMark.Credit;
                    break;
                case "D":
                    balance.Mark = DebitCreditMark.Debit;
                    break;
                default:
                    throw new FormatException($"{_rm.GetString("dc", _cultureInfo)} {value}");
            }

            ReadStatementDate(ref balance);
        }

        private void ReadStatementDate(ref Balance balance)
        {
            var value = _reader.ReadWhile(char.IsDigit, 6);
            if (value.Length < 6)
            {
                throw new InvalidDataException(_rm.GetString("endUnexpectedlyDate", _cultureInfo));
            }

            balance.Date = DateParser.Parse(value);

            ReadCurrency(ref balance);
        }

        private void ReadCurrency(ref Balance balance)
        {
            var value = _reader.ReadWhile(c => char.IsLetter(c) && char.IsUpper(c), 3);
            if (value.Length < 3)
            {
                throw new InvalidDataException(_rm.GetString("endUnexpectedlyCCY", _cultureInfo));
            }

            balance.Currency = value;

            ReadAmount(ref balance);
        }

        private void ReadAmount(ref Balance balance)
        {
            var value = _reader.Read(15);
            if (value.Length <= 0)
            {
                throw new InvalidDataException(_rm.GetString("endUnexpectedlyAmount", _cultureInfo));
            }

            if (!decimal.TryParse(value, NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("de"), out var amount))
            {
                throw new FormatException($"{_rm.GetString("noDecimal", _cultureInfo)} {value}");
            }

            balance.Amount = amount;
        }
    }
}
