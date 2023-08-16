using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Everyone
{
    /// <summary>
    /// A type that can be used to run tests.
    /// </summary>
    public interface TestRunner
    {
        public const string defaultFullNameSeparator = " ";
        public const string defaultTestClassFullNameSeparator = ".";

        /// <summary>
        /// Start a test group.
        /// </summary>
        /// <param name="name">The name of the test group.</param>
        /// <param name="testGroupAction">The <see cref="Action"/> that defines the test group.</param>
        /// <param name="fullNameSeparator">The separator <see cref="string"/> that will be applied
        /// before the new <see cref="TestGroup"/>'s name.</param>
        public void TestGroup(string name, Action testGroupAction, string fullNameSeparator = TestRunner.defaultFullNameSeparator);

        /// <summary>
        /// Start a test group.
        /// </summary>
        /// <param name="name">The name of the test group.</param>
        /// <param name="testGroupAction">The <see cref="Action"/> that defines the test group.</param>
        /// <param name="fullNameSeparator">The separator <see cref="string"/> that will be applied
        /// before the new <see cref="TestGroup"/>'s name.</param>
        public void TestGroup(string name, Func<Task> testGroupAction, string fullNameSeparator = TestRunner.defaultFullNameSeparator);

        /// <summary>
        /// Start a test group that will test the provided type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type whose name will be used as the name of the test group.</typeparam>
        /// <param name="testGroupAction">The <see cref="Action"/> that defines the test group.</param>
        /// <param name="fullNameSeparator">The separator <see cref="string"/> that will be applied
        /// before the new <see cref="TestGroup"/>'s name.</param>
        public void TestType<T>(Action testGroupAction, string fullNameSeparator = TestRunner.defaultTestClassFullNameSeparator);

        /// <summary>
        /// Start a test group that will test the provided type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type whose name will be used as the name of the test group.</typeparam>
        /// <param name="testGroupAction">The <see cref="Action"/> that defines the test group.</param>
        /// <param name="fullNameSeparator">The separator <see cref="string"/> that will be applied
        /// before the new <see cref="TestGroup"/>'s name.</param>
        public void TestType<T>(Func<Task> testGroupAction, string fullNameSeparator = TestRunner.defaultTestClassFullNameSeparator);

        /// <summary>
        /// Start a test group that will test the provided type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type whose name will be used as the name of the test.</typeparam>
        /// <param name="testAction">The <see cref="Action"/> that defines the test.</param>
        /// <param name="fullNameSeparator">The separator <see cref="string"/> that will be applied
        /// before the new <see cref="TestGroup"/>'s name.</param>
        public void TestType<T>(Action<Test> testAction, string fullNameSeparator = TestRunner.defaultTestClassFullNameSeparator);

        /// <summary>
        /// Start a test group that will test the provided type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type whose name will be used as the name of the test.</typeparam>
        /// <param name="testGroupAction">The <see cref="Func"/> that defines the test.</param>
        /// <param name="fullNameSeparator">The separator <see cref="string"/> that will be applied
        /// before the new <see cref="TestGroup"/>'s name.</param>
        public void TestType<T>(Func<Test,Task> testGroupAction, string fullNameSeparator = TestRunner.defaultTestClassFullNameSeparator);

        /// <summary>
        /// Start a test group that will test a method.
        /// </summary>
        /// <param name="methodName">The name of the method to test.</param>
        /// <param name="testGroupAction">The <see cref="Action"/> that defines the test group.</param>
        /// <param name="fullNameSeparator">The separator <see cref="string"/> that will be applied
        /// before the new <see cref="TestGroup"/>'s name.</param>
        public void TestMethod(string methodName, Action testGroupAction, string fullNameSeparator = TestRunner.defaultTestClassFullNameSeparator);

        /// <summary>
        /// Start a test group that will test a method.
        /// </summary>
        /// <param name="methodName">The name of the method to test.</param>
        /// <param name="testGroupAction">The <see cref="Func"/> that defines the test group.</param>
        /// <param name="fullNameSeparator">The separator <see cref="string"/> that will be applied
        /// before the new <see cref="TestGroup"/>'s name.</param>
        public void TestMethod(string methodName, Func<Task> testGroupAction, string fullNameSeparator = TestRunner.defaultTestClassFullNameSeparator);

        /// <summary>
        /// Start a test group that will test a method.
        /// </summary>
        /// <param name="methodName">The name of the method to test.</param>
        /// <param name="testAction">The <see cref="Action"/> that defines the test.</param>
        /// <param name="fullNameSeparator">The separator <see cref="string"/> that will be applied
        /// before the new <see cref="TestGroup"/>'s name.</param>
        public void TestMethod(string methodName, Action<Test> testAction, string fullNameSeparator = TestRunner.defaultTestClassFullNameSeparator);

        /// <summary>
        /// Start a test group that will test a method.
        /// </summary>
        /// <param name="methodName">The name of the method to test.</param>
        /// <param name="testAction">The <see cref="Func"/> that defines the test.</param>
        /// <param name="fullNameSeparator">The separator <see cref="string"/> that will be applied
        /// before the new <see cref="TestGroup"/>'s name.</param>
        public void TestMethod(string methodName, Func<Test,Task> testAction, string fullNameSeparator = TestRunner.defaultTestClassFullNameSeparator);

        /// <summary>
        /// Start a test.
        /// </summary>
        /// <param name="name">The name of the test.</param>
        /// <param name="testAction">The <see cref="Action"/> that defines the test.</param>
        /// <param name="fullNameSeparator">The separator <see cref="string"/> that will be applied
        /// before the new <see cref="TestGroup"/>'s name.</param>
        public void Test(string name, Action<Test> testAction, string fullNameSeparator = TestRunner.defaultFullNameSeparator);

        /// <summary>
        /// Start a test.
        /// </summary>
        /// <param name="name">The name of the test.</param>
        /// <param name="testAction">The <see cref="Func"/> that defines the test.</param>
        /// <param name="fullNameSeparator">The separator <see cref="string"/> that will be applied
        /// before the new <see cref="TestGroup"/>'s name.</param>
        public void Test(string name, Func<Test, Task> testAction, string fullNameSeparator = TestRunner.defaultFullNameSeparator);

        /// <summary>
        /// Get the <see cref="string"/> representation of the provided <paramref name="value"/>
        /// based on the ToString functions that have been registered with this
        /// <see cref="TestRunner"/>.
        /// </summary>
        /// <param name="value">The value to get the <see cref="string"/> representation of.</param>
        public string ToString<T>(T? value);
    }

    public abstract class TestRunnerBase : TestRunner
    {
        public abstract void TestGroup(string name, Action testGroupAction, string fullNameSeparator = TestRunner.defaultFullNameSeparator);

        public virtual void TestGroup(string name, Func<Task> testGroupAction, string fullNameSeparator = TestRunner.defaultFullNameSeparator)
        {
            PreCondition.AssertNotNullAndNotEmpty(name, nameof(name));
            PreCondition.AssertNotNull(testGroupAction, nameof(testGroupAction));

            this.TestGroup(
                name: name,
                testGroupAction: () =>
                {
                    testGroupAction.Invoke().Await();
                },
                fullNameSeparator: fullNameSeparator);
        }

        public virtual void TestType<T>(Action testGroupAction, string fullNameSeparator = TestRunner.defaultTestClassFullNameSeparator)
        {
            this.TestGroup(
                name: Types.GetFullName<T>(),
                testGroupAction: testGroupAction,
                fullNameSeparator: fullNameSeparator);
        }

        public virtual void TestType<T>(Func<Task> testGroupAction, string fullNameSeparator = ".")
        {
            this.TestType<T>(
                testGroupAction: () =>
                {
                    testGroupAction.Invoke().Await();
                },
                fullNameSeparator: fullNameSeparator);
        }

        public virtual void TestType<T>(Action<Test> testAction, string fullNameSeparator = TestRunner.defaultTestClassFullNameSeparator)
        {
            this.Test(
                name: Types.GetFullName<T>(),
                testAction: testAction,
                fullNameSeparator: fullNameSeparator);
        }

        public virtual void TestType<T>(Func<Test,Task> testAction, string fullNameSeparator = ".")
        {
            this.TestType<T>(
                testAction: (Test test) =>
                {
                    testAction.Invoke(test).Await();
                },
                fullNameSeparator: fullNameSeparator);
        }

        public virtual void TestMethod(string name, Action testGroupAction, string fullNameSeparator = TestRunner.defaultTestClassFullNameSeparator)
        {
            this.TestGroup(
                name: name,
                testGroupAction: testGroupAction,
                fullNameSeparator: fullNameSeparator);
        }

        public virtual void TestMethod(string name, Func<Task> testGroupAction, string fullNameSeparator = TestRunner.defaultTestClassFullNameSeparator)
        {
            this.TestMethod(
                name: name,
                testGroupAction: () =>
                {
                    testGroupAction.Invoke().Await();
                },
                fullNameSeparator: fullNameSeparator);
        }

        public virtual void TestMethod(string name, Action<Test> testAction, string fullNameSeparator = TestRunner.defaultTestClassFullNameSeparator)
        {
            this.Test(
                name: name,
                testAction: testAction,
                fullNameSeparator: fullNameSeparator);
        }

        public virtual void TestMethod(string name, Func<Test,Task> testAction, string fullNameSeparator = TestRunner.defaultTestClassFullNameSeparator)
        {
            this.TestMethod(
                name: name,
                testAction: (Test test) =>
                {
                    testAction.Invoke(test).Await();
                },
                fullNameSeparator: fullNameSeparator);
        }

        public abstract void Test(string name, Action<Test> testAction, string fullNameSeparator = TestRunner.defaultFullNameSeparator);

        public virtual void Test(string name, Func<Test, Task> testAction, string fullNameSeparator = TestRunner.defaultFullNameSeparator)
        {
            this.Test(name, (Test test) =>
            {
                testAction.Invoke(test).Await();
            });
        }

        public abstract string ToString<T>(T? value);
    }
}
