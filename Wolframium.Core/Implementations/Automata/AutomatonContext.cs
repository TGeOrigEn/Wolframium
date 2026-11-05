using System.Diagnostics.CodeAnalysis;

namespace Wolframium.Core.Implementations.Automata
{
    /// <summary>
    /// Предоставляет методы для чтения значений входного алфавита типа <typeparamref name="TValue"/>.
    /// </summary>
    /// <typeparam name="TValue">Тип значений входного алфавита.</typeparam>
    public abstract class AutomatonContext<TValue> where TValue : notnull
    {
        /// <summary>
        /// Пытается получить значение, на которое указывает курсор.
        /// </summary>
        /// <param name="value">Значение, на которое указывает курсор.</param>
        /// <returns>
        /// <see langword="true"/>, если значение доступно; иначе — <see langword="false"/>.
        /// </returns>
        public abstract bool TryGetValue([MaybeNullWhen(false)] out TValue value);

        /// <summary>
        /// Сдвигает курсор вперёд.
        /// </summary>
        public abstract void NextValue();
    }
}
