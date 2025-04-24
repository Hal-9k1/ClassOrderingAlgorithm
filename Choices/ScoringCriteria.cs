using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Choices
{
    internal class ScoringCriteria
    {
        private readonly IChoiceReader choiceReader;

        internal ScoringCriteria(IChoiceReader choiceReader)
        {
            this.choiceReader = choiceReader;
        }
        internal int GetSolutionError(Solution solution)
        {
            int error = 0;
            Dictionary<ClassData, int> classPopulation = new();
            foreach (var pair in solution.GetSchedules())
            {
                StudentChoices choices = choiceReader.GetChoices(pair.Key);
                Schedule schedule = pair.Value;
                for (int i = 0; i < Constants.PERIODS; i++)
                {
                    error += schedule.GetError(choices, ScoreChoiceRank);
                }
                error += GetSportsClassError(schedule);
                error += GetDuplicateClassError(schedule);
                error += GetStudentRankingError(schedule, choices);
                error += GetQiGongError(schedule, choices);
                schedule.ForEach((cls) =>
                {
                    if (!classPopulation.TryGetValue(cls, out int value))
                    {
                        classPopulation[cls] = 1;
                    }
                    else
                    {
                        classPopulation[cls] = ++value;
                    }
                });
            }
            error += GetStudentCountError(classPopulation);
            return error;
        }

        private int GetQiGongError(Schedule schedule, StudentChoices choices)
        {
            int error = 0;
            schedule.ForEach((cls) =>
            {
                error += (cls.Name == "QiGong" && choices.GetRank(1, cls) >= 3) ? 200 : 0;
            });
            return error;
        }

        private int GetStudentCountError(Dictionary<ClassData, int> classPopulation)
        {
            return classPopulation.Values.Where(x => x < 12).Count();
        }

        private int GetStudentRankingError(Schedule schedule, StudentChoices choices)
        {
            for (int i = 0; i < Constants.PERIODS; i++)
            {
                if (schedule.GetError(choices) == 1)
                {
                    return 0;
                }
            }
            return 20;
        }

        private int GetDuplicateClassError(Schedule schedule)
        {
            int error = 0;
            schedule.ForEach((cls) =>
            {
                int penalties = schedule.CountOccurrences(cls) - (cls.IsSports ? 2 : 1);
                error += Math.Max(0, penalties) * 2000;
            });
            return error;
        }

        private int GetSportsClassError(Schedule schedule)
        {
            return Math.Max(2, schedule.GetSportsClasses()) * 10;
        }

        private int ScoreChoiceRank(int rank)
        {
            return rank;
        }

    }
}
