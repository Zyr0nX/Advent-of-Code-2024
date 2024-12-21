namespace AdventOfCode;

public class SolutionDay10() : SolutionBase(10)
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
            if (Input[i] != '0') continue;
            
            stack.Clear();
            visited.Clear();
            stack.Push(i);

            while (stack.Count > 0)
            {
                var position = stack.Pop();
                if (Input[position] == '9' && visited.Add(position))
                {
                    res += 1;
                    continue;
                }

                foreach (var direction in directions)
                {
                    var newPosition = position + direction;
                    if (newPosition < 0 ||
                        newPosition > Input.Length - 1 ||
                        newPosition % rowLength > m ||
                        Input[position] + 1 != Input[newPosition])
                    {
                        continue;
                    }

                    stack.Push(position + direction);
                }
            }
        }

        return res.ToString();
    }


    public override string Part2Solver()
    {
        var m = Input.Contains(Environment.NewLine) ? Input.IndexOf(Environment.NewLine, StringComparison.Ordinal) : Input.Length;
        var rowLength = m + Environment.NewLine.Length;
        var res = 0;
        Span<int> directions = [-rowLength, 1, rowLength, -1];
        var stack = new Stack<int>();
        for (var i = 0; i < Input.Length; i++)
        {
            if (Input[i] != '0') continue;
            
            stack.Clear();
            stack.Push(i);
            
            while (stack.Count > 0)
            {
                var position = stack.Pop();
                if (Input[position] == '9')
                {
                    res += 1;
                    continue;
                }

                foreach (var direction in directions)
                {
                    var newPosition = position + direction;
                    if (newPosition < 0 ||
                        newPosition > Input.Length - 1 ||
                        newPosition % rowLength > m ||
                        Input[position] + 1 != Input[newPosition])
                    {
                        continue;
                    }

                    stack.Push(position + direction);
                }
            }
        }

        return res.ToString();
    }
}