using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Nodo.Extensions;

public static class StackExtensions
{
    public static bool TryPop<T>(this Stack<T> stack, [MaybeNullWhen(false)] out T result)
    {
        int index = stack.Count - 1;
        T[] array = stack.ToArray();
        if ((uint) index >= (uint) array.Length)
        {
            result = default (T);
            return false;
        }
        this._size = index;
        result = array[index];
        if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
            array[index] = default (T);
        return true;
    }
}