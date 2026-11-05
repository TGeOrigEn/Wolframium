using System;
using System.Diagnostics.CodeAnalysis;

namespace Wolframium.Core.Implementations
{
    /// <summary>
    /// Представляет фрагмент текста с указанием на его источник и позицию в нём.
    /// </summary>
    public readonly struct Fragment : IEquatable<Fragment>
    {
        /// <summary>
        /// Возвращает источник фрагмента или <see langword="null"/>.
        /// </summary>
        public string? Source { get; }

        /// <summary>
        /// Возвращает позицию фрагмента в источнике.
        /// </summary>
        public FragmentPosition Position { get; }

        /// <summary>
        /// Инициализирует новый экземпляр структуры <see cref="Fragment"/>.
        /// </summary>
        /// <param name="source">Источник фрагмента или <see langword="null"/>.</param>
        /// <param name="position">Позиция фрагмента в источнике.</param>
        public Fragment(string? source, FragmentPosition position)
        {
            Source = source;
            Position = position;
        }

        public static bool operator ==(Fragment left, Fragment right) => left.Equals(right);

        public static bool operator !=(Fragment left, Fragment right) => !(left == right);

        public bool Equals(Fragment other) => Position == other.Position && Source == other.Source;

        public override bool Equals([NotNullWhen(true)] object? obj) => obj is Fragment fragment && Equals(fragment);

        public override int GetHashCode() => HashCode.Combine(Position, Source);

        public override string ToString() => $"{nameof(Source)}: {(Source is null ? "null" : $"'{Source}'")}, {Position}";
    }
}
