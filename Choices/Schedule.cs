using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Choices
{
    internal class Schedule
    {
        private List<ClassData> schedule = [];
        public void PushPeriod(ClassData cls)
        {
            schedule.Add(cls);
            if (schedule.Count > Constants.PERIODS)
            {
                throw new Exception("too many periods");
            }
        }

        public int GetError(StudentChoices choice, ErrorFunc? errorFunction = null)
        {
            errorFunction ??= (int rank) => rank;
            int error = 0;
            for (int period = 0; period < schedule.Count; period++)
            {
                error += errorFunction(choice.GetRank(period, schedule[period]));
            }
            return error;
        }
        public int GetSportsClasses() => schedule.Where((ClassData cls) => cls.IsSports).Count();
    }

    delegate int ErrorFunc(int rank);
}
