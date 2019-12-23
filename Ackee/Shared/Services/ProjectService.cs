using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ackee.Shared.Services
{    
    public class ProjectService
    {
        public string GetProjectAbbreviation(string projectName)
        {
            string abbreviation = "";

            if (string.IsNullOrEmpty(projectName))
            {
                return abbreviation;
            }

            List<string> separateWords = projectName.Split(' ').ToList();

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
    }
}
