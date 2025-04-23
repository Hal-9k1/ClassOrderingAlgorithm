
using System.Text.Json;

namespace Choices
{
    internal class ClassReader
    {
        private readonly string path;
        private IList<ClassData>? classes;

        internal ClassReader(string path)
        {
            this.path = path;
        }

        internal IList<ClassData> GetClasses()
        {
            return classes ?? throw new Exception("Not read yet");
        }

        internal async Task Read()
        {
            using var stream = File.OpenRead(path);
            IList<JsonClassData>? result = await JsonSerializer.DeserializeAsync<IList<JsonClassData>>(stream);
            classes = [.. (result ?? throw new Exception("Failed to read class data")).Select((jsonData) => new ClassData(jsonData))];
        }
    }

    internal record JsonClassData(string name, bool isSports, int capacity, IList<int> periods);
}