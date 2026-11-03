using System;
using System.Diagnostics.CodeAnalysis;

namespace Wolframium.Core.Implementations
{
    public readonly struct FragmentPosition : IComparable<FragmentPosition>, IComparable, IEquatable<FragmentPosition>
    {
        private static readonly FragmentPosition _minValue = default;

        public static ref readonly FragmentPosition MinValue => ref _minValue;

        public CharacterPosition Start { get; }

        public CharacterPosition End { get; }

        public int Length => End.Index - Start.Index + 1;

        public FragmentPosition(CharacterPosition start, CharacterPosition end)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(end, start);

            Start = start;
            End = end;
        }

        public static bool operator >=(FragmentPosition left, FragmentPosition right) => left.CompareTo(right) >= 0;

        public static bool operator <=(FragmentPosition left, FragmentPosition right) => left.CompareTo(right) <= 0;

        public static bool operator >(FragmentPosition left, FragmentPosition right) => left.CompareTo(right) > 0;

        public static bool operator <(FragmentPosition left, FragmentPosition right) => left.CompareTo(right) < 0;

        public static bool operator ==(FragmentPosition left, FragmentPosition right) => left.Equals(right);

        public static bool operator !=(FragmentPosition left, FragmentPosition right) => !(left == right);

        public int CompareTo(FragmentPosition other)
        {
            int result = Start.CompareTo(other.Start);

            if (result != 0)
                return result;

            return End.CompareTo(other.End);
        }

        public int CompareTo(object? obj)
        {
            if (obj is null)
                return 1;

            if (obj is FragmentPosition position)
                return CompareTo(position);

            throw new ArgumentException($"Object must be of type {nameof(FragmentPosition)}.", nameof(obj));
        }

        public bool Equals(FragmentPosition other) => Start == other.Start && End == other.End;

        public override bool Equals([NotNullWhen(true)] object? obj) => obj is FragmentPosition position && Equals(position);

        public override int GetHashCode() => HashCode.Combine(Start, End);

        public override string ToString()
        {
            string column = ToString(nameof(CharacterPosition.Column), Start.Column, End.Column);
            string row = ToString(nameof(CharacterPosition.Row), Start.Row, End.Row);

            return $"{row}, {column}";
        }

        private static string ToString(string name, int start, int end) => start == end ? $"{name}: {start}" : $"{name}: [{start}..{end}]";
    }
}
