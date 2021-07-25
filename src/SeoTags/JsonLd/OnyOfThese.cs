//using System;

//namespace SeoTags
//{
//    //TODO: AnyOfThese, OneOrMany

//    /// <summary>
//    /// Value can be any of the specified types.
//    /// </summary>
//    /// <typeparam name="T1">The first type.</typeparam>
//    /// <typeparam name="T2">The second type.</typeparam>
//    public readonly struct OneOfThese<T1, T2> : IEquatable<OneOfThese<T1, T2>>
//    {
//        /// <summary>
//        /// Initializes a new instance of the <see cref="OneOfThese{T1, T2}"/>.
//        /// </summary>
//        /// <param name="value1">The value of type <typeparamref name="T1"/>.</param>
//        public OneOfThese(T1 value1)
//        {
//            Value1 = value1;
//            Value2 = default;
//            Value = value1;

//            HasValue1 = true;
//            HasValue2 = false;
//            HasValue = true;
//        }

//        /// <summary>
//        /// Initializes a new instance of the <see cref="OneOfThese{T1, T2}"/>.
//        /// </summary>
//        /// <param name="value2">The value of type <typeparamref name="T2"/>.</param>
//        public OneOfThese(T2 value2)
//        {
//            Value1 = default;
//            Value2 = value2;
//            Value = value2;

//            HasValue1 = false;
//            HasValue2 = true;
//            HasValue = true;
//        }

//        /// <summary>
//        /// Gets a value indicating whether this instance has a value.
//        /// </summary>
//        public bool HasValue { get; }

//        /// <summary>
//        /// Gets a value indicating whether the value of type <typeparamref name="T1" /> has a value.
//        /// </summary>
//        public bool HasValue1 { get; }

//        /// <summary>
//        /// Gets a value indicating whether the value of type <typeparamref name="T2" /> has a value.
//        /// </summary>
//        public bool HasValue2 { get; }

//        /// <summary>
//        /// Gets the value.
//        /// </summary>
//        public object Value { get; }

//        /// <summary>
//        /// Gets the value of type <typeparamref name="T1" />.
//        /// </summary>
//        public T1 Value1 { get; }

//        /// <summary>
//        /// Gets the value of type <typeparamref name="T2" />.
//        /// </summary>
//        public T2 Value2 { get; }

//        /// <summary>
//        /// Performs an implicit conversion from <typeparamref name="T1"/> to <see cref="OneOfThese{T1, T2}"/>.
//        /// </summary>
//        /// <param name="value1">Value 1.</param>
//        /// <returns>The result of the conversion.</returns>
//        public static implicit operator OneOfThese<T1, T2>(T1 value1) => new(value1);

//        /// <summary>
//        /// Performs an implicit conversion from <typeparamref name="T2"/> to <see cref="OneOfThese{T1, T2}"/>.
//        /// </summary>
//        /// <param name="value2">Value 2.</param>
//        /// <returns>The result of the conversion.</returns>
//        public static implicit operator OneOfThese<T1, T2>(T2 value2) => new(value2);

//        /// <summary>
//        /// Performs an implicit conversion from <see cref="OneOfThese{T1, T2}"/> to the first item of type <typeparamref name="T1"/>.
//        /// </summary>
//        /// <param name="value">The value.</param>
//        /// <returns>
//        /// The result of the conversion.
//        /// </returns>
//        public static implicit operator T1(OneOfThese<T1, T2> value) => value.Value1;

//        /// <summary>
//        /// Performs an implicit conversion from <see cref="OneOfThese{T1, T2}"/> to the first item of type <typeparamref name="T2"/>.
//        /// </summary>
//        /// <param name="value">The value.</param>
//        /// <returns>
//        /// The result of the conversion.
//        /// </returns>
//        public static implicit operator T2(OneOfThese<T1, T2> value) => value.Value2;

