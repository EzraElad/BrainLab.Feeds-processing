using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BrainLab.WordProcess.Services
{
    public class CountService : ICountService
    {
        public int CountWords(IEnumerable<string> stringList)
        {
            int sum = 0;

            foreach (var str in stringList)
            {
                if (String.IsNullOrWhiteSpace(str))
                {
                    continue;
                }
                string strWithoutExtraWhitesapaces = Regex.Replace(str, @"\s+", " ");
                sum += strWithoutExtraWhitesapaces.Trim().Split(" ").Length;
            }

            return sum;
        }
    }
}
