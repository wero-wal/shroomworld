namespace Shroomworld {
    public interface IEntity {
        IType Type { get; }
        Sprite Sprite { get; }
        EntityHealthData HealthData { get; }
        Physics.Body Body { get; }
    }
}
