using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ContactBook.Helpers
{
    static class Validator
    {
        public static bool IsValidMailAddress(string mailAddress)
        {
            return Regex.Match(mailAddress, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success;
        }
        public static bool IsValidPhone(string phone)
        {
            return Regex.Match(phone, @"^((\+?7|8)[ \-] ?)?((\(\d{3}\))|(\d{3}))?([ \-])?(\d{3}[\- ]?\d{2}[\- ]?\d{2})$").Success;
        }

        public static bool IsValidName(string name)
        {
            return Regex.Match(name, @"^[а-яА-ЯA-Za-z]+$").Success;
        }
    }
}
