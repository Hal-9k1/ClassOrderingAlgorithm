namespace Choices
{
    internal class StudentChoices
    {
        private List<List<ClassData>> choices;
        public StudentChoices(List<List<ClassData>> choices)
        {
            if (choices.Count != Constants.PERIODS) {
                throw new Exception("Invalid length");
            }
            this.choices = choices;
        }
        public int GetRank(int period, ClassData cls)
        {
            return choices[period].IndexOf(cls);
        }
        public IEnumerator<ClassData> IteratePeriodChoices(int period)
        {
            return choices[period].GetEnumerator();
        }
        public ClassData? GetFirstMatching(int period, Predicate<ClassData> predicate)
        {
            return choices[period].Find(predicate);
        }
    }
}
