/// <summary>
/// Stores a single filename or folder name for a path
/// </summary>
public class PathSegment
{
	/// <summary>
	/// The name of this segment
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// The fileextension of this segment
	/// </summary>
	public string Extension { get; set; }

	/// <summary>
	/// Makes a new empty segment
	/// </summary>
	public PathSegment() { }

	/// <summary>
	/// Parses the string into a segment
	/// </summary>
	/// <param name="s"></param>
	public PathSegment(string s)
	{
		int per = s.LastIndexOf('.');

		if (per == -1) // No file extension found
		{
			Name = s;
		}
		else // Split into Name and Extension
		{
			Name = s.Substring(0, per);
			Extension = s.Substring(per + 1, s.Length - per - 1);
		}
	}

	public override string ToString()
	{
		if (Extension == null || Extension == "")
			return Name;
		else
			return string.Format("{0}.{1}", Name, Extension);
	}

	public static implicit operator PathSegment(string s)
	{
		return new PathSegment(s);
	}
}