namespace Everyone
{
    public interface TestChild
    {
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
        public string GetFullName();
    }

    
}
