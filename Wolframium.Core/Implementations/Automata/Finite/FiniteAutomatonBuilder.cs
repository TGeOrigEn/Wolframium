using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Wolframium.Core.Implementations.Automata.Finite
{
    public sealed partial class FiniteAutomatonBuilder<TState, TContext, TValue>
        where TState : notnull
        where TContext : notnull, AutomatonContext<TValue>
        where TValue : notnull
    {
        private readonly Dictionary<TState, StateInformation> _states;

        private readonly IEqualityComparer<TState>? _stateComparer;

        private readonly IEqualityComparer<TValue>? _valueComparer;

        private readonly TState _initialState;

        public FiniteAutomatonBuilder(TState initialState, IEqualityComparer<TState>? stateComparer = null, IEqualityComparer<TValue>? valueComparer = null)
        {
            ArgumentNullException.ThrowIfNull(initialState);

            _stateComparer = stateComparer;
            _valueComparer = valueComparer;
            _initialState = initialState;

            _states = new Dictionary<TState, StateInformation>(stateComparer)
            {
                { initialState, new StateInformation(initialState, valueComparer) }
            };
        }

        public void SetCompleteHandler(TState state, Action<TContext>? handler) => GetStateInformation(state).CompleteHandler = handler;

        public bool HasCompleteHandler(TState state) => GetStateInformation(state).CompleteHandler is not null;

        public void AddState(TState state)
        {
            ArgumentNullException.ThrowIfNull(state);

            if (!_states.TryAdd(state, new StateInformation(state, _valueComparer)))
            {
                string exceptionMessage = $"State '{state}' has already been added.";
                throw new ArgumentException(exceptionMessage, nameof(state));
            }
        }

        public bool TryAddState(TState state)
        {
            ArgumentNullException.ThrowIfNull(state);
            return _states.TryAdd(state, new StateInformation(state, _valueComparer));
        }

        public void AddTransition(TState sourceState, TState destinationState, TValue transitionValue)
        {
            ArgumentNullException.ThrowIfNull(transitionValue);

            StateInformation sourceStateInformation = GetStateInformation(sourceState);
            StateInformation destinationStateInformation = GetStateInformation(destinationState);

            if (!sourceStateInformation.Transitions.TryAdd(transitionValue, destinationStateInformation))
            {
                string exceptionMessage = $"State '{sourceState}' already has transition via '{transitionValue}'.";
                throw new ArgumentException(exceptionMessage, nameof(transitionValue));
            }
        }

        public bool TryAddTransition(TState sourceState, TState destinationState, TValue transitionValue)
        {
            ArgumentNullException.ThrowIfNull(transitionValue);

            StateInformation sourceStateInformation = GetStateInformation(sourceState);
            StateInformation destinationStateInformation = GetStateInformation(destinationState);

            return sourceStateInformation.Transitions.TryAdd(transitionValue, destinationStateInformation);
        }

        public void AddOrUpdateTransition(TState sourceState, TState destinationState, TValue transitionValue)
        {
            ArgumentNullException.ThrowIfNull(transitionValue);

            StateInformation sourceStateInformation = GetStateInformation(sourceState);
            StateInformation destinationStateInformation = GetStateInformation(destinationState);

            sourceStateInformation.Transitions[transitionValue] = destinationStateInformation;
        }

        public void SetDefaultTransition(TState sourceState, TState destinationState) => GetStateInformation(sourceState).DefaultTransition = GetStateInformation(destinationState);

        public bool HasDefaultTransition(TState state) => GetStateInformation(state).DefaultTransition is not null;

        public FiniteAutomaton<TContext, TValue> Build() => new FrozenFiniteAutomaton(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private StateInformation GetStateInformation(TState state, [CallerArgumentExpression(nameof(state))] string? paramName = null)
        {
            ArgumentNullException.ThrowIfNull(state, paramName);

            if (!_states.TryGetValue(state, out StateInformation? stateInformation))
            {
                string exceptionMessage = $"State '{state}' does not exist.";
                throw new ArgumentException(exceptionMessage, paramName);
            }

            return stateInformation;
        }
    }
}
