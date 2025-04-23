using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Choices
{
    internal class ChoiceReader
    {
        List<Student>? students;
        Dictionary<Student, StudentChoices>? choices;
        public async Task Read() {
            students = null;
            choices = null;

        }
        public List<Student> GetStudents()
        {
            return students ?? throw new Exception("Reading not finished");
        }

        public StudentChoices GetChoices(Student student)
        {
            return (choices ?? throw new Exception("Reading not finished"))[student];
        }
    }
}
