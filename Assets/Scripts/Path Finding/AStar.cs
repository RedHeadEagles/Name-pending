using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class AStar : MonoSingleton<AStar>
{
	[Range(1, 32)]
	public int maxPathThreads = 4;

	public static int MaxPathThreads { get { return Instance.maxPathThreads; } }

	public static NavigationMesh Mesh { get; private set; }

	public Queue<PathRequest> pathRequests = new Queue<PathRequest>();

	public static int pathThreads = 0;

	protected override void OnFirstRun()
	{
		Mesh = GetComponent<NavigationMesh>();
		maxPathThreads = Mathf.Clamp(System.Environment.ProcessorCount / 4, 1, System.Environment.ProcessorCount);
	}

	private static List<Vector3> ReducePath(List<Vector3> path)
	{
		for (int i = 1; i < path.Count - 1; i++)
		{
			var a = path[i - 1];
			var b = path[i];

			if (a.x == b.x || a.y == b.y)
			{
				path.RemoveAt(i - 1);
				i--;
			}
		}

		return path;
	}

	private static List<Vector3> BuildPath(Dictionary<Point, Vertex> cameFrom, Vertex end)
	{
		var path = new List<Vector3>();

		while (end != null)
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

	public static void FindPath(IPathAgent agent, Vector3 endLocation)
	{
		var dir = endLocation - agent.transform.position;
		var hit = Physics2D.Raycast(agent.transform.position, dir, dir.magnitude, Mesh.terrainLayer);

		if (hit.collider == null)
		{
			agent.OnPathFound(new List<Vector3>() { endLocation }, float.Epsilon);
			return;
		}

		var start = Mesh[agent.transform.position];
		var end = Mesh[endLocation];

		if (start == null || start.disabled || end == null || end.disabled)
		{
			agent.OnPathFailed(5);
			return;
		}

		var request = new PathRequest()
		{
			agent = agent,
			start = start,
			end = end
		};

		Instance.pathRequests.Enqueue(request);

		if(pathThreads< MaxPathThreads)
		{
			pathThreads++;
			Task task = new Task(PathThread);
			task.Start();
		}
	}

	public static List<Vector3> FindPath(Vector3 startLocation, Vector3 endLocation)
	{
		var dir = endLocation - startLocation;
		var hit = Physics2D.Raycast(startLocation, dir, dir.magnitude, Mesh.terrainLayer);

		if (hit.collider == null)
			return new List<Vector3>() { endLocation };

		var start = Mesh[startLocation];
		var end = Mesh[endLocation];

		if (start == null || start.disabled || end == null || end.disabled)
			return new List<Vector3>();

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

	private static void PathThread()
	{
		while(Instance.pathRequests.Count>0)
		{
			var request = Instance.pathRequests.Dequeue();

			var path = FindPath(request.start, request.end);

			var time = (float)(System.DateTime.Now - request.time).TotalSeconds;

			if (path.Count == 0)
				request.agent.OnPathFailed(time);
			else
				request.agent.OnPathFound(path, time);
		}

		pathThreads--;
	}
}
