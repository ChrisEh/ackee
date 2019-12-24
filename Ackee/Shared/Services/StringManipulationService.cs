using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Ackee.Shared.Services
{    
    public class StringManipulationService
    {
        // 
        public string GetStringAbbreviation(string input)
        {
            string abbreviation = "";

            if (string.IsNullOrEmpty(input))
            {
                return abbreviation;
            }

            List<string> separateWords = input.Split(' ').ToList();

            if (separateWords.Count == 1)
            {
                abbreviation = separateWords[0][0].ToString();
            }
            else
            {
                for (int i = 0; i <= 1; i++)
                {
                    abbreviation += separateWords[i][0].ToString();
                }
            }

            return abbreviation.ToUpper();
        }

        public string CapitalizeFirstLetterOfString(string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input.First().ToString().ToUpper() + input.Substring(1);
            }
        }

        public string ToTitleCase(string input)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
        }
    }
}
