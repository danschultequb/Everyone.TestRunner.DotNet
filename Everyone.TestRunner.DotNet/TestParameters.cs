using System.Threading.Tasks;
using System;

namespace Everyone
{
    public class TestParameters : TestChildParameters<TestParameters>
    {
        private Action<Test> action = (Test test) => { };

        protected TestParameters()
        {
        }

        public static TestParameters Create()
        {
            return new TestParameters();
        }

        public Action<Test> GetAction()
        {
            return this.action;
        }

        public TestParameters SetAction(Action<Test> action)
        {
            PreCondition.AssertNotNull(action, nameof(action));

            this.action = action;

            return this;
        }
    }
}
