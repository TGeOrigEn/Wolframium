using System.Diagnostics.CodeAnalysis;

namespace Wolframium.Core.Implementations.Automata
{
    public abstract class AutomatonContext<TValue> where TValue : notnull
    {
        public abstract bool TryGetValue([MaybeNullWhen(false)] out TValue value);

        public abstract void NextValue();
    }
}
