using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Wolframium.Core.Implementations
{
    public class Token<TKind>(Fragment fragment, TKind kind) : IEquatable<Token<TKind>> where TKind : struct, Enum
    {
        public Fragment Fragment { get; } = fragment;

        public TKind Kind { get; } = kind;

        public bool Equals([NotNullWhen(true)] Token<TKind>? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return EqualityComparer<TKind>.Default.Equals(Kind, other.Kind) && Fragment == other.Fragment;
        }

        public override bool Equals([NotNullWhen(true)] object? obj) => obj is Token<TKind> token && Equals(token);

        public override int GetHashCode() => HashCode.Combine(Kind, Fragment);

        public override string ToString() => $"{nameof(Kind)}: {Kind}, {Fragment}";
    }

    public class Token<TKind, TValue>(Fragment fragment, TKind kind, TValue value) : Token<TKind>(fragment, kind) where TKind : struct, Enum
    {
        public TValue Value { get; } = value;

        public override string ToString() => $"{nameof(Kind)}: {Kind}, {nameof(Value)}: '{Value}', {Fragment}";
    }
}
