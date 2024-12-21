namespace AdventOfCode;

public class SolutionDay8() : SolutionBase(8)
{
    public override string Part1Solver()
    {
        var m = Input.Contains(Environment.NewLine) ? Input.IndexOf(Environment.NewLine, StringComparison.Ordinal) : Input.Length;
        var rowLength = m + Environment.NewLine.Length;
        var n = (Input.Length + Environment.NewLine.Length) / rowLength;
        var antennaDict = new Dictionary<char, List<int>>();
        for (var i = 0; i < Input.Length; i++)
        {
            var antenna = Input[i];
            if (!char.IsLetterOrDigit(antenna)) continue;
            
            if (!antennaDict.TryAdd(antenna, [i]))
            {
                antennaDict[antenna].Add(i);
            }
        }

        var antinodes = new HashSet<int>();
        
        foreach (var antennaPositions in antennaDict.Values)
        {
            for (var i = 0; i < antennaPositions.Count; i++)
            {
                var x1 = antennaPositions[i] % rowLength;
                var y1 = antennaPositions[i] / rowLength;
                for (var j = i + 1; j < antennaPositions.Count; j++)
                {
                    var x2 = antennaPositions[j] % rowLength;
                    var y2 = antennaPositions[j] / rowLength;

                    var xDiff = x2 - x1;
                    var yDiff = y2 - y1;

                    var xAntinode1 = x1 - xDiff;
                    var yAntinode1 = y1 - yDiff;
                    if (xAntinode1 >= 0 && xAntinode1 < m && yAntinode1 >= 0 && yAntinode1 < n)
                    {
                        antinodes.Add(yAntinode1 * rowLength + xAntinode1);
                    }
                    
                    var xAntinode2 = x2 + xDiff;
                    var yAntinode2 = y2 + yDiff;
                    if (xAntinode2 >= 0 && xAntinode2 < m && yAntinode2 >= 0 && yAntinode2 < n)
                    {
                        antinodes.Add(yAntinode2 * rowLength + xAntinode2);
                    }
                }
            }
        }

        return antinodes.Count.ToString();
    }

    public override string Part2Solver()
    {
        var m = Input.Contains(Environment.NewLine) ? Input.IndexOf(Environment.NewLine, StringComparison.Ordinal) : Input.Length;
        var rowLength = m + Environment.NewLine.Length;
        var n = (Input.Length + Environment.NewLine.Length) / rowLength;
        var antennaDict = new Dictionary<char, List<int>>();
        for (var i = 0; i < Input.Length; i++)
        {
            var antenna = Input[i];
            if (!char.IsLetterOrDigit(antenna)) continue;
            
            if (!antennaDict.TryAdd(antenna, [i]))
            {
                antennaDict[antenna].Add(i);
            }
        }

        var antinodes = new HashSet<int>();
        
        foreach (var antennaPositions in antennaDict.Values)
        {
            for (var i = 0; i < antennaPositions.Count; i++)
            {
                var x1 = antennaPositions[i] % rowLength;
                var y1 = antennaPositions[i] / rowLength;
                for (var j = i + 1; j < antennaPositions.Count; j++)
                {
                    var x2 = antennaPositions[j] % rowLength;
                    var y2 = antennaPositions[j] / rowLength;

                    var xDiff = x2 - x1;
                    var yDiff = y2 - y1;

                    var xAntinode1 = x1;
                    var yAntinode1 = y1;
                    while (xAntinode1 >= 0 && xAntinode1 < m && yAntinode1 >= 0 && yAntinode1 < n)
                    {
                        antinodes.Add(yAntinode1 * rowLength + xAntinode1);
                        xAntinode1 -= xDiff;
                        yAntinode1 -= yDiff;
                    }
                    
                    var xAntinode2 = x2;
                    var yAntinode2 = y2;
                    while (xAntinode2 >= 0 && xAntinode2 < m && yAntinode2 >= 0 && yAntinode2 < n)
                    {
                        antinodes.Add(yAntinode2 * rowLength + xAntinode2);
                        xAntinode2 += xDiff;
                        yAntinode2 += yDiff;
                    }
                }
            }
        }

        return antinodes.Count.ToString();
    }
}