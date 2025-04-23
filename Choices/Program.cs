using Choices;
using System.Text.Json;

public static class Program {
    private static readonly string CLASS_DATA_PATH = "choiceData.json";

    public static void Main(string[] args)
    {
        ClassReader classReader = new(CLASS_DATA_PATH);
        Task classesTask = classReader.Read();
        ChoiceReader choiceReader = new();
        Task choicesTask = choiceReader.Read();
        Task.WaitAll(classesTask, choicesTask);
        Algorithm algorithm = new(classReader.GetClasses(), choiceReader);

        algorithm.Run();
    }
}