//        /// <summary>
//        /// Implements the operator ==.
//        /// </summary>
//        /// <param name="left">The left.</param>
//        /// <param name="right">The right.</param>
//        /// <returns>
//        /// The result of the operator.
//        /// </returns>
//        public static bool operator ==(OneOfThese<T1, T2> left, OneOfThese<T1, T2> right) => left.Equals(right);

//        /// <summary>
//        /// Implements the operator !=.
//        /// </summary>
//        /// <param name="left">The left.</param>
//        /// <param name="right">The right.</param>
//        /// <returns>
//        /// The result of the operator.
//        /// </returns>
//        public static bool operator !=(OneOfThese<T1, T2> left, OneOfThese<T1, T2> right) => !(left == right);

//        /// <summary>Deconstructs the specified items.</summary>
//        /// <param name="value1">Value 1.</param>
//        /// <param name="value2">Value 2.</param>
//        public void Deconstruct(out T1 value1, out T2 value2)
//        {
//            value1 = Value1;
//            value2 = Value2;
//        }

//        /// <summary>
//        /// Indicates whether the current object is equal to another object of the same type.
//        /// </summary>
//        /// <param name="other">An object to compare with this object.</param>
//        /// <returns>
//        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
//        /// </returns>
//        public bool Equals(OneOfThese<T1, T2> other)
//        {
//            if (!other.HasValue && !HasValue)
//                return true;

//            return Value1.Equals(other.Value1) && Value2.Equals(other.Value2);
//        }

//        /// <summary>
//        /// Determines whether the specified <see cref="object" />, is equal to this instance.
//        /// </summary>
//        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
//        /// <returns>
//        /// <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
//        /// </returns>
//        public override bool Equals(object obj) => obj is OneOfThese<T1, T2> value && Equals(value);

//        /// <summary>
//        /// Returns a hash code for this instance.
//        /// </summary>
//        /// <returns>
//        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
//        /// </returns>
//        public override int GetHashCode() => System.HashCode.Combine(Value1, Value2); //Schema.NET.HashCode.Of(Value1).And(Value2);
//    }

//    /// <summary>
//    /// Value can be any of the specified types.
//    /// </summary>
//    /// <typeparam name="T1">The first type.</typeparam>
//    /// <typeparam name="T2">The second type.</typeparam>
//    /// <typeparam name="T3">The third type.</typeparam>
//    public readonly struct OneOfThese<T1, T2, T3> : IEquatable<OneOfThese<T1, T2, T3>>
//    {
//        /// <summary>
//        /// Initializes a new instance of the <see cref="OneOfThese{T1, T2, T3}"/>.
//        /// </summary>
//        /// <param name="value1">The value of type <typeparamref name="T1"/>.</param>
//        public OneOfThese(T1 value1)
//        {
//            Value1 = value1;
//            Value2 = default;
//            Value3 = default;
//            Value = value1;

//            HasValue1 = true;
//            HasValue2 = false;
//            HasValue3 = false;
//            HasValue = true;
//        }

//        /// <summary>
//        /// Initializes a new instance of the <see cref="OneOfThese{T1, T2, T3}"/>.
//        /// </summary>
//        /// <param name="value2">The value of type <typeparamref name="T2"/>.</param>
//        public OneOfThese(T2 value2)
//        {
//            Value1 = default;
//            Value2 = value2;
//            Value3 = default;
//            Value = value2;

//            HasValue1 = false;
//            HasValue2 = true;
//            HasValue3 = false;
//            HasValue = true;
//        }

//        /// <summary>
//        /// Initializes a new instance of the <see cref="OneOfThese{T1, T2, T3}"/>.
//        /// </summary>
//        /// <param name="value3">The value of type <typeparamref name="T3"/>.</param>
//        public OneOfThese(T3 value3)
//        {
//            Value1 = default;
//            Value2 = default;
//            Value3 = value3;
//            Value = value3;

//            HasValue1 = false;
//            HasValue2 = false;
//            HasValue3 = true;
//            HasValue = true;
//        }

