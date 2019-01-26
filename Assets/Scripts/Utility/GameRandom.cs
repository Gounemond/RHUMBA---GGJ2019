using System;
using System.Collections.Generic;
using System.Linq;

public static class GameRandom {
    public static IRandom Core { get; set; }
    //public static IRandom View { get; set; }
}

public interface IRandom {
    int NextInt();
    int NextInt(int max);
    int NextInt(int min, int max);
    int NextSign();
    float NextFloat();
    float NextFloat(float max);
    float NextFloat(float min, float max);
    T NextElement<T>(IEnumerable<T> enumerable);
    IEnumerable<T> Shuffle<T>(IEnumerable<T> enumerable);
    int NextEnum(Type enumType);
    IEnumerable<int> ShuffleEnum(Type enumType);
}

public class DefaultRandom : IRandom {
    Random _random;

    public DefaultRandom() {
        _random = new Random();
    }

    public DefaultRandom(int seed) {
        _random = new Random(seed);
    }

    public int NextInt() {
        return _random.Next();
    }

    public int NextInt(int max) {
        return _random.Next(max);
    }

    public int NextInt(int min, int max) {
        return _random.Next(min, max);
    }

    public int NextSign() {
        return NextInt(0, 1) * 2 - 1;
    }

    public float NextFloat() {
        return (float) _random.NextDouble();
    }

    public float NextFloat(float max) {
        if(max < 0) {
            throw new ArgumentOutOfRangeException(nameof(max), max,
                string.Format("'{0}' must be greater than zero.", nameof(max)));
        }
        return NextFloat(0, max);
    }

    public float NextFloat(float min, float max) {
        if(min > max) {
            throw new ArgumentOutOfRangeException(string.Format("{0} | {1}", nameof(min), nameof(max)),
                string.Format("{0} | {1}", min, max),
                string.Format("'{0}' cannot be greater than '{1}'.", nameof(min), nameof(max)));
        }
        return (float) (min + ((double) max - min) * _random.NextDouble());
    }

    public T NextElement<T>(IEnumerable<T> enumerable) {
        if(enumerable == null) {
            throw new ArgumentNullException(nameof(enumerable),
                string.Format("'{0}' cannot be null.", nameof(enumerable)));
        }
        int count = enumerable.Count();
        if(count == 0) {
            throw new ArgumentOutOfRangeException(nameof(enumerable),
                string.Format("'{0}' cannot be empty.", nameof(enumerable)));
        }
        return enumerable.ElementAt(NextInt(count));
    }

    public IEnumerable<T> Shuffle<T>(IEnumerable<T> enumerable) {
        if(enumerable == null) {
            throw new ArgumentNullException(nameof(enumerable),
                string.Format("'{0}' cannot be null.", nameof(enumerable)));
        }
        if(enumerable.Count() == 0) {
            throw new ArgumentOutOfRangeException(nameof(enumerable),
                string.Format("'{0}' cannot be empty.", nameof(enumerable)));
        }
        T[] array = enumerable.ToArray();
        for(int i = array.Length - 1; i >= 0; i--) {
            // Swap element "i" with a random earlier element it (or itself)
            // ... except we don't really need to swap it fully, as we can
            // return it immediately, and afterwards it's irrelevant.
            int swapIndex = NextInt(i + 1);
            yield return array[swapIndex];
            array[swapIndex] = array[i];
        }
    }

    public int NextEnum(Type enumType) {
        IEnumerable<int> enumValues = Enum.GetValues(enumType).OfType<int>();
        if(enumValues.Count() == 0) {
            throw new ArgumentOutOfRangeException(nameof(enumType),
                string.Format("Enum of type '{0}' cannot have no values.", nameof(enumValues)));
        }
        return NextElement(enumValues);
    }

    public IEnumerable<int> ShuffleEnum(Type enumType) {
        int[] enumValues = Enum.GetValues(enumType) as int[];
        if(enumValues.Count() == 0) {
            throw new ArgumentOutOfRangeException(nameof(enumValues),
                string.Format("Enum of type '{0}' cannot have no values.", nameof(enumValues)));
        }
        return Shuffle(enumValues);
    }
}