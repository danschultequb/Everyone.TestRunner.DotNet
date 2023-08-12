namespace Everyone
{
    public abstract class TestChild
    {
        protected TestChild(string name, TestGroup? parent)
        {
            this.Name = name;
            this.Parent = parent;
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
        public string GetFullName(string separator = " ")
        {
            string result = "";
            if (this.Parent != null)
            {
                result = this.Parent.GetFullName(separator: separator) + separator;
            }
            result += this.Name;

            return result;
        }
    }
}