//        /// <summary>
//        /// Gets a value indicating whether this instance has a value.
//        /// </summary>
//        public bool HasValue { get; }

//        /// <summary>
//        /// Gets a value indicating whether the value of type <typeparamref name="T1" /> has a value.
//        /// </summary>
//        public bool HasValue1 { get; }

//        /// <summary>
//        /// Gets a value indicating whether the value of type <typeparamref name="T2" /> has a value.
//        /// </summary>
//        public bool HasValue2 { get; }

//        /// <summary>
//        /// Gets a value indicating whether the value of type <typeparamref name="T3" /> has a value.
//        /// </summary>
//        public bool HasValue3 { get; }

//        /// <summary>
//        /// Gets the value.
//        /// </summary>
//        public object Value { get; }

//        /// <summary>
//        /// Gets the value of type <typeparamref name="T1" />.
//        /// </summary>
//        public T1 Value1 { get; }

//        /// <summary>
//        /// Gets the value of type <typeparamref name="T2" />.
//        /// </summary>
//        public T2 Value2 { get; }

//        /// <summary>
//        /// Gets the value of type <typeparamref name="T3" />.
//        /// </summary>
//        public T3 Value3 { get; }

//        /// <summary>
//        /// Performs an implicit conversion from <typeparamref name="T1"/> to <see cref="OneOfThese{T1, T2, T3}"/>.
//        /// </summary>
//        /// <param name="value1">Value 1.</param>
//        /// <returns>The result of the conversion.</returns>
//        public static implicit operator OneOfThese<T1, T2, T3>(T1 value1) => new(value1);

//        /// <summary>
//        /// Performs an implicit conversion from <typeparamref name="T2"/> to <see cref="OneOfThese{T1, T2, T3}"/>.
//        /// </summary>
//        /// <param name="value2">Value 2.</param>
//        /// <returns>The result of the conversion.</returns>
//        public static implicit operator OneOfThese<T1, T2, T3>(T2 value2) => new(value2);

//        /// <summary>
//        /// Performs an implicit conversion from <typeparamref name="T3"/> to <see cref="OneOfThese{T1, T2, T3}"/>.
//        /// </summary>
//        /// <param name="value3">Value 3.</param>
//        /// <returns>The result of the conversion.</returns>
//        public static implicit operator OneOfThese<T1, T2, T3>(T3 value3) => new(value3);

//        /// <summary>
//        /// Performs an implicit conversion from <see cref="OneOfThese{T1, T2, T3}"/> to the first item of type <typeparamref name="T1"/>.
//        /// </summary>
//        /// <param name="value">The value.</param>
//        /// <returns>
//        /// The result of the conversion.
//        /// </returns>
//        public static implicit operator T1(OneOfThese<T1, T2, T3> value) => value.Value1;

//        /// <summary>
//        /// Performs an implicit conversion from <see cref="OneOfThese{T1, T2, T3}"/> to the first item of type <typeparamref name="T2"/>.
//        /// </summary>
//        /// <param name="value">The value.</param>
//        /// <returns>
//        /// The result of the conversion.
//        /// </returns>
//        public static implicit operator T2(OneOfThese<T1, T2, T3> value) => value.Value2;

//        /// <summary>
//        /// Performs an implicit conversion from <see cref="OneOfThese{T1, T2, T3}"/> to the first item of type <typeparamref name="T3"/>.
//        /// </summary>
//        /// <param name="value">The value.</param>
//        /// <returns>
//        /// The result of the conversion.
//        /// </returns>
//        public static implicit operator T3(OneOfThese<T1, T2, T3> value) => value.Value3;

//        /// <summary>
//        /// Implements the operator ==.
//        /// </summary>
//        /// <param name="left">The left.</param>
//        /// <param name="right">The right.</param>
//        /// <returns>
//        /// The result of the operator.
//        /// </returns>
//        public static bool operator ==(OneOfThese<T1, T2, T3> left, OneOfThese<T1, T2, T3> right) => left.Equals(right);

