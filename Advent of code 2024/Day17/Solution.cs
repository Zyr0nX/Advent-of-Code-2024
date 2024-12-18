namespace Advent_of_code_2024;

public class SolutionDay17() : SolutionBase(17)
{
    public override string Part1Solver()
    {
        var inputSpan = Input.AsSpan();
        var separatorIdx = inputSpan.IndexOf($"{Environment.NewLine}{Environment.NewLine}", StringComparison.Ordinal);
        var registers = inputSpan[..separatorIdx];
        var programs = inputSpan[(separatorIdx + Environment.NewLine.Length * 2 + 9)..];

        var lineSeparators = registers.IndexOf(Environment.NewLine, StringComparison.Ordinal) == -1
            ? registers.Length
            : registers.IndexOf(Environment.NewLine, StringComparison.Ordinal);

        var registersA = registers[..lineSeparators];
        var a = long.Parse(registersA[12..]);
        registers = registers[(lineSeparators + Environment.NewLine.Length)..];
        lineSeparators = registers.IndexOf(Environment.NewLine, StringComparison.Ordinal) == -1
            ? registers.Length
            : registers.IndexOf(Environment.NewLine, StringComparison.Ordinal);
        var registersB = registers[..lineSeparators];
        var b = long.Parse(registersB[12..]);
        registers = registers[(lineSeparators + Environment.NewLine.Length)..];
        lineSeparators = registers.IndexOf(Environment.NewLine, StringComparison.Ordinal) == -1
            ? registers.Length
            : registers.IndexOf(Environment.NewLine, StringComparison.Ordinal);
        var registersC = registers[..lineSeparators];
        var c = long.Parse(registersC[12..]);

        var instructionsCount = programs.Count(',') + 1;
        var instructions = new long[instructionsCount];
        var i = 0;
        foreach (var instructionRange in programs.Split(','))
        {
            instructions[i] = int.Parse(programs[instructionRange]);
            i++;
        }

        var res = new List<long>();

        for (var instructionPointer = 0L; instructionPointer < instructions.Length;)
        {
            var opcode = instructions[instructionPointer];
            var literalOperand = instructions[instructionPointer + 1];
            var comboOperand = literalOperand switch
            {
                1 => 1,
                2 => 2,
                3 => 3,
                4 => a,
                5 => b,
                6 => c,
                _ => throw new ArgumentOutOfRangeException()
            };

            switch (opcode)
            {
                case 0:
                    a = (int)Math.Floor(a / Math.Pow(2, comboOperand));
                    break;
                case 1:
                    b ^= literalOperand;
                    break;
                case 2:
                    b = comboOperand % 8;
                    break;
                case 3:
                    if (a != 0)
                    {
                        instructionPointer = literalOperand;
                        continue;
                    }

                    break;
                case 4:
                    b ^= c;
                    break;
                case 5:
                    res.Add(comboOperand % 8);
                    break;
                case 6:
                    b = (int)Math.Floor(a / Math.Pow(2, comboOperand));
                    break;
                case 7:
                    c = (int)Math.Floor(a / Math.Pow(2, comboOperand));
                    break;
            }

            instructionPointer += 2;
        }
        return string.Join(',', res);
    }

    public override string Part2Solver()
    {
        return "";
        // var inputSpan = Input.AsSpan();
        // var separatorIdx = inputSpan.IndexOf($"{Environment.NewLine}{Environment.NewLine}", StringComparison.Ordinal);
        // var programs = inputSpan[(separatorIdx + Environment.NewLine.Length * 2 + 9)..];
    }

    // int Find(ReadOnlySpan<char> input, long ans)
    // {
    //     foreach (var t in Enumerable.Range(0, 8))
    //     {
    //         var a = ans << 3 | (uint)t;
    //         var b = 0;
    //         var c = 0;
    //         
    //     }
    // }
}