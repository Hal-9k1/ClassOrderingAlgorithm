
using System.Text.Json;

namespace Choices
{
    internal class ClassReader
    {
        private readonly IList<ClassData> classes;

        internal ClassReader(string path)
        {
            using var stream = File.OpenRead(path);
            IList<JsonClassData>? result = JsonSerializer.Deserialize<IList<JsonClassData>>(stream);
            classes = [.. (result ?? throw new Exception("Failed to read class data"))
                .Select((jsonData) => new ClassData(jsonData))];
        }

        internal IList<ClassData> GetClasses() => classes;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Must match JSON format")]
    internal record JsonClassData(string name, bool isSports, int capacity, IList<int> periods);
}