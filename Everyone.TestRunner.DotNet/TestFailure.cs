using System;

namespace Everyone
{
    public class TestFailure
    {
        private TestFailure(string fullName, Exception exception)
        {
            PreCondition.AssertNotNullAndNotEmpty(fullName, nameof(fullName));
            PreCondition.AssertNotNull(exception, nameof(exception));

            this.FullName = fullName;
            this.Exception = exception;
        }

        public static TestFailure Create(string fullName, Exception exception)
        {
            return new TestFailure(fullName, exception);
        }

        public string FullName { get; }

        public Exception Exception { get; }
    }
}
