using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace System
{
    public static class StringExtensions
    {
        public static bool IsValidCPF(this string cpf)
        {
            const string REGEX_TO_VALIDATE_ONLY_NUMBERS = @"/^\d{3}\d{3}\d{3}\d{2}$/";

            string cpfWithoutNumbers = String.Join("", Regex.Split(cpf, @"[^\d]"));

            Regex regex = new Regex(REGEX_TO_VALIDATE_ONLY_NUMBERS);
            Match match = regex.Match(cpfWithoutNumbers);
            return match.Success;
        }
    }
}
