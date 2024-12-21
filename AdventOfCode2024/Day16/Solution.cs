using System.Collections.Immutable;

namespace AdventOfCode;

public class SolutionDay16() : SolutionBase(16)
{
    public override string Part1Solver()
    {
        var m = Input.Contains(Environment.NewLine)
            ? Input.IndexOf(Environment.NewLine, StringComparison.Ordinal)
            : Input.Length;
        var rowLength = m + Environment.NewLine.Length;
        var n = (Input.Length + Environment.NewLine.Length) / rowLength;
        var startTile = Input.IndexOf('S');
        var endTile = Input.IndexOf('E');

        var pqueue = new PriorityQueue<(int, int), int>();
        var visited = new HashSet<int>();
        Span<int> directions = [-rowLength, 1, rowLength, -1];

        pqueue.Enqueue((startTile, directions[1]), 0);
        visited.Add(startTile);

        while (pqueue.TryDequeue(out var positionDirection, out var priority))
        {
            var (currentPosition, currentDirection) = positionDirection;
            if (currentPosition == endTile)
            {
                return priority.ToString();
            }

            for (int i = 0; i < directions.Length; i++)
            {
                var newPosition = currentPosition + directions[i];
                if (!visited.Add(newPosition) || Input[newPosition] == '#')
                {
                    continue;
                }

                if (currentDirection == i)
                {
                    pqueue.Enqueue((newPosition, i), priority + 1);
                }
                else
                {
                    pqueue.Enqueue((newPosition, i), priority + 1001);
                }
            }

            {
            }
        }

        return "";
    }

    public string Part1SolverQueue()
    {
        var m = Input.Contains(Environment.NewLine)
            ? Input.IndexOf(Environment.NewLine, StringComparison.Ordinal)
            : Input.Length;
        var rowLength = m + Environment.NewLine.Length;
        var startTile = Input.IndexOf('S');
        var endTile = Input.IndexOf('E');

        var priorityQueue = new PriorityQueue<(int position, int direction), int>();
        var minCosts = new Dictionary<(int position, int direction), int>();
        Span<int> directions = [-rowLength, 1, rowLength, -1];

        priorityQueue.Enqueue((startTile, 1), 0);
        minCosts[(startTile, 1)] = 0;

        while (priorityQueue.TryDequeue(out var current, out var currentCost))
        {
            var (currentPosition, currentDirection) = current;

            if (currentPosition == endTile)
            {
                return currentCost.ToString();
            }

            for (var newDirection = 0; newDirection < directions.Length; newDirection++)
            {
                var newPosition = currentPosition + directions[newDirection];

                if (Input[newPosition] == '#')
                {
                    continue;
                }

                var rotationCost = currentDirection == newDirection ? 0 : 1000;
                const int moveCost = 1;
                var newCost = currentCost + rotationCost + moveCost;

                var newState = (newPosition, newDirection);
                if (minCosts.TryGetValue(newState, out var oldCost))
                {
                    if (newCost >= oldCost)
                    {
                        continue;
                    }
                }

                minCosts[newState] = newCost;
                priorityQueue.Enqueue(newState, newCost);
            }
        }

        return "";
    }

    public override string Part2Solver()
    {
        var m = Input.Contains(Environment.NewLine)
            ? Input.IndexOf(Environment.NewLine, StringComparison.Ordinal)
            : Input.Length;
        var rowLength = m + Environment.NewLine.Length;
        var startTile = Input.IndexOf('S');
        var endTile = Input.IndexOf('E');

        var priorityQueue = new PriorityQueue<(int position, int direction, ImmutableHashSet<int> paths), int>();
        var minCosts = new Dictionary<(int position, int direction), int>();
        Span<int> directions = stackalloc[] { -rowLength, 1, rowLength, -1 };
        var minPaths = new HashSet<int>();

        priorityQueue.Enqueue((startTile, 1, ImmutableHashSet<int>.Empty), 0);
        minCosts[(startTile, 1)] = 0;
        int? shortestCost = null;

        while (priorityQueue.TryDequeue(out var current, out var currentCost))
        {
            var (currentPosition, currentDirection, paths) = current;

            if (currentPosition == endTile)
            {
                if (shortestCost == null || currentCost == shortestCost)
                {
                    shortestCost = currentCost;
                    minPaths.UnionWith(paths.Add(currentPosition));
                }

                continue;
            }

            for (var turn = -1; turn <= 1; turn++)
            {
                var newDirection = Mod(currentDirection + turn, directions.Length);
                var newPosition = currentPosition + directions[newDirection];

                if (Input[newPosition] == '#')
                {
                    continue;
                }

                var rotationCost = currentDirection == newDirection ? 0 : 1000;
                const int moveCost = 1;
                var newCost = currentCost + rotationCost + moveCost;

                if (minCosts.TryGetValue((newPosition, newDirection), out var oldCost) && newCost > oldCost)
                {
                    continue;
                }

                var newPaths = paths.Add(currentPosition);

                var newState = (newPosition, newDirection, newPaths);
                minCosts[(newPosition, newDirection)] = newCost;
                priorityQueue.Enqueue(newState, newCost);
            }
        }

        minPaths.Add(startTile);
        minPaths.Add(endTile);

        return minPaths.Count.ToString();
    }

