using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SixthAssignment
{
    public static class MathOperations
    {
        public static async Task<int> FactorialDigitSum(int n)
        {
            return await Task.Run(() =>
            {
                double rez = 1;
                for (int i = 1; i <= n; i++)
                {
                    rez *= i;
                }
                return rez.ToString().Select(e => int.Parse(e.ToString())).Sum();
            });
        }
    }
}
