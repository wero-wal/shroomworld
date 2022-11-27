using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld
{
    internal class StatisticsDictionary : IDictionary<string, int>
    {
        // Properties
		public ICollection<string> Keys => _keys;
		public ICollection<int> Values => _values;
		public int this[string key]
        {
			get
			{
				if (!ContainsKey(key))
				{
					throw new KeyNotFoundException();
				}
                return _values[_keys.IndexOf(key)];
			}
			set
			{
                throw _noAccessException;
			}
        }
        public int Count => _values.Count;
		public bool IsReadOnly => false;
        

        // Fields
        private static InvalidOperationException _maxCapacityException = new InvalidOperationException("Cannot add item, as the dictionary is at maximum capacity.");
		private static AccessViolationException _noAccessException = new AccessViolationException("Cannot directly set this value. Try the [...] method.");

        private List<string> _keys;
        private List<int> _values;


        // Constructors
        public StatisticsDictionary()
        {
            _keys = new List<string>();
            _values = new List<int>();
        }
        public StatisticsDictionary(int capacity)
		{
            _keys = new List<string>(capacity);
            _values = new List<int>(capacity);
		}


        // Methods
		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <param name="amount">amount to increase the value by</param>
		public void IncreaseStatisticBy(string key, int amount)
		{
			if (!ContainsKey(key))
			{
				throw new KeyNotFoundException();
			}
			_values[_keys.IndexOf(key)] += Math.Abs(amount);
		}
		public void ResetStatistics()
		{
			for (int i = 0; i < _values.Count; i++)
			{
				_values[i] = 0;
			}
			int start = _values.Count;
			for (int i = start; i < _values.Capacity; i++)
			{
				_values.Add(0);
			}
		}
		public void Add(string key, int value)
		{
			if (_values.Count == _values.Capacity)
			{
                throw _maxCapacityException;
			}
            _keys.Add(key);
            _values.Add(value);
		}
		public void Add(KeyValuePair<string, int> item)
		{
			Add(item.Key, item.Value);
		}
		public void Clear()
		{
			_keys.Clear();
            _values.Clear();
		}
		public bool Contains(KeyValuePair<string, int> item)
		{
			if (!_keys.Contains(item.Key))
			{
				return false;
			}
			return _keys.IndexOf(item.Key) == _values.IndexOf(item.Value);
		}
		public bool ContainsKey(string key)
		{
			return _keys.Contains(key);
		}
		public void CopyTo(KeyValuePair<string, int>[] array, int arrayIndex)
		{
			for (int i = arrayIndex; i < (Count + arrayIndex); i++)
			{
				array[i] = new KeyValuePair<string, int>(_keys[i], _values[i]);
			}
		}
		public IEnumerator<KeyValuePair<string, int>> GetEnumerator()
		{
			throw new NotImplementedException();
		}
		// TODO: finish writing these methods
		public bool Remove(string key)
		{

		}

		public bool Remove(KeyValuePair<string, int> item)
		{
			return Remove(item.Key);
		}

		public bool TryGetValue(string key, [MaybeNullWhen(false)] out int value)
		{
			value = default(int);
			if (!ContainsKey(key))
			{
				return false;
			}
			value = _values[_keys.IndexOf(key)];
			return true;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}
