using System;

namespace Everyone
{
    public class BasicTestRunner : TestRunnerBase
    {
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

        public override void TestGroup(string name, Action testGroupAction, string fullNameSeparator = TestRunner.defaultFullNameSeparator)
        {
            PreCondition.AssertNotNullAndNotEmpty(name, nameof(name));
            PreCondition.AssertNotNull(testGroupAction, nameof(testGroupAction));
            PreCondition.AssertNotNull(fullNameSeparator, nameof(fullNameSeparator));

            this.CurrentTestGroup = Everyone.TestGroup.Create(
                name: name,
                parent: this.CurrentTestGroup,
                fullNameSeparator: fullNameSeparator);
            try
            {
                this.testGroupStarted.Invoke(this.CurrentTestGroup);

                testGroupAction.Invoke();
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

        public override void Test(string name, Action<Test> testAction, string fullNameSeparator = TestRunner.defaultFullNameSeparator)
        {
            Test test = Everyone.Test.Create(name, this.CurrentTestGroup, fullNameSeparator, this.compareFunctions, this.messageFunctions);
            if (this.ShouldInvokeTest(test))
            {
                try
                {
                    this.testStarted.Invoke(test);

                    testAction.Invoke(test);

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