    public string Part2SolverOptimize()
    {
        var m = Input.Contains(Environment.NewLine)
            ? Input.IndexOf(Environment.NewLine, StringComparison.Ordinal)
            : Input.Length;
        var rowLength = m + Environment.NewLine.Length;
        var startTile = Input.IndexOf('S');
        var endTile = Input.IndexOf('E');

        var priorityQueue = new PriorityQueue<(int position, int direction), int>();
        var predecessors = new Dictionary<(int position, int direction), List<(int position, int direction)>>();
        var costs = new Dictionary<(int position, int direction), int>();
        Span<int> directions = [-rowLength, 1, rowLength, -1];

        priorityQueue.Enqueue((startTile, 1), 0);
        costs[(startTile, 1)] = 0;
        predecessors[(startTile, 1)] = [];
        while (priorityQueue.TryDequeue(out var current, out var currentCost))
        {
            var (currentPosition, currentDirection) = current;

            if (currentPosition == endTile)
            {
                break;
            }

            for (var turn = -1; turn <= 1; turn++)
            {
                var newDirection = Mod(currentDirection + turn, directions.Length);
                var newPosition = currentPosition + directions[newDirection];

                if (Input[newPosition] == '#')
                {
                    continue;
                }

                var newState = (newPosition, newDirection);
                var rotationCost = currentDirection == newDirection ? 0 : 1000;
                const int moveCost = 1;
                var newCost = currentCost + rotationCost + moveCost;

                if (!costs.TryGetValue(newState, out int oldCost) || newCost < oldCost)
                {
                    costs[newState] = newCost;
                    priorityQueue.Enqueue(newState, newCost);

                    if (!predecessors.TryAdd(newState, [current]))
                    {
                        predecessors[newState] = [current];
                    }
                }
                else if (newCost == oldCost)
                {
                    predecessors[newState].Add(current);
                }
            }
        }

        var stack = new Stack<(int, int)>();
        var visited = new Dictionary<int, List<int>>();

        foreach (var direction in directions)
        {
            if (!predecessors.ContainsKey((endTile, direction))) continue;
            stack.Push((endTile, direction));

            if (!visited.TryAdd(endTile, [direction]))
            {
                visited[endTile].Add(direction);
            }
        }

        while (stack.Count > 0)
        {
            var current = stack.Pop();
            foreach (var predecessor in predecessors[current].Where(predecessor =>
                         visited.TryAdd(predecessor.position, [predecessor.direction]) ||
                         !visited[predecessor.position].Contains(predecessor.direction)))
            {
                stack.Push(predecessor);
            }
        }

        return visited.Count.ToString();
    }
    
    public string Part2SolverOptimize2()
    {
        var m = Input.Contains(Environment.NewLine)
            ? Input.IndexOf(Environment.NewLine, StringComparison.Ordinal)
            : Input.Length;
        var rowLength = m + Environment.NewLine.Length;
        var startTile = Input.IndexOf('S');
        var endTile = Input.IndexOf('E');

        var priorityQueue = new PriorityQueue<(int position, int direction), int>();
        var predecessors = new Dictionary<(int position, int direction), List<(int position, int direction)>>();
        var costs = new Dictionary<(int position, int direction), int>();
        ReadOnlySpan<int> directions = [-rowLength, 1, rowLength, -1];

        priorityQueue.Enqueue((startTile, 1), 0);
        costs[(startTile, 1)] = 0;
        predecessors[(startTile, 1)] = [];
        while (priorityQueue.TryDequeue(out var current, out var currentCost))
        {
            var (currentPosition, currentDirection) = current;

            if (currentPosition == endTile)
            {
                break;
            }

            for (var turn = -1; turn <= 1; turn++)
            {
                var newDirection = Mod(currentDirection + turn, directions.Length);
                var newPosition = currentPosition + directions[newDirection];

                if (Input[newPosition] == '#')
                {
                    continue;
                }

                var newState = (newPosition, newDirection);
                var rotationCost = currentDirection == newDirection ? 0 : 1000;
                const int moveCost = 1;
                var newCost = currentCost + rotationCost + moveCost;

                if (!costs.TryGetValue(newState, out int oldCost) || newCost < oldCost)
                {
                    costs[newState] = newCost;
                    priorityQueue.Enqueue(newState, newCost);

                    predecessors[newState] = [current];
                }
                else if (newCost == oldCost)
                {
                    predecessors[newState].Add(current);
                }
            }
        }

        var stack = new Stack<(int position, int direction)>();
        var visited = new Dictionary<int, HashSet<int>>();

        foreach (var direction in directions)
        {
            var state = (endTile, direction);
            if (!predecessors.ContainsKey(state)) continue;
            stack.Push(state);

            if (!visited.TryAdd(endTile, [direction]))
            {
                visited[endTile].Add(direction);
            }
        }

        while (stack.TryPop(out var current))
        {
            foreach (var predecessor in predecessors[current].Where(predecessor =>
                         visited.TryAdd(predecessor.position, [predecessor.direction]) ||
                         !visited[predecessor.position].Contains(predecessor.direction)))
            {
                stack.Push(predecessor);
            }
        }

        return visited.Count.ToString();
    }

    private static int Mod(int x, int m)
    {
        var r = x % m;
        return r < 0 ? r + m : r;
    }
}