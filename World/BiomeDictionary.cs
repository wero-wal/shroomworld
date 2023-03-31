using System;
using System.Linq;
using System.Collections.Generic;

namespace Shroomworld;

/// <summary>
/// Use to store biomes and their locations in a world.
/// </summary>
public class BiomeDictionary {
	public class CapacityReachedException : Exception { }

	// ----- Properties -----
	public int Count => _biomes.Length;
	/// <summary>
	/// 
	/// </summary>
	/// <param name="x">x-coordinate at which you want to know the biome.</param>
	/// <returns>The biome at the given <paramref name="x"/> coordinate.</returns>
	/// <exception cref="KeyNotFoundException">Thrown if not enough biomes have been set.</exception>
	public BiomeType this[int x] {
		get {
			for (int i = 0; i < _count; i++) {
				if (x < _biomeEndXCoords[i]) {
					return _biomes[i];
				}
			}
			throw new KeyNotFoundException();
		}
		set {
			if (!_biomeEndXCoords.Contains(x)) {
				throw new KeyNotFoundException();
			}
			_biomes[x] = value;
		}
	}

	// ----- Fields -----
	private readonly int[] _biomeEndXCoords;
	private readonly BiomeType[] _biomes;

	private readonly int _count = 0;
	
	private int _endPointer = 0;

	// ----- Constructors -----
	public BiomeDictionary(int count, int mapWidth) {
		_count = count;
		_biomeEndXCoords = new int[count];
		_biomes = new BiomeType[count];
		_biomeEndXCoords[^1] = mapWidth;
	}

	// ----- Methods -----
	public void Add(int x, BiomeType biomeType) {
		if (_endPointer == _count) {
			throw new CapacityReachedException();
		}
		if (_endPointer != 0) {
			_biomeEndXCoords[_endPointer - 1] = x;
		}
		_biomes[_endPointer++] = biomeType;
	}
	public string ToString(string separator2, string separator3) {
		string str = string.Empty;
		for (int i = 0; i < _count; i++) {
			str += _biomes[i].IdData.Id + separator3 + _biomeEndXCoords[i] + separator2;
		}
		return str.Remove(_count - 1);
	}
}