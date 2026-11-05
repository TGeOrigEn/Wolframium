using System;

namespace Wolframium.Core.Implementations.Automata.Finite
{
    /// <summary>
    /// Представляет абстрактный конечный автомат.
    /// </summary>
    /// <typeparam name="TContext">Тип контекста автомата.</typeparam>
    /// <typeparam name="TValue">Тип значений входного алфавита.</typeparam>
    public abstract class FiniteAutomaton<TContext, TValue>
        where TContext : notnull, AutomatonContext<TValue>
        where TValue : notnull
    {
        /// <summary>
        /// Запускает конечный автомат на входных данных, предоставляемых контекстом. 
        /// </summary>
        /// <param name="context">Контекст, предоставляющий входные данные.</param>
        /// <returns>
        /// <see langword="true"/>, если конечный автомат остановился в принимающем состоянии; иначе — <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public abstract bool Run(TContext context);
    }
}
