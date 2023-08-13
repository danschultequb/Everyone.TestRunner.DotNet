using System;
using System.Collections.Generic;
using System.Linq;

namespace Everyone
{
    public static class TestTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestGroup(typeof(Test), () =>
            {
                runner.TestGroup("Create(string,TestGroup?,CompareFunctions,AssertMessageFunctions)", () =>
                {
                    void CreateErrorTest(string? name, TestGroup? parent, CompareFunctions? compareFunctions, AssertMessageFunctions? messageFunctions, Exception expected)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { name, parent?.GetFullName(), compareFunctions, messageFunctions }.Select(runner.ToString))}", (Test test) =>
                        {
                            test.AssertThrows(() => Everyone.Test.Create(name!, parent, compareFunctions!, messageFunctions!),
                                expected);
                        });
                    };

                    CreateErrorTest(null, null, null, null,
                        new PreConditionFailure(
                            "Expression: name",
                            "Expected: not null and not empty",
                            "Actual:   null"));
                    CreateErrorTest("", null, null, null,
                        new PreConditionFailure(
                            "Expression: name",
                            "Expected: not null and not empty",
                            "Actual:   \"\""));
                    CreateErrorTest("abc", null, null, null,
                        new PreConditionFailure(
                            "Expression: compareFunctions",
                            "Expected: not null",
                            "Actual:       null"));
                    CreateErrorTest("abc", null, CompareFunctions.Create(), null,
                        new PreConditionFailure(
                            "Expression: assertMessageFunctions",
                            "Expected: not null",
                            "Actual:       null"));