//        /// <summary>
//        /// Implements the operator !=.
//        /// </summary>
//        /// <param name="left">The left.</param>
//        /// <param name="right">The right.</param>
//        /// <returns>
//        /// The result of the operator.
//        /// </returns>
//        public static bool operator !=(OneOfThese<T1, T2, T3> left, OneOfThese<T1, T2, T3> right) => !(left == right);

//        /// <summary>Deconstructs the specified items.</summary>
//        /// <param name="value1">Value 1.</param>
//        /// <param name="value2">Value 2.</param>
//        /// <param name="value3">Value 3.</param>
//        public void Deconstruct(out T1 value1, out T2 value2, out T3 value3)
//        {
//            value1 = Value1;
//            value2 = Value2;
//            value3 = Value3;
//        }

//        /// <summary>
//        /// Indicates whether the current object is equal to another object of the same type.
//        /// </summary>
//        /// <param name="other">An object to compare with this object.</param>
//        /// <returns>
//        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
//        /// </returns>
//        public bool Equals(OneOfThese<T1, T2, T3> other)
//        {
//            if (!other.HasValue && !HasValue)
//                return true;

//            return Value1.Equals(other.Value1) && Value2.Equals(other.Value2);
//        }

//        /// <summary>
//        /// Determines whether the specified <see cref="object" />, is equal to this instance.
//        /// </summary>
//        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
//        /// <returns>
//        /// <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
//        /// </returns>
//        public override bool Equals(object obj) => obj is OneOfThese<T1, T2, T3> value && Equals(value);

//        /// <summary>
//        /// Returns a hash code for this instance.
//        /// </summary>
//        /// <returns>
//        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
//        /// </returns>
//        public override int GetHashCode() => System.HashCode.Combine(Value1, Value2); //Schema.NET.HashCode.Of(Value1).And(Value2);
//    }

//    /// <summary>
//    /// Value can be any of the specified types.
//    /// </summary>
//    /// <typeparam name="T1">The first type.</typeparam>
//    /// <typeparam name="T2">The second type.</typeparam>
//    /// <typeparam name="T3">The third type.</typeparam>
//    /// <typeparam name="T4">The fourth type.</typeparam>
//    public readonly struct OneOfThese<T1, T2, T3, T4> : IEquatable<OneOfThese<T1, T2, T3, T4>>
//    {
//        /// <summary>
//        /// Initializes a new instance of the <see cref="OneOfThese{T1, T2, T3, T4}"/>.
//        /// </summary>
//        /// <param name="value1">The value of type <typeparamref name="T1"/>.</param>
//        public OneOfThese(T1 value1)
//        {
//            Value1 = value1;
//            Value2 = default;
//            Value3 = default;
//            Value4 = default;
//            Value = value1;

//            HasValue1 = true;
//            HasValue2 = false;
//            HasValue3 = false;
//            HasValue4 = false;
//            HasValue = true;
//        }

//        /// <summary>
//        /// Initializes a new instance of the <see cref="OneOfThese{T1, T2, T3, T4}"/>.
//        /// </summary>
//        /// <param name="value2">The value of type <typeparamref name="T2"/>.</param>
//        public OneOfThese(T2 value2)
//        {
//            Value1 = default;
//            Value2 = value2;
//            Value3 = default;
//            Value4 = default;
//            Value = value2;

//            HasValue1 = false;
//            HasValue2 = true;
//            HasValue3 = false;
//            HasValue4 = false;
//            HasValue = true;
//        }

//        /// <summary>
//        /// Initializes a new instance of the <see cref="OneOfThese{T1, T2, T3, T4}"/>.
//        /// </summary>
//        /// <param name="value3">The value of type <typeparamref name="T3"/>.</param>
//        public OneOfThese(T3 value3)
//        {
//            Value1 = default;
//            Value2 = default;
//            Value3 = value3;
//            Value4 = default;
//            Value = value3;

