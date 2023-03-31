using System.Collections.Generic;

namespace Shroomworld;
public class GameData {

	// ----- Fields -----
	private readonly List<int> _worldIds;

	// ----- Constructors -----
	public GameData(int[] worldIds) {
		_worldIds = new List<int> (worldIds);
	}

	// ----- Methods -----
	public bool WorldIdsContains(int id) {
		return _worldIds.Contains(id);
	}
	public int NextWorldId() {
		int nextWorldId = _worldIds.Count;
		while (_worldIds.Contains(nextWorldId++)) {
			return nextWorldId;
		}
		return -1;
	}
}