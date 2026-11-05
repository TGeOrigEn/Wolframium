using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Wolframium.Core.Implementations
{
    public class Token<TKind> : IEquatable<Token<TKind>> where TKind : struct, Enum
    {
        private static readonly FrozenSet<TKind> _availableKinds = Enum.GetValues<TKind>().ToFrozenSet();

        public Token(Fragment fragment, TKind kind)
        {
            if (!_availableKinds.Contains(kind))
                throw new Exception();

            Fragment = fragment;
            Kind = kind;
        }

        public Fragment Fragment { get; }

        public TKind Kind { get; }

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
}
