using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Wolframium.Core.Implementations
{
    /// <summary>
    /// Представляет единицу языка, содержащую информацию о соответствующем ей фрагменте текста и её виде.
    /// </summary>
    /// <typeparam name="TKind">Тип перечисления, задающего вид токена.</typeparam>
    public class Token<TKind> : IEquatable<Token<TKind>> where TKind : struct, Enum
    {
        /// <summary>
        /// Возвращает фрагмент текста, соответствующий токену.
        /// </summary>
        public Fragment Fragment { get; }

        /// <summary>
        /// Возвращает вид токена.
        /// </summary>
        public TKind Kind { get; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Token{TKind}"/>.
        /// </summary>
        /// <param name="fragment">Фрагмент текста, соответствующий токену.</param>
        /// <param name="kind">Вид токена.</param>
        public Token(Fragment fragment, TKind kind)
        {
            Fragment = fragment;
            Kind = kind;
        }

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
