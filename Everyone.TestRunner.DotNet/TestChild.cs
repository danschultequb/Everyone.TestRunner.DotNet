namespace Everyone
{
    public abstract class TestChild
    {
        private readonly string fullNameSeparator;

        protected TestChild(string name, TestGroup? parent, string fullNameSeparator)
        {
            PreCondition.AssertNotNullAndNotEmpty(name, nameof(name));
            PreCondition.AssertNotNull(fullNameSeparator, nameof(fullNameSeparator));

            this.Name = name;
            this.Parent = parent;
            this.fullNameSeparator = fullNameSeparator;
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
            if (this.Parent != null)
            {
                result = this.Parent.GetFullName() + this.fullNameSeparator;
            }
            result += this.Name;

            return result;
        }
    }
}