//            HasValue1 = false;
//            HasValue2 = false;
//            HasValue3 = true;
//            HasValue4 = false;
//            HasValue = true;
//        }

//        /// <summary>
//        /// Initializes a new instance of the <see cref="OneOfThese{T1, T2, T3, T4}"/>.
//        /// </summary>
//        /// <param name="value4">The value of type <typeparamref name="T4"/>.</param>
//        public OneOfThese(T4 value4)
//        {
//            Value1 = default;
//            Value2 = default;
//            Value3 = default;
//            Value4 = value4;
//            Value = value4;

//            HasValue1 = false;
//            HasValue2 = false;
//            HasValue3 = false;
//            HasValue4 = true;
//            HasValue = true;
//        }

//        /// <summary>
//        /// Gets a value indicating whether this instance has a value.
//        /// </summary>
//        public bool HasValue { get; }

//        /// <summary>
//        /// Gets a value indicating whether the value of type <typeparamref name="T1" /> has a value.
//        /// </summary>
//        public bool HasValue1 { get; }

//        /// <summary>
//        /// Gets a value indicating whether the value of type <typeparamref name="T2" /> has a value.
//        /// </summary>
//        public bool HasValue2 { get; }

//        /// <summary>
//        /// Gets a value indicating whether the value of type <typeparamref name="T3" /> has a value.
//        /// </summary>
//        public bool HasValue3 { get; }

//        /// <summary>
//        /// Gets a value indicating whether the value of type <typeparamref name="T4" /> has a value.
//        /// </summary>
//        public bool HasValue4 { get; }

//        /// <summary>
//        /// Gets the value.
//        /// </summary>
//        public object Value { get; }

//        /// <summary>
//        /// Gets the value of type <typeparamref name="T1" />.
//        /// </summary>
//        public T1 Value1 { get; }

//        /// <summary>
//        /// Gets the value of type <typeparamref name="T2" />.
//        /// </summary>
//        public T2 Value2 { get; }

//        /// <summary>
//        /// Gets the value of type <typeparamref name="T3" />.
//        /// </summary>
//        public T3 Value3 { get; }

//        /// <summary>
//        /// Gets the value of type <typeparamref name="T4" />.
//        /// </summary>
//        public T4 Value4 { get; }

//        /// <summary>
//        /// Performs an implicit conversion from <typeparamref name="T1"/> to <see cref="OneOfThese{T1, T2, T3, T4}"/>.
//        /// </summary>
//        /// <param name="value1">Value 1.</param>
//        /// <returns>The result of the conversion.</returns>
//        public static implicit operator OneOfThese<T1, T2, T3, T4>(T1 value1) => new(value1);

//        /// <summary>
//        /// Performs an implicit conversion from <typeparamref name="T2"/> to <see cref="OneOfThese{T1, T2, T3, T4}"/>.
//        /// </summary>
//        /// <param name="value2">Value 2.</param>
//        /// <returns>The result of the conversion.</returns>
//        public static implicit operator OneOfThese<T1, T2, T3, T4>(T2 value2) => new(value2);

//        /// <summary>
//        /// Performs an implicit conversion from <typeparamref name="T3"/> to <see cref="OneOfThese{T1, T2, T3, T4}"/>.
//        /// </summary>
//        /// <param name="value3">Value 3.</param>
//        /// <returns>The result of the conversion.</returns>
//        public static implicit operator OneOfThese<T1, T2, T3, T4>(T3 value3) => new(value3);

//        /// <summary>
//        /// Performs an implicit conversion from <typeparamref name="T4"/> to <see cref="OneOfThese{T1, T2, T3, T4}"/>.
//        /// </summary>
//        /// <param name="value4">Value 4.</param>
//        /// <returns>The result of the conversion.</returns>
//        public static implicit operator OneOfThese<T1, T2, T3, T4>(T4 value4) => new(value4);

