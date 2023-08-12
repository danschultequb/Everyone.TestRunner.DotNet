﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace Everyone
{
    /// <summary>
    /// An individual test.
    /// </summary>
    public class Test : TestChild
    {
        private Test(
            string name,
            TestGroup? parent,
            CompareFunctions compareFunctions,
            AssertMessageFunctions assertMessageFunctions)
            : base(name, parent)
        {
            PreCondition.AssertNotNullAndNotEmpty(name, nameof(name));
            PreCondition.AssertNotNull(compareFunctions, nameof(compareFunctions));
            PreCondition.AssertNotNull(assertMessageFunctions, nameof(assertMessageFunctions));

            this.CompareFunctions = compareFunctions;
            this.AssertMessageFunctions = assertMessageFunctions;
        }

        public static Test Create(
            string name,
            TestGroup? parent,
            CompareFunctions compareFunctions,
            AssertMessageFunctions messageFunctions)
        {
            return new Test(name, parent, compareFunctions, messageFunctions);
        }

        private CompareFunctions CompareFunctions { get; }
        private AssertMessageFunctions AssertMessageFunctions { get; }

        private CompareFunctions GetCompareFunctions(AssertParameters? parameters)
        {
            return parameters?.CompareFunctions ?? this.CompareFunctions;
        }

        private AssertMessageFunctions GetAssertMessageFunctions(AssertParameters? parameters)
        {
            return parameters?.AssertMessageFunctions ?? this.AssertMessageFunctions;
        }

        /// <summary>
        /// Fail the current <see cref="Test"/>.
        /// </summary>
        /// <param name="message">The message to include in the <see cref="TestFailureException"/>.</param>
        public void Fail(string message)
        {
            throw new TestFailureException(message);
        }

        /// <summary>
        /// Assert that the provided values are the same.
        /// </summary>
        /// <typeparam name="T">The type of values to compare.</typeparam>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The actual value.</param>
        public void AssertSame<T,U>(T? expected, U? actual, string? message = null)
        {
            this.AssertSame(expected, actual, new AssertParameters { Message = message });
        }

        /// <summary>
        /// Assert that the provided values are the same.
        /// </summary>
        /// <typeparam name="T">The type of values to compare.</typeparam>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The actual value.</param>
        public void AssertSame<T,U>(T? expected, U? actual, AssertParameters? parameters)
        {
            if (!object.ReferenceEquals(expected, actual))
            {
                throw new TestFailureException(this.GetAssertMessageFunctions(parameters).ExpectedSame(expected, actual, parameters));
            }
        }

        /// <summary>
        /// Assert that the provided values are not the same.
        /// </summary>
        /// <typeparam name="T">The type of values to compare.</typeparam>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The actual value.</param>
        public void AssertNotSame<T,U>(T? expected, U? actual, string? message = null)
        {
            this.AssertNotSame(expected, actual, new AssertParameters { Message = message });
        }

        /// <summary>
        /// Assert that the provided values are not the same.
        /// </summary>
        /// <typeparam name="T">The type of values to compare.</typeparam>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The actual value.</param>
        public void AssertNotSame<T,U>(T? expected, U? actual, AssertParameters? parameters)
        {
            if (object.ReferenceEquals(expected, actual))
            {
                throw new TestFailureException(this.GetAssertMessageFunctions(parameters).ExpectedNotSame(expected, actual, parameters));
            }
        }

        /// <summary>
        /// Assert that the provided values are equal.
        /// </summary>
        /// <typeparam name="T">The type of values to compare.</typeparam>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The actual value.</param>
        public void AssertEqual<T,U>(T? expected, U? actual, string? message = null)
        {
            this.AssertEqual(expected, actual, new AssertParameters { Message = message });
        }

        /// <summary>
        /// Assert that the provided values are equal.
        /// </summary>
        /// <typeparam name="T">The type of values to compare.</typeparam>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The actual value.</param>
        public void AssertEqual<T,U>(T? expected, U? actual, AssertParameters? parameters)
        {
            if (!this.GetCompareFunctions(parameters).AreEqual(expected, actual))
            {
                throw new TestFailureException(this.GetAssertMessageFunctions(parameters).ExpectedEqual(expected, actual, parameters));
            }
        }

        /// <summary>
        /// Assert that the provided values are equal.
        /// </summary>
        /// <typeparam name="T">The type of values to compare.</typeparam>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The actual value.</param>
        public void AssertNotEqual<T,U>(T? expected, U? actual, string? message = null)
        {
            this.AssertNotEqual(expected, actual, new AssertParameters { Message = message });
        }

        /// <summary>
        /// Assert that the provided values are equal.
        /// </summary>
        /// <typeparam name="T">The type of values to compare.</typeparam>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The actual value.</param>
        public void AssertNotEqual<T,U>(T? expected, U? actual, AssertParameters? parameters)
        {
            if (!this.GetCompareFunctions(parameters).AreNotEqual(expected, actual))
            {
                throw new TestFailureException(this.GetAssertMessageFunctions(parameters).ExpectedNotEqual(expected, actual, parameters));
            }
        }

        /// <summary>
        /// Assert that the provided value is null.
        /// </summary>
        /// <param name="value">The value to check.</param>
        public void AssertNull<T>(T? value, string? message = null)
        {
            this.AssertNull(value, new AssertParameters { Message = message });
        }

        /// <summary>
        /// Assert that the provided value is null.
        /// </summary>
        /// <param name="value">The value to check.</param>
        public void AssertNull<T>(T? value, AssertParameters? parameters)
        {
            if (!object.ReferenceEquals(value, null))
            {
                throw new TestFailureException(this.GetAssertMessageFunctions(parameters).ExpectedEqual<object, object>(null, value, parameters));
            }
        }

        /// <summary>
        /// Assert that the provided value is not null.
        /// </summary>
        /// <param name="value">The value to check.</param>
        public void AssertNotNull<T>([NotNull]T? value, string? message = null)
        {
            this.AssertNotNull(value, new AssertParameters { Message = message });
        }

        /// <summary>
        /// Assert that the provided value is not null.
        /// </summary>
        /// <param name="value">The value to check.</param>
        public void AssertNotNull<T>([NotNull]T? value, AssertParameters? parameters)
        {
            if (object.ReferenceEquals(value, null))
            {
                throw new TestFailureException(this.GetAssertMessageFunctions(parameters).ExpectedNotEqual<object, object>(null, value, parameters));
            }
        }

        /// <summary>
        /// Assert that the provided value is true.
        /// </summary>
        /// <param name="value">The value to check.</param>
        public void AssertTrue(bool value, string? message = null)
        {
            this.AssertTrue(value, new AssertParameters { Message = message });
        }

        /// <summary>
        /// Assert that the provided value is true.
        /// </summary>
        /// <param name="value">The value to check.</param>
        public void AssertTrue(bool value, AssertParameters? parameters)
        {
            if (!this.GetCompareFunctions(parameters).AreEqual(true, value))
            {
                throw new TestFailureException(this.GetAssertMessageFunctions(parameters).ExpectedEqual(true, value, parameters));
            }
        }

        /// <summary>
        /// Assert that the provided value is false.
        /// </summary>
        /// <param name="value">The value to check.</param>
        public void AssertFalse(bool value, string? message = null)
        {
            this.AssertFalse(value, new AssertParameters { Message = message });
        }

        /// <summary>
        /// Assert that the provided value is false.
        /// </summary>
        /// <param name="value">The value to check.</param>
        public void AssertFalse(bool value, AssertParameters? parameters)
        {
            if (!this.GetCompareFunctions(parameters).AreEqual(false, value))
            {
                throw new TestFailureException(this.GetAssertMessageFunctions(parameters).ExpectedEqual(false, value, parameters));
            }
        }

        public void AssertThrows(Exception expected, Action action, string? message = null)
        {
            this.AssertThrows(expected, action, new AssertParameters { Message = message });
        }

        public void AssertThrows(Exception expected, Action action, AssertParameters? parameters)
        {
            Exception? actual = null;
            try
            {
                action.Invoke();
            }
            catch (Exception e)
            {
                actual = e;
            }

            this.AssertEqual(expected, actual, parameters);
        }

        public void AssertThrows(Action action, Exception expected, string? message = null)
        {
            this.AssertThrows(action, expected, new AssertParameters { Message = message });
        }

        public void AssertThrows(Action action, Exception expected, AssertParameters? parameters)
        {
            this.AssertThrows(expected, action, parameters);
        }
    }
}
