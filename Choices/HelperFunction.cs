public static class Helpers {
    public static void ShowChoices(List<int[]> choices) {
        Console.WriteLine("CHOICES -");
        for (int i = 0; i < choices.Count; i++) {
            Console.Write($"Student{i} - ");
            for (int j = 0; j < choices[i].Length; j++) {
                Console.Write($"{choices[i][j]}, ");
            }
            Console.WriteLine();
        }
    }

    public static void ShowClasses(Dictionary<string, (int, List<int>)> classes) {
        Console.WriteLine("Classes:");
        foreach (var (key, value) in classes) {
            Console.Write($"{key}: ");
            foreach (int i in value.Item2) Console.Write($" {i}, ");
            Console.WriteLine();
        }
    }

    public static List<T> RandomizeList<T>(List<T> list) {
        Random random = new Random();
        List<T> randList = new List<T>(list);

        for (int i = 0; i < list.Count; i++) {
            int randomVal = random.Next(i, list.Count);
            (randList[i], randList[randomVal]) = (randList[randomVal], randList[i]);
        }

        return randList;
    }

public static Dictionary<string, (int, List<int>)> DeepCopyClasses(Dictionary<string, (int, List<int>)> original) {
    var newDict = new Dictionary<string, (int, List<int>)>();
    foreach (var kvp in original) {
        int maxSize = kvp.Value.Item1;
        List<int> copiedList = new List<int>(kvp.Value.Item2);
        newDict[kvp.Key] = (maxSize, copiedList);
    }
    return newDict;
}
}