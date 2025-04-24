
namespace Choices
{
    internal class StudentChoices
    {
        private List<List<ClassData>> choices;
        public StudentChoices(List<List<ClassData>> choices, bool forceScienceRecovery, bool forceMathRecovery)
        {
            if (choices.Count != Constants.PERIODS) {
                throw new Exception("Invalid length");
            }
            this.choices = new(Constants.PERIODS);
            RecoveryComparer comparer = new(forceScienceRecovery, forceMathRecovery);
            for (int i = 0; i < Constants.PERIODS; i++)
            {
                this.choices.Add([.. choices[i].OrderBy(cls => cls, comparer)]);
            }
            foreach (var periodChoices in this.choices)
            {
                Console.WriteLine($"[{string.Join(", ", periodChoices)}]");
            }
            Console.WriteLine();
        }
        public int GetRank(int period, ClassData cls)
        {
            return choices[period].IndexOf(cls);
        }
        public ClassData? GetFirstMatching(int period, Predicate<ClassData> predicate)
        {
            return choices[period].Find(predicate);
        }
    }

    internal class RecoveryComparer : IComparer<ClassData>
    {
        private bool forceScienceRecovery;
        private bool forceMathRecovery;

        public RecoveryComparer(bool forceScienceRecovery, bool forceMathRecovery)
        {
            this.forceScienceRecovery = forceScienceRecovery;
            this.forceMathRecovery = forceMathRecovery;
        }

        public int Compare(ClassData? x, ClassData? y)
        {
            if (forceScienceRecovery && x.Name.ToLower().Contains(Constants.SCIENCE_RECOVERY_LOWER_PATTERN))
            {
                return -1;
            }
            else if (forceScienceRecovery && y.Name.ToLower().Contains(Constants.SCIENCE_RECOVERY_LOWER_PATTERN))
            {
                return 1;
            }
            else if (forceMathRecovery && x.Name.ToLower().Contains(Constants.MATH_RECOVERY_LOWER_PATTERN))
            {
                return -1;
            }
            else if (forceMathRecovery && y.Name.ToLower().Contains(Constants.MATH_RECOVERY_LOWER_PATTERN))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
