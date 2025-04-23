


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
            schedules.GetValueOrDefault(student, new()).PushPeriod(cls);
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
            throw new NotImplementedException();
        }
    }
}