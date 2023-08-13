using System;

namespace Everyone
{
    public static class TestFailureTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestGroup(typeof(TestFailure), () =>
            {
                runner.TestGroup("Create(string,Exception)", () =>
                {
                    void CreateTest(string? fullName, Exception? exception, Exception? expectedException = null)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { fullName, exception }.Map(runner.ToString))}", (Test test) =>
                        {
                            if (expectedException == null)
                            {
                                TestFailure failure = TestFailure.Create(fullName!, exception!);
                                test.AssertNotNull(failure);
                                test.AssertEqual(fullName, failure.FullName);
                                test.AssertEqual(exception, failure.Exception);
                            }
                            else
                            {
                                test.AssertThrows(expectedException, () => TestFailure.Create(fullName!, exception!));
                            }
                        });
                    }

                    CreateTest(
                        fullName: null,
                        exception: new Exception("Hello"),
                        expectedException: new PreConditionFailure(
                            "Expression: fullName",
                            "Expected: not null and not empty",
                            "Actual:   null"));
                    CreateTest(
                        fullName: "",
                        exception: new Exception("Hello"),
                        expectedException: new PreConditionFailure(
                            "Expression: fullName",
                            "Expected: not null and not empty",
                            "Actual:   \"\""));
                    CreateTest(
                        fullName: "a b c",
                        exception: null,
                        expectedException: new PreConditionFailure(
                            "Expression: exception",
                            "Expected: not null",
                            "Actual:       null"));
                    CreateTest(
                        fullName: "a b c",
                        exception: new Exception("hello"));
                });
            });
        }
    }
}
