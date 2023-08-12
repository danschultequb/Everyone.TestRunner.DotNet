using System;

namespace Everyone
{
    /// <summary>
    /// The <see cref="Exception"/> that is thrown when a <see cref="Test"/> assertion fails.
    /// </summary>
    public class TestFailureException : System.Exception
    {
        public TestFailureException(params string[] messageLines)
            : base(string.Join(Environment.NewLine, messageLines))
        {
        }
    }
}
