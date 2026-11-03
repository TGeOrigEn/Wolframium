using System;
using System.Collections.Frozen;
using System.Collections.Immutable;

namespace Wolframium.Core.Implementations.Automata.Finite
{
    public sealed partial class FiniteAutomatonBuilder<TState, TContext, TValue>
    {
        private sealed class FrozenStateInformation
        {
            public FrozenDictionary<TValue, FrozenStateInformation> Transitions { get; private set; }

            public FrozenStateInformation? DefaultTransition { get; private set; }

            public TState State { get; }

            private FrozenStateInformation(StateInformation stateInformation)
            {
                ArgumentNullException.ThrowIfNull(stateInformation);

                State = stateInformation.State;
                Transitions = FrozenDictionary<TValue, FrozenStateInformation>.Empty;
            }

            public static FrozenStateInformation CreateInitialState(FiniteAutomatonBuilder<TState, TContext, TValue> builder)
            {
                ArgumentNullException.ThrowIfNull(builder);

                FrozenDictionary<TState, FrozenStateInformation> buffer = builder._states
                    .ToFrozenDictionary(pair => pair.Key, pair => new FrozenStateInformation(pair.Value), builder._stateComparer);

                ImmutableArray<FrozenStateInformation> values = buffer.Values;

                for (int index = 0; index < values.Length; index++)
                {
                    FrozenStateInformation frozenStateInformation = values[index];
                    StateInformation stateInformation = builder._states[frozenStateInformation.State];

                    if (stateInformation.DefaultTransition is not null)
                        frozenStateInformation.DefaultTransition = buffer[stateInformation.DefaultTransition.State];

                    if (stateInformation.Transitions.Count > 0)
                        frozenStateInformation.Transitions = stateInformation.Transitions
                            .ToFrozenDictionary(pair => pair.Key, pair => buffer[pair.Value.State], builder._valueComparer);
                }

                return buffer[builder._initialState];
            }

            public override string? ToString() => State.ToString();
        }
    }
}
