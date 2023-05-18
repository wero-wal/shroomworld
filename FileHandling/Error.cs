using System.ComponentModel;

namespace Shroomworld;

public class Error {
	// ----- Enums -----
	[DefaultValue(Unknown)]
	public enum Types {
		Unknown,
		FileLoading,
		FileParsing,
	}

	// ----- Properties -----
	// ----- Fields -----
	[DefaultValue("An error has occurred.")]
	public readonly string Message;
	public readonly Types Type;

    // ----- Constructors -----
    public Error(string message = default, Types type = default) {
        Message = message;
        Type = type;
    }

    // ----- Methods -----
}