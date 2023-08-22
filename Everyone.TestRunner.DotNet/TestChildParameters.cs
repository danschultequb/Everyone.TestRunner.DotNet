using System;

namespace Everyone
{
    public abstract class TestChildParameters<TDerived> where TDerived : class
    {
        private string? name;
        private string? fullNameSeparator;

        protected TestChildParameters()
        {
        }

        public string GetName()
        {
            PreCondition.AssertNotNullAndNotEmpty(this.name, nameof(this.name));

            return this.name!;
        }
        
        public TDerived SetName(string name)
        {
            PreCondition.AssertNotNullAndNotEmpty(name, nameof(name));

            this.name = name;

            return (this as TDerived)!;
        }

        public TDerived SetName(Type type)
        {
            PreCondition.AssertNotNull(type, nameof(type));

            return this.SetName(Types.GetFullName(type));
        }

        public TDerived SetName<TType>()
        {
            return this.SetName(typeof(TType));
        }

        public string GetFullNameSeparator()
        {
            PreCondition.AssertNotNullAndNotEmpty(this.fullNameSeparator, nameof(this.fullNameSeparator));

            return this.fullNameSeparator!;
        }

        public TDerived SetFullNameSeparator(string fullNameSeparator)
        {
            PreCondition.AssertNotNull(fullNameSeparator, nameof(fullNameSeparator));

            this.fullNameSeparator = fullNameSeparator;

            return (this as TDerived)!;
        }

        public TDerived SetFullNameSeparatorIfUnset(string fullNameSeparator)
        {
            PreCondition.AssertNotNull(fullNameSeparator, nameof(fullNameSeparator));

            if (this.fullNameSeparator == null)
            {
                this.SetFullNameSeparator(fullNameSeparator);
            }

            return (this as TDerived)!;
        }
    }
}
