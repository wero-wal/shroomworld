using System;

namespace Shroomworld;

public class Either<TValue, TError> {
	// ----- Enums -----
	// ----- Properties -----
	public bool IsError => !_isValue;

	// ----- Fields -----
	public TValue _value;
	public TError _error;
	public bool _isValue;

	// ----- Constructors -----
	public Either(TValue value) {
		_value = value;
		_isValue = true;
	}
	public Either(TError value) {
		_error = value;
		_isValue = false;
	}
	
	// ----- Methods -----
	public static implicit operator Either<TValue, TError>(TValue value) {
		return new Either<TValue, TError>(value);
	}
	public static implicit operator Either<TValue, TError>(TError error) {
		return new Either<TValue, TError>(error);
	}
	public static implicit operator TValue(Either<TValue, TError> either) {
		return either._value;
	}
	public static implicit operator TError(Either<TValue, TError> either) {
		return either._error;
	}

	public bool TryGetValue(out TValue value, out TError error) {
		value = default;
		error = default;

		if (_isValue) {
			value = _value;
			return true;
		}
		error = _error;
		return false;
	}
	public TResult Bind<TResult>(Func<TValue, TResult> valueFunction, Func<TError, TResult> errorFunction) {
		if (_isValue) {
			return valueFunction(_value);
		}
		return errorFunction(_error);
	}
	public void Map(Action<TValue> valueAction, Action<TError> errorAction) {
		if (_isValue) {
			valueAction(_value);
		}
		else {
			errorAction(_error);
		}
	}
}