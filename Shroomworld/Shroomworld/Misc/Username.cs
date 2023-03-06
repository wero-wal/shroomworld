using System;

namespace Shroomworld;

public struct Username {

    // ----- Fields -----
    private static Range<int> _allowedlengths;

    private string _value;

    // ----- Constructors -----
    public Username() {
        _value = string.Empty;
    }

    // ----- Methods -----
    public static implicit operator string(Username username) {
        return username._value;
    }

    /// <summary>
    /// If a range has not yet been set, set the range for the
    /// allowed length of usernames to <paramref name="range"/>.
    /// </summary>
    /// <param name="range">The (inclusive) range of allowed lengths for usernames.</param>
    public static void SetLengthRange(Range<int> range) {
        if (_allowedlengths is null) {
            _allowedlengths = range;
        }
    }

    /// <summary>
    /// Change or set the username.
    /// </summary>
    /// <param name="username">The new username.</param>
    /// <returns><see langword="true"/> if the passed <paramref name="username"/> is valid. Otherwise, <see langword="false"/>.</returns>
    public bool TrySet(string username) {
        if (_allowedlengths.CheckIfInRange(username.Length)) {
            _value = username;
            return true;
        }
        return false;
    }
}
