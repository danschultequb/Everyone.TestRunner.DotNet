using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Everyone
{
    public static class BasicTestRunnerTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestGroup(typeof(BasicTestRunner), () =>
            {
                runner.Test("Create()", (Test test) =>
                {
                    BasicTestRunner btr = BasicTestRunner.Create();
                    test.AssertNotNull(btr);
                    test.AssertNull(btr.CurrentTestGroup);
                });

                runner.TestGroup("TestGroup(Type,Action)", () =>
                {
                    runner.Test($"with null {nameof(Type)}", (Test test) =>
                    {
                        TestRunner btr = BasicTestRunner.Create();
                        test.AssertThrows(new ArgumentNullException("type"), () =>
                        {
                            ((TestRunner)btr).TestGroup((Type)null!, () => { });
                        });
                    });

                    runner.Test($"with null {nameof(Action)}", (Test test) =>
                    {
                        BasicTestRunner btr = BasicTestRunner.Create();
                        test.AssertThrows(() => ((TestRunner)btr).TestGroup(typeof(string), (Action)null!),
                            new PreConditionFailure(
                                "Expression: testGroupAction",
                                "Expected: not null",
                                "Actual:       null"));
                    });

                    runner.Test("with valid arguments", (Test test) =>
                    {
                        BasicTestRunner btr = BasicTestRunner.Create();
                        int counter = 0;
                        ((TestRunner)btr).TestGroup(typeof(string), () =>
                        {
                            test.AssertNotNull(btr.CurrentTestGroup);
                            test.AssertEqual("System.String", btr.CurrentTestGroup.GetFullName());
                            counter++;
                        });
                        test.AssertNull(btr.CurrentTestGroup);
                        test.AssertEqual(1, counter);
                    });
                });

                runner.TestGroup("TestGroup(Type,Func<Task>)", () =>
                {
                    runner.Test($"with null {nameof(Type)}", (Test test) =>
                    {
                        TestRunner btr = BasicTestRunner.Create();
                        test.AssertThrows(new ArgumentNullException("type"), () =>
                        {
                            ((TestRunner)btr).TestGroup((Type)null!, () => Task.CompletedTask);
                        });
                    });

                    runner.Test($"with null {nameof(Func<Task>)}", (Test test) =>
                    {
                        BasicTestRunner btr = BasicTestRunner.Create();
                        test.AssertThrows(new ArgumentNullException("testGroupAction"), () =>
                        {
                            ((TestRunner)btr).TestGroup(typeof(string), (Func<Task>)null!);
                        });
                    });

                    runner.Test("with valid arguments", (Test test) =>
                    {
                        BasicTestRunner btr = BasicTestRunner.Create();
                        int counter = 0;
                        ((TestRunner)btr).TestGroup(typeof(string), () =>
                        {
                            test.AssertNotNull(btr.CurrentTestGroup);
                            test.AssertEqual("System.String", btr.CurrentTestGroup.GetFullName());
                            counter++;
                            return Task.CompletedTask;
                        });
                        test.AssertNull(btr.CurrentTestGroup);
                        test.AssertEqual(1, counter);
                    });
                });

                runner.TestGroup("TestGroup(string,Action)", () =>
                {
                    runner.Test($"with null name", (Test test) =>
                    {
                        TestRunner btr = BasicTestRunner.Create();
                        test.AssertThrows(() => ((TestRunner)btr).TestGroup((string)null!, () => { }),
                            new PreConditionFailure(
                                "Expression: name",
                                "Expected: not null and not empty",
                                "Actual:   null"));
                    });

                    runner.Test($"with empty name", (Test test) =>
                    {
                        TestRunner btr = BasicTestRunner.Create();
                        test.AssertThrows(() => ((TestRunner)btr).TestGroup("", () => { }),
                            new PreConditionFailure(
                                "Expression: name",
                                "Expected: not null and not empty",
                                "Actual:   \"\""));
                    });

                    runner.Test($"with null {nameof(Action)}", (Test test) =>
                    {
                        BasicTestRunner btr = BasicTestRunner.Create();
                        test.AssertThrows(() => ((TestRunner)btr).TestGroup("hello", (Action)null!),
                            new PreConditionFailure(
                                "Expression: testGroupAction",
                                "Expected: not null",
                                "Actual:       null"));
                    });

                    runner.Test("with valid arguments", (Test test) =>
                    {
                        BasicTestRunner btr = BasicTestRunner.Create();
                        int counter = 0;
                        ((TestRunner)btr).TestGroup("hello", () =>
                        {
                            test.AssertNotNull(btr.CurrentTestGroup);
                            test.AssertEqual("hello", btr.CurrentTestGroup.GetFullName());
                            counter++;
                        });
                        test.AssertNull(btr.CurrentTestGroup);
                        test.AssertEqual(1, counter);
                    });
                });

                runner.TestGroup("TestGroup(string,Func<Task>)", () =>
                {
                    runner.Test($"with null name", (Test test) =>
                    {
                        TestRunner btr = BasicTestRunner.Create();
                        test.AssertThrows(new ArgumentException("name cannot be null or empty.", "name"), () =>
                        {
                            ((TestRunner)btr).TestGroup((string)null!, () => Task.CompletedTask);
                        });
                    });

                    runner.Test($"with empty name", (Test test) =>
                    {
                        TestRunner btr = BasicTestRunner.Create();
                        test.AssertThrows(new ArgumentException("name cannot be null or empty.", "name"), () =>
                        {
                            ((TestRunner)btr).TestGroup("", () => Task.CompletedTask);
                        });
                    });

                    runner.Test($"with null {nameof(Func<Task>)}", (Test test) =>
                    {
                        BasicTestRunner btr = BasicTestRunner.Create();
                        test.AssertThrows(new ArgumentNullException("testGroupAction"), () =>
                        {
                            ((TestRunner)btr).TestGroup("hello", (Func<Task>)null!);
                        });
                    });

                    runner.Test("with valid arguments", (Test test) =>
                    {
                        BasicTestRunner btr = BasicTestRunner.Create();
                        int counter = 0;
                        ((TestRunner)btr).TestGroup("hello", () =>
                        {
                            test.AssertNotNull(btr.CurrentTestGroup);
                            test.AssertEqual("hello", btr.CurrentTestGroup.GetFullName());
                            counter++;
                            return Task.CompletedTask;
                        });
                        test.AssertNull(btr.CurrentTestGroup);
                        test.AssertEqual(1, counter);
                    });
                });

                runner.TestGroup("OnTestGroupStarted(Action<TestGroup>)", () =>
                {
                    runner.Test("with null", (Test test) =>
                    {
                        BasicTestRunner btr = BasicTestRunner.Create();
                        test.AssertThrows(new ArgumentNullException("action"), () =>
                        {
                            btr.OnTestGroupStarted((Action<TestGroup>)null!);
                        });
                    });

                    runner.Test("with non-null", (Test test) =>
                    {
                        List<string> startedTestGroups = new List<string>();
                        BasicTestRunner btr = BasicTestRunner.Create();

                        Disposable subscription = btr.OnTestGroupStarted((TestGroup tg) => startedTestGroups.Add(tg.GetFullName()));
                        test.AssertNotNull(subscription);
                        test.AssertFalse(subscription.Disposed);

                        btr.TestGroup("fake test group", () =>
                        {
                            test.AssertEqual(
                                new[] { "fake test group" },
                                startedTestGroups);
                        });
                        test.AssertEqual(
                            new[] { "fake test group" },
                            startedTestGroups);

                        test.AssertTrue(subscription.Dispose());
                        test.AssertTrue(subscription.Disposed);

                        btr.TestGroup("fake test group 2", () =>
                        {
                            test.AssertEqual(
                                new[] { "fake test group" },
                                startedTestGroups);
                        });
                        test.AssertEqual(
                            new[] { "fake test group" },
                            startedTestGroups);

                        test.AssertFalse(subscription.Dispose());
                        test.AssertTrue(subscription.Disposed);
                    });
                });

                runner.TestGroup("OnTestGroupFailed(Action<TestGroup,Exception>)", () =>
                {
                    runner.Test("with null", (Test test) =>
                    {
                        BasicTestRunner btr = BasicTestRunner.Create();
                        test.AssertThrows(new ArgumentNullException("action"), () =>
                        {
                            btr.OnTestGroupFailed((Action<TestGroup,Exception>)null!);
                        });
                    });

                    runner.Test($"with {nameof(TestFailureException)}", (Test test) =>
                    {
                        List<(string,Exception)> failedTestGroups = new List<(string,Exception)>();
                        BasicTestRunner btr = BasicTestRunner.Create();

                        Disposable subscription = btr.OnTestGroupFailed((TestGroup tg, Exception e) => failedTestGroups.Add((tg.GetFullName(), e)));
                        test.AssertNotNull(subscription);
                        test.AssertFalse(subscription.Disposed);

                        btr.TestGroup("fake test group", () =>
                        {
                            test.AssertEqual(new (string, Exception)[0], failedTestGroups);

                            throw new TestFailureException("oops!");
                        });
                        test.AssertEqual(
                            new (string,Exception)[]
                            {
                                ("fake test group", new TestFailureException("oops!"))
                            },
                            failedTestGroups);

                        test.AssertTrue(subscription.Dispose());
                        test.AssertTrue(subscription.Disposed);

                        btr.TestGroup("fake test group 2", () =>
                        {
                            test.AssertEqual(
                                new (string, Exception)[]
                                {
                                    ("fake test group", new TestFailureException("oops!"))
                                },
                                failedTestGroups);

                            throw new TestFailureException("oops again!");
                        });
                        test.AssertEqual(
                            new (string, Exception)[]
                            {
                                ("fake test group", new TestFailureException("oops!"))
                            },
                            failedTestGroups);

                        test.AssertFalse(subscription.Dispose());
                        test.AssertTrue(subscription.Disposed);
                    });

                    runner.Test($"with {nameof(ArgumentNullException)}", (Test test) =>
                    {
                        List<(string, Exception)> failedTestGroups = new List<(string, Exception)>();
                        BasicTestRunner btr = BasicTestRunner.Create();

                        Disposable subscription = btr.OnTestGroupFailed((TestGroup tg, Exception e) => failedTestGroups.Add((tg.GetFullName(), e)));
                        test.AssertNotNull(subscription);
                        test.AssertFalse(subscription.Disposed);

                        btr.TestGroup("fake test group", () =>
                        {
                            test.AssertEqual(new (string, Exception)[0], failedTestGroups);

                            throw new ArgumentNullException("oops!");
                        });
                        test.AssertEqual(
                            new (string, Exception)[]
                            {
                                ("fake test group", new ArgumentNullException("oops!"))
                            },
                            failedTestGroups);

                        test.AssertTrue(subscription.Dispose());
                        test.AssertTrue(subscription.Disposed);

                        btr.TestGroup("fake test group 2", () =>
                        {
                            test.AssertEqual(
                                new (string, Exception)[]
                                {
                                    ("fake test group", new ArgumentNullException("oops!"))
                                },
                                failedTestGroups);

                            throw new ArgumentNullException("oops again!");
                        });
                        test.AssertEqual(
                            new (string, Exception)[]
                            {
                                ("fake test group", new ArgumentNullException("oops!"))
                            },
                            failedTestGroups);

                        test.AssertFalse(subscription.Dispose());
                        test.AssertTrue(subscription.Disposed);
                    });
                });

                runner.TestGroup("OnTestGroupEnded(Action<TestGroup>)", () =>
                {
                    runner.Test("with null", (Test test) =>
                    {
                        BasicTestRunner btr = BasicTestRunner.Create();
                        test.AssertThrows(new ArgumentNullException("action"), () =>
                        {
                            btr.OnTestGroupEnded((Action<TestGroup>)null!);
                        });
                    });

                    runner.Test($"with {nameof(TestFailureException)}", (Test test) =>
                    {
                        List<string> endedTestGroups = new List<string>();
                        BasicTestRunner btr = BasicTestRunner.Create();

                        Disposable subscription = btr.OnTestGroupEnded((TestGroup tg) => endedTestGroups.Add((tg.GetFullName())));
                        test.AssertNotNull(subscription);
                        test.AssertFalse(subscription.Disposed);

                        btr.TestGroup("fake test group", () =>
                        {
                            test.AssertEqual(new string[0], endedTestGroups);

                            throw new TestFailureException("oops!");
                        });
                        test.AssertEqual(
                            new[] { "fake test group" },
                            endedTestGroups);

                        test.AssertTrue(subscription.Dispose());
                        test.AssertTrue(subscription.Disposed);

                        btr.TestGroup("fake test group 2", () =>
                        {
                            test.AssertEqual(
                                new[] { "fake test group" },
                                endedTestGroups);

                            throw new TestFailureException("oops again!");
                        });
                        test.AssertEqual(
                            new[] { "fake test group" },
                            endedTestGroups);

                        test.AssertFalse(subscription.Dispose());
                        test.AssertTrue(subscription.Disposed);
                    });

                    runner.Test($"with {nameof(ArgumentNullException)}", (Test test) =>
                    {
                        List<string> endedTestGroups = new List<string>();
                        BasicTestRunner btr = BasicTestRunner.Create();

                        Disposable subscription = btr.OnTestGroupEnded((TestGroup tg) => endedTestGroups.Add((tg.GetFullName())));
                        test.AssertNotNull(subscription);
                        test.AssertFalse(subscription.Disposed);

                        btr.TestGroup("fake test group", () =>
                        {
                            test.AssertEqual(new string[0], endedTestGroups);

                            throw new ArgumentNullException("oops!");
                        });
                        test.AssertEqual(
                            new[] { "fake test group" },
                            endedTestGroups);

                        test.AssertTrue(subscription.Dispose());
                        test.AssertTrue(subscription.Disposed);

                        btr.TestGroup("fake test group 2", () =>
                        {
                            test.AssertEqual(
                                new[] { "fake test group" },
                                endedTestGroups);

                            throw new ArgumentNullException("oops again!");
                        });
                        test.AssertEqual(
                            new[] { "fake test group" },
                            endedTestGroups);

                        test.AssertFalse(subscription.Dispose());
                        test.AssertTrue(subscription.Disposed);
                    });

                    runner.Test($"with no exceptions", (Test test) =>
                    {
                        List<string> endedTestGroups = new List<string>();
                        BasicTestRunner btr = BasicTestRunner.Create();

                        Disposable subscription = btr.OnTestGroupEnded((TestGroup tg) => endedTestGroups.Add((tg.GetFullName())));
                        test.AssertNotNull(subscription);
                        test.AssertFalse(subscription.Disposed);

                        btr.TestGroup("fake test group", () =>
                        {
                            test.AssertEqual(new string[0], endedTestGroups);
                        });
                        test.AssertEqual(
                            new[] { "fake test group" },
                            endedTestGroups);

                        test.AssertTrue(subscription.Dispose());
                        test.AssertTrue(subscription.Disposed);

                        btr.TestGroup("fake test group 2", () =>
                        {
                            test.AssertEqual(
                                new[] { "fake test group" },
                                endedTestGroups);
                        });
                        test.AssertEqual(
                            new[] { "fake test group" },
                            endedTestGroups);

                        test.AssertFalse(subscription.Dispose());
                        test.AssertTrue(subscription.Disposed);
                    });
                });

                runner.TestGroup("OnTestStarted(Action<Test>)", () =>
                {
                    runner.Test("with null", (Test test) =>
                    {
                        BasicTestRunner btr = BasicTestRunner.Create();
                        test.AssertThrows(new ArgumentNullException("action"), () =>
                        {
                            btr.OnTestStarted((Action<Test>)null!);
                        });
                    });

                    runner.Test("with passing test", (Test test) =>
                    {
                        List<string> startedTests = new List<string>();
                        BasicTestRunner btr = BasicTestRunner.Create();

                        Disposable subscription = btr.OnTestStarted((Test t) => startedTests.Add(t.GetFullName()));
                        test.AssertNotNull(subscription);
                        test.AssertFalse(subscription.Disposed);

                        btr.TestGroup("fake test group", () =>
                        {
                            test.AssertEqual(new string[0], startedTests);

                            btr.Test("fake test", (Test test) => { });

                            test.AssertEqual(
                                new[] { "fake test group fake test" },
                                startedTests);
                        });
                        test.AssertEqual(
                            new[] { "fake test group fake test" },
                            startedTests);

                        test.AssertTrue(subscription.Dispose());
                        test.AssertTrue(subscription.Disposed);

                        btr.TestGroup("fake test group 2", () =>
                        {
                            test.AssertEqual(
                                new[] { "fake test group fake test" },
                                startedTests);

                            btr.Test("fake test", (Test test) => { });

                            test.AssertEqual(
                                new[] { "fake test group fake test" },
                                startedTests);
                        });
                        test.AssertEqual(
                            new[] { "fake test group fake test" },
                            startedTests);

                        test.AssertFalse(subscription.Dispose());
                        test.AssertTrue(subscription.Disposed);
                    });

                    runner.Test("with failing test", (Test test) =>
                    {
                        List<string> startedTests = new List<string>();
                        BasicTestRunner btr = BasicTestRunner.Create();

                        Disposable subscription = btr.OnTestStarted((Test t) => startedTests.Add(t.GetFullName()));
                        test.AssertNotNull(subscription);
                        test.AssertFalse(subscription.Disposed);

                        btr.TestGroup("fake test group", () =>
                        {
                            test.AssertEqual(new string[0], startedTests);

                            btr.Test("fake test", (Test test) => { test.Fail("oops!"); });

                            test.AssertEqual(
                                new[] { "fake test group fake test" },
                                startedTests);
                        });
                        test.AssertEqual(
                            new[] { "fake test group fake test" },
                            startedTests);

                        test.AssertTrue(subscription.Dispose());
                        test.AssertTrue(subscription.Disposed);

                        btr.TestGroup("fake test group 2", () =>
                        {
                            test.AssertEqual(
                                new[] { "fake test group fake test" },
                                startedTests);

                            btr.Test("fake test", (Test test) => { test.Fail("oops!"); });

                            test.AssertEqual(
                                new[] { "fake test group fake test" },
                                startedTests);
                        });
                        test.AssertEqual(
                            new[] { "fake test group fake test" },
                            startedTests);

                        test.AssertFalse(subscription.Dispose());
                        test.AssertTrue(subscription.Disposed);
                    });
                });

                runner.TestGroup("OnTestFailed(Action<Test,Exception>)", () =>
                {
                    runner.Test("with null", (Test test) =>
                    {
                        BasicTestRunner btr = BasicTestRunner.Create();
                        test.AssertThrows(new ArgumentNullException("action"), () =>
                        {
                            btr.OnTestFailed((Action<Test,Exception>)null!);
                        });
                    });

                    runner.Test("with passing test", (Test test) =>
                    {
                        List<(string,Exception)> failedTests = new List<(string,Exception)>();
                        BasicTestRunner btr = BasicTestRunner.Create();

                        Disposable subscription = btr.OnTestFailed((Test t, Exception e) => failedTests.Add((t.GetFullName(), e)));
                        test.AssertNotNull(subscription);
                        test.AssertFalse(subscription.Disposed);

                        btr.TestGroup("fake test group", () =>
                        {
                            test.AssertEqual(
                                new (string,Exception)[0],
                                failedTests);

                            btr.Test("fake test", (Test test) => { });

                            test.AssertEqual(
                                new (string, Exception)[0],
                                failedTests);
                        });
                        test.AssertEqual(
                                new (string, Exception)[0],
                                failedTests);

                        test.AssertTrue(subscription.Dispose());
                        test.AssertTrue(subscription.Disposed);

                        btr.TestGroup("fake test group 2", () =>
                        {
                            test.AssertEqual(
                                new (string, Exception)[0],
                                failedTests);

                            btr.Test("fake test", (Test test) => { });

                            test.AssertEqual(
                                new (string, Exception)[0],
                                failedTests);
                        });
                        test.AssertEqual(
                                new (string, Exception)[0],
                                failedTests);

                        test.AssertFalse(subscription.Dispose());
                        test.AssertTrue(subscription.Disposed);
                    });

                    runner.Test($"with test failing with {nameof(TestFailureException)}", (Test test) =>
                    {
                        List<(string, Exception)> failedTests = new List<(string, Exception)>();
                        BasicTestRunner btr = BasicTestRunner.Create();

                        Disposable subscription = btr.OnTestFailed((Test t, Exception e) => failedTests.Add((t.GetFullName(), e)));
                        test.AssertNotNull(subscription);
                        test.AssertFalse(subscription.Disposed);

                        btr.TestGroup("fake test group", () =>
                        {
                            test.AssertEqual(
                                new (string,Exception)[0],
                                failedTests);

                            btr.Test("fake test", (Test test) => { test.Fail("oops!"); });

                            test.AssertEqual(
                                new (string,Exception)[] { ("fake test group fake test", new TestFailureException("oops!")) },
                                failedTests);
                        });
                        test.AssertEqual(
                                new (string, Exception)[] { ("fake test group fake test", new TestFailureException("oops!")) },
                                failedTests);

                        test.AssertTrue(subscription.Dispose());
                        test.AssertTrue(subscription.Disposed);

                        btr.TestGroup("fake test group 2", () =>
                        {
                            test.AssertEqual(
                                new (string, Exception)[] { ("fake test group fake test", new TestFailureException("oops!")) },
                                failedTests);

                            btr.Test("fake test", (Test test) => { test.Fail("oops!"); });

                            test.AssertEqual(
                                new (string, Exception)[] { ("fake test group fake test", new TestFailureException("oops!")) },
                                failedTests);
                        });
                        test.AssertEqual(
                            new (string, Exception)[] { ("fake test group fake test", new TestFailureException("oops!")) },
                            failedTests);

                        test.AssertFalse(subscription.Dispose());
                        test.AssertTrue(subscription.Disposed);
                    });

                    runner.Test($"with test failing with {nameof(ArgumentException)}", (Test test) =>
                    {
                        List<(string, Exception)> failedTests = new List<(string, Exception)>();
                        BasicTestRunner btr = BasicTestRunner.Create();

                        Disposable subscription = btr.OnTestFailed((Test t, Exception e) => failedTests.Add((t.GetFullName(), e)));
                        test.AssertNotNull(subscription);
                        test.AssertFalse(subscription.Disposed);

                        btr.TestGroup("fake test group", () =>
                        {
                            test.AssertEqual(
                                new (string, Exception)[0],
                                failedTests);

                            btr.Test("fake test", (Test test) => { throw new ArgumentException("oops!"); });

                            test.AssertEqual(
                                new (string, Exception)[] { ("fake test group fake test", new ArgumentException("oops!")) },
                                failedTests);
                        });
                        test.AssertEqual(
                                new (string, Exception)[] { ("fake test group fake test", new ArgumentException("oops!")) },
                                failedTests);

                        test.AssertTrue(subscription.Dispose());
                        test.AssertTrue(subscription.Disposed);

                        btr.TestGroup("fake test group 2", () =>
                        {
                            test.AssertEqual(
                                new (string, Exception)[] { ("fake test group fake test", new ArgumentException("oops!")) },
                                failedTests);

                            btr.Test("fake test", (Test test) => { test.Fail("oops!"); });

                            test.AssertEqual(
                                new (string, Exception)[] { ("fake test group fake test", new ArgumentException("oops!")) },
                                failedTests);
                        });
                        test.AssertEqual(
                            new (string, Exception)[] { ("fake test group fake test", new ArgumentException("oops!")) },
                            failedTests);

                        test.AssertFalse(subscription.Dispose());
                        test.AssertTrue(subscription.Disposed);
                    });
                });

                runner.TestGroup("OnTestPassed(Action<Test>)", () =>
                {
                    runner.Test("with null", (Test test) =>
                    {
                        BasicTestRunner btr = BasicTestRunner.Create();
                        test.AssertThrows(new ArgumentNullException("action"), () =>
                        {
                            btr.OnTestPassed((Action<Test>)null!);
                        });
                    });

                    runner.Test("with passing test", (Test test) =>
                    {
                        List<string> passingTests = new List<string>();
                        BasicTestRunner btr = BasicTestRunner.Create();

                        Disposable subscription = btr.OnTestPassed((Test t) => passingTests.Add((t.GetFullName())));
                        test.AssertNotNull(subscription);
                        test.AssertFalse(subscription.Disposed);

                        btr.TestGroup("fake test group", () =>
                        {
                            test.AssertEqual(
                                new string[0],
                                passingTests);

                            btr.Test("fake test", (Test test) => { });

                            test.AssertEqual(
                                new[] { "fake test group fake test" },
                                passingTests);
                        });
                        test.AssertEqual(
                            new[] { "fake test group fake test" },
                            passingTests);

                        test.AssertTrue(subscription.Dispose());
                        test.AssertTrue(subscription.Disposed);

                        btr.TestGroup("fake test group 2", () =>
                        {
                            test.AssertEqual(
                                new[] { "fake test group fake test" },
                                passingTests);

                            btr.Test("fake test", (Test test) => { });

                            test.AssertEqual(
                                new[] { "fake test group fake test" },
                                passingTests);
                        });
                        test.AssertEqual(
                            new[] { "fake test group fake test" },
                            passingTests);

                        test.AssertFalse(subscription.Dispose());
                        test.AssertTrue(subscription.Disposed);
                    });

                    runner.Test($"with test failing with {nameof(TestFailureException)}", (Test test) =>
                    {
                        List<string> passingTests = new List<string>();
                        BasicTestRunner btr = BasicTestRunner.Create();

                        Disposable subscription = btr.OnTestPassed((Test t) => passingTests.Add((t.GetFullName())));
                        test.AssertNotNull(subscription);
                        test.AssertFalse(subscription.Disposed);

                        btr.TestGroup("fake test group", () =>
                        {
                            test.AssertEqual(
                                new string[0],
                                passingTests);

                            btr.Test("fake test", (Test test) => { test.Fail("oops!"); });

                            test.AssertEqual(
                                new string[0],
                                passingTests);
                        });
                        test.AssertEqual(
                            new string[0],
                            passingTests);

                        test.AssertTrue(subscription.Dispose());
                        test.AssertTrue(subscription.Disposed);

                        btr.TestGroup("fake test group 2", () =>
                        {
                            test.AssertEqual(
                                new string[0],
                                passingTests);

                            btr.Test("fake test", (Test test) => { test.Fail("oops!"); });

                            test.AssertEqual(
                                new string[0],
                                passingTests);
                        });
                        test.AssertEqual(
                            new string[0],
                            passingTests);

                        test.AssertFalse(subscription.Dispose());
                        test.AssertTrue(subscription.Disposed);
                    });

                    runner.Test($"with test failing with {nameof(ArgumentException)}", (Test test) =>
                    {
                        List<string> passingTests = new List<string>();
                        BasicTestRunner btr = BasicTestRunner.Create();

                        Disposable subscription = btr.OnTestPassed((Test t) => passingTests.Add((t.GetFullName())));
                        test.AssertNotNull(subscription);
                        test.AssertFalse(subscription.Disposed);

                        btr.TestGroup("fake test group", () =>
                        {
                            test.AssertEqual(
                                new string[0],
                                passingTests);

                            btr.Test("fake test", (Test test) => { throw new ArgumentException("oops!"); });

                            test.AssertEqual(
                                new string[0],
                                passingTests);
                        });
                        test.AssertEqual(
                            new string[0],
                            passingTests);

                        test.AssertTrue(subscription.Dispose());
                        test.AssertTrue(subscription.Disposed);

                        btr.TestGroup("fake test group 2", () =>
                        {
                            test.AssertEqual(
                                new string[0],
                                passingTests);

                            btr.Test("fake test", (Test test) => { test.Fail("oops!"); });

                            test.AssertEqual(
                                new string[0],
                                passingTests);
                        });
                        test.AssertEqual(
                            new string[0],
                            passingTests);

                        test.AssertFalse(subscription.Dispose());
                        test.AssertTrue(subscription.Disposed);
                    });
                });

                runner.TestGroup("OnTestEnded(Action<Test>)", () =>
                {
                    runner.Test("with null", (Test test) =>
                    {
                        BasicTestRunner btr = BasicTestRunner.Create();
                        test.AssertThrows(new ArgumentNullException("action"), () =>
                        {
                            btr.OnTestEnded((Action<Test>)null!);
                        });
                    });

                    runner.Test("with passing test", (Test test) =>
                    {
                        List<string> endedTests = new List<string>();
                        BasicTestRunner btr = BasicTestRunner.Create();

                        Disposable subscription = btr.OnTestEnded((Test t) => endedTests.Add(t.GetFullName()));
                        test.AssertNotNull(subscription);
                        test.AssertFalse(subscription.Disposed);

                        btr.TestGroup("fake test group", () =>
                        {
                            test.AssertEqual(new string[0], endedTests);

                            btr.Test("fake test", (Test test) => { });

                            test.AssertEqual(
                                new[] { "fake test group fake test" },
                                endedTests);
                        });
                        test.AssertEqual(
                            new[] { "fake test group fake test" },
                            endedTests);

                        test.AssertTrue(subscription.Dispose());
                        test.AssertTrue(subscription.Disposed);

                        btr.TestGroup("fake test group 2", () =>
                        {
                            test.AssertEqual(
                                new[] { "fake test group fake test" },
                                endedTests);

                            btr.Test("fake test", (Test test) => { });

                            test.AssertEqual(
                                new[] { "fake test group fake test" },
                                endedTests);
                        });
                        test.AssertEqual(
                            new[] { "fake test group fake test" },
                            endedTests);

                        test.AssertFalse(subscription.Dispose());
                        test.AssertTrue(subscription.Disposed);
                    });

                    runner.Test("with failing test", (Test test) =>
                    {
                        List<string> endedTests = new List<string>();
                        BasicTestRunner btr = BasicTestRunner.Create();

                        Disposable subscription = btr.OnTestStarted((Test t) => endedTests.Add(t.GetFullName()));
                        test.AssertNotNull(subscription);
                        test.AssertFalse(subscription.Disposed);

                        btr.TestGroup("fake test group", () =>
                        {
                            test.AssertEqual(new string[0], endedTests);

                            btr.Test("fake test", (Test test) => { test.Fail("oops!"); });

                            test.AssertEqual(
                                new[] { "fake test group fake test" },
                                endedTests);
                        });
                        test.AssertEqual(
                            new[] { "fake test group fake test" },
                            endedTests);

                        test.AssertTrue(subscription.Dispose());
                        test.AssertTrue(subscription.Disposed);

                        btr.TestGroup("fake test group 2", () =>
                        {
                            test.AssertEqual(
                                new[] { "fake test group fake test" },
                                endedTests);

                            btr.Test("fake test", (Test test) => { test.Fail("oops!"); });

                            test.AssertEqual(
                                new[] { "fake test group fake test" },
                                endedTests);
                        });
                        test.AssertEqual(
                            new[] { "fake test group fake test" },
                            endedTests);

                        test.AssertFalse(subscription.Dispose());
                        test.AssertTrue(subscription.Disposed);
                    });
                });
            });
        }
    }
}
