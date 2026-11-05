using System;
using System.Diagnostics.CodeAnalysis;

namespace Wolframium.Core.Implementations
{
    /// <summary>
    /// Представляет позицию символа в тексте, задаваемую номером строки, номером столбца и абсолютным индексом.
    /// </summary>
    public readonly struct CharacterPosition : IComparable<CharacterPosition>, IComparable, IEquatable<CharacterPosition>
    {
        private static readonly CharacterPosition _minValue = default;

        /// <summary>
        /// Представляет наименьшее возможное значение <see cref="CharacterPosition"/>.
        /// </summary>
        public static ref readonly CharacterPosition MinValue => ref _minValue;

        /// <summary>
        /// Возвращает номер столбца.
        /// </summary>
        /// <remarks>
        /// Минимально возможное значение — ноль.
        /// </remarks>
        public int Column { get; }

        /// <summary>
        /// Возвращает абсолютный индекс символа в тексте.
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// Возвращает номер строки.
        /// </summary>
        /// <remarks>
        /// Минимально возможное значение — ноль.
        /// </remarks>
        public int Row { get; }

        /// <summary>
        /// Инициализирует новый экземпляр структуры <see cref="CharacterPosition"/>.
        /// </summary>
        /// <param name="row">Номер строки, отсчитываемый от нуля.</param>
        /// <param name="column">Номер столбца, отсчитываемый от нуля.</param>
        /// <param name="index">Абсолютный индекс символа в тексте.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public CharacterPosition(int row, int column, int index)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(column);
            ArgumentOutOfRangeException.ThrowIfNegative(index);
            ArgumentOutOfRangeException.ThrowIfNegative(row);

            if (row == 0) ArgumentOutOfRangeException.ThrowIfNotEqual(index, column);
            else ArgumentOutOfRangeException.ThrowIfLessThan(index, column + row);

            Column = column;
            Index = index;
            Row = row;
        }

        public static bool operator >=(CharacterPosition left, CharacterPosition right) => left.CompareTo(right) >= 0;

        public static bool operator <=(CharacterPosition left, CharacterPosition right) => left.CompareTo(right) <= 0;

        public static bool operator >(CharacterPosition left, CharacterPosition right) => left.CompareTo(right) > 0;

        public static bool operator <(CharacterPosition left, CharacterPosition right) => left.CompareTo(right) < 0;

        public static bool operator ==(CharacterPosition left, CharacterPosition right) => left.Equals(right);

        public static bool operator !=(CharacterPosition left, CharacterPosition right) => !(left == right);

        public int CompareTo(CharacterPosition other)
        {
            int result = Index.CompareTo(other.Index);

            if (result != 0)
                return result;

            result = Row.CompareTo(other.Row);

            if (result != 0)
                return result;

            return Column.CompareTo(other.Column);
        }

        public int CompareTo(object? obj)
        {
            if (obj is null)
                return 1;

            if (obj is CharacterPosition position)
                return CompareTo(position);

            throw new ArgumentException($"Object must be of type {nameof(CharacterPosition)}.", nameof(obj));
        }

        public bool Equals(CharacterPosition other) => Index == other.Index && Row == other.Row && Column == other.Column;

        public override bool Equals([NotNullWhen(true)] object? obj) => obj is CharacterPosition position && Equals(position);

        public override int GetHashCode() => HashCode.Combine(Index, Row, Column);

        public override string ToString() => $"{nameof(Row)}: {Row}, {nameof(Column)}: {Column}, {nameof(Index)}: {Index}";
    }
}
