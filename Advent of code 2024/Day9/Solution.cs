namespace Advent_of_code_2024;

public class SolutionDay9() : SolutionBase(9)
{
    public override string Part1Solver()
    {
        var inputSpan = Input.AsSpan();
        var blocks = new List<long?>();
        var isFile = true;
        var id = 0;
        foreach (var numChar in inputSpan)
        {
            var num = numChar - '0';
            if (isFile)
            {
                for (var i = 0; i < num; i++)
                {
                    blocks.Add(id);
                }

                id++;
            }
            else
            {
                for (var i = 0; i < num; i++)
                {
                    blocks.Add(null);
                }
            }

            isFile = !isFile;
        }

        var freeSpace = 0;
        var fileBlock = blocks.Count - 1;

        while (freeSpace < fileBlock)
        {
            while (blocks[freeSpace] != null)
            {
                freeSpace += 1;
            }

            while (blocks[fileBlock] == null)
            {
                fileBlock -= 1;
            }

            blocks[freeSpace] = blocks[fileBlock];
            blocks[fileBlock] = null;
        }

        var sum = blocks.Select((b, i) => (b, i)).Sum(b => b.b == null ? 0 : b.b * b.i);

        return sum.ToString()!;
    }

    public override string Part2Solver()
    {
        var inputSpan = Input.AsSpan();
        var blocks = new List<long?>();
        var isFile = true;
        var id = 0;
        foreach (var numChar in inputSpan)
        {
            var num = numChar - '0';
            if (isFile)
            {
                for (var i = 0; i < num; i++)
                {
                    blocks.Add(id);
                }

                id++;
            }
            else
            {
                for (var i = 0; i < num; i++)
                {
                    blocks.Add(null);
                }
            }

            isFile = !isFile;
        }

        id--;
        var fileBlock = blocks.Count - 1;
        
        while (id >= 0)
        {
            while (fileBlock > 0 && blocks[fileBlock] != id)
            {
                fileBlock -= 1;
            }

            var file = blocks[fileBlock];
            var fileLength = 0;
            while (fileBlock - fileLength > 0 && blocks[fileBlock - fileLength] == file)
            {
                fileLength++;
            }

            var freeSpaceLength = 0;

            var left = 0;
            for (var right = 0; right < fileBlock - fileLength + 1; right++)
            {
                if (blocks[right] == null)
                {
                    freeSpaceLength++;
                }
                
                if (right - left + 1 > fileLength)
                {
                    if (blocks[left] == null)
                    {
                        freeSpaceLength--;
                    }
                    left++;
                }
                
                if (freeSpaceLength == fileLength)
                {
                    break;
                }

            }

            if (freeSpaceLength == fileLength)
            {
                for (var i = 0; i < fileLength; i++)
                {
                    blocks[left + i] = file;
                    blocks[fileBlock - i] = null;
                }
            }
            id--;
            
        }
        
        var sum = blocks.Select((b, i) => (b, i)).Sum(b => b.b == null ? 0 : b.b * b.i);

        return sum.ToString()!;
    }
}