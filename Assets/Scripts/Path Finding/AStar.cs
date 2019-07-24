using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

public class AStar : MonoSingleton<AStar>
{
	public static NavigationMesh Mesh { get; private set; }

	public Queue<Thread> pathRequests = new Queue<Thread>();

	private void Update()
	{
		if(pathRequests.Count>0)
		{
			var path = pathRequests.Dequeue();
			path.Start();
		}
	}

	protected override void OnFirstRun()
	{
		Mesh = GetComponent<NavigationMesh>();
	}

	private static List<Vector3> BuildPath(Dictionary<Point, Vertex> cameFrom, Vertex end)
	{
		var path = new List<Vector3>();

		while(end!=null)
		{
			path.Insert(0, end.worldLocation);
			end = cameFrom[end.location];
		}

		return path;
	}

	private static float Estimate(Point a, Point b)
	{
		return Mathf.Abs(b.x - a.x) + Mathf.Abs(b.y - a.y);
	}

	private static float Score(Dictionary<Point, float> scores, Point key)
	{
		if (scores.TryGetValue(key, out float score))
			return score;

		return float.MaxValue;
	}

	public static void FindPath(IPathAgent agent, Vector2 endLocation)
	{
		var start = Mesh[agent.transform.position];
		var end = Mesh[endLocation];

		var thread = new Thread(new ThreadStart(() => PathThread(agent, start, end)));
		Instance.pathRequests.Enqueue(thread);
	}

	public static List<Vector3> FindPath(Vector2 startLocation, Vector2 endLocation)
	{
		var start = Mesh[startLocation];
		var end = Mesh[endLocation];

		return FindPath(start, end);
	}
	
	private static List<Vector3> FindPath(Vertex start, Vertex end)
	{
		var open = new List<Point>() { start.location };
		var cameFrom = new Dictionary<Point, Vertex>();
		var gScore = new Dictionary<Point, float>();
		var fScore = new Dictionary<Point, float>();

		cameFrom[start.location] = null;
		gScore[start.location] = 0;
		fScore[end.location] = 0;

		while (open.Count > 0)
		{
			open = open.OrderBy(f => Score(fScore, f)).ToList();
			var current = open[0];
			open.RemoveAt(0);

			var currentV = Mesh[current.x, current.y];

			if (current == end.location)
				return BuildPath(cameFrom, currentV);

			foreach (var neighbor in currentV)
			{
				var score = Score(gScore, current) + Estimate(current, neighbor);

				if (score < Score(gScore, neighbor))
				{
					cameFrom[neighbor] = currentV;
					gScore[neighbor] = score;
					fScore[neighbor] = Score(gScore, neighbor) + 1;

					if (!open.Contains(neighbor))
						open.Insert(0, neighbor);
				}
			}
		}

		return new List<Vector3>();
	}

	private static void PathThread(IPathAgent agent, Vertex start, Vertex end)
	{
		var path = FindPath(start, end);

		if (path.Count == 0)
			agent.OnPathFailed();
		else
			agent.OnPathFound(path);
	}
}
