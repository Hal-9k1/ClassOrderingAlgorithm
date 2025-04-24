using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Choices
{
    internal class CsvChoiceReader : IChoiceReader
    {
        private readonly Dictionary<Student, StudentChoices> choices;
        private readonly List<Student> students;

        internal CsvChoiceReader(StreamReader reader, IList<ClassData> classes)
        {
            string[]? row;
            while ((row = reader.ReadLine()?.Split(',')) != null)
            {
                int i = 1;
                int grade = int.Parse(row[i++]);
                string firstName = row[i++];
                string lastName = row[i++];
                string email = row[i++];
                i += 6;
                List<List<ClassData>> choices = [];
                for (int period = 0; period < Constants.PERIODS; period++)
                {
                    List<ClassData> periodChoices = [];
                    for (int j = 0; j < CountClassesForPeriod(classes, period); j++)
                    {
                        periodChoices.Add(GetClassByName(classes, row[i++]));
                    }
                    choices.Add(periodChoices);
                }
            }
        }

        private ClassData GetClassByName(IList<ClassData> classes, string name)
        {
            return classes.First((cls) => cls.Name == name);
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
