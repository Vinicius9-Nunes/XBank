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
            const string REGEX_TO_VALIDATE_ONLY_NUMBERS = @"^\d{3}\d{3}\d{3}\d{2}$";

            string cpfWithoutNumbers = String.Join("", Regex.Split(cpf, @"[^\d]"));

            Regex regex = new Regex(REGEX_TO_VALIDATE_ONLY_NUMBERS);
            bool match = regex.IsMatch(cpfWithoutNumbers);
            if (match)
            {
                cpf = cpfWithoutNumbers;
                return match;
            }
            return false;
        }

        public static string RemoveCpfLetters(this string cpf)
        {
            cpf = String.Join("", Regex.Split(cpf, @"[^\d]"));
            return cpf;
        }
    }
}
