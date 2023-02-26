using System;

namespace Shroomworld
{
    internal interface IEntity
    {
        public IType Type { get; }
        public HealthData HealthData { get; }
        public MovementData MovementData { get; }
        public Physics.Body Body { get; }
        public Sprite Sprite { get; }
    }
}
