namespace Choices
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            ClassReader classReader = new(Constants.CLASS_DATA_PATH);
            //IChoiceReader choiceReader = new TestChoiceReader(classReader.GetClasses());
            IChoiceReader choiceReader = new CsvChoiceReader(new StreamReader(Constants.INPUT_PATH), classReader.GetClasses());
            ValidateAllChoices(choiceReader);

            new Algorithm(classReader.GetClasses(), choiceReader).Run();
        }

        private static void ValidateAllChoices(IChoiceReader choiceReader)
        {
            foreach (var student in choiceReader.GetStudents())
            {
                for (int period = 0; period < Constants.PERIODS; period++)
                {
                    ClassData? invalid = choiceReader.GetChoices(student).GetFirstMatching(period,
                        (cls) => !cls.Periods.Contains(period + 1));
                    if (invalid != null)
                    {
                        throw new Exception($"Choices for student {student} period {period + 1} contains class {
                            invalid} not offered this period. Choices: {choiceReader.GetChoices(student)}");
                    }
                }
            }
        }
    }
}