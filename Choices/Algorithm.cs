public class Algorithm { 
    Random random = new Random();

    IList<ClassData> classes;

    //INDEX 1 is STUDENT, INDEX 2 is class Order - Item is CLASS 
    // int[,] choices = new int[30, 5];
    List<int[]> choices = new List<int[]>();
    int studentCount = 35;
    int choiceCount = 5;

    async void LoadClasses() {
        using var stream = File.OpenRead(CLASS_DATA_PATH);
        IList<ClassData>? result = await JsonDeserializer.DeserializeAsync<IList<ClassData>>(stream);
        if (!result)
        {
            throw new Exception("Failed to read class data");
        }
        choices = result;
    }
    void SetUp() {
        LoadClasses();
        for (int i = 0; i < classNames.Length; i++) {
            classes.Add(classNames[i], (5, new List<int>()));
        }

        List<int> posClasses = new List<int>();
        for (int i = 0; i < classNames.Length; i++) posClasses.Add(i);

        for (int i = 0; i < studentCount; i++) {
            List<int> posClassesCopy = posClasses.ToList();
            int[] studentChoices = new int[choiceCount];
            for (int j = 0; j < choiceCount; j++) {
                int value = random.Next(0, posClassesCopy.Count);
                studentChoices[j] = posClassesCopy[value];
                posClassesCopy.RemoveAt(value);
            }
            choices.Add(studentChoices);
        }
    }

    void GreedyAlgorithm(int depth = 1000) {
        int minError = 9999;
        Dictionary<string, (int, List<int>)> tempClasses;
        Dictionary<string, (int, List<int>)> bestClasses = Helpers.DeepCopyClasses(classes);


        for (int iteration = 0; iteration < depth; iteration++) {
            int error = 0;
            tempClasses = Helpers.DeepCopyClasses(classes);
            List<int[]> randChoices = Helpers.RandomizeList(choices);


            for (int i = 0; i < randChoices.Count; i++) {
                int[] student = randChoices[i];
                bool wasStudentAdded = false;
                for (int j = 0; j < student.Length; j++) {
                    string className = classNames[student[j]];
                    var (maxClassSize, studentList) = tempClasses[className];
                    if (studentList.Count < maxClassSize) {
                        studentList.Add(i);
                        error += j;
                        wasStudentAdded = true;
                        break;
                    } 
                }
                if (wasStudentAdded == false) {
                    error += 10;
                }
            }

            Console.WriteLine(error);

            if (error < minError) {
                minError = error;
                bestClasses = Helpers.DeepCopyClasses(tempClasses);
            }
        }

        Console.WriteLine(minError);
        classes = bestClasses;
    }

    public void Run() {
        SetUp();
        // Helpers.ShowChoices(choices);
        GreedyAlgorithm();
        Helpers.ShowClasses(classes);
    }
}