                    void ConstructorTest(string name, TestGroup? parent, CompareFunctions compareFunctions, AssertMessageFunctions messageFunctions)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { name, parent?.GetFullName(), compareFunctions, messageFunctions }.Select(runner.ToString))}", (Test test) =>
                        {
                            Test t = Everyone.Test.Create(name, parent, compareFunctions, messageFunctions);
                            test.AssertEqual(name, t.Name);
                            test.AssertEqual(parent, t.Parent);
                        });
                    };

                    ConstructorTest("abc", null, CompareFunctions.Create(), AssertMessageFunctions.Create());
                    ConstructorTest("a", new TestGroup("b"), CompareFunctions.Create(), AssertMessageFunctions.Create());
                    ConstructorTest("a", new TestGroup("b", new TestGroup("c")), CompareFunctions.Create(), AssertMessageFunctions.Create());
                });

                runner.TestGroup("GetFullName()", () =>
                {
                    void GetFullNameTest(Test t, string expected)
                    {
                        runner.Test($"with {t.GetFullName()}", (Test test) =>
                        {
                            test.AssertEqual(expected, t.GetFullName());
                        });
                    }

                    GetFullNameTest(CreateTest("a"), "a");
                    GetFullNameTest(CreateTest("a", new TestGroup("b")), "b a");
                    GetFullNameTest(CreateTest("a", new TestGroup("b", new TestGroup("c"))), "c b a");
                });

                runner.TestGroup("Fail(string)", () =>
                {
                    void FailTest(string message)
                    {
                        runner.Test($"with {message.EscapeAndQuote()}", (Test test) =>
                        {
                            test.AssertThrows(new TestFailureException(message), () =>
                            {
                                test.Fail(message);
                            });
                        });
                    }

                    FailTest("");
                    FailTest("abc");
                });

                runner.TestGroup("AssertEqual<T,U>(T?,U?)", () =>
                {
                    void AssertEqualErrorTest<T,U>(T? lhs, U? rhs, TestFailureException expected)
                    {
                        runner.Test($"with {runner.ToString(lhs)} and {runner.ToString(rhs)}", (Test test) =>
                        {
                            test.AssertThrows(expected, () =>
                            {
                                test.AssertEqual(lhs, rhs);
                            });
                        });
                    }

                    AssertEqualErrorTest(
                        (bool?)null,
                        true,
                        new TestFailureException(
                            "Expected: null",
                            "Actual:   True"));
                    AssertEqualErrorTest(
                        false,
                        (bool?)null,
                        new TestFailureException(
                            "Expected: False",
                            "Actual:   null"));
                    AssertEqualErrorTest(
                        false,
                        true,
                        new TestFailureException(
                            "Expected: False",
                            "Actual:   True"));
                    AssertEqualErrorTest(
                        1,
                        2,
                        new TestFailureException(
                            "Expected: 1",
                            "Actual:   2"));
                    AssertEqualErrorTest(
                        "",
                        "oops",
                        new TestFailureException(
                            "Expected: \"\"",
                            "Actual:   \"oops\""));
                    AssertEqualErrorTest(
                        new Exception("a"),
                        new Exception("b"),
                        new TestFailureException(
                            "Expected: System.Exception: \"a\"",
                            "Actual:   System.Exception: \"b\""));
                    AssertEqualErrorTest(
                        new Exception("a"),
                        new InvalidOperationException("a"),
                        new TestFailureException(
                            "Expected: System.Exception: \"a\"",
                            "Actual:   System.InvalidOperationException: \"a\""));

                    void AssertEqualTest<T,U>(T? lhs, U? rhs)
                    {
                        runner.Test($"with {runner.ToString(lhs)} and {runner.ToString(rhs)}", (Test test) =>
                        {
                            test.AssertEqual(lhs, rhs);
                        });
                    }

                    AssertEqualTest((bool?)null, (bool?)null);
                    AssertEqualTest(1, 1);
                    AssertEqualTest(1, 1.0);
                    AssertEqualTest('a', 'a');
                    AssertEqualTest("abc", "abc");
                    AssertEqualTest(new string("abc"), new string("abc"));
                    AssertEqualTest(false, false);
                    AssertEqualTest(true, true);
                    AssertEqualTest(new int[0], new int[0]);
                    AssertEqualTest("def", new[] { 'd', 'e', 'f' });
                    AssertEqualTest(new Exception("a"), new Exception("a"));
                });

                runner.TestGroup("AssertEqual(IEnumerable<T>,IEnumerable<T>)", () =>
                {
                    void AssertEqualErrorTest(IEnumerable<int> expected, IEnumerable<int> actual, TestFailureException expectedError)
                    {
                        runner.Test($"with {runner.ToString(expected)} and {runner.ToString(actual)}", (Test test) =>
                        {
                            test.AssertThrows(expectedError, () => test.AssertEqual(expected, actual));
                        });
                    }

                    AssertEqualErrorTest(
                        null!,
                        new int[0],
                        new TestFailureException(string.Join(Environment.NewLine,
                            "Expected: null",
                            "Actual:   []")));
                    AssertEqualErrorTest(
                        null!,
                        new[] { 1, 2, 3 },
                        new TestFailureException(string.Join(Environment.NewLine,
                            "Expected: null",
                            "Actual:   [1,2,3]")));

                    void AssertEqualTest(IEnumerable<int> expected, IEnumerable<int> actual)
                    {
                        runner.Test($"with {runner.ToString(expected)} and {runner.ToString(actual)}", (Test test) =>
                        {
                            test.AssertEqual(expected, actual);
                        });
                    }

                    AssertEqualTest(null!, null!);
                    AssertEqualTest(new int[0], new int[0]);
                    AssertEqualTest(new int[0], new List<int>());
                    AssertEqualTest(new[] { 1 }, new[] { 1 });
                    AssertEqualTest(new[] { 1 }, new List<int>() { 1 });
                });

                runner.TestGroup("AssertSame(T,T)", () =>
                {
                    void AssertSameErrorTest<T>(T lhs, T rhs, TestFailureException expected)
                    {
                        runner.Test($"with {runner.ToString(lhs)} and {runner.ToString(rhs)}", (Test test) =>
                        {
                            test.AssertThrows(expected, () =>
                            {
                                test.AssertSame(lhs, rhs);
                            });
                        });
                    }

                    AssertSameErrorTest<bool?>(
                        null,
                        true,
                        new TestFailureException(
                            "Expected: same as null",
                            "Actual:           True"));
                    AssertSameErrorTest<bool?>(
                        false,
                        null,
                        new TestFailureException(
                            "Expected: same as False",
                            "Actual:           null"));
                    AssertSameErrorTest(
                        false,
                        false,
                        new TestFailureException(
                            "Expected: same as False",
                            "Actual:           False"));
                    AssertSameErrorTest(
                        false,
                        true,
                        new TestFailureException(
                            "Expected: same as False",
                            "Actual:           True"));
                    AssertSameErrorTest(
                        1,
                        1,
                        new TestFailureException(
                            "Expected: same as 1",
                            "Actual:           1"));
                    AssertSameErrorTest(
                        1,
                        2,
                        new TestFailureException(
                            "Expected: same as 1",
                            "Actual:           2"));
                    AssertSameErrorTest(
                        "",
                        "oops",
                        new TestFailureException(
                            "Expected: same as \"\"",
                            "Actual:           \"oops\""));
                    AssertSameErrorTest(
                        new string("abc"),
                        new string("abc"),
                        new TestFailureException(
                            "Expected: same as \"abc\"",
                            "Actual:           \"abc\""));

                    void AssertSameTest<T>(T lhs, T rhs)
                    {
                        runner.Test($"with {runner.ToString(lhs)} and {runner.ToString(rhs)}", (Test test) =>
                        {
                            test.AssertSame(lhs, rhs);
                        });
                    }

                    AssertSameTest<bool?>(null, null);
                    AssertSameTest("abc", "abc");
                });

                runner.TestGroup("AssertNotSame(T,T)", () =>
                {
                    void AssertNotSameErrorTest<T>(T lhs, T rhs, TestFailureException expected)
                    {
                        runner.Test($"with {runner.ToString(lhs)} and {runner.ToString(rhs)}", (Test test) =>
                        {
                            test.AssertThrows(expected, () =>
                            {
                                test.AssertNotSame(lhs, rhs);
                            });
                        });
                    }

                    AssertNotSameErrorTest<bool?>(
                        null,
                        null,
                        new TestFailureException(
                            "Expected: not same as null",
                            "Actual:               null"));
                    AssertNotSameErrorTest(
                        "abc",
                        "abc",
                        new TestFailureException(
                            "Expected: not same as \"abc\"",
                            "Actual:               \"abc\""));

                    void AssertNotSameTest<T>(T lhs, T rhs)
                    {
                        runner.Test($"with {runner.ToString(lhs)} and {runner.ToString(rhs)}", (Test test) =>
                        {
                            test.AssertNotSame(lhs, rhs);
                        });
                    }

                    AssertNotSameTest<bool?>(null, true);
                    AssertNotSameTest<bool?>(false, null);
                    AssertNotSameTest(false, false);
                    AssertNotSameTest(false, true);
                    AssertNotSameTest(1, 1);
                    AssertNotSameTest(1, 2);
                    AssertNotSameTest("", "oops");
                    AssertNotSameTest(new string("abc"), new string("abc"));
                });

                runner.TestGroup("AssertNotEqual(T,T)", () =>
                {
                    void AssertNotEqualErrorTest<T>(T lhs, T rhs, TestFailureException expected)
                    {
                        runner.Test($"with {runner.ToString(lhs)} and {runner.ToString(rhs)}", (Test test) =>
                        {
                            test.AssertThrows(expected, () =>
                            {
                                test.AssertNotEqual(lhs, rhs);
                            });
                        });
                    }

                    AssertNotEqualErrorTest<bool?>(
                        null,
                        null,
                        new TestFailureException(
                            "Expected: not null",
                            "Actual:       null"));
                    AssertNotEqualErrorTest(
                        false,
                        false,
                        new TestFailureException(
                            "Expected: not False",
                            "Actual:       False"));
                    AssertNotEqualErrorTest(
                        true,
                        true,
                        new TestFailureException(
                            "Expected: not True",
                            "Actual:       True"));
                    AssertNotEqualErrorTest(
                        5,
                        5,
                        new TestFailureException(
                            "Expected: not 5",
                            "Actual:       5"));
                    AssertNotEqualErrorTest(
                        "",
                        "",
                        new TestFailureException(
                            "Expected: not \"\"",
                            "Actual:       \"\""));

                    void AssertNotEqualTest<T>(T lhs, T rhs)
                    {
                        runner.Test($"with {runner.ToString(lhs)} and {runner.ToString(rhs)}", (Test test) =>
                        {
                            test.AssertNotEqual(lhs, rhs);
                        });
                    }

                    AssertNotEqualTest<bool?>(null, false);
                    AssertNotEqualTest(1, 2);
                    AssertNotEqualTest(1.1, 1.0);
                    AssertNotEqualTest('a', 'b');
                    AssertNotEqualTest("abc", "abcd");
                    AssertNotEqualTest(new string("abcd"), new string("abc"));
                    AssertNotEqualTest(false, true);
                });

                runner.TestGroup("AssertNull<T>(T?)", () =>
                {
                    void AssertNullErrorTest(object value, TestFailureException expected)
                    {
                        runner.Test($"with {runner.ToString(value)}", (Test test) =>
                        {
                            test.AssertThrows(expected, () => { test.AssertNull(value); });
                        });
                    }

                    AssertNullErrorTest(false, new TestFailureException("Expected: null", "Actual:   False"));
                    AssertNullErrorTest(true, new TestFailureException("Expected: null", "Actual:   True"));
                    AssertNullErrorTest(0, new TestFailureException("Expected: null", "Actual:   0"));
                    AssertNullErrorTest(1, new TestFailureException("Expected: null", "Actual:   1"));
                    AssertNullErrorTest("", new TestFailureException("Expected: null", "Actual:   \"\""));

                    runner.Test("with null", (Test test) =>
                    {
                        test.AssertNull((object?)null);
                    });
                });

                runner.TestGroup("AssertNotNull<T>(T?)", () =>
                {
                    runner.Test("with null", (Test test) =>
                    {
                        test.AssertThrows(new TestFailureException("Expected: not null", "Actual:       null"), () =>
                        {
                            test.AssertNotNull((object?)null);
                        });
                    });

                    void AssertNotNullTest(object value)
                    {
                        runner.Test($"with {runner.ToString(value)}", (Test test) =>
                        {
                            test.AssertNotNull(value);
                        });
                    }

                    AssertNotNullTest(false);
                    AssertNotNullTest(true);
                    AssertNotNullTest(0);
                    AssertNotNullTest(1);
                    AssertNotNullTest("");
                });

                runner.TestGroup("AssertTrue(bool)", () =>
                {
                    runner.Test("with true", (Test test) =>
                    {
                        test.AssertTrue(true);
                    });

                    runner.Test("with false", (Test test) =>
                    {
                        test.AssertThrows(new TestFailureException("Expected: True", "Actual:   False"), () =>
                        {
                            test.AssertTrue(false);
                        });
                    });
                });

                runner.TestGroup("AssertFalse(bool)", () =>
                {
                    runner.Test("with true", (Test test) =>
                    {
                        test.AssertThrows(new TestFailureException("Expected: False", "Actual:   True"), () =>
                        {
                            test.AssertFalse(true);
                        });
                    });

                    runner.Test("with false", (Test test) =>
                    {
                        test.AssertFalse(false);
                    });
                });

                runner.TestGroup("AssertThrows(Exception,Action)", () =>
                {
                    runner.Test("with no exception thrown", (Test test) =>
                    {
                        test.AssertThrows(new TestFailureException("Expected: System.Exception: \"abc\"", "Actual:   null"), () =>
                        {
                            test.AssertThrows(new Exception("abc"), () => { });
                        });
                    });

                    runner.Test("with the wrong type of exception thrown", (Test test) =>
                    {
                        test.AssertThrows(new TestFailureException("Expected: System.Exception: \"abc\"", "Actual:   System.ArgumentException: \"abc\""), () =>
                        {
                            test.AssertThrows(new Exception("abc"), () => { throw new ArgumentException("abc"); });
                        });
                    });

                    runner.Test("with the wrong exception message", (Test test) =>
                    {
                        test.AssertThrows(new TestFailureException("Expected: System.Exception: \"abc\"", "Actual:   System.Exception: \"def\""), () =>
                        {
                            test.AssertThrows(new Exception("abc"), () => { throw new Exception("def"); });
                        });
                    });

                    runner.Test("with matching exception", (Test test) =>
                    {
                        test.AssertThrows(new Exception("abc"), () => { throw new Exception("abc"); });
                    });
                });

                runner.TestGroup("AssertGreaterThan<T,U>(T?,U?,string?)", () =>
                {
                    void AssertGreaterThanTest<T, U>(T? value, U? lowerBound, string? expression = null, Exception? expectedException = null)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { value, lowerBound, expression }.Map(runner.ToString))}", (Test test) =>
                        {
                            Test t = CreateTest("fake-test");
                            if (expectedException == null)
                            {
                                t.AssertGreaterThan(value, lowerBound, expression);
                            }
                            else
                            {
                                test.AssertThrows(expectedException, () =>
                                {
                                    t.AssertGreaterThan(value, lowerBound, expression);
                                });
                            }
                        });
                    }

                    AssertGreaterThanTest(
                        value: 1,
                        lowerBound: 0);
                    AssertGreaterThanTest(
                        value: 1,
                        lowerBound: 0,
                        expression: "hello");
                    AssertGreaterThanTest(
                        value: 1,
                        lowerBound: 1,
                        expectedException: new TestFailureException(
                            "Expected: greater than 1",
                            "Actual:                1"));
                    AssertGreaterThanTest(
                        value: 1,
                        lowerBound: 1,
                        expression: "hello",
                        expectedException: new TestFailureException(
                            "Message: hello",
                            "Expected: greater than 1",
                            "Actual:                1"));
                    AssertGreaterThanTest(
                        value: 1,
                        lowerBound: 2,
                        expectedException: new TestFailureException(
                            "Expected: greater than 2",
                            "Actual:                1"));
                    AssertGreaterThanTest(
                        value: 1,
                        lowerBound: 2,
                        expression: "hello",
                        expectedException: new TestFailureException(
                            "Message: hello",
                            "Expected: greater than 2",
                            "Actual:                1"));
                });

                runner.TestGroup("AssertGreaterThanOrEqualTo<T,U>(T?,U?,string?)", () =>
                {
                    void AssertGreaterThanOrEqualToTest<T, U>(T? value, U? lowerBound, string? expression = null, Exception? expectedException = null)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { value, lowerBound, expression }.Map(runner.ToString))}", (Test test) =>
                        {
                            Test t = CreateTest("fake-test");
                            if (expectedException == null)
                            {
                                t.AssertGreaterThanOrEqualTo(value, lowerBound, expression);
                            }
                            else
                            {
                                test.AssertThrows(expectedException, () =>
                                {
                                    t.AssertGreaterThanOrEqualTo(value, lowerBound, expression);
                                });
                            }
                        });
                    }

                    AssertGreaterThanOrEqualToTest(
                        value: 1,
                        lowerBound: 0);
                    AssertGreaterThanOrEqualToTest(
                        value: 1,
                        lowerBound: 0,
                        expression: "hello");
                    AssertGreaterThanOrEqualToTest(
                        value: 1,
                        lowerBound: 1);
                    AssertGreaterThanOrEqualToTest(
                        value: 1,
                        lowerBound: 1,
                        expression: "hello");
                    AssertGreaterThanOrEqualToTest(
                        value: 1,
                        lowerBound: 2,
                        expectedException: new TestFailureException(
                            "Expected: greater than or equal to 2",
                            "Actual:                            1"));
                    AssertGreaterThanOrEqualToTest(
                        value: 1,
                        lowerBound: 2,
                        expression: "hello",
                        expectedException: new TestFailureException(
                            "Message: hello",
                            "Expected: greater than or equal to 2",
                            "Actual:                            1"));
                });

                runner.TestGroup("AssertBetween<T,U,V>(T?,U?,V?,string?)", () =>
                {
                    void AssertBetweenTest<T, U, V>(T? lowerBound, U? value, V? upperBound, string? message = null, Exception? expectedException = null)
                    {
                        runner.Test($"with {Language.AndList(new object?[] { lowerBound, value, upperBound, message }.Map(runner.ToString))}", (Test test) =>
                        {
                            Test t = CreateTest("fake-test");
                            if (expectedException == null)
                            {
                                t.AssertBetween(
                                    lowerBound: lowerBound,
                                    value: value,
                                    upperBound: upperBound,
                                    message: message);
                            }
                            else
                            {
                                test.AssertThrows(expectedException, () =>
                                {
                                    t.AssertBetween(
                                        lowerBound: lowerBound,
                                        value: value,
                                        upperBound: upperBound,
                                        message: message);
                                });
                            }
                        });
                    }

                    AssertBetweenTest(
                        lowerBound: 1,
                        value: 2,
                        upperBound: 3);
                    AssertBetweenTest(
                        lowerBound: 1,
                        value: 2,
                        upperBound: 3,
                        message: "hello");
                    AssertBetweenTest(
                        lowerBound: 1,
                        value: 0,
                        upperBound: 3,
                        expectedException: new TestFailureException(
                            "Expected: between 1 and 3",
                            "Actual:           0"));
                    AssertBetweenTest(
                        lowerBound: 1,
                        value: 0,
                        upperBound: 3,
                        message: "hello",
                        expectedException: new TestFailureException(
                            "Message: hello",
                            "Expected: between 1 and 3",
                            "Actual:           0"));

                    AssertBetweenTest(
                        lowerBound: '1',
                        value: '2',
                        upperBound: '3');
                    AssertBetweenTest(
                        lowerBound: '1',
                        value: '2',
                        upperBound: '3',
                        message: "hello");
                    AssertBetweenTest(
                        lowerBound: '1',
                        value: '0',
                        upperBound: '3',
                        expectedException: new TestFailureException(
                            "Expected: between '1' and '3'",
                            "Actual:           '0'"));
                    AssertBetweenTest(
                        lowerBound: '1',
                        value: '0',
                        upperBound: '3',
                        message: "hello",
                        expectedException: new TestFailureException(
                            "Message: hello",
                            "Expected: between '1' and '3'",
                            "Actual:           '0'"));

                    AssertBetweenTest(
                        lowerBound: "no compare function",
                        value: false,
                        upperBound: 40,
                        expectedException: new InvalidOperationException("No compare function found that matches the types System.String and System.Boolean."));
                    AssertBetweenTest(
                        lowerBound: "no compare function",
                        value: false,
                        upperBound: 40,
                        message: "hello",
                        expectedException: new InvalidOperationException("No compare function found that matches the types System.String and System.Boolean."));

                    AssertBetweenTest(
                        lowerBound: "no compare",
                        value: "function",
                        upperBound: 40,
                        expectedException: new TestFailureException(
                            "Expected: between \"no compare\" and 40",
                            "Actual:           \"function\""));
                    AssertBetweenTest(
                        lowerBound: "no compare",
                        value: "function",
                        upperBound: 40,
                        message: "hello",
                        expectedException: new TestFailureException(
                            "Message: hello",
                            "Expected: between \"no compare\" and 40",
                            "Actual:           \"function\""));

                    AssertBetweenTest(
                        lowerBound: "no compare",
                        value: "oops",
                        upperBound: 40,
                        expectedException: new InvalidOperationException("No compare function found that matches the types System.String and System.Int32."));
                    AssertBetweenTest(
                        lowerBound: "no compare",
                        value: "oops",
                        upperBound: 40,
                        message: "hello",
                        expectedException: new InvalidOperationException("No compare function found that matches the types System.String and System.Int32."));
                });
            });
        }

        private static Test CreateTest(string name, TestGroup? parent = null)
        {
            return Everyone.Test.Create(name, parent, CompareFunctions.Create(), AssertMessageFunctions.Create());
        }
    }
}
