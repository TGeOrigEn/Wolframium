namespace Wolframium.Core.Implementations.Automata.Finite
{
    public abstract class FiniteAutomaton<TState, TContext, TValue>
        where TState : notnull
        where TContext : notnull, AutomatonContext<TValue>
        where TValue : notnull
    {
        public abstract TState Run(TContext context);
    }
}
