using System;

namespace Everyone
{
    /// <summary>
    /// An individual test.
    /// </summary>
    public class Test : AssertionsBase<Test>, TestChild
    {
        private readonly TestChild testChild;

        public string Name => this.testChild.Name;

        public TestGroup? Parent => this.testChild.Parent;

        protected Test(
            string name,
            TestGroup? parent,
            string fullNameSeparator,
            CompareFunctions compareFunctions,
            AssertMessageFunctions assertMessageFunctions)
            : base(
                  createExceptionFunction: (string message) => new TestFailureException(message),
                  assertMessageFunctions: assertMessageFunctions,
                  compareFunctions: compareFunctions)
        {
            this.testChild = BasicTestChild.Create(name, parent, fullNameSeparator);
        }

        public static Test Create(
            string name,
            TestGroup? parent,
            string fullNameSeparator,
            CompareFunctions compareFunctions,
            AssertMessageFunctions messageFunctions)
        {
            return new Test(
                name: name,
                parent: parent,
                fullNameSeparator: fullNameSeparator,
                compareFunctions: compareFunctions,
                assertMessageFunctions: messageFunctions);
        }

        public string GetFullName()
        {
            return this.testChild.GetFullName();
        }

        /// <summary>
        /// Invoke the provided <see cref="Action"/> and catch and return an
        /// <see cref="Exception"/> of type <typeparamref name="T"/> if it is thrown. If no
        /// <see cref="Exception"/> is thrown, then null will be returned. If an
        /// <see cref="Exception"/> of a type different than <typeparamref name="T"/> is thrown, it
        /// will not be caught and regular exception handling will occur.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="Exception"/> to catch and return.</typeparam>
        /// <param name="action">The <see cref="Action"/> to run.</param>
        public T? Catch<T>(Action action) where T : Exception
        {
            Pre.Condition.AssertNotNull(action, nameof(action));

            T? result = null;
            try
            {
                action.Invoke();
            }
            catch (T exception)
            {
                result = exception;
            }
            return result;
        }

        /// <summary>
        /// Assert that the provided <see cref="Action"/> will throw the provided
        /// <paramref name="expected"/> <see cref="Exception"/>. If <paramref name="expected"/> is
        /// null, then this will assert that the provided <see cref="Action"/> does not thrown an
        /// <see cref="Exception"/>.
        /// </summary>
        /// <param name="expected">The expected <see cref="Exception"/>.</param>
        /// <param name="action">The <see cref="Action"/> to invoke.</param>
        /// <param name="message">A message that describes the failure.</param>
        public void AssertThrows(Exception? expected, Action action, string? message = null)
        {
            this.AssertThrows(expected, action, new AssertParameters { Message = message });
        }

        /// <summary>
        /// Assert that the provided <see cref="Action"/> will throw the provided
        /// <paramref name="expected"/> <see cref="Exception"/>. If <paramref name="expected"/> is
        /// null, then this will assert that the provided <see cref="Action"/> does not thrown an
        /// <see cref="Exception"/>.
        /// </summary>
        /// <param name="expected">The expected <see cref="Exception"/>.</param>
        /// <param name="action">The <see cref="Action"/> to invoke.</param>
        /// <param name="parameters"><see cref="AssertParameters"/> that are used to define the
        /// assertion.</param>
        public void AssertThrows(Exception? expected, Action action, AssertParameters? parameters)
        {
            Exception? actual = this.Catch<Exception>(action);
            
            Exception? unwrappedActual = actual;
            if (unwrappedActual != null && expected != null)
            {
                unwrappedActual = Exceptions.UnwrapTo(unwrappedActual, expected.GetType());
            }
            
            if (!this.GetCompareFunctions(parameters).AreEqual(expected, unwrappedActual))
            {
                this.AssertEqual(expected, actual, parameters);
            }
        }

        /// <summary>
        /// Assert that the provided <see cref="Action"/> will throw the provided
        /// <paramref name="expected"/> <see cref="Exception"/>. If <paramref name="expected"/> is
        /// null, then this will assert that the provided <see cref="Action"/> does not thrown an
        /// <see cref="Exception"/>.
        /// </summary>
        /// <param name="action">The <see cref="Action"/> to invoke.</param>
        /// <param name="expected">The expected <see cref="Exception"/>.</param>
        /// <param name="message">A message that describes the failure.</param>
        public void AssertThrows(Action action, Exception? expected, string? message = null)
        {
            this.AssertThrows(action, expected, new AssertParameters { Message = message });
        }

        /// <summary>
        /// Assert that the provided <see cref="Action"/> will throw the provided
        /// <paramref name="expected"/> <see cref="Exception"/>. If <paramref name="expected"/> is
        /// null, then this will assert that the provided <see cref="Action"/> does not thrown an
        /// <see cref="Exception"/>.
        /// </summary>
        /// <param name="action">The <see cref="Action"/> to invoke.</param>
        /// <param name="expected">The expected <see cref="Exception"/>.</param>
        /// <param name="parameters"><see cref="AssertParameters"/> that are used to define the
        /// assertion.</param>
        public void AssertThrows(Action action, Exception? expected, AssertParameters? parameters)
        {
            this.AssertThrows(expected, action, parameters);
        }
    }
}
