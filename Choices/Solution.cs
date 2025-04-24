


using Google.Apis.Services;
using Google.Apis.Sheets.v4;

namespace Choices
{
    internal class Solution
    {
        private Dictionary<Student, Schedule> schedules = new();
        public void PutSchedule(Student student, Schedule schedule)
        {
            schedules[student] = schedule;
        }
        internal IEnumerable<KeyValuePair<Student, Schedule>> GetSchedules()
        {
            return schedules;
        }

        internal void PushPeriod(Student student, ClassData cls)
        {
            if (!schedules.ContainsKey(student))
            {
                schedules[student] = new();
            }
            schedules[student].PushPeriod(cls);
        }
        internal void Print()
        {
            foreach (var pair in schedules)
            {
                Console.WriteLine($"{pair.Key} : {pair.Value}");
            }
        }

        internal void Upload()
        {

        }
    }
}