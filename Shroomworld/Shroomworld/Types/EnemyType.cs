using Microsoft.Xna.Framework.Graphics;
namespace Shroomworld;
public class EnemyType : IEntity {

	// ----- Enums -----
	// ----- Properties -----
	public IdData IdData => _idData;
	public Texture2D Texture => _texture;
	public PhysicsData PhysicsData => _physicsData;
	public HealthData HealthData => _healthData;
	public AttackData AttackData => _attackData;


	// ----- Fields -----
	private readonly IdData _idData;
	private readonly Texture2D _texture;
	private readonly PhysicsData _physicsData;
	private readonly HealthData _healthData;
	private readonly AttackData _attackData;


	// ----- Constructors -----
	public EnemyType(IdData idData, Texture2D texture, PhysicsData physicsData, HealthData healthData, AttackData attackData) {
		_idData = idData;
		_texture = texture;
		_physicsData = physicsData;
		_healthData = healthData;
		_attackData = attackData;
	}


	// ----- Methods -----
	public Enemy GetNewEnemy() {
		var sprite = new Sprite(_texture);
		return new Enemy(_idData.Id, sprite, new Physics.Body(sprite, _physicsData), new EntityHealthData(_healthData));
	}
}
