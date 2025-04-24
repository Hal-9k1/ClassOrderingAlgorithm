using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Choices
{
    internal interface IChoiceReader
    {
        List<Student> GetStudents();
        StudentChoices GetChoices(Student student);
    }
}
