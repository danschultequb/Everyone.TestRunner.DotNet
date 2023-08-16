namespace Everyone
{
    /// <summary>
    /// The parent of a group of tests.
    /// </summary>
    public class TestGroup : TestChild
    {
        protected TestGroup(string name, TestGroup? parent, string fullNameSeparator)
            : base(name: name, parent: parent, fullNameSeparator: fullNameSeparator)
        {
        }

        public static TestGroup Create(string name, TestGroup? parent, string fullNameSeparator)
        {
            return new TestGroup(
                name: name,
                parent: parent,
                fullNameSeparator: fullNameSeparator);
        }
    }
}
