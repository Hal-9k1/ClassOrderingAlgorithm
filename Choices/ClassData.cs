namespace Choices
{
    internal record ClassData(string Name, bool IsSports, int Capacity, HashSet<int> Periods)
    {
        public ClassData(JsonClassData jsonData)
            : this(jsonData.name, jsonData.isSports, /*jsonData.capacity*/1, [.. jsonData.periods])
        { }
        public override string ToString() => Name;
    }
}