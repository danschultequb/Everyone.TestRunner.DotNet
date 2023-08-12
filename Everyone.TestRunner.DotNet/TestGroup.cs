namespace Everyone
{
    /// <summary>
    /// The parent of a group of tests.
    /// </summary>
    public class TestGroup : TestChild
    {
        public TestGroup(string name, TestGroup? parent = null)
            : base(name, parent)
        {
        }
    }
}
