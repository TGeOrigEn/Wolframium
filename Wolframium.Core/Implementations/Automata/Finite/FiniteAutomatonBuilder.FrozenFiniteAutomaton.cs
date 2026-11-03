using System;

namespace Wolframium.Core.Implementations.Automata.Finite
{
    public sealed partial class FiniteAutomatonBuilder<TState, TContext, TValue>
    {
        private sealed class FrozenFiniteAutomaton : FiniteAutomaton<TState, TContext, TValue>
        {
            private readonly FrozenStateInformation _initialState;

            public FrozenFiniteAutomaton(FiniteAutomatonBuilder<TState, TContext, TValue> builder)
            {
                ArgumentNullException.ThrowIfNull(builder);
                _initialState = FrozenStateInformation.CreateInitialState(builder);
            }

            public override TState Run(TContext context)
            {
                ArgumentNullException.ThrowIfNull(context);

                FrozenStateInformation currentState = _initialState;

                while (context.TryGetValue(out TValue? value))
                {
                    if (currentState.Transitions.TryGetValue(value, out FrozenStateInformation? nextState))
                    {
                        currentState = nextState;
                        context.NextValue();
                    }
                    else if (currentState.DefaultTransition is not null)
                    {
                        currentState = currentState.DefaultTransition;
                        context.NextValue();
                    }
                    else break;
                }

                return currentState.State;
            }
        }
    }
}
