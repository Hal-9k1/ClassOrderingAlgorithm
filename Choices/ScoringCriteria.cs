using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Choices
{
    internal class ScoringCriteria
    {
        private readonly ChoiceReader choiceReader;

        internal ScoringCriteria(ChoiceReader choiceReader)
        {
            this.choiceReader = choiceReader;
        }
        internal int ScoreSolution(Solution solution)
        {
            int error = 0;
            foreach (var pair in solution.GetSchedules())
            {
                for (int i = 0; i < Constants.PERIODS; i++)
                {
                    error += pair.Value.GetError(choiceReader.GetChoices(pair.Key), ScoreChoiceRank);
                }
                error += ScoreSportsClassCount(pair.Value);
                error += ScoreDuplicateClasses(pair.Value);
                error += ScoreStudentGrades(pair.Value);
                error += ScoreStudentCount(pair.Value);
            }

            return error;
        }

        private int ScoreStudentCount(Schedule value)
        {
            throw new NotImplementedException();
        }

        private int ScoreStudentGrades(Schedule value)
        {
            throw new NotImplementedException();
        }

        private int ScoreDuplicateClasses(Schedule value)
        {
            throw new NotImplementedException();
        }

        private int ScoreSportsClassCount(Schedule schedule)
        {
            return schedule.GetSportsClasses();
        }
        private int ScoreChoiceRank(int rank)
        {
            return rank;
        }

    }
}
