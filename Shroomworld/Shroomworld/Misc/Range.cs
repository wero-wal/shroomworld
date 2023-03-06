using System;

namespace Shroomworld;

public class Range<T> where T : IComparable<T> {

    // ----- Fields -----
    private readonly T _min;
    private readonly T _max;

    // ----- Constructors -----
    /// <summary>
    /// Create a range.
    /// </summary>
    /// <param name="min">Inclusive minimum value in the range.</param>
    /// <param name="max">Inclusive maximum value in the range.</param>
    public Range(T min, T max) {
        _min = min;
        _max = max;
    }

    // ----- Methods -----
    /// <summary>
    /// Check whether <paramref name="value"/> is within the range and which boundary it exceeds, if any.
    /// </summary>
    /// <param name="value"></param>
    /// <returns><paramref name="value"/> if it's within the range, or <see cref="_min"/> or <see cref="_max"/>
    /// if <paramref name="value"/> exceeds the range in the respective direction.</returns>
    public T ClampToRange(T value) {
        if (!AboveMin(value)) {
            return _min;
        }
        if (!BelowMax(value)) {
            return _max;
        }
        return value;
    }
    /// <summary>
    /// Check if a given <paramref name="value"/> lies within the range.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns><see langword="true"/> if it does, <see langword="false"/> if not.</returns>
    public bool CheckIfInRange(T value) {
        return AboveMin(value) && BelowMax(value);
    }
    private bool AboveMin(T value) {
        return _min.CompareTo(value) <= 0;
    }
    private bool BelowMax(T value) {
        return _max.CompareTo(value) >= 0;
    }
}
