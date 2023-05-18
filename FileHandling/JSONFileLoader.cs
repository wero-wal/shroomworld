using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System;

namespace Shroomworld;
public class JsonFileLoader {
	private const string FilePathFilePath = "file-paths.txt";
	private const string FileNotFoundErrorMessage = "File not found.";
	private const string FileNotParsedErrorMessage = "File not parsed.";
	private readonly Error FileNotFoundError = new Error(FileNotFoundErrorMessage, Error.Types.FileLoading);
	private readonly Error FileNotParsedError = new Error(FileNotParsedErrorMessage, Error.Types.FileParsing);
	private Dictionary<Type, string> _typeFilePaths;
	private Dictionary<Type, string> _textureFilePaths;
	private Dictionary<Type, string> _dataFilePaths;
	public Either<List<T>, Error> LoadTypes<T>() {
		// Check if the type exists in the path dictionary.
		if (!_typeFilePaths.TryGetValue(typeof(T), out string path)) {
			return new Error($"Path for {typeof(T).ToString().Split('.')[^1]} type file not found.", Error.Types.FileLoading);
		}
		// Check if the path is valid.
		if (!File.Exists(path)) {
			return FileNotFoundError;
		}
		// Load and deserialise types.
		List<T> types = new List<T>();
		StreamReader streamReader = new StreamReader(path);
		try {
			while (!streamReader.EndOfStream) {
				types.Add(JsonSerializer.Deserialize<T>(streamReader.ReadLine()));
			}
			return types;
		}
		catch {
			return FileNotParsedError;
		}
		finally {
			streamReader.Close();
		}
	}
}