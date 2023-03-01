using System;
namespace Shroomworld {

	/* These classes are taken from https://github.com/ymassad/MaybeExamples.git, though I have
	   refactored them to match my own preferences and standard c# style guidelines.

	   Here is an in-depth explanation of the methods and how to use the class:
	   https://www.dotnetcurry.com/patterns-practices/1510/maybe-monad-csharp . */

	/// <summary>
	/// Monadic <see langword="struct"/> used to prevent the need for constant <see langword="null"/>
	/// checks and to make method interfaces more transparent.
	/// </summary>
    public struct Maybe<T> {

		// ----- Fields -----
        private readonly T _value;
        private readonly bool _hasValue;


		// ----- Constructors -----
		private Maybe() {
			_value = default(T);
			_hasValue = false;
		}
        private Maybe(T value) {
            _value = value;
            _hasValue = true;
        }

		// ----- Methods -----
        public static implicit operator Maybe<T>(T value) {
            if(value == null) {
                return new Maybe<T>();
			}
            return new Maybe<T>(value);
        }
        public static implicit operator Maybe<T>(Maybe.MaybeNone value) {
            return new Maybe<T>();
        }
        
		/// <summary>
		/// Performs a function based on whether or not there is a value.
		/// </summary>
		/// <param name="some">The function to perform if there is a value.</param>
		/// <param name="none">The function to perform if there is no value.</param>
		/// <typeparam name="TResult">The type of the return value.</typeparam>
		/// <returns>The result of the function performed.</returns>
		public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none) {
            if (_hasValue) {
                return some(_value);
			}
            return none();
        }
        /// <summary>
		/// Performs an action based on whether or not there is a value.
		/// </summary>
		/// <param name="some">The action to perform if there is a value.</param>
		/// <param name="none">The action to perform if there is no value.</param>
		public void Match(Action<T> some, Action none) {
            if (_hasValue) {
                some(_value);
            }
            else {
                none();
            }
        }
        public bool TryGetValue(out T value) {
            if (_hasValue) {
                value = _value;
                return true;
            }
            value = default(T);
            return false;
        }
        
		/// <summary>
		/// Converts the value (if there is one) to a different type.
		/// <para>See also: <seealso cref="Bind"/>.</para>
		/// </summary>
		/// <param name="convert">The conversion function.</param>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <returns>The converted value, if there is a value to convert. Otherwise,
		/// <see cref="Maybe.None"/>.</returns>
		public Maybe<TResult> Map<TResult>(Func<T, TResult> convert) {
            if(!_hasValue) {
                return new Maybe<TResult>();
			}
            return convert(_value);
        }
        public Maybe<TResult> Select<TResult>(Func<T, TResult> convert) {
            if (!_hasValue) {
                return new Maybe<TResult>();
			}
            return convert(_value);
        }
        /// <summary>
        /// Converts the value (if there is one) to a <see cref="Maybe"/> of a different type.
		/// <para>See also: <seealso cref="Map"/>.</para>
        /// </summary>
        /// <param name="convert">The conversion function.</param>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <returns>A <see cref="Maybe"/>&lt;<see paramtyperef="T"/>&gt; of the result, or
		/// <see cref="Maybe.None"/>.</returns>
		public Maybe<TResult> Bind<TResult>(Func<T, Maybe<TResult>> convert) {
            if (!_hasValue) {
                return new Maybe<TResult>();
			}
            return convert(_value);
        }
        
		public Maybe<TResult> SelectMany<T2, TResult>(Func<T, Maybe<T2>> convert,
			Func<T, T2, TResult> finalSelect) {
            if (!_hasValue) {
                return new Maybe<TResult>();
			}
            Maybe<T2> converted = convert(_value);

            if (!converted._hasValue) {
                return new Maybe<TResult>();
			}
            return finalSelect(_value, converted._value);
        }
        public Maybe<T> Where(Func<T, bool> predicate) {
            if (!_hasValue) {
                return new Maybe<T>();
			}
            if (predicate(_value)) {
                return this;
			}
            return new Maybe<T>();
        }
        
		public T ValueOr(T defaultValue) {
            if (_hasValue) {
                return _value;
			}
            return defaultValue;
        }
        public T ValueOr(Func<T> defaultValueFactory) {
            if (_hasValue) {
                return _value;
			}
            return defaultValueFactory();
        }
        public Maybe<T> ValueOrMaybe(Maybe<T> alternativeValue) {
            if (_hasValue) {
                return this;
			}
            return alternativeValue;
        }
        public Maybe<T> ValueOrMaybe(Func<Maybe<T>> alternativeValueFactory) {
            if (_hasValue) {
                return this;
			}
            return alternativeValueFactory();
        }
        public T ValueOrThrow(string errorMessage) {
            if (_hasValue) {
                return _value;
			}
            throw new Exception(errorMessage);
        }
    }

    public static class Maybe {
        public class MaybeNone { }

        public static MaybeNone None { get; } = new MaybeNone();
        public static Maybe<T> Some<T>(T value)
        {
            if (value == null) {
                throw new ArgumentNullException(nameof(value));
			}
            return value;
        }
    }
}
