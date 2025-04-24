namespace Choices
{
    internal class Schedule
    {
        private List<ClassData> schedule = [];
        internal void PushPeriod(ClassData cls)
        {
            schedule.Add(cls);
            if (schedule.Count > Constants.PERIODS)
            {
                throw new Exception("too many periods");
            }
        }

        internal int GetError(StudentChoices choice, ErrorFunc? errorFunction = null)
        {
            errorFunction ??= (int rank) => rank;
            int error = 0;
            for (int period = 0; period < schedule.Count; period++)
            {
                error += errorFunction(choice.GetRank(period, schedule[period]));
            }
            return error;
        }
        internal int GetSportsClasses() => schedule.Where((ClassData cls) => cls.IsSports).Count();

        internal void ForEach(Action<ClassData> func, bool unique = false)
        {
            if (unique)
            {
                foreach (var item in new HashSet<ClassData>(schedule))
                {
                    func(item);
                }
            }
            else
            {
                schedule.ForEach(func);
            }
        }

        internal int CountOccurrences(ClassData cls) => schedule.Count(c => c == cls);
        public override string ToString() => $"[{string.Join(", ", schedule)}]";
    }

    delegate int ErrorFunc(int rank);
}
