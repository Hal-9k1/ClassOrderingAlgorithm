namespace Choices
{
    internal record Student(string FirstName, string LastName, string Email, int grade)
    {
        public string FullName() => $"{FirstName} {LastName}";
        public override string ToString() => FullName();
    }
}
