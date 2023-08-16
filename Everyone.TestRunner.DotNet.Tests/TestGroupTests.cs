using System;

namespace Everyone
{
    public static class TestGroupTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestType<TestGroup>(() =>
            {
                runner.TestMethod("Create(string,TestGroup?,string)", () =>
                {
                    void ConstructorTest(string? name, TestGroup? parent, string? fullNameSeparator, Exception? expectedException = null)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { name, parent, fullNameSeparator }.Map(runner.ToString))}", (Test test) =>
                        {
                            test.AssertThrows(expectedException, () =>
                            {
                                TestGroup tg = TestGroup.Create(name!, parent, fullNameSeparator!);
                                test.AssertEqual(name, tg.Name);
                                test.AssertEqual(parent, tg.Parent);
                            });
                        });
                    };

                    ConstructorTest(
                        name: null,
                        parent: null,
                        fullNameSeparator: null,
                        expectedException: new PreConditionFailure(
                            "blah"));
                    ConstructorTest(
                        name: "",
                        parent: null,
                        fullNameSeparator: null,
                        expectedException: new PreConditionFailure(
                            "blah"));
                    ConstructorTest(
                        name: "abc",
                        parent: null,
                        fullNameSeparator: null,
                        expectedException: new PreConditionFailure(
                            "blah"));
                    ConstructorTest(
                        name: null,
                        parent: CreateTestGroup("b"),
                        fullNameSeparator: null,
                        expectedException: new PreConditionFailure(
                            "blah"));
                    ConstructorTest(
                        name: "",
                        parent: CreateTestGroup("b"),
                        fullNameSeparator: null,
                        expectedException: new PreConditionFailure(
                            "blah"));
                    ConstructorTest(
                        name: "a",
                        parent: CreateTestGroup("b"),
                        fullNameSeparator: null,
                        expectedException: new PreConditionFailure(
                            "blah"));
                    ConstructorTest(
                        name: null,
                        parent: CreateTestGroup("b", CreateTestGroup("c")),
                        fullNameSeparator: null,
                        expectedException: new PreConditionFailure(
                            "blah"));
                    ConstructorTest(
                        name: "",
                        parent: CreateTestGroup("b", CreateTestGroup("c")),
                        fullNameSeparator: null,
                        expectedException: new PreConditionFailure(
                            "blah"));
                    ConstructorTest(
                        name: "a",
                        parent: CreateTestGroup("b", CreateTestGroup("c")),
                        fullNameSeparator: null,
                        expectedException: new PreConditionFailure(
                            "blah"));
                });

                runner.TestMethod("GetFullName()", () =>
                {
                    void GetFullNameTest(TestGroup tg, string expected)
                    {
                        runner.Test($"with {tg.GetFullName()}", (Test test) =>
                        {
                            test.AssertEqual(expected, tg.GetFullName());
                        });
                    }

                    GetFullNameTest(CreateTestGroup("a"), "a");
                    GetFullNameTest(CreateTestGroup("a", CreateTestGroup("b")), "b a");
                    GetFullNameTest(CreateTestGroup("a", CreateTestGroup("b", CreateTestGroup("c"))), "c b a");
                });
            });
        }

        private static TestGroup CreateTestGroup(string name, TestGroup? parent = null, string fullNameSeparator = TestRunner.defaultFullNameSeparator)
        {
            return TestGroup.Create(
                name: name,
                parent: parent,
                fullNameSeparator: fullNameSeparator);
        }
    }
}
