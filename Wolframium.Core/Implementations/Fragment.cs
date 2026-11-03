using System;
using System.Diagnostics.CodeAnalysis;

namespace Wolframium.Core.Implementations
{
    public readonly struct Fragment(string? source, FragmentPosition position) : IEquatable<Fragment>
    {
        public string? Source { get; } = source;

        public FragmentPosition Position { get; } = position;

        public static bool operator ==(Fragment left, Fragment right) => left.Equals(right);

        public static bool operator !=(Fragment left, Fragment right) => !(left == right);

        public bool Equals(Fragment other) => Position == other.Position && Source == other.Source;

        public override bool Equals([NotNullWhen(true)] object? obj) => obj is Fragment fragment && Equals(fragment);

        public override int GetHashCode() => HashCode.Combine(Position, Source);

        public override string ToString() => $"{nameof(Source)}: {(Source is null ? "null" : $"'{Source}'")}, {Position}";
    }
}
