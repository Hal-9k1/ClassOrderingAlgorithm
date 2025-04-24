using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Choices
{
    internal class CsvChoiceReader : IChoiceReader
    {
        private readonly Dictionary<Student, StudentChoices> choices;
        private readonly List<Student> students;
        private readonly Dictionary<int, Dictionary<string, string>> remappings = new() {
            { 6,
                new() {
                    {
                        "Social and Board Games",
                        "Math Focus"
                    }
                }
            }
        };

        internal CsvChoiceReader(StreamReader reader, IList<ClassData> classes)
        {
            choices = [];
            students = [];
            string[]? row;
            reader.ReadLine();
            while ((row = reader.ReadLine()?.Split(',')) != null)
            {
                if (row[0].Length == 0)
                {
                    continue;
                }
                int i = 1;
                int grade = int.Parse(Regex.Match(row[i++], @"^\d+").Value);
                string firstName = row[i++];
                string lastName = row[i++];
                string email = row[i++];
                i += 6;
                List<List<ClassData>> choiceList = [];
                for (int period = 0; period < Constants.PERIODS; period++)
                {
                    List<ClassData> periodChoices = [];
                    for (int j = 0; j < CountClassesForPeriod(classes, period + 1); j++)
                    {
                        periodChoices.Add(GetClassByName(classes, row[i++], period + 1));
                    }
                    choiceList.Add(periodChoices);
                }
                Student student = new(firstName, lastName, email, grade);
                students.Add(student);
                choices[student] = new(choiceList, false, false);
            }
        }

        private ClassData GetClassByName(IList<ClassData> classes, string name, int period)
        {
            return classes.First((cls) =>
                cls.Name == (remappings.ContainsKey(period) && remappings[period].ContainsKey(name) ? remappings[period][name] : name));
        }

        private int CountClassesForPeriod(IList<ClassData> classes, int period) => classes.Count((cls) => cls.Periods.Contains(period));

        public StudentChoices GetChoices(Student student)
        {
            return choices[student];
        }

        public List<Student> GetStudents()
        {
            return students;
        }
    }
}
