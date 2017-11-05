using System;
using System.Collections.Generic;
using System.Linq;
using FirstAssignment;
using FourthAssignment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ThirdAssignment
{
    [TestClass]
    public class FourthAssignmentUnitTest
    {
        private int[] _intArray;
        private List<University> _universityList;
        private List<Student> _studentList;
        private List<Student> _specialStudents;
        private List<Student> _allStudents;

        [TestInitialize]
        public void InitializeMethod()
        {
            _intArray = new[] {1, 3, 3, 4, 2, 2, 2, 3, 3, 4, 5};
            _studentList = new List<Student>();
            for (int i = 0; i < 20; i++)
            {
                _studentList.Add(new Student($"Marko{i}", i.ToString())
                {
                    Gender = i % 2 == 0 ? Gender.Male : Gender.Female
                });
            }
            _specialStudents = new List<Student>();
            for (int i = 20; i < 40; i++)
            {
                _specialStudents.Add(new Student($"Posebni{i}", i.ToString())
                {
                    Gender = i % 2 == 0 ? Gender.Male : Gender.Female
                });
            }
            _universityList = new List<University>();
            for (int i = 2; i < 6; i++)
            {
                _universityList.Add(new University()
                {
                    Name = $"University{i}",
                    Students = _studentList.Where(s => Convert.ToInt32(s.Jmbag) % i == 0).ToArray()
                });
            }
            _allStudents = new List<Student>(_studentList);
            _allStudents.AddRange(_specialStudents);
            _universityList.Add(new University()
            {
                Name = "University1",
                Students = _allStudents.ToArray()
            });

            _universityList.Add(new University()
            {
                Name = "Univerity",
                Students = _studentList.Where(s => s.Gender == Gender.Female).ToArray()
            });



        }

        [TestMethod]
        public void TestLinq1()
        {
            Assert.AreEqual("Broj 1 ponavlja se 1 puta", HomeworkLinqQueries.Linq1(_intArray)[0]);
            Assert.AreEqual("Broj 3 ponavlja se 4 puta", HomeworkLinqQueries.Linq1(_intArray)[2]);
            Assert.AreEqual("Broj 5 ponavlja se 1 puta", HomeworkLinqQueries.Linq1(_intArray)[4]);
        }

        [TestMethod]
        public void TestLinq2_1()
        {
            foreach (var university in HomeworkLinqQueries.Linq2_1(_universityList.ToArray()))
            {
                foreach (var student in university.Students)
                {
                    Assert.AreEqual(Gender.Male, student.Gender);
                }
            }

        }

        [TestMethod]
        public void TestLinq2_2()
        {
            var countFirst = _universityList[0].Students.Length;
            var countSecond = _universityList[1].Students.Length;
            var countThird = _universityList[2].Students.Length;
            var countFourth = _universityList[3].Students.Length;
            var countFifth = _universityList[4].Students.Length;
            var countSixth = _universityList[5].Students.Length;
            var avg = (countFirst + countSecond + countThird + countFourth + countFifth + countSixth) / 6.0;
            foreach (var university in HomeworkLinqQueries.Linq2_2(_universityList.ToArray()))
            {
                Assert.IsTrue(university.Students.Length <= avg);
            }
        }

        [TestMethod]
        public void TestLinq2_3()
        {
            Assert.AreEqual(_allStudents.Count, HomeworkLinqQueries.Linq2_3(_universityList.ToArray()).Length);
            Assert.IsTrue(HomeworkLinqQueries.Linq2_3(_universityList.ToArray()).OrderBy(e => e.Jmbag)
                .SequenceEqual(_allStudents.OrderBy(e => e.Jmbag)));
        }

        [TestMethod]
        public void TestLinq2_4()
        {
            var allFemaleUniversities = _universityList.Where(e => e.Students.All(s => s.Gender == Gender.Female));
            var allFemaleStudents = HomeworkLinqQueries.Linq2_3(allFemaleUniversities.ToArray());
            var allMaleUniversities = _universityList.Where(e => e.Students.All(s => s.Gender == Gender.Male));
            var allMaleStudents = HomeworkLinqQueries.Linq2_3(allMaleUniversities.ToArray());
            foreach (var student in HomeworkLinqQueries.Linq2_4(_universityList.ToArray()))
            {
                Assert.IsTrue(student.Gender == Gender.Female
                    ? allFemaleStudents.Contains(student)
                    : allMaleStudents.Contains(student));
            }
        }

        [TestMethod]
        public void TestLinq2_5()
        {
            Assert.AreEqual(_studentList.Count, HomeworkLinqQueries.Linq2_5(_universityList.ToArray()).Length);
            Assert.IsTrue(HomeworkLinqQueries.Linq2_5(_universityList.ToArray()).OrderBy(e => e.Jmbag)
                .SequenceEqual(_studentList.OrderBy(e => e.Jmbag)));
        }
    }
}
