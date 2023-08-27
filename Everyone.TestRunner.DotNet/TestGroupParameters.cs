using System;
using System.Threading.Tasks;

namespace Everyone
{
    public class TestGroupParameters : TestChildParameters<TestGroupParameters>
    {
        private Action? action;

        protected TestGroupParameters()
        {
        }

        public static TestGroupParameters Create()
        {
            return new TestGroupParameters();
        }

        public Action GetAction()
        {
            Pre.Condition.AssertNotNull(this.action, nameof(this.action));

            return this.action!;
        }

        public TestGroupParameters SetAction(Action action)
        {
            Pre.Condition.AssertNotNull(action, nameof(action));

            this.action = action;

            return this;
        }
    }
}
