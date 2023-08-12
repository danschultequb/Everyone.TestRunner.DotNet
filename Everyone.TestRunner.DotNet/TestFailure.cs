using System;

namespace Everyone
{
    public class TestFailure
    {
        public TestFailure(string fullName, Exception exception)
        {
            this.FullName = fullName;
            this.Exception = exception;
        }

        public string FullName { get; }

        public Exception Exception { get; }
    }
}
