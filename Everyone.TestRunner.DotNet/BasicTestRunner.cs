using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Everyone
{
    public class BasicTestRunner : TestRunnerBase<BasicTestRunner>
    {
        private readonly IDictionary<string, string> fullNameSeparatorMap;
        private readonly CompareFunctions compareFunctions;
        private readonly ToStringFunctions toStringFunctions;
        private readonly AssertMessageFunctions messageFunctions;

        private readonly RunnableEvent<TestGroup> testGroupStarted = Event.Create<TestGroup>();
        private readonly RunnableEvent<TestGroup, Exception> testGroupFailed = Event.Create<TestGroup,Exception>();
        private readonly RunnableEvent<TestGroup> testGroupEnded = Event.Create<TestGroup>();

        private readonly RunnableEvent<Test> testStarted = Event.Create<Test>();
        private readonly RunnableEvent<Test,Exception> testFailed = Event.Create<Test,Exception>();
        private readonly RunnableEvent<Test> testPassed = Event.Create<Test>();
        private readonly RunnableEvent<Test> testEnded = Event.Create<Test>();

        protected BasicTestRunner(
            CompareFunctions? compareFunctions,
            AssertMessageFunctions? messageFunctions,
            ToStringFunctions? toStringFunctions)
        {
            this.fullNameSeparatorMap = new Dictionary<string, string>();
            this.compareFunctions = compareFunctions ?? CompareFunctions.Create();
            if (messageFunctions == null)
            {
                if (toStringFunctions == null)
                {
                    toStringFunctions = ToStringFunctions.Create();
                }
                messageFunctions = AssertMessageFunctions.Create(toStringFunctions);
            }
            else if (toStringFunctions == null)
            {
                toStringFunctions = messageFunctions.ToStringFunctions;
            }
            this.toStringFunctions = toStringFunctions;
            this.messageFunctions = messageFunctions;
        }

        public static BasicTestRunner Create(
            CompareFunctions? compareFunctions = null,
            AssertMessageFunctions? messageFunctions = null,
            ToStringFunctions? toStringFunctions = null)
        {
            return new BasicTestRunner(compareFunctions, messageFunctions, toStringFunctions);
        }

        public TestGroup? CurrentTestGroup { get; private set; }

        public Disposable OnTestGroupStarted(Action<TestGroup> action)
        {
            return this.testGroupStarted.Subscribe(action);
        }

        public Disposable OnTestGroupFailed(Action<TestGroup,Exception> action)
        {
            return this.testGroupFailed.Subscribe(action);
        }

        public Disposable OnTestGroupEnded(Action<TestGroup> action)
        {
            return this.testGroupEnded.Subscribe(action);
        }

        public Disposable OnTestStarted(Action<Test> action)
        {
            return this.testStarted.Subscribe(action);
        }

        public Disposable OnTestFailed(Action<Test,Exception> action)
        {
            return this.testFailed.Subscribe(action);
        }

        public Disposable OnTestPassed(Action<Test> action)
        {
            return this.testPassed.Subscribe(action);
        }

        public Disposable OnTestEnded(Action<Test> action)
        {
            return this.testEnded.Subscribe(action);
        }

        public virtual bool ShouldInvokeTest(Test test)
        {
            return true;
        }

        public override BasicTestRunner SetFullNameSeparator(string methodName, string fullNameSeparator)
        {
            Pre.Condition.AssertNotNullAndNotEmpty(methodName, nameof(methodName));
            Pre.Condition.AssertNotNullAndNotEmpty(fullNameSeparator, nameof(fullNameSeparator));

            this.fullNameSeparatorMap[methodName] = fullNameSeparator;

            return this;
        }

        public override string GetFullNameSeparator([CallerMemberName] string methodName = "")
        {
            string? result;
            if (!this.fullNameSeparatorMap.TryGetValue(methodName, out result) || result == null)
            {
                result = " ";
            }
            return result;
        }

        protected override void TestGroupInner(TestGroupParameters parameters)
        {
            Pre.Condition.AssertNotNull(parameters, nameof(parameters));
            Pre.Condition.AssertNotNullAndNotEmpty(parameters.GetName(), "parameters.GetName()");
            Pre.Condition.AssertNotNullAndNotEmpty(parameters.GetFullNameSeparator(), "parameters.GetFullNameSeparator()");
            Pre.Condition.AssertNotNull(parameters.GetAction(), "parameters.GetAction()");

            string name = parameters.GetName()!;
            string fullNameSeparator = parameters.GetFullNameSeparator()!;
            Action action = parameters.GetAction()!;

            this.CurrentTestGroup = Everyone.TestGroup.Create(
                name: name,
                parent: this.CurrentTestGroup,
                fullNameSeparator: fullNameSeparator);
            try
            {
                this.testGroupStarted.Invoke(this.CurrentTestGroup);

                action.Invoke();
            }
            catch (Exception e)
            {
                this.testGroupFailed.Invoke(this.CurrentTestGroup, e);
            }
            finally
            {
                this.testGroupEnded.Invoke(this.CurrentTestGroup);

                this.CurrentTestGroup = this.CurrentTestGroup.Parent;
            }
        }

        protected override void TestInner(TestParameters parameters)
        {
            Pre.Condition.AssertNotNull(parameters, nameof(parameters));
            Pre.Condition.AssertNotNullAndNotEmpty(parameters.GetName(), "parameters.GetName()");
            Pre.Condition.AssertNotNullAndNotEmpty(parameters.GetFullNameSeparator(), "parameters.GetFullNameSeparator()");
            Pre.Condition.AssertNotNull(parameters.GetAction(), "parameters.GetAction()");

            string name = parameters.GetName()!;
            string fullNameSeparator = parameters.GetFullNameSeparator()!;
            Action<Test> action = parameters.GetAction()!;

            Test test = Everyone.Test.Create(name, this.CurrentTestGroup, fullNameSeparator, this.compareFunctions, this.messageFunctions);
            if (this.ShouldInvokeTest(test))
            {
                try
                {
                    this.testStarted.Invoke(test);

                    action.Invoke(test);

                    this.testPassed.Invoke(test);
                }
                catch (Exception e)
                {
                    this.testFailed.Invoke(test, e);
                }
                finally
                {
                    this.testEnded.Invoke(test);
                }
            }
        }

        public override string ToString<T>(T? value) where T : default
        {
            return this.toStringFunctions.ToString(value);
        }
    }
}
