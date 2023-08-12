namespace Everyone
{
    public static class TestGroupTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestGroup(typeof(TestGroup), () =>
            {
                runner.TestGroup("Constructor(string,TestGroup?)", () =>
                {
                    void ConstructorTest(string? name, TestGroup? parent)
                    {
                        runner.Test($"with {name.EscapeAndQuote()} and {Strings.EscapeAndQuote(parent?.GetFullName())}", (Test test) =>
                        {
                            TestGroup tg = new TestGroup(name!, parent);
                            test.AssertEqual(name, tg.Name);
                            test.AssertEqual(parent, tg.Parent);
                        });
                    };

                    ConstructorTest(null, null);
                    ConstructorTest("", null);
                    ConstructorTest("abc", null);
                    ConstructorTest(null, new TestGroup("b"));
                    ConstructorTest("", new TestGroup("b"));
                    ConstructorTest("a", new TestGroup("b"));
                    ConstructorTest(null, new TestGroup("b", new TestGroup("c")));
                    ConstructorTest("", new TestGroup("b", new TestGroup("c")));
                    ConstructorTest("a", new TestGroup("b", new TestGroup("c")));
                });

                runner.TestGroup("GetFullName()", () =>
                {
                    void GetFullNameTest(TestGroup tg, string expected)
                    {
                        runner.Test($"with {tg.GetFullName()}", (Test test) =>
                        {
                            test.AssertEqual(expected, tg.GetFullName());
                        });
                    }

                    GetFullNameTest(new TestGroup("a"), "a");
                    GetFullNameTest(new TestGroup("a", new TestGroup("b")), "b a");
                    GetFullNameTest(new TestGroup("a", new TestGroup("b", new TestGroup("c"))), "c b a");
                });
            });
        }
    }
}
