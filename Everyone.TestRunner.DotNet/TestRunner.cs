using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Everyone
{
    /// <summary>
    /// A type that can be used to run tests.
    /// </summary>
    public interface TestRunner
    {
        /// <summary>
        /// Set the full name separator that will be applied to the method with the provided name.
        /// </summary>
        /// <param name="methodName">The name of the method to apply the provided
        /// <paramref name="fullNameSeparator"/> to.</param>
        /// <param name="fullNameSeparator">The full name separator to apply to the method with the
        /// provided name.</param>
        public TestRunner SetFullNameSeparator(string methodName, string fullNameSeparator);

        /// <summary>
        /// Get the full name separator <see cref="string"/> that has been registered for the
        /// provided method name.
        /// </summary>
        /// <param name="methodName">The name of the method to get the full name separator of.</param>
        public string GetFullNameSeparator([CallerMemberName] string methodName = "");

        /// <summary>
        /// Start a new test group.
        /// </summary>
        /// <param name="parameters">The <see cref="TestGroupParameters"/> that define the test
        /// group.</param>
        public void TestGroup(TestGroupParameters parameters);

        /// <summary>
        /// Start a test group.
        /// </summary>
        /// <param name="name">The name of the test group.</param>
        /// <param name="action">The <see cref="Action"/> that defines the test group.</param>
        /// <param name="fullNameSeparator">The separator <see cref="string"/> that will be applied
        /// before the new <see cref="TestGroup"/>'s name.</param>
        public void TestGroup(string name, Action action);

        /// <summary>
        /// Start a test group.
        /// </summary>
        /// <param name="name">The name of the test group.</param>
        /// <param name="action">The <see cref="Func{TResult}"/> that defines the test group.</param>
        /// <param name="fullNameSeparator">The separator <see cref="string"/> that will be applied
        /// before the new <see cref="TestGroup"/>'s name.</param>
        public void TestGroup(string name, Func<Task> action);

        /// <summary>
        /// Start a test group that will test a type.
        /// </summary>
        /// <param name="parameters">The <see cref="TestGroupParameters"/> that define the test
        /// group.</param>
        public void TestType(TestGroupParameters parameters);

        /// <summary>
        /// Start a test group that will test the provided type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="typeName">The name of the type that will be used for the test group's
        /// name.</param>
        /// <param name="action">The <see cref="Action"/> that defines the test group.</param>
        public void TestType(string typeName, Action action);

        /// <summary>
        /// Start a test group that will test the provided type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="typeName">The name of the type that will be used for the test group's
        /// name.</param>
        /// <param name="action">The <see cref="Func{TResult}<"/> that defines the test group.</param>
        public void TestType(string typeName, Func<Task> action);

        /// <summary>
        /// Start a test group that will test the provided <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type that will be used for the test group's name.</param>
        /// <param name="action">The <see cref="Action"/> that defines the test group.</param>
        public void TestType(Type type, Action action);

        /// <summary>
        /// Start a test group that will test the provided <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type that will be used for the test group's name.</param>
        /// <param name="action">The <see cref="Func{TResult}"/> that defines the test group.</param>
        public void TestType(Type type, Func<Task> action);

        /// <summary>
        /// Start a test group that will test the provided type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type whose name will be used as the name of the test group.</typeparam>
        /// <param name="action">The <see cref="Action"/> that defines the test group.</param>
        public void TestType<T>(Action action);

        /// <summary>
        /// Start a test group that will test the provided type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type whose name will be used as the name of the test group.</typeparam>
        /// <param name="action">The <see cref="Func{TResult}"/> that defines the test group.</param>
        public void TestType<T>(Func<Task> action);

        /// <summary>
        /// Start a test group that will test a method.
        /// </summary>
        /// <param name="parameters">The <see cref="TestGroupParameters"/> that define the test
        /// group.</param>
        public void TestMethod(TestGroupParameters parameters);

        /// <summary>
        /// Start a test group that will test a method.
        /// </summary>
        /// <param name="methodName">The name of the method to test.</param>
        /// <param name="action">The <see cref="Action"/> that defines the test group.</param>
        public void TestMethod(string methodName, Action action);

        /// <summary>
        /// Start a test group that will test a method.
        /// </summary>
        /// <param name="methodName">The name of the method to test.</param>
        /// <param name="action">The <see cref="Func{TResult}"/> that defines the test group.</param>
        public void TestMethod(string methodName, Func<Task> action);

        /// <summary>
        /// Start a test.
        /// </summary>
        /// <param name="parameters">The <see cref="TestParameters"/> that define the test.</param>
        public void Test(TestParameters parameters);

        /// <summary>
        /// Start a test.
        /// </summary>
        /// <param name="name">The name of the test.</param>
        /// <param name="action">The <see cref="Action{T1}"/> that defines the test.</param>
        public void Test(string name, Action<Test> action);

        /// <summary>
        /// Start a test.
        /// </summary>
        /// <param name="name">The name of the test.</param>
        /// <param name="action">The <see cref="Func{T1,TResult}"/> that defines the test.</param>
        public void Test(string name, Func<Test, Task> action);

        /// <summary>
        /// Start a test that will test a type.
        /// </summary>
        /// <param name="parameters"></param>
        public void TestType(TestParameters parameters);

        /// <summary>
        /// Start a test that will test the provided type.
        /// </summary>
        /// <param name="typeName">The name of the type that will be used for the test's name</param>
        /// <param name="action">The <see cref="Action{T1}"/> that defines the test.</param>
        public void TestType(string typeName, Action<Test> action);

        /// <summary>
        /// Start a test that will test the provided type.
        /// </summary>
        /// <param name="typeName">The name of the type that will be used for the test's name</param>
        /// <param name="action">The <see cref="Func{T1,TResult}"/> that defines the test.</param>
        public void TestType(string typeName, Func<Test, Task> action);

        /// <summary>
        /// Start a test group that will test the provided <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> that will be used for the test's name.</param>
        /// <param name="action">The <see cref="Action{T1}"/> that defines the test.</param>
        public void TestType(Type type, Action<Test> action);

        /// <summary>
        /// Start a test that will test the provided <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> that will be used for the test's name.</param>
        /// <param name="action">The <see cref="Func{T1,TResult}"/> that defines the test.</param>
        public void TestType(Type type, Func<Test, Task> action);

        /// <summary>
        /// Start a test that will test the provided type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type whose name will be used as the name of the test.</typeparam>
        /// <param name="action">The <see cref="Action{T}"/> that defines the test.</param>
        public void TestType<T>(Action<Test> action);

        /// <summary>
        /// Start a test that will test the provided type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type whose name will be used as the name of the test.</typeparam>
        /// <param name="action">The <see cref="Func{T,TResult}"/> that defines the test.</param>
        public void TestType<T>(Func<Test, Task> action);

        /// <summary>
        /// Start a test that will test a method.
        /// </summary>
        /// <param name="parameters">The <see cref="TestParameters"/> that define the test.</param>
        public void TestMethod(TestParameters parameters);

        /// <summary>
        /// Start a test that will test a method.
        /// </summary>
        /// <param name="methodName">The name of the method to test.</param>
        /// <param name="action">The <see cref="Action{T1}"/> that defines the test.</param>
        public void TestMethod(string methodName, Action<Test> action);

        /// <summary>
        /// Start a test that will test a method.
        /// </summary>
        /// <param name="methodName">The name of the method to test.</param>
        /// <param name="action">The <see cref="Func{T1,TResult}"/> that defines the test.</param>
        public void TestMethod(string methodName, Func<Test, Task> action);

        /// <summary>
        /// Get the <see cref="string"/> representation of the provided <paramref name="value"/>
        /// based on the ToString functions that have been registered with this
        /// <see cref="TestRunner"/>.
        /// </summary>
        /// <param name="value">The value to get the <see cref="string"/> representation of.</param>
        public string ToString<T>(T? value);

        /// <summary>
        /// Convert the provided <see cref="Func{TResult}"/> to an <see cref="Action"/>.
        /// </summary>
        /// <param name="asyncAction">The <see cref="Func{TResult}"/> to convert.</param>
        public Action ToAction(Func<Task> asyncAction);

        /// <summary>
        /// Convert the provided <see cref="Func{T1,TResult}"/> to an <see cref="Action{T1}"/>.
        /// </summary>
        /// <param name="asyncAction">The <see cref="Func{TResult}"/> to convert.</param>
        public Action<Test> ToAction(Func<Test,Task> asyncAction);
    }

    public abstract class TestRunnerBase<TDerived> : TestRunner where TDerived : TestRunner
    {
        TestRunner TestRunner.SetFullNameSeparator(string methodName, string fullNameSeparator)
        {
            return this.SetFullNameSeparator(methodName: methodName, fullNameSeparator: fullNameSeparator);
        }

        public abstract TDerived SetFullNameSeparator(string methodName, string fullNameSeparator);

        public abstract string GetFullNameSeparator([CallerMemberName] string methodName = "");

        protected abstract void TestInner(TestParameters parameters);

        public void Test(TestParameters parameters)
        {
            this.TestInner(parameters
                .SetFullNameSeparatorIfUnset(this.GetFullNameSeparator()));
        }

        public void Test(string name, Action<Test> action)
        {
            this.TestInner(TestParameters.Create()
                .SetName(name)
                .SetAction(action)
                .SetFullNameSeparatorIfUnset(this.GetFullNameSeparator()));
        }

        public void Test(string name, Func<Test, Task> action)
        {
            this.TestInner(TestParameters.Create()
                .SetName(name)
                .SetAction(this.ToAction(action))
                .SetFullNameSeparatorIfUnset(this.GetFullNameSeparator()));
        }

        protected abstract void TestGroupInner(TestGroupParameters parameters);

        public void TestGroup(TestGroupParameters parameters)
        {
            this.TestGroupInner(parameters
                .SetFullNameSeparatorIfUnset(this.GetFullNameSeparator()));
        }

        public void TestGroup(string name, Action action)
        {
            this.TestGroupInner(TestGroupParameters.Create()
                .SetName(name)
                .SetAction(action)
                .SetFullNameSeparatorIfUnset(this.GetFullNameSeparator()));
        }

        public void TestGroup(string name, Func<Task> action)
        {
            this.TestGroupInner(TestGroupParameters.Create()
                .SetName(name)
                .SetAction(this.ToAction(action))
                .SetFullNameSeparatorIfUnset(this.GetFullNameSeparator()));
        }

        public void TestMethod(TestGroupParameters parameters)
        {
            PreCondition.AssertNotNull(parameters, nameof(parameters));

            this.TestGroupInner(parameters
                .SetFullNameSeparatorIfUnset(this.GetFullNameSeparator()));
        }

        public void TestMethod(string methodName, Action action)
        {
            this.TestGroupInner(TestGroupParameters.Create()
                .SetName(methodName)
                .SetAction(action)
                .SetFullNameSeparatorIfUnset(this.GetFullNameSeparator()));
        }

        public void TestMethod(string methodName, Func<Task> action)
        {
            this.TestGroupInner(TestGroupParameters.Create()
                .SetName(methodName)
                .SetAction(this.ToAction(action))
                .SetFullNameSeparatorIfUnset(this.GetFullNameSeparator()));
        }

        public void TestMethod(TestParameters parameters)
        {
            PreCondition.AssertNotNull(parameters, nameof(parameters));

            this.TestInner(parameters
                .SetFullNameSeparatorIfUnset(this.GetFullNameSeparator()));
        }

        public void TestMethod(string methodName, Action<Test> action)
        {
            this.TestInner(TestParameters.Create()
                .SetName(methodName)
                .SetAction(action)
                .SetFullNameSeparatorIfUnset(this.GetFullNameSeparator()));
        }

        public void TestMethod(string methodName, Func<Test, Task> action)
        {
            this.TestInner(TestParameters.Create()
                .SetName(methodName)
                .SetAction(this.ToAction(action))
                .SetFullNameSeparatorIfUnset(this.GetFullNameSeparator()));
        }

        public void TestType(TestGroupParameters parameters)
        {
            PreCondition.AssertNotNull(parameters, nameof(parameters));

            this.TestGroupInner(parameters
                .SetFullNameSeparatorIfUnset(this.GetFullNameSeparator()));
        }

        public void TestType(string typeName, Action action)
        {
            this.TestGroupInner(TestGroupParameters.Create()
                .SetName(typeName)
                .SetAction(action)
                .SetFullNameSeparatorIfUnset(this.GetFullNameSeparator()));
        }

        public void TestType(string typeName, Func<Task> action)
        {
            this.TestGroupInner(TestGroupParameters.Create()
                .SetName(typeName)
                .SetAction(this.ToAction(action))
                .SetFullNameSeparatorIfUnset(this.GetFullNameSeparator()));
        }

        public void TestType(Type type, Action action)
        {
            this.TestGroupInner(TestGroupParameters.Create()
                .SetName(type)
                .SetAction(action)
                .SetFullNameSeparatorIfUnset(this.GetFullNameSeparator()));
        }

        public void TestType(Type type, Func<Task> action)
        {
            this.TestGroupInner(TestGroupParameters.Create()
                .SetName(type)
                .SetAction(this.ToAction(action))
                .SetFullNameSeparatorIfUnset(this.GetFullNameSeparator()));
        }

        public void TestType<T>(Action action)
        {
            this.TestGroupInner(TestGroupParameters.Create()
                .SetName<T>()
                .SetAction(action)
                .SetFullNameSeparatorIfUnset(this.GetFullNameSeparator()));
        }

        public void TestType<T>(Func<Task> action)
        {
            this.TestGroupInner(TestGroupParameters.Create()
                .SetName<T>()
                .SetAction(this.ToAction(action))
                .SetFullNameSeparatorIfUnset(this.GetFullNameSeparator()));
        }

        public void TestType(TestParameters parameters)
        {
            PreCondition.AssertNotNull(parameters, nameof(parameters));

            this.TestInner(parameters
                .SetFullNameSeparatorIfUnset(this.GetFullNameSeparator()));
        }

        public void TestType(string typeName, Action<Test> action)
        {
            this.TestInner(TestParameters.Create()
                .SetName(typeName)
                .SetAction(action)
                .SetFullNameSeparatorIfUnset(this.GetFullNameSeparator()));
        }

        public void TestType(string typeName, Func<Test, Task> action)
        {
            this.TestInner(TestParameters.Create()
                .SetName(typeName)
                .SetAction(this.ToAction(action))
                .SetFullNameSeparatorIfUnset(this.GetFullNameSeparator()));
        }

        public void TestType(Type type, Action<Test> action)
        {
            this.TestInner(TestParameters.Create()
                .SetName(type)
                .SetAction(action)
                .SetFullNameSeparatorIfUnset(this.GetFullNameSeparator()));
        }

        public void TestType(Type type, Func<Test, Task> action)
        {
            this.TestInner(TestParameters.Create()
                .SetName(type)
                .SetAction(this.ToAction(action))
                .SetFullNameSeparatorIfUnset(this.GetFullNameSeparator()));
        }

        public void TestType<T>(Action<Test> action)
        {
            this.TestInner(TestParameters.Create()
                .SetName<T>()
                .SetAction(action)
                .SetFullNameSeparatorIfUnset(this.GetFullNameSeparator()));
        }

        public void TestType<T>(Func<Test, Task> action)
        {
            this.TestInner(TestParameters.Create()
                .SetName<T>()
                .SetAction(this.ToAction(action))
                .SetFullNameSeparatorIfUnset(this.GetFullNameSeparator()));
        }

        public virtual Action ToAction(Func<Task> asyncAction)
        {
            PreCondition.AssertNotNull(asyncAction, nameof(asyncAction));

            return () => asyncAction.Invoke().Await();
        }

        public virtual Action<Test> ToAction(Func<Test, Task> asyncAction)
        {
            PreCondition.AssertNotNull(asyncAction, nameof(asyncAction));

            return (Test test) => asyncAction.Invoke(test).Await();
        }

        public abstract string ToString<T>(T? value);
    }
}
