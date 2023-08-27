namespace Everyone
{
    /// <summary>
    /// The parent of a group of tests.
    /// </summary>
    public class TestGroup : BasicTestChild
    {
        protected TestGroup(string name, TestGroup? parent, string fullNameSeparator)
            : base(name: name, parent: parent, fullNameSeparator: fullNameSeparator)
        {
        }

        public new static TestGroup Create(string name, TestGroup? parent, string fullNameSeparator)
        {
            return new TestGroup(
                name: name,
                parent: parent,
                fullNameSeparator: fullNameSeparator);
        }
    }
}
