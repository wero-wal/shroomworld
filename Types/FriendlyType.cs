using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld;

public class FriendlyType : IType {

	// ----- Properties -----
	public IdData IdData => _idData;
	public HealthData HealthData => _healthData;
	internal PhysicsData PhysicsData => _physicsData;
	internal Quest Quest => _quest;

	// ----- Fields -----
	private readonly IdData _idData;
	private readonly Texture2D _texture;
	private readonly HealthData _healthData;
	private readonly PhysicsData _physicsData;
	private readonly Quest _quest;

	// ----- Constructors -----
	public FriendlyType(IdData idData, Texture2D texture, PhysicsData movementData, Quest quest) {
		_idData = idData;
		_texture = texture;
		_physicsData = movementData;
		_quest = quest;
	}

	public Friendly CreateNew(IDisplayHandler displayHandler) {
		Sprite sprite = new Sprite(_texture, displayHandler);
		return new Friendly(_idData.Id, sprite, new EntityHealthData(_healthData), new Physics.Body(sprite, _physicsData));
	}
}
