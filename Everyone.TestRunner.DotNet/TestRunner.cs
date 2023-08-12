using System;
using System.Threading.Tasks;

namespace Everyone
{
    /// <summary>
    /// A type that can be used to run tests.
    /// </summary>
    public interface TestRunner
    {
        /// <summary>
        /// Start a test group.
        /// </summary>
        /// <param name="type">The type whose name will be used as the name of the test group.</param>
        /// <param name="testGroupAction">The <see cref="Action"/> that defines the test group.</param>
        public void TestGroup(Type type, Action testGroupAction)
        {
            this.TestGroup(Types.GetFullName(type), testGroupAction);
        }
        
        /// <summary>
        /// Start a test group.
        /// </summary>
        /// <param name="type">The type whose name will be used as the name of the test group.</param>
        /// <param name="testGroupAction">The <see cref="Action"/> that defines the test group.</param>
        public void TestGroup(Type type, Func<Task> testGroupAction)
        {
            this.TestGroup(Types.GetFullName(type), testGroupAction);
        }

        /// <summary>
        /// Start a test group.
        /// </summary>
        /// <param name="name">The name of the test group.</param>
        /// <param name="testGroupAction">The <see cref="Action"/> that defines the test group.</param>
        public void TestGroup(string name, Action testGroupAction);

        /// <summary>
        /// Start a test group.
        /// </summary>
        /// <param name="name">The name of the test group.</param>
        /// <param name="testGroupAction">The <see cref="Action"/> that defines the test group.</param>
        public void TestGroup(string name, Func<Task> testGroupAction)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"{nameof(name)} cannot be null or empty.", nameof(name));
            }
            if (testGroupAction == null)
            {
                throw new ArgumentNullException(nameof(testGroupAction));
            }

            this.TestGroup(name, () =>
            {
                testGroupAction.Invoke().Await();
            });
        }

        /// <summary>
        /// Start a test.
        /// </summary>
        /// <param name="name">The name of the test.</param>
        /// <param name="testAction">The <see cref="Action"/> that defines the test.</param>
        public void Test(string name, Action<Test> testAction);

        /// <summary>
        /// Start a test.
        /// </summary>
        /// <param name="name">The name of the test.</param>
        /// <param name="testAction">The <see cref="Action"/> that defines the test.</param>
        public void Test(string name, Func<Test,Task> testAction)
        {
            this.Test(name, (Test test) =>
            {
                testAction.Invoke(test).Await();
            });
        }

        /// <summary>
        /// Get the <see cref="string"/> representation of the provided <paramref name="value"/>
        /// based on the ToString functions that have been registered with this
        /// <see cref="TestRunner"/>.
        /// </summary>
        /// <param name="value">The value to get the <see cref="string"/> representation of.</param>
        public string ToString<T>(T? value);
    }
}
