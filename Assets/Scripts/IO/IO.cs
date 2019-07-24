using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Handles the various read and write requests to the disk
/// </summary>
public static class IO
{
	private static Dictionary<string, string> rootFolders = new Dictionary<string, string>() {
			{ "app", Application.dataPath.Substring(0, Application.dataPath.LastIndexOf('/') + 1) },
			{ "root", System.IO.Path.GetPathRoot(Application.dataPath) }
		};

	/// <summary>
	/// Gets the root folder path string
	/// </summary>
	/// <param name="root">Name of the root</param>
	public static string GetRootFolder(string root)
	{
		string tmp;
		if (rootFolders.TryGetValue(root, out tmp))
			return tmp;

		Debug.LogWarning(string.Format("Unknown root folder: '{0}'", root));
		return "";
	}

	/// <summary>
	/// Defines a root directory for the Paths to use
	/// </summary>
	/// <param name="root">Name for the root</param>
	/// <param name="path">Path representing root directory</param>
	public static void SetRootFolder(string root, string path)
	{
		rootFolders[root] = path;
	}

	/// <summary>
	/// Reads all the data from the given file as a singele string
	/// </summary>
	/// <param name="path">File to read from</param>
	/// <returns>A string representing the file's contents</returns>
	public static string ReadString(Path path)
	{
		return File.ReadAllText(path.FullName);
	}

	/// <summary>
	/// Writes a string to the given file
	/// </summary>
	/// <param name="path">File to write to</param>
	/// <param name="data">String to be written</param>
	public static void WriteString(Path path, string data)
	{
		if (!path.Parent.Exists)
			CreateDirectory(path.Parent);

		File.WriteAllText(path.FullName, data);
	}

	/// <summary>
	/// Reads each line from the given file
	/// </summary>
	/// <param name="path">File to read from</param>
	/// <returns>An array of strings</returns>
	public static string[] ReadLines(Path path)
	{
		return File.ReadAllLines(path.FullName);
	}

	/// <summary>
	/// Writs all lines to the given file
	/// </summary>
	/// <param name="path">File to write to</param>
	/// <param name="data">lines to be written</param>
	public static void WriteLines(Path path, string[] data)
	{
		if (!path.Parent.Exists)
			CreateDirectory(path.Parent);

		File.WriteAllLines(path.FullName, data);
	}

	public static byte[] ReadBytes(Path path)
	{
		return File.ReadAllBytes(path.FullName);
	}

	public static void WriteBytes(Path path, byte[] data)
	{
		if (!path.Parent.Exists)
			CreateDirectory(path.Parent);

		File.WriteAllBytes(path.FullName, data);
	}

	/// <summary>
	/// Reads the data from the disk using json
	/// </summary>
	/// <typeparam name="T">Type of the data being read</typeparam>
	/// <param name="path">File to read from</param>
	public static T ReadJson<T>(Path path)
	{
		var json = File.ReadAllText(path.FullName);
		return JsonUtility.FromJson<T>(json);
	}

	public static void WriteJson<T>(Path path, T data)
	{
		var json = JsonUtility.ToJson(data, true);
		File.WriteAllText(path.FullName, json);
	}


	/// <summary>
	/// Reads the data from the disk using serialization
	/// </summary>
	/// <typeparam name="T">Type of the data being read</typeparam>
	/// <param name="path">File to read from</param>
	/// <param name="compressed">Is the data compressed</param>
	public static T ReadSerial<T>(Path path, bool compressed = true)
	{
		BinaryFormatter bf = new BinaryFormatter();

		using (var file = new FileStream(path.FullName, FileMode.Open, FileAccess.Read))
		using (var comp = new DeflateStream(file, CompressionMode.Decompress))
			if (compressed)
				return (T)bf.Deserialize(comp);
			else
				return (T)bf.Deserialize(file);
	}

	public static void WriteSerial<T>(Path path, T data, bool compressed = true)
	{
		if (!path.Parent.Exists)
			CreateDirectory(path.Parent);

		BinaryFormatter bf = new BinaryFormatter();

		using (var file = new FileStream(path.FullName, FileMode.Create, FileAccess.Write))
		using (var comp = new DeflateStream(file, CompressionMode.Compress))
			if (compressed)
				bf.Serialize(comp, data);
			else
				bf.Serialize(file, data);
	}

	/// <summary>
	/// Writes the given data to the disk using the best method for the data type
	/// </summary>
	/// <typeparam name="T">Type of the data being read</typeparam>
	/// <param name="path">File to write to</param>
	/// <param name="data">Data to be written</param>
	public static void Write<T>(Path path, T data)
	{
		if (data is string)
			WriteString(path, data as string);
		else if (data is string[])
			WriteLines(path, data as string[]);
		else if (data is byte[])
			WriteBytes(path, data as byte[]);
		else
			WriteSerial(path, data);
	}

	public static void CreateDirectory(Path path)
	{
		Directory.CreateDirectory(path.FullName);
	}
}
