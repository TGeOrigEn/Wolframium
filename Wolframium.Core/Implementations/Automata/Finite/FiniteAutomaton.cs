namespace Wolframium.Core.Implementations.Automata.Finite
{
    public abstract class FiniteAutomaton<TContext, TValue>
        where TContext : notnull, AutomatonContext<TValue>
        where TValue : notnull
    {
        public abstract bool Run(TContext context);
    }
}