//        /// <summary>
//        /// Performs an implicit conversion from <see cref="OneOfThese{T1, T2, T3, T4}"/> to the first item of type <typeparamref name="T1"/>.
//        /// </summary>
//        /// <param name="value">The value.</param>
//        /// <returns>
//        /// The result of the conversion.
//        /// </returns>
//        public static implicit operator T1(OneOfThese<T1, T2, T3, T4> value) => value.Value1;

//        /// <summary>
//        /// Performs an implicit conversion from <see cref="OneOfThese{T1, T2, T3, T4}"/> to the first item of type <typeparamref name="T2"/>.
//        /// </summary>
//        /// <param name="value">The value.</param>
//        /// <returns>
//        /// The result of the conversion.
//        /// </returns>
//        public static implicit operator T2(OneOfThese<T1, T2, T3, T4> value) => value.Value2;

//        /// <summary>
//        /// Performs an implicit conversion from <see cref="OneOfThese{T1, T2, T3, T4}"/> to the first item of type <typeparamref name="T3"/>.
//        /// </summary>
//        /// <param name="value">The value.</param>
//        /// <returns>
//        /// The result of the conversion.
//        /// </returns>
//        public static implicit operator T3(OneOfThese<T1, T2, T3, T4> value) => value.Value3;

//        /// <summary>
//        /// Performs an implicit conversion from <see cref="OneOfThese{T1, T2, T3, T4}"/> to the first item of type <typeparamref name="T4"/>.
//        /// </summary>
//        /// <param name="value">The value.</param>
//        /// <returns>
//        /// The result of the conversion.
//        /// </returns>
//        public static implicit operator T4(OneOfThese<T1, T2, T3, T4> value) => value.Value4;

//        /// <summary>
//        /// Implements the operator ==.
//        /// </summary>
//        /// <param name="left">The left.</param>
//        /// <param name="right">The right.</param>
//        /// <returns>
//        /// The result of the operator.
//        /// </returns>
//        public static bool operator ==(OneOfThese<T1, T2, T3, T4> left, OneOfThese<T1, T2, T3, T4> right) => left.Equals(right);

//        /// <summary>
//        /// Implements the operator !=.
//        /// </summary>
//        /// <param name="left">The left.</param>
//        /// <param name="right">The right.</param>
//        /// <returns>
//        /// The result of the operator.
//        /// </returns>
//        public static bool operator !=(OneOfThese<T1, T2, T3, T4> left, OneOfThese<T1, T2, T3, T4> right) => !(left == right);

//        /// <summary>Deconstructs the specified items.</summary>
//        /// <param name="value1">Value 1.</param>
//        /// <param name="value2">Value 2.</param>
//        /// <param name="value3">Value 3.</param>
//        /// <param name="value4">Value 4.</param>
//        public void Deconstruct(out T1 value1, out T2 value2, out T3 value3, out T4 value4)
//        {
//            value1 = Value1;
//            value2 = Value2;
//            value3 = Value3;
//            value4 = Value4;
//        }

//        /// <summary>
//        /// Indicates whether the current object is equal to another object of the same type.
//        /// </summary>
//        /// <param name="other">An object to compare with this object.</param>
//        /// <returns>
//        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
//        /// </returns>
//        public bool Equals(OneOfThese<T1, T2, T3, T4> other)
//        {
//            if (!other.HasValue && !HasValue)
//                return true;

//            return Value1.Equals(other.Value1) && Value2.Equals(other.Value2);
//        }

//        /// <summary>
//        /// Determines whether the specified <see cref="object" />, is equal to this instance.
//        /// </summary>
//        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
//        /// <returns>
//        /// <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
//        /// </returns>
//        public override bool Equals(object obj) => obj is OneOfThese<T1, T2, T3, T4> value && Equals(value);

//        /// <summary>
//        /// Returns a hash code for this instance.
//        /// </summary>
//        /// <returns>
//        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
//        /// </returns>
//        public override int GetHashCode() => System.HashCode.Combine(Value1, Value2); //Schema.NET.HashCode.Of(Value1).And(Value2);
//    }
//}