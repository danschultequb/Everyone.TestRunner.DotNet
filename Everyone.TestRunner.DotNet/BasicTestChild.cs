namespace Everyone
{
    /// <summary>
    /// A basic implementation of the <see cref="TestChild"/> interface.
    /// </summary>
    public class BasicTestChild : TestChild
    {
        private readonly string fullNameSeparator;

        protected BasicTestChild(string name, TestGroup? parent, string fullNameSeparator)
        {
            Pre.Condition.AssertNotNullAndNotEmpty(name, nameof(name));
            Pre.Condition.AssertNotNull(fullNameSeparator, nameof(fullNameSeparator));

            this.Name = name;
            this.Parent = parent;
            this.fullNameSeparator = fullNameSeparator;
        }

        public static BasicTestChild Create(string name, TestGroup? parent, string fullNameSeparator)
        {
            return new BasicTestChild(name, parent, fullNameSeparator);
        }

        /// <summary>
        /// The name of this <see cref="TestChild"/>.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The <see cref="TestGroup"/> parent of this <see cref="TestChild"/>.
        /// </summary>
        public TestGroup? Parent { get; }

        /// <summary>
        /// Get the full name of this <see cref="TestChild"/>.
        /// </summary>
        public string GetFullName()
        {
            string result = "";
            TestGroup? parent = this.Parent;
            if (parent != null)
            {
                result = parent.GetFullName() + this.fullNameSeparator;
            }
            result += this.Name;

            return result;
        }
    }
}
