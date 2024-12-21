namespace AdventOfCode;

public class SolutionDay9() : SolutionBase(9)
{
    public override string Part1Solver()
    {
        var blocks = new List<long?>();
        var isFile = true;
        var id = 0;
        foreach (var num in Input.Select(numChar => numChar - '0'))
        {
            for (var i = 0; i < num; i++)
            {
                blocks.Add(isFile ? id : null);
            }

            if (isFile)
            {
                id++;
            }

            isFile = !isFile;
        }

        var freeSpace = 0;
        var fileBlock = blocks.Count - 1;
        while (freeSpace < fileBlock)
        {
            while (freeSpace < blocks.Count && blocks[freeSpace] != null)
            {
                freeSpace++;
            }

            while (fileBlock >= 0 && blocks[fileBlock] == null)
            {
                fileBlock--;
            }

            if (freeSpace >= fileBlock) break;
            blocks[freeSpace] = blocks[fileBlock];
            blocks[fileBlock] = null;
        }

        var sum = 0L;

        for (var i = 0; i < blocks.Count; i++)
        {
            if (blocks[i].HasValue)
            {
                sum += blocks[i]!.Value * i;
            }
        }

        return sum.ToString();
    }

    public override string Part2Solver()
    {
        var blocks = new List<long?>();
        var isFile = true;
        var id = 0;
        foreach (var num in Input.Select(numChar => numChar - '0'))
        {
            for (var i = 0; i < num; i++)
            {
                blocks.Add(isFile ? id : null);
            }

            if (isFile)
            {
                id++;
            }

            isFile = !isFile;
        }

        id--;
        var fileBlock = blocks.Count - 1;

        while (id >= 0)
        {
            while (fileBlock > 0 && blocks[fileBlock] != id)
            {
                fileBlock--;
            }
            
            var fileLength = 0;
            while (fileBlock - fileLength > 0 && blocks[fileBlock - fileLength] == id)
            {
                fileLength++;
            }

            var freeSpaceLength = 0;
            var left = 0;
            var freeSpaceStart = -1;

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

                if (freeSpaceLength != fileLength) continue;
                freeSpaceStart = left;
                break;
            }

            if (freeSpaceStart != -1)
            {
                for (var i = 0; i < fileLength; i++)
                {
                    blocks[left + i] = id;
                    blocks[fileBlock - i] = null;
                }
            }

            id--;
        }

        var sum = 0L;

        for (var i = 0; i < blocks.Count; i++)
        {
            if (blocks[i].HasValue)
            {
                sum += blocks[i]!.Value * i;
            }
        }

        return sum.ToString();
    }
}