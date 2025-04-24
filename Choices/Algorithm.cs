using System.Numerics;
using System.Security.Cryptography;

namespace Choices
{
    internal class Algorithm
    {
        readonly Random random;

        readonly IList<ClassData> classes;
        readonly IChoiceReader choiceReader;
        private readonly ScoringCriteria criteria;

        public Algorithm(IList<ClassData> classes, IChoiceReader choiceReader)
        {
            this.classes = classes;
            this.choiceReader = choiceReader;
            criteria = new(choiceReader);
            random = new();
        }

        Solution GreedyAlgorithm()
        {
            Solution? bestSolution = null;
            int bestError = int.MaxValue;

            for (int i = 0; i < Constants.GREEDY_ALGORITHM_ITERATIONS; i++)
            {
                Solution solution = GenerateRandomSolution();
                int error = criteria.GetSolutionError(solution);
                if (error < bestError)
                {
                    bestError = error;
                    bestSolution = solution;
                }
            }
            if (bestSolution == null)
            {
                throw new Exception("No iters");
            }
            return bestSolution;
        }

        private Solution GenerateRandomSolution()
        {
            Solution solution = new();
            Dictionary<Student, Schedule> schedules = [];
            for (int period = 0; period < Constants.PERIODS; period++)
            {
                Dictionary<ClassData, int> studentsInClasses = [];
                Student[] students = choiceReader.GetStudents().ToArray();
                random.Shuffle(students);
                foreach (var student in students)
                {
                    var cls = choiceReader.GetChoices(student).GetFirstMatching(
                        period,
                        (cls) => studentsInClasses.GetValueOrDefault(cls, 0) < cls.Capacity
                    );
                    if (cls == null)
                    {
                        throw new Exception("Not enough class space in period " + period);
                    }
                    studentsInClasses[cls] = studentsInClasses.GetValueOrDefault(cls, 0) + 1;
                    solution.PushPeriod(student, cls);
                }
            }
            return solution;
        }

        public void Run()
        {
            Solution solution = GreedyAlgorithm();
            //solution.Print();
            solution.Upload();
            Console.WriteLine("Finish");
        }
    }

}
