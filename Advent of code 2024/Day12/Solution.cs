namespace Advent_of_code_2024;

public class SolutionDay12() : SolutionBase(12)
{
    public override string Part1Solver()
    {
        var m = Input.Contains(Environment.NewLine)
            ? Input.IndexOf(Environment.NewLine, StringComparison.Ordinal)
            : Input.Length;
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
        var m = Input.Contains(Environment.NewLine)
            ? Input.IndexOf(Environment.NewLine, StringComparison.Ordinal)
            : Input.Length;
        var rowLength = m + Environment.NewLine.Length;
        var res = 0;

        var stack = new Stack<int>();
        var visited = new HashSet<int>();
        for (var i = 0; i < Input.Length; i++)
        {
            if (i % rowLength > m - 1 || visited.Contains(i)) continue;
            stack.Push(i);
            visited.Add(i);
            var corners = 0;
            var plants = 0;
            while (stack.Count > 0)
            {
                var plot = stack.Pop();
                var plant = Input[plot];
                plants++;

                var top = plot - rowLength;
                var right = plot + 1;
                var bottom = plot + rowLength;
                var left = plot - 1;

                var sameTop = top >= 0 && plant == Input[top];
                var sameRight = right <= Input.Length - 1 && right % rowLength <= m - 1 && plant == Input[right];
                var sameBottom = bottom <= Input.Length - 1 && plant == Input[bottom];
                var sameLeft = left >= 0 && left % rowLength <= m - 1 && plant == Input[left];

                if (sameBottom && sameLeft)
                {
                    var diagonal = plot + rowLength - 1;
                    var sameDiagonal = diagonal >= 0 && diagonal <= Input.Length - 1 && diagonal % rowLength <= m - 1 &&
                                       plant == Input[diagonal];
                    if (!sameDiagonal)
                    {
                        corners += 1;
                    }
                }

                if (sameTop && sameLeft)
                {
                    var diagonal = plot - rowLength - 1;
                    var sameDiagonal = diagonal >= 0 && diagonal <= Input.Length - 1 && diagonal % rowLength <= m - 1 &&
                                       plant == Input[diagonal];
                    if (!sameDiagonal)
                    {
                        corners += 1;
                    }
                }

                if (sameTop && sameRight)
                {
                    var diagonal = plot - rowLength + 1;
                    var sameDiagonal = diagonal >= 0 && diagonal <= Input.Length - 1 && diagonal % rowLength <= m - 1 &&
                                       plant == Input[diagonal];
                    if (!sameDiagonal)
                    {
                        corners += 1;
                    }
                }

                if (sameRight && sameBottom)
                {
                    var diagonal = plot + rowLength + 1;
                    var sameDiagonal = diagonal >= 0 && diagonal <= Input.Length - 1 && diagonal % rowLength <= m - 1 &&
                                       plant == Input[diagonal];
                    if (!sameDiagonal)
                    {
                        corners += 1;
                    }
                }

                if (!sameTop && !sameRight && !sameBottom && !sameLeft)
                {
                    corners += 4;
                }
                else if (!sameRight && !sameBottom && !sameLeft ||
                    !sameTop && !sameBottom && !sameLeft ||
                    !sameTop && !sameRight && !sameLeft ||
                    !sameTop && !sameRight && !sameBottom)
                {
                    corners += 2;
                }
                else if ((!sameTop && !sameRight) ||
                         (!sameRight && !sameBottom) ||
                         (!sameBottom && !sameLeft) ||
                         (!sameLeft && !sameTop))
                {
                    corners += 1;
                }

                if (sameTop)
                {
                    if (visited.Add(top))
                    {
                        stack.Push(top);
                    }
                }

                if (sameRight)
                {
                    if (visited.Add(right))
                    {
                        stack.Push(right);
                    }
                }

                if (sameBottom)
                {
                    if (visited.Add(bottom))
                    {
                        stack.Push(bottom);
                    }
                }

                if (sameLeft)
                {
                    if (visited.Add(left))
                    {
                        stack.Push(left);
                    }
                }
            }

            res += corners * plants;
        }

        return res.ToString();
    }
}