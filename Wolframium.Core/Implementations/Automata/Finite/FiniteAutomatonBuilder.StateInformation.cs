using System;
using System.Collections.Generic;

namespace Wolframium.Core.Implementations.Automata.Finite
{
    public sealed partial class FiniteAutomatonBuilder<TState, TContext, TValue>
    {
        private sealed class StateInformation
        {
            public Dictionary<TValue, StateInformation> Transitions { get; }

            public StateInformation? DefaultTransition { get; set; }

            public TState State { get; }

            public StateInformation(TState state, IEqualityComparer<TValue>? valueComparer)
            {
                ArgumentNullException.ThrowIfNull(state);

                Transitions = new(valueComparer);
                State = state;
            }

            public override string? ToString() => State.ToString();
        }
    }
}
