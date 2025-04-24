using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Choices
{
    internal class TestChoiceReader : IChoiceReader
    {
        private readonly List<Student> students;
        private readonly Dictionary<Student, StudentChoices> choices;

        public TestChoiceReader(IList<ClassData> classes)
        {
            students = [
                new("Baz", "Baf", "bazbaf@ausdg.us", 11),
                new("Foo", "Bar", "foobar@ausdg.us", 12),
            ];
            choices = new()
            {
                {
                    students[0],
                    new([
                        [
                            classes[0],
                            classes[1],
                            classes[2],
                            classes[3],
                            classes[4],
                        ],
                        [
                            classes[5],
                            classes[6],
                            classes[7],
                            classes[8],
                            classes[9],
                            classes[10],
                        ],
                        [
                            classes[6],
                            classes[8],
                            classes[9],
                            classes[10],
                            classes[11],
                            classes[12],
                        ],
                        [
                            classes[8],
                            classes[12],
                            classes[13],
                            classes[14],
                            classes[15],
                            classes[16],
                        ],
                        [
                            classes[17],
                            classes[18],
                            classes[19],
                            classes[20],
                            classes[21],
                        ],
                        [
                            classes[9],
                            classes[22],
                            classes[23],
                            classes[24],
                        ]
                    ], true, true)
                },
                {
                    students[1],
                    new([
                        [
                            classes[0],
                            classes[3],
                            classes[1],
                            classes[4],
                            classes[2],
                        ],
                        [
                            classes[7],
                            classes[8],
                            classes[5],
                            classes[9],
                            classes[6],
                            classes[10],
                        ],
                        [
                            classes[6],
                            classes[8],
                            classes[9],
                            classes[10],
                            classes[11],
                            classes[12],
                        ],
                        [
                            classes[8],
                            classes[12],
                            classes[13],
                            classes[14],
                            classes[15],
                            classes[16],
                        ],
                        [
                            classes[17],
                            classes[18],
                            classes[19],
                            classes[20],
                            classes[21],
                        ],
                        [
                            classes[9],
                            classes[22],
                            classes[23],
                            classes[24],
                        ]
                    ], false, false)
                },
            };
        }

        public List<Student> GetStudents()
        {
            return students;
        }

        public StudentChoices GetChoices(Student student)
        {
            return choices[student];
        }
    }
}
