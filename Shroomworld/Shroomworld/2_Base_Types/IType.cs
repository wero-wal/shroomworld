namespace Shroomworld
{
	internal interface IType
	{
		public int Id { get; }
		public string FullId { get => $"{GetType().ToString().Split('.')[1]}{Id}"; }
		public string Name { get; }
	}
}
