using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirstAssignment;

namespace FourthAssignment
{
    public class HomeworkLinqQueries
    {
        public static string[] Linq1(int[] intArray)
        {
            return intArray.OrderBy(s => s).Distinct().
                Select(s => $"Broj {s} ponavlja se {intArray.Count(p => p == s)} puta").ToArray();
        }
        public static University[] Linq2_1(University[] universityArray)
        {
            return universityArray.Where(s => 
                s.Students.All(p => p.Gender == Gender.Male)).ToArray();
        }
        public static University[] Linq2_2(University[] universityArray)
        {
            return universityArray.
                Where(s => s.Students.Length < 
                    universityArray.Average(e => e.Students.Length)).ToArray();
        }
        public static Student[] Linq2_3(University[] universityArray)
        {
            return universityArray.SelectMany(u => u.Students, (university, student) => student).Distinct().ToArray();
        }
        public static Student[] Linq2_4(University[] universityArray)
        {
            return universityArray.Where(s => s.Students.All(e => e.Gender == s.Students.FirstOrDefault()?.Gender))
                .SelectMany(u => u.Students, (university, student) => student).Distinct().ToArray();
        }
        public static Student[] Linq2_5(University[] universityArray)
        {
            return universityArray.SelectMany(u => u.Students, (university, student) => student).GroupBy(s => s.Jmbag)
                .Where(s => s.Count() > 1).SelectMany(s => s).Distinct().ToArray();
        }
    }
}
