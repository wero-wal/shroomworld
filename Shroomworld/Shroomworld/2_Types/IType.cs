internal interface IType
{
	// ----- Properties -----
	public static Dictionary<string, IType> Dictionary { get; }

	public int Id { get; }
	public string FullId { get; }
	public string Name { get; }
	public string PluralName { get; }

	// ----- Constructors -----
	public Type(string plainText); // ***

	// ----- Methods -----
	public Sprite GetSprite(); // ***


	/*
	the starred (***) methods and properties are the only methods which will change from class to class.
	Is it worth creating an interface, or should I just make an abstract class?? cos they also share a
	lot of fields, so wouldn't it just be easier? or is inheritance so bad that an interface would still
	be better?

	We have:
	. 3 polymorphic methods/properties.
	. 5 repeated properties and fields ( i.e. they will have to be rewritten in each class ).

	An abstract class would greatly reduce the amount of repitition, at the cost of making the code look
	/ feel a bit confusing ( as you can't see the fields in each class ).

	ughhhhhhhhhhhh


	[ right column margin: 105 ]
	*/
}