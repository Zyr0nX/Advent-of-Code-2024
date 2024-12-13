namespace Advent_of_code_2024;

public class SolutionDay12() : SolutionBase(12)
{
    public override string Part1Solver()
    {
        var m = Input.Contains(Environment.NewLine) ? Input.IndexOf(Environment.NewLine, StringComparison.Ordinal) : Input.Length;
        var rowLength = m + Environment.NewLine.Length;
        var res = 0;
        Span<int> directions = [-rowLength, 1, rowLength, -1];

        var stack = new Stack<int>();
        var visited = new HashSet<int>();
        for (var i = 0; i < Input.Length; i++)
        {
            if (i % rowLength > m - 1 || visited.Contains(i)) continue;
            stack.Push(i);
            visited.Add(i);
            var fences = 0;
            var plants = 0;
            while (stack.Count > 0)
            {
                var plot = stack.Pop();
                var plant = Input[plot];
                plants += 1;

                foreach (var direction in directions)
                {
                    var newPlot = plot + direction;
                    if (newPlot < 0 ||
                        newPlot > Input.Length - 1 ||
                        newPlot % rowLength > m - 1 ||
                        plant != Input[newPlot])
                    {
                        fences += 1;
                        continue;
                    }
                    
                    if (visited.Add(newPlot))
                    {
                        stack.Push(newPlot);
                    }
                }
            }

            res += fences * plants;
        }

        return res.ToString();
    }

    public override string Part2Solver()
    {
        var m = Input.Contains(Environment.NewLine) ? Input.IndexOf(Environment.NewLine, StringComparison.Ordinal) : Input.Length;
        var rowLength = m + Environment.NewLine.Length;
        var res = 0;
        Span<int> directions = [-rowLength, 1, rowLength, -1];
        var inputSpan = Input.AsSpan();

        var stack = new Stack<int>();
        var visited = new HashSet<int>();
        for (var i = 0; i < inputSpan.Length; i++)
        {
            if (i % rowLength > m - 1 || visited.Contains(i)) continue;
            stack.Push(i);
            visited.Add(i);
            var sides = 0;
            var plants = 0;
            while (stack.Count > 0)
            {
                var plot = stack.Pop();
                var plant = inputSpan[plot];
                plants++;

                foreach (var direction in directions)
                {
                    var newPlot = plot + direction;
                    if (newPlot < 0 ||
                        newPlot > Input.Length - 1 ||
                        newPlot % rowLength > m - 1)
                    {
                        sides++;
                        continue;
                    }
                    if (inputSpan[newPlot] != plant)
                    {
                        sides++;
                    } 
                    else if (visited.Add(newPlot))
                    {
                        stack.Push(newPlot);
                    }
                }
            }

            res += sides * plants;
        }

        return res.ToString();
    }
}