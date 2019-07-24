using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// Simplifies file paths and adds relative path to specified root folders
/// </summary>
public class Path
{
	/// <summary>
	/// The chain of folders and file names
	/// </summary>
	private List<PathSegment> segments = new List<PathSegment>();

	/// <summary>
	/// What folder this path starts at
	/// </summary>
	private string root = "app";

	/// <summary>
	/// Number of segments in this path
	/// </summary>
	private int Length { get { return segments.Count; } }

	/// <summary>
	/// Index of the last element
	/// </summary>
	private int Last { get { return segments.Count - 1; } }

	/// <summary>
	/// Gets the last segment for this path
	/// </summary>
	private PathSegment LastSegment { get { return segments[segments.Count - 1]; } }

	/// <summary>
	/// What folder this path starts at
	/// </summary>
	public string Root
	{
		get { return root; }
		set { root = value; }
	}

	/// <summary>
	/// Gets or sets only the segments portion of the path (the part after the root folder)
	/// </summary>
	public string SegmentPath
	{
		get
		{
			string path = "";

			for (int i = 0; i < Length; i++)
			{
				if (i < Length - 1)
					path = string.Format("{0}{1}/", path, segments[i]);
				else
					path += segments[i].ToString();
			}

			return path;
		}
		set
		{
			segments.Clear();

			AddSegment(value);
		}
	}

	/// <summary>
	/// Gets the full system path string
	/// </summary>
	public string FullName
	{
		get
		{
			return string.Format("{0}{1}", IO.GetRootFolder(root), SegmentPath);
		}
	}

	/// <summary>
	/// Gets or sets both the Name and Extension
	/// </summary>
	public string Filename
	{
		get { return LastSegment.ToString(); }
		set { segments[Last] = value; }
	}

	/// <summary>
	/// Gets or sets the name of the file
	/// </summary>
	public string Name
	{
		get { return LastSegment.Name; }
		set { LastSegment.Name = value; }
	}

	/// <summary>
	/// Gets or sets the file extension of the file
	/// </summary>
	public string Extension
	{
		get { return LastSegment.Extension; }
		set { LastSegment.Extension = value; }
	}

	/// <summary>
	/// Gets the parent folder
	/// </summary>
	public Path Parent
	{
		get
		{
			Path parent = new Path
			{
				root = root
			};

			for (int i = 0; i < Length - 1; i++)
				parent.segments.Add(segments[i].ToString());

			return parent;
		}
	}

	/// <summary>
	/// Checks if this path exists as a file on the disk
	/// </summary>
	public bool IsFile { get { return File.Exists(FullName); } }

	/// <summary>
	/// Checks if this path exists as a directory on the disk
	/// </summary>
	public bool IsDirectory { get { return Directory.Exists(FullName); } }

	/// <summary>
	/// Checks if this path exists on the disk
	/// </summary>
	public bool Exists { get { return IsFile || IsDirectory; } }

	/// <summary>
	/// Gets the DirectoryInfo for the path
	/// </summary>
	public DirectoryInfo GetDirectoryInfo { get { return new DirectoryInfo(FullName); } }

	/// <summary>
	/// Gets the FileInfo for the path
	/// </summary>
	public FileInfo GetFileInfo { get { return new FileInfo(FullName); } }

	public Path() { }

	public Path(string path, string root = "app")
	{
		SegmentPath = path;
		this.root = root;
	}

	/// <summary>
	/// Removes the last segment of the path
	/// </summary>
	public void PopSegment()
	{
		if (Length > 0)
			segments.RemoveAt(Last);
	}

	/// <summary>
	/// Adds a new segment to the end of the path
	/// </summary>
	/// <param name="segment"></param>
	public void AddSegment(string segment)
	{
		segment = segment.Replace(@"\", "/");

		while (segment.Contains("//"))
			segment = segment.Replace("//", "/");

		string[] segs = segment.Split('/');

		for (int i = 0; i < segs.Length; i++)
			segments.Add(segs[i]);
	}

	public override string ToString()
	{
		return FullName;
	}

	public static implicit operator Path(string s)
	{
		return new Path(s);
	}

	public static Path operator +(Path p, string s)
	{
		Path path = new Path
		{
			root = p.root,
			SegmentPath = p.SegmentPath
		};

		path.AddSegment(s);
		return path;
	}
